using EmiratesIslamic.Controllers.Uis.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmiratesIslamic.Controllers.Uis;

[Route("")]
public class HomeController : Controller
{
    [Route("")]
    public IActionResult Index() => View("Index");


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("error")]
    public IActionResult Error()
    {
        var errorViewModel = new ErrorViewModel(Activity.Current?.Id ?? HttpContext.TraceIdentifier);
        return View("Error", errorViewModel);
    }
}