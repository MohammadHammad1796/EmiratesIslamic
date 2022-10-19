using EmiratesIslamic.Controllers.Uis.Base;
using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Core.Repositories;
using EmiratesIslamic.CustomAttributes.Route;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Uis.Dashboard;

[DashboardRoute("currencies")]
[Authorize(Roles = "Sales Supervisor, Admin")]
public class CurrenciesController : BaseDashboardUiController
{
    private readonly ICurrenciesRepository _currenciesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CurrenciesController(ICurrenciesRepository currenciesRepository,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _currenciesRepository = currenciesRepository;
    }

    [Route("list")]
    public async Task<ActionResult> Index()
    {
        var currencies = await _currenciesRepository.GetAllAsync();
        return View("List", currencies);
    }

    [Route("edit/{code}")]
    public async Task<ActionResult> Edit(string code)
    {
        var currency = await _currenciesRepository.GetByCodeAsync(code);
        if (currency == null)
            return NotFound();

        return View("Form", currency);
    }

    [HttpPost("edit/{code}")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(string code, Currency currency)
    {
        if (!ModelState.IsValid)
            return View("Form", currency);

        var currencyInDb = await _currenciesRepository.GetByCodeAsync(code);
        if (currencyInDb == null)
            return NotFound();

        currencyInDb.Buy = currency.Buy;
        currencyInDb.Sell = currency.Sell;
        currencyInDb.IsAvailable = currency.IsAvailable;
        var savedSuccessfully = await _unitOfWork.SaveChangesAsync();
        if (!savedSuccessfully)
            return RedirectToAction("Edit", new { code = currencyInDb.Code, currency = currencyInDb });

        return RedirectToAction(nameof(Index));
    }
}