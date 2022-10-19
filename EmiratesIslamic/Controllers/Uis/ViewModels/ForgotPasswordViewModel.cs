using System.ComponentModel.DataAnnotations;

namespace EmiratesIslamic.Controllers.Uis.ViewModels;

public class ForgotPasswordViewModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }
}