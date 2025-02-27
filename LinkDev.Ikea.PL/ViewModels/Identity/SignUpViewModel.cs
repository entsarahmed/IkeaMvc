using System.ComponentModel.DataAnnotations;

namespace LinkDev.Ikea.PL.ViewModels.Identity
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="First Name is Required")]
		public string FName { get; set; }

		[Required(ErrorMessage ="Last Name is Required")]
		public string LName { get; set; }

		[Required(ErrorMessage ="UserName is Required")]
		public string UserName { get; set; }

		[Required(ErrorMessage ="Email is Required")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage ="ConfirmPassword is Required")]
		[Display(Name ="Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage ="Confirm Password doesn't match with Password")]
		public string ConfirmPassword { get; set; } 
		public bool IsAgree { get; set; }
	}
}
