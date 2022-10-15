using EmiratesIslamic.Core.Repositories;
using EmiratesIslamic.CustomAttributes.Route;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Apis;

[ApiRoute("products")]
public class ProductsController : Controller
{
    private readonly IProductsRepository _productsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhotosRepository _photosRepository;

    public ProductsController(IProductsRepository productsRepository,
        IUnitOfWork unitOfWork, IPhotosRepository photosRepository)
    {
        _productsRepository = productsRepository;
        _unitOfWork = unitOfWork;
        _photosRepository = photosRepository;
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productsRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        _productsRepository.Delete(product);
        var savedSuccessfully = await _unitOfWork.SaveChangesAsync();
        if (!savedSuccessfully)
            return StatusCode(StatusCodes.Status500InternalServerError);

        _photosRepository.Delete(product.ImagePath);
        return Ok();
    }
}