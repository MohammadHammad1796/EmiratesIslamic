using EmiratesIslamic.Controllers.Uis.Base;
using EmiratesIslamic.Controllers.Uis.ViewModels;
using EmiratesIslamic.Core.Repositories;
using EmiratesIslamic.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Uis.Client;

[Route("account")]
public class AccountController : BaseClientUiController
{
    private readonly ISignInManager _signInManager;
    private readonly IUserManager _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IPhotosRepository _photosRepository;

    public AccountController(ISignInManager signInManager, IUserManager userManager,
        IEmailService emailService,
        IUnitOfWork unitOfWork, IPhotosRepository photosRepository)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
        _photosRepository = photosRepository;
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login([FromQuery] RedirectViewModel redirect)
    {
        redirect.ReturnUrl ??= "/";
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        var viewModel = new LoginViewModel(redirect.ReturnUrl);
        return View(viewModel);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        viewModel.ReturnUrl ??= "/";

        var user = await _userManager.FindByEmailAsync(viewModel.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Failed login attemp");
            return View(viewModel);
        }

        var succeeded = await _signInManager
            .PasswordSignInAsync(user.UserName, viewModel.Password,
                viewModel.RememberMe, lockoutOnFailure: false);
        if (succeeded)
            return LocalRedirect(viewModel.ReturnUrl);

        ModelState.AddModelError(string.Empty, "Failed login attemp");
        return View(viewModel);
    }

    [HttpGet("forgotPassword")]
    public IActionResult ForgotPassword()
    {
        var viewModel = new ForgotPasswordViewModel();
        return View(viewModel);
    }

    [HttpPost("forgotPassword")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var user = await _userManager.FindByEmailAsync(viewModel.Email);
        if (user == null)
            return View("WeSendEmail");

        var code = await _userManager.GeneratePasswordResetTokenAsync();
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callbackUrl = Url.Page(
            pageName: null,
            pageHandler: null,
            values: new
            {
                code,
                controller = "account",
                action = "resetPassword"
            },
            protocol: Request.Scheme);

        await _emailService.SendEmailAsync(viewModel.Email, "Reset password",
            $"Please <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>click here</a> to " +
            "reset your password.");

        return View("WeSendEmail");
    }

    [HttpGet("resetPassword")]
    public IActionResult ResetPassword(string code)
    {
        if (string.IsNullOrEmpty(code))
            return NotFound();

        var viewModel = new ResetPasswordViewModel();
        return View(viewModel);
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel, string code)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        if (string.IsNullOrWhiteSpace(code))
        {
            ModelState.AddModelError(string.Empty,
                "You should request forgot password first to reset it.");
            return View(viewModel);
        }

        var user = await _userManager.FindByEmailAsync(viewModel.Email);
        if (user == null)
            return RedirectToAction("ResetPassword");

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ResetPasswordAsync(code, viewModel.Password);
        if (result.Succeeded)
        {
            if (await _unitOfWork.SaveChangesAsync())
                return RedirectToAction("Login");

            ModelState.AddModelError(string.Empty, "There is server error, please try again.");
            return View(viewModel);
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(viewModel);
    }

    [Route("accessDenied")]
    public ActionResult AccessDenied()
    {
        return View();
    }

    [Authorize]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpGet("changeEmail")]
    public IActionResult ChangeEmail()
    {
        var viewModel = new ChangeEmailViewModel();
        return View(viewModel);
    }

    [Authorize]
    [HttpPost("changeEmail")]
    public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var user = await _userManager.FindByEmailAsync(viewModel.Email);
        if (user == null)
        {
            ModelState.AddModelError(nameof(viewModel.Email), "Your email is not currect");
            return View(viewModel);
        }

        if (await _userManager.FindByEmailAsync(viewModel.NewEmail) != null)
        {
            ModelState.AddModelError(nameof(viewModel.NewEmail), "This email used with another account");
            return View(viewModel);
        }

        var code = await _userManager.GenerateChangeEmailTokenAsync(viewModel.NewEmail);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callbackUrl = Url.Page(
            pageName: null,
            pageHandler: null,
            values: new
            {
                code,
                controller = "account",
                action = "changeEmailConfirmation"
            },
            protocol: Request.Scheme);

        await _emailService.SendEmailAsync(viewModel.NewEmail, "Change email confirmation",
            $"Please <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>click here</a> to " +
            "confirm email change.");

        return View("WeSendEmail");
    }

    [HttpGet("changeEmailConfirmation")]
    public IActionResult ChangeEmailConfirmation(string code)
    {
        if (string.IsNullOrEmpty(code))
            return NotFound();

        var viewModel = new ChangeEmailViewModel();
        return View("ChangeEmail", viewModel);
    }

    [HttpPost("changeEmailConfirmation")]
    public async Task<IActionResult> ChangeEmailConfirmation(ChangeEmailViewModel viewModel, string code)
    {
        if (!ModelState.IsValid)
            return View("ChangeEmail", viewModel);

        if (string.IsNullOrWhiteSpace(code))
        {
            ModelState.AddModelError(string.Empty,
                "You should request change email first to change it.");
            return View("ChangeEmail", viewModel);
        }

        var user = await _userManager.FindByEmailAsync(viewModel.Email);
        if (user == null)
            return RedirectToAction("ChangeEmail");

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ChangeEmailAsync(viewModel.NewEmail, code);
        if (result.Succeeded)
        {
            if (await _unitOfWork.SaveChangesAsync())
            {
                await _userManager.UpdateNormalizedEmailAsync();
                result = await _userManager.SetUserNameAsync(viewModel.NewEmail);
                await _userManager.UpdateNormalizedUserNameAsync();
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(viewModel.NewEmail, false);
                    return View("EmailChanged");
                }
            }

            ModelState.AddModelError(string.Empty, "There is server error, please try again.");
            return View("ChangeEmail", viewModel);
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View("ChangeEmail", viewModel);
    }

    [Authorize]
    [HttpGet("changePassword")]
    public IActionResult ChangePassword()
    {
        var viewModel = new ChangePasswordViewModel();
        return View(viewModel);
    }

    [Authorize]
    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var user = await _userManager.GetUserAsync(User);
        var validOldPassword = await _userManager.CheckPasswordAsync(user, viewModel.OldPassword);
        if (!validOldPassword)
        {
            ModelState.AddModelError(nameof(viewModel.OldPassword), "Your password isn't correct");
            return View(viewModel);
        }

        var result = await _userManager
            .ChangePasswordAsync(viewModel.OldPassword, viewModel.NewPassword);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);
            return View(viewModel);
        }

        return View("PasswordChanged");
    }

    [Authorize]
    [HttpGet("changePersonalInfo")]
    public async Task<IActionResult> ChangePersonalInfo()
    {
        var user = await _userManager.GetUserAsync(User);
        var viewModel = new ChangePersonalInfoViewModel()
        {
            FullName = user.FullName,
            ImagePath = user.ImagePath,
            PhoneNumber = user.PhoneNumber
        };
        return View(viewModel);
    }

    [Authorize]
    [HttpPost("changePersonalInfo")]
    public async Task<IActionResult> ChangePersonalInfo(ChangePersonalInfoViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var previousImagePath = string.Empty;
        var user = await _userManager.GetUserAsync(User);
        if (viewModel.Image != null)
        {
            previousImagePath = user.ImagePath;
            user.ImagePath = await _photosRepository.SaveAsync(viewModel.Image!, "users");
            if (string.IsNullOrWhiteSpace(user.ImagePath))
                return RedirectToAction("ChangePersonalInfo", viewModel);
        }

        user.FullName = viewModel.FullName;
        user.PhoneNumber = viewModel.PhoneNumber;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);
            return View(viewModel);
        }

        if (!string.IsNullOrWhiteSpace(previousImagePath))
            _photosRepository.Delete(previousImagePath);

        return RedirectToAction("ChangePersonalInfo");
    }
}