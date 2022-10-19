using EmiratesIslamic.CustomAttributes.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EmiratesIslamic.Controllers.Uis.ViewModels;

public class UserFormViewModel
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Full name")]
    [MaxLength(100)]
    public string FullName { get; set; }

    [Display(Name = "Phone")]
    [RegularExpression("^09[0-9]{8}$", ErrorMessage = "{0} is not a valid mobile number")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Role")]
    public int RoleId { get; set; }

    [FileExtension("jpg", "jpeg",
        ErrorMessage = "The {0} field should be image, with one of the following extensions: {1}")]
    public IFormFile? Image { get; set; }

    public string? ImagePath { get; set; }
}