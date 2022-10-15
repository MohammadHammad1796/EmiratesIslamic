using EmiratesIslamic.Controllers.Uis.Base;
using EmiratesIslamic.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Uis.Client;

[Route("offers")]
public class ReadOffersController : BaseClientUiController
{
    private readonly IOffersRepository _offersRepository;

    public ReadOffersController(IOffersRepository offersRepository)
    {
        _offersRepository = offersRepository;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        var offers = await _offersRepository.GetAllAsync();
        return View("List", offers);
    }

    [Route("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var offer = await _offersRepository.GetByIdAsync(id);
        if (offer == null)
            return NotFound();

        return View("Details", offer);
    }
}