using EmiratesIslamic.Controllers.Uis.Base;
using EmiratesIslamic.Controllers.Uis.ViewModels;
using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Uis.Client;

[Route("")]
public class HomeController : BaseClientUiController
{
    private readonly IProductsRepository _productsRepository;
    private readonly IOffersRepository _offersRepository;
    private readonly IRepository<Function> _functionsRepository;
    private readonly ICurrenciesRepository _currenciesRepository;

    public HomeController(IProductsRepository productsRepository,
        IOffersRepository offersRepository, IRepository<Function> functionsRepository,
        ICurrenciesRepository currenciesRepository)
    {
        _productsRepository = productsRepository;
        _offersRepository = offersRepository;
        _functionsRepository = functionsRepository;
        _currenciesRepository = currenciesRepository;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeViewModel();
        viewModel.Products = await _productsRepository.GetLatestAsync(3);
        viewModel.Offers = await _offersRepository.GetLatestAsync(3);
        viewModel.Functions = await _functionsRepository.GetAllAsync();
        viewModel.Currencies = await _currenciesRepository.GetAvailableAsync();

        return View("Index", viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("error")]
    public IActionResult Error()
    {
        var errorViewModel = new ErrorViewModel(Activity.Current?.Id ?? HttpContext.TraceIdentifier);
        return View("Error", errorViewModel);
    }

    [Route("notFound")]
    public IActionResult NotFoundPage()
    {
        return NotFound();
    }
}