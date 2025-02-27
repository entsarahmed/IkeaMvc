using LinkDev.Ikea.DAL.Entities.Identity;
using LinkDev.Ikea.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Ikea.PL.Controllers
{
    public class AccountController : Controller
    { 
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController( UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager=userManager;
			_signInManager=signInManager;
		}

        //Register
        #region Register (SignUp)
         
        [HttpGet]  // Get: /Account/SignUp
        public IActionResult SignUp()
        {

        return View();
        
        }

        [HttpPost] //POST

        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {

            /*if (!ModelState.IsValid)
                return BadRequest();
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is { })

            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This username is already in user for another account.");
                return View(model);
            
            }
					user = new ApplicationUser()
                {
                    FName=model.FName,
                    LName=model.LName,
                    UserName = model.UserName,
                    Email = model.Email,
                    IsAgree = model.IsAgree,

                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return RedirectToAction(nameof(SignIn));

                foreach(var error in result.Errors)
                    ModelState.AddModelError(string.Empty,error.Description);   


         

            return View(model);
          */

            if(ModelState.IsValid)   //Server Side Validation
            {
                var User = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree
                };

            var Result =  await  _userManager.CreateAsync(User, model.Password);

                if (Result.Succeeded)
                    return RedirectToAction("SignIn");
                else
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);

        }


        #endregion


        //Login

        
        #region Login (Sign In)
        [HttpGet]  // Get: /Account/SignIn
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost] //Post
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            { // return BadRequest();
                var User = await _userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                   
                 var Flag = await _userManager.CheckPasswordAsync(User,model.Password);
                    if(Flag)
                    {
                        //Login
                     var Result = await  _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false );
                        if (Result.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError(string.Empty,"Incorrect Password");
                }
                else
                    ModelState.AddModelError(string.Empty, "Email is not Exists");
                /*var flag = await _userManager.CheckPasswordAsync(User, model.Password);

                //if (flag)
                //{
                //    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                //    if (result.IsNotAllowed)
                //        ModelState.AddModelError(string.Empty, "Your account is not confirmed yet!");

                //    if (result.IsLockedOut)
                //        ModelState.AddModelError(string.Empty, "Your account is locked!!");

                    //if (result.RequiresTwoFactor)
                    //{
                    //}

                    if (Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                */
            }
            return View(model);
        }
        #endregion


        //Sign Out

        #region Sign Out
        
        public async new Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        //ForgetPassword

        //Reset Password
    }
}
