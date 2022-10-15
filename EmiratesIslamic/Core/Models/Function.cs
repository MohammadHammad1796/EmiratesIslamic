using System.ComponentModel.DataAnnotations;

namespace EmiratesIslamic.Core.Models;

public class Function
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string ImagePath { get; set; }
}