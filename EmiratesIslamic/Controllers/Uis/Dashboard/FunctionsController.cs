using EmiratesIslamic.Controllers.Uis.Base;
using EmiratesIslamic.Controllers.Uis.ViewModels;
using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Core.Repositories;
using EmiratesIslamic.CustomAttributes.Route;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Uis.Dashboard;

[DashboardRoute("functions")]
public class FunctionsController : BaseDashboardUiController
{
    private readonly IRepository<Function> _functionsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhotosRepository _photosRepository;

    public FunctionsController(IRepository<Function> functionsRepository,
        IUnitOfWork unitOfWork, IPhotosRepository photosRepository)
    {
        _unitOfWork = unitOfWork;
        _functionsRepository = functionsRepository;
        _photosRepository = photosRepository;
    }

    [Route("list")]
    public async Task<ActionResult> Index()
    {
        var functions = await _functionsRepository.GetAllAsync();
        return View("List", functions);
    }

    [Route("new")]
    public ActionResult Create()
    {
        var viewModel = new FunctionFormViewModel();
        return View("Form", viewModel);
    }

    [HttpPost("new")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(FunctionFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View("Form", viewModel);

        var imagePath = await _photosRepository.SaveAsync(viewModel.Image!, "functions");
        if (string.IsNullOrWhiteSpace(imagePath))
            return RedirectToAction("Create", viewModel);

        await _functionsRepository.AddAsync(new Function()
        {
            Name = viewModel.Name,
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
        var function = await _functionsRepository.GetByIdAsync(id);
        if (function == null)
            return NotFound();

        var viewModel = new FunctionFormViewModel()
        {
            Id = id,
            Name = function.Name,
            ImagePath = function.ImagePath
        };
        return View("Form", viewModel);
    }

    [HttpPost("edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, FunctionFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View("Form", viewModel);

        var function = await _functionsRepository.GetByIdAsync(id);
        if (function == null)
            return NotFound();

        var previousImagePath = string.Empty;
        if (viewModel.Image != null)
        {
            previousImagePath = function.ImagePath;
            var imagePath = await _photosRepository.SaveAsync(viewModel.Image!, "functions");
            if (string.IsNullOrWhiteSpace(imagePath))
                return RedirectToAction("Edit", new { id = viewModel.Id, viewModel = viewModel });
            function.ImagePath = imagePath;
        }

        function.Name = viewModel.Name;
        var savedSuccessfully = await _unitOfWork.SaveChangesAsync();
        if (!savedSuccessfully)
            return RedirectToAction("Edit", new { id = viewModel.Id, viewModel = viewModel });

        if (!string.IsNullOrWhiteSpace(previousImagePath))
            _photosRepository.Delete(previousImagePath);

        return RedirectToAction(nameof(Index));
    }
}