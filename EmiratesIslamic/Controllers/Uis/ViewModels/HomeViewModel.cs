using EmiratesIslamic.Core.Models;
using System.Collections.Generic;

namespace EmiratesIslamic.Controllers.Uis.ViewModels;

public class HomeViewModel
{
    public IEnumerable<Function> Functions { get; set; }

    public IEnumerable<Product> Products { get; set; }

    public IEnumerable<Offer> Offers { get; set; }

    public IEnumerable<Currency> Currencies { get; set; }

    public HomeViewModel()
    {
        Functions = new List<Function>();
        Products = new List<Product>();
        Offers = new List<Offer>();
        Currencies = new List<Currency>();
    }
}