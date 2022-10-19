using EmiratesIslamic.Controllers.Uis.Base;
using EmiratesIslamic.CustomAttributes.Route;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmiratesIslamic.Controllers.Uis.Dashboard;

[DashboardRoute("")]
[Authorize(Roles = "Sales Supervisor, Services Supervisor, Admin")]
public class DashboardController : BaseDashboardUiController
{
    [Route("")]
    public IActionResult Index() => View("Index");
}