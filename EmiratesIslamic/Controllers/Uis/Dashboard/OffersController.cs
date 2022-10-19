using EmiratesIslamic.Controllers.Uis.Base;
using EmiratesIslamic.Controllers.Uis.ViewModels;
using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Core.Repositories;
using EmiratesIslamic.CustomAttributes.Route;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Uis.Dashboard;

[DashboardRoute("offers")]
[Authorize(Roles = "Sales Supervisor, Admin")]
public class OffersController : BaseDashboardUiController
{
    private readonly IOffersRepository _offersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhotosRepository _photosRepository;

    public OffersController(IOffersRepository offersRepository,
        IUnitOfWork unitOfWork, IPhotosRepository photosRepository)
    {
        _unitOfWork = unitOfWork;
        _offersRepository = offersRepository;
        _photosRepository = photosRepository;
    }

    [Route("list")]
    public async Task<ActionResult> Index()
    {
        var offers = await _offersRepository.GetAllAsync();
        return View("List", offers);
    }

    [Route("new")]
    public ActionResult Create()
    {
        var viewModel = new OfferFormViewModel();
        return View("Form", viewModel);
    }

    [HttpPost("new")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(OfferFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View("Form", viewModel);

        var imagePath = await _photosRepository.SaveAsync(viewModel.Image!, "offers");
        if (string.IsNullOrWhiteSpace(imagePath))
            return RedirectToAction("Create", viewModel);

        await _offersRepository.AddAsync(new Offer()
        {
            Title = viewModel.Title,
            Text = viewModel.Text,
            ImagePath = imagePath
        });
        var savedSuccessfully = await _unitOfWork.SaveChangesAsync();

        return savedSuccessfully ?
            RedirectToAction(nameof(Index)) :
            RedirectToAction("Create", viewModel);
    }

    [Route("edit/{id:int}")]
    public async Task<ActionResult> Edit(int id)
    {
        var offer = await _offersRepository.GetByIdAsync(id);
        if (offer == null)
            return NotFound();

        var viewModel = new OfferFormViewModel()
        {
            Id = id,
            Title = offer.Title,
            Text = offer.Text,
            ImagePath = offer.ImagePath
        };
        return View("Form", viewModel);
    }

    [HttpPost("edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, OfferFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View("Form", viewModel);

        var offer = await _offersRepository.GetByIdAsync(id);
        if (offer == null)
            return NotFound();

        var previousImagePath = string.Empty;
        if (viewModel.Image != null)
        {
            previousImagePath = offer.ImagePath;
            var imagePath = await _photosRepository.SaveAsync(viewModel.Image!, "offers");
            if (string.IsNullOrWhiteSpace(imagePath))
                return RedirectToAction("Edit", new { id = viewModel.Id, viewModel });
            offer.ImagePath = imagePath;
        }

        offer.Title = viewModel.Title;
        offer.Text = viewModel.Text;
        var savedSuccessfully = await _unitOfWork.SaveChangesAsync();
        if (!savedSuccessfully)
            return RedirectToAction("Edit", new { id = viewModel.Id, viewModel });

        if (!string.IsNullOrWhiteSpace(previousImagePath))
            _photosRepository.Delete(previousImagePath);

        return RedirectToAction(nameof(Index));
    }
}