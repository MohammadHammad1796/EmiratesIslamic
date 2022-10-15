using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmiratesIslamic.Controllers.Uis.Base;

public class BaseUiController : Controller
{
    protected ApplicationLayout CurrentLayout;

    public override ViewResult View()
    {
        ViewBag.Layout = CurrentLayout.ToString();
        return base.View();
    }

    public override ViewResult View(string? viewName)
    {
        ViewBag.Layout = CurrentLayout.ToString();
        return base.View(viewName);
    }

    public override ViewResult View(object? model)
    {
        ViewBag.Layout = CurrentLayout.ToString();
        return base.View(model);
    }

    public override ViewResult View(string? viewName, object? model)
    {
        ViewBag.Layout = CurrentLayout.ToString();
        return base.View(viewName, model);
    }

    protected new ViewResult NotFound()
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        return View("NotFound");
    }
}