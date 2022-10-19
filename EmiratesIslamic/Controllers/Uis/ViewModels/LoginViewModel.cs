using System.ComponentModel.DataAnnotations;

namespace EmiratesIslamic.Controllers.Uis.ViewModels;

public class LoginViewModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	[StringLength(100, MinimumLength = 6)]
	[DataType(DataType.Password)]
	public string Password { get; set; }

	[Display(Name = "Remember me?")]
	public bool RememberMe { get; set; }

	public string? ReturnUrl { get; set; }

	public LoginViewModel()
	{
	}

	public LoginViewModel(string returnUrl)
	{
		ReturnUrl = returnUrl;
	}
}