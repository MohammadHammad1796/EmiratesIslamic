using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Core.Repositories;
using EmiratesIslamic.CustomAttributes.Route;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Apis;

[ApiRoute("functions")]
public class FunctionsController : Controller
{
    private readonly IRepository<Function> _functionsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhotosRepository _photosRepository;

    public FunctionsController(IRepository<Function> functionsRepository,
        IUnitOfWork unitOfWork, IPhotosRepository photosRepository)
    {
        _functionsRepository = functionsRepository;
        _unitOfWork = unitOfWork;
        _photosRepository = photosRepository;
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var function = await _functionsRepository.GetByIdAsync(id);
        if (function == null)
            return NotFound();

        _functionsRepository.Delete(function);
        var savedSuccessfully = await _unitOfWork.SaveChangesAsync();
        if (!savedSuccessfully)
            return StatusCode(StatusCodes.Status500InternalServerError);

        _photosRepository.Delete(function.ImagePath);
        return Ok();
    }
}