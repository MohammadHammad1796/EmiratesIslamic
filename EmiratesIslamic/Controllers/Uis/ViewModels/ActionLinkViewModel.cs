using EmiratesIslamic.Controllers.Uis.Dashboard;
using System.Collections.Generic;

namespace EmiratesIslamic.Controllers.Uis.ViewModels;

public class ActionLinkViewModel
{
    public string Controller { get; }

    public string Action { get; }

    public string Title { get; }

    public ActionLinkViewModel(string controller, string action, string title)
    {
        if (controller.Contains("Controller"))
            controller = controller.Split("Controller")[0];

        Controller = controller;
        Action = action;
        Title = title;
    }

    public static IEnumerable<ActionLinkViewModel> GetByRole(string role)
    {
        switch (role)
        {
            case "admin":
                return new List<ActionLinkViewModel>()
                {
                    new(nameof(DashboardController), nameof(DashboardController.Index), "Dashboard"),
                    new(nameof(FunctionsController), nameof(FunctionsController.Index), "Functions"),
                    new(nameof(ProductsController), nameof(ProductsController.Index), "Products"),
                    new(nameof(OffersController), nameof(OffersController.Index), "Offers"),
                    new(nameof(CurrenciesController), nameof(CurrenciesController.Index), "Currencies"),
                    new(nameof(UsersController), nameof(UsersController.Index), "Users")
                };
            case "sales supervisor":
                return new List<ActionLinkViewModel>()
                {
                    new(nameof(DashboardController), nameof(DashboardController.Index), "Dashboard"),
                    new(nameof(ProductsController), nameof(ProductsController.Index), "Products"),
                    new(nameof(OffersController), nameof(OffersController.Index), "Offers"),
                    new(nameof(CurrenciesController), nameof(CurrenciesController.Index), "Currencies")
                };
            case "services supervisor":
                return new List<ActionLinkViewModel>()
                {
                    new(nameof(DashboardController), nameof(DashboardController.Index), "Dashboard"),
                    new(nameof(FunctionsController), nameof(FunctionsController.Index), "Functions")

                };
        }

        return new List<ActionLinkViewModel>();
    }
}