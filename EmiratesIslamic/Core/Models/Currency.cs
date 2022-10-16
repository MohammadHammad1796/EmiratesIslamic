using System.ComponentModel.DataAnnotations;

namespace EmiratesIslamic.Core.Models;

public class Currency
{
    [MaxLength(5)]
    public string Code { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public float Buy { get; set; }

    public float Sell { get; set; }

    [Display(Name = "Status")]
    public bool IsAvailable { get; set; }
}