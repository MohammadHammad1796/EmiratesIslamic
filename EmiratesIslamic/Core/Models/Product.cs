using System.ComponentModel.DataAnnotations;

namespace EmiratesIslamic.Core.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(250)]
    public string Text { get; set; }

    [Required]
    [MaxLength(250)]
    public string ImagePath { get; set; }
}