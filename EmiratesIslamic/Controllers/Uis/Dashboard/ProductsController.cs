using EmiratesIslamic.Controllers.Uis.Base;
using EmiratesIslamic.Controllers.Uis.ViewModels;
using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Core.Repositories;
using EmiratesIslamic.CustomAttributes.Route;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmiratesIslamic.Controllers.Uis.Dashboard;

[DashboardRoute("products")]
public class ProductsController : BaseDashboardUiController
{
    private readonly IProductsRepository _productsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhotosRepository _photosRepository;

    public ProductsController(IProductsRepository productsRepository,
        IUnitOfWork unitOfWork, IPhotosRepository photosRepository)
    {
        _unitOfWork = unitOfWork;
        _productsRepository = productsRepository;
        _photosRepository = photosRepository;
    }

    [Route("list")]
    public async Task<ActionResult> Index()
    {
        var products = await _productsRepository.GetAllAsync();
        return View("List", products);
    }

    [Route("new")]
    public ActionResult Create()
    {
        var viewModel = new ProductFormViewModel();
        return View("Form", viewModel);
    }

    [HttpPost("new")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(ProductFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View("Form", viewModel);

        var imagePath = await _photosRepository.SaveAsync(viewModel.Image!, "products");
        if (string.IsNullOrWhiteSpace(imagePath))
            RedirectToAction("Create", viewModel);

        await _productsRepository.AddAsync(new Product()
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
        var product = await _productsRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        var viewModel = new ProductFormViewModel()
        {
            Id = id,
            Title = product.Title,
            Text = product.Text,
            ImagePath = product.ImagePath
        };
        return View("Form", viewModel);
    }

    [HttpPost("edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, ProductFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View("Form", viewModel);

        var product = await _productsRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        var previousImagePath = string.Empty;
        if (viewModel.Image != null)
        {
            previousImagePath = product.ImagePath;
            var imagePath = await _photosRepository.SaveAsync(viewModel.Image!, "products");
            if (string.IsNullOrWhiteSpace(imagePath))
                return RedirectToAction("Edit", new { id = viewModel.Id, viewModel = viewModel });
            product.ImagePath = imagePath;
        }

        product.Title = viewModel.Title;
        product.Text = viewModel.Text;
        var savedSuccessfully = await _unitOfWork.SaveChangesAsync();
        if (!savedSuccessfully)
            return RedirectToAction("Edit", new { id = viewModel.Id, viewModel = viewModel });

        if (!string.IsNullOrWhiteSpace(previousImagePath))
            _photosRepository.Delete(previousImagePath);

        return RedirectToAction(nameof(Index));
    }
}