using EmiratesIslamic.Controllers.Uis.Base;
using EmiratesIslamic.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Uis.Client;

[Route("products")]
public class ReadProductsController : BaseClientUiController
{
    private readonly IProductsRepository _productsRepository;

    public ReadProductsController(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        var products = await _productsRepository.GetAllAsync();
        return View("List", products);
    }

    [Route("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productsRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        return View("Details", product);
    }
}