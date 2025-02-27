using System.ComponentModel.DataAnnotations;

namespace LinkDev.Ikea.PL.ViewModels.Identity
{
	public class SignInViewModel
	{
		[EmailAddress(ErrorMessage ="Email is Required")]
		public string Email { get; set; } = null!;
		//[MinLength(5)]
		[DataType(DataType.Password)]
		[Required(ErrorMessage ="Password is Required")]
		public string Password { get; set; } = null!;
		
		public bool RememberMe { get; set; }
	}
}
