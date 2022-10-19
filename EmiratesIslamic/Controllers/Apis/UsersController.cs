using EmiratesIslamic.Core.Repositories;
using EmiratesIslamic.Core.Services;
using EmiratesIslamic.CustomAttributes.Route;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Apis;

[ApiRoute("users")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly IUserManager _userManager;
    private readonly IPhotosRepository _photosRepository;

    public UsersController(IUserManager userManager,
        IPhotosRepository photosRepository)
    {
        _userManager = userManager;
        _photosRepository = photosRepository;
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        var result = await _userManager.DeleteAsync();
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError);

        if (!string.IsNullOrWhiteSpace(user.ImagePath))
            _photosRepository.Delete(user.ImagePath);

        return Ok();
    }

}