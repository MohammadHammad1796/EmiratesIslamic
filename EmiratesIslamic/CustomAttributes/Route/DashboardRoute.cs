using Microsoft.AspNetCore.Mvc;

namespace EmiratesIslamic.CustomAttributes.Route;

public class DashboardRoute : RouteAttribute
{
    public DashboardRoute(string template)
        : base($"dashboard/{template}")
    {
    }
}