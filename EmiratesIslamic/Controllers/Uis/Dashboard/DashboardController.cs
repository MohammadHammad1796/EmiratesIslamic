using EmiratesIslamic.Controllers.Uis.Base;
using EmiratesIslamic.CustomAttributes.Route;
using Microsoft.AspNetCore.Mvc;

namespace EmiratesIslamic.Controllers.Uis.Dashboard;

[DashboardRoute("")]
public class DashboardController : BaseDashboardUiController
{
    [Route("")]
    public IActionResult Index() => View("Index");
}