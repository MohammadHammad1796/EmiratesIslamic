using EmiratesIslamic.Core.Repositories;
using EmiratesIslamic.CustomAttributes.Route;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Apis;

[ApiRoute("offers")]
[Authorize(Roles = "Sales Supervisor, Admin")]
public class OffersController : Controller
{
    private readonly IOffersRepository _offersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhotosRepository _photosRepository;

    public OffersController(IOffersRepository offersRepository,
        IUnitOfWork unitOfWork, IPhotosRepository photosRepository)
    {
        _offersRepository = offersRepository;
        _unitOfWork = unitOfWork;
        _photosRepository = photosRepository;
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var offer = await _offersRepository.GetByIdAsync(id);
        if (offer == null)
            return NotFound();

        _offersRepository.Delete(offer);
        var savedSuccessfully = await _unitOfWork.SaveChangesAsync();
        if (!savedSuccessfully)
            return StatusCode(StatusCodes.Status500InternalServerError);

        _photosRepository.Delete(offer.ImagePath);
        return Ok();
    }
}