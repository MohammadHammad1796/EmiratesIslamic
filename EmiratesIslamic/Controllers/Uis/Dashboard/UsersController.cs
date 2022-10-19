using EmiratesIslamic.Controllers.Uis.Base;
using EmiratesIslamic.Controllers.Uis.ViewModels;
using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Core.Repositories;
using EmiratesIslamic.Core.Services;
using EmiratesIslamic.CustomAttributes.Route;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Uis.Dashboard;

[DashboardRoute("users")]
[Authorize(Roles = "Admin")]
public class UsersController : BaseDashboardUiController
{
    private readonly IUserManager _userManager;
    private readonly IPhotosRepository _photosRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<Role> _rolesExceptAdmin;

    public UsersController(IUserManager userManager,
        IPhotosRepository photosRepository, IUnitOfWork unitOfWork,
        IRoleManager roleManager)
    {
        _photosRepository = photosRepository;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _rolesExceptAdmin = roleManager.GetRolesExceptAdminAsync().Result;
    }

    [Route("list")]
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.GetUsersWithRolesExceptAdminAsync();
        return View("List", users);
    }

    [Route("new")]
    public ActionResult Create()
    {
        ViewData["roles"] = _rolesExceptAdmin;
        var viewModel = new UserFormViewModel();
        return View("Form", viewModel);
    }

    [HttpPost("new")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(UserFormViewModel viewModel)
    {
        ViewData["roles"] = _rolesExceptAdmin;
        if (!ModelState.IsValid)
            return View("Form", viewModel);

        if (_rolesExceptAdmin.All(r => r.Id != viewModel.RoleId))
        {
            ModelState.AddModelError(nameof(viewModel.RoleId), "Selected role isn't supported.");
            return View("Form", viewModel);
        }

        var imagePath = string.Empty;
        if (viewModel.Image != null)
        {
            imagePath = await _photosRepository.SaveAsync(viewModel.Image!, "users");
            if (string.IsNullOrWhiteSpace(imagePath))
                RedirectToAction("Create", viewModel);
        }

        var user = new User()
        {
            Email = viewModel.Email,
            PhoneNumber = viewModel.PhoneNumber,
            FullName = viewModel.FullName,
            RoleId = viewModel.RoleId
        };
        if (viewModel.Image != null)
            user.ImagePath = imagePath;

        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);
            return View("Form", viewModel);
        }
        var savedSuccessfully = await _unitOfWork.SaveChangesAsync();

        return savedSuccessfully ?
            RedirectToAction(nameof(Index)) :
            RedirectToAction("Create", viewModel);
    }

    [Route("edit/{id:int}")]
    public async Task<ActionResult> Edit(int id)
    {
        var user = await _userManager.GetByIdWithRoleAsync(id);
        if (user == null)
            return NotFound();

        ViewData["roles"] = _rolesExceptAdmin;
        var viewModel = new UserFormViewModel()
        {
            Id = id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            FullName = user.FullName,
            RoleId = user.RoleId,
            ImagePath = user.ImagePath
        };
        return View("Form", viewModel);
    }

    [HttpPost("edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, UserFormViewModel viewModel)
    {
        ViewData["roles"] = _rolesExceptAdmin;
        if (!ModelState.IsValid)
            return View("Form", viewModel);

        var user = await _userManager.GetByIdWithRoleAsync(id);
        if (user == null)
            return NotFound();

        if (_rolesExceptAdmin.All(r => r.Id != viewModel.RoleId))
        {
            ModelState.AddModelError(nameof(viewModel.RoleId), "Selected role isn't supported.");
            return View("Form", viewModel);
        }

        var previousImagePath = string.Empty;
        if (viewModel.Image != null)
        {
            previousImagePath = user.ImagePath;
            var imagePath = await _photosRepository.SaveAsync(viewModel.Image!, "users");
            if (string.IsNullOrWhiteSpace(imagePath))
                return RedirectToAction("Edit", new { id = viewModel.Id, viewModel });
            user.ImagePath = imagePath;
        }

        user.Email = viewModel.Email;
        user.PhoneNumber = viewModel.PhoneNumber;
        user.FullName = viewModel.FullName;
        user.UserName = viewModel.Email;

        if (user.RoleId != viewModel.RoleId)
        {
            var previousRole = _rolesExceptAdmin
                .Single(r => r.Id == user.RoleId);
            var newRole = _rolesExceptAdmin
                .Single(r => r.Id == viewModel.RoleId);

            var result = await _userManager.RemoveFromRoleAsync(previousRole.Name);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View("Form", viewModel);
            }

            result = await _userManager.AddToRoleAsync(newRole.Name);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View("Form", viewModel);
            }
        }

        var savedSuccessfully = await _unitOfWork.SaveChangesAsync();
        if (!savedSuccessfully)
            return RedirectToAction("Edit", new { id = viewModel.Id, viewModel });

        if (!string.IsNullOrWhiteSpace(previousImagePath))
            _photosRepository.Delete(previousImagePath);

        return RedirectToAction(nameof(Index));
    }
}