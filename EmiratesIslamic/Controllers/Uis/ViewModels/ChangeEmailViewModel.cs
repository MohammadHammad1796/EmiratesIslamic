using System.ComponentModel.DataAnnotations;

namespace EmiratesIslamic.Controllers.Uis.ViewModels;

public class ChangeEmailViewModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	[EmailAddress]
	[Display(Name = "New email")]
	public string NewEmail { get; set; }
}