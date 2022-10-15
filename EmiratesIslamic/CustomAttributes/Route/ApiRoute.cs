using Microsoft.AspNetCore.Mvc;

namespace EmiratesIslamic.CustomAttributes.Route;

public class ApiRoute : RouteAttribute
{
    public ApiRoute(string template)
        : base($"api/{template}")
    {
    }
}