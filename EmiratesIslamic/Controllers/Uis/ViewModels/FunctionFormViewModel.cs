using EmiratesIslamic.CustomAttributes.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EmiratesIslamic.Controllers.Uis.ViewModels;

public class FunctionFormViewModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [RequiredFile(nameof(Id), ErrorMessage = "The {0} is required.")]
    [FileExtension("jpg", "jpeg", "png",
        ErrorMessage = "The {0} field should be image, with one of the following extensions: {1}")]
    public IFormFile? Image { get; set; }

    public string? ImagePath { get; set; }
}