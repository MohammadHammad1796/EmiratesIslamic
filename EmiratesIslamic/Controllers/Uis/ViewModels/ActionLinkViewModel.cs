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
}