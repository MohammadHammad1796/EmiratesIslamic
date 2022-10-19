using EmiratesIslamic.Core.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmiratesIslamic.Infrastructure.Identity;

public class SignInManager : ISignInManager
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public SignInManager(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<bool> PasswordSignInAsync(string userName, string password,
        bool isPersistent, bool lockoutOnFailure)
    {
        var result = await _signInManager
            .PasswordSignInAsync(userName, password,
                isPersistent, lockoutOnFailure: lockoutOnFailure);
        return result.Succeeded;
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task SignInAsync(string email, bool isPersistent)
    {
        var user = await _userManager.FindByEmailAsync(email);
        await _signInManager.SignInAsync(user, isPersistent);
    }

    public bool IsSignedIn(ClaimsPrincipal principal)
    {
        return _signInManager.IsSignedIn(principal);
    }
}