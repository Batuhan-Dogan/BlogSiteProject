using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Concrete;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace ProgrammersBlog.Mvc.Controllers
{
    public class AccountController : Controller
    {
        #region Fields
        private readonly IServiceManager _serviceManager;
        private readonly SignInManager<User> _signInManager;
        #endregion
        #region Constractors
        public AccountController(IServiceManager serviceManager, SignInManager<User> signInManager) 
        {
            _serviceManager= serviceManager;
            _signInManager= signInManager;
        }
        #endregion
        #region Methods
        public IActionResult LogIn( string ReturnUrl)
        {
            if(ReturnUrl is not null)
            {
                 ViewBag.ReturnUrl = ReturnUrl;
            }
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(UserLoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                Entities.Concrete.User user= await _serviceManager.AuthService.UserManager.FindByEmailAsync(loginDto.Email);
                if (user is not null) 
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user,loginDto.Password,loginDto.RememberMe,false)).Succeeded)
                    {
                        return Redirect(loginDto.ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-posta adresiniz veya şifreniz hatalıdır.");
                        return View(loginDto);
                    }
                }
                else
                {
                    ModelState.AddModelError("","E-posta adresiniz veya şifreniz hatalıdır.");
                    return View(loginDto);
                }
        
            }
            else
            {
                return View(loginDto);
            }
        }
        
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Json(new { success = true }); ;
        }

        public IActionResult AccessDenied() {  return View(); }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword( ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordDto);
            }
            var result = await _serviceManager.AuthService.ChangePasswordAsync(resetPasswordDto);
            if (result.ResultStatus==ResultStatus.Success)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.PasswordSignInAsync(result.Data,resetPasswordDto.NewPassword,true,false);
                TempData["success"] = result.Message;
                return Redirect("/");
            }
            else
            {
                TempData["danger"] = result.Message;
                return View(resetPasswordDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Register() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserAddDto userAddDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userAddDto);
            }
            else
            {
                var result=await _serviceManager.AuthService.AddUserAsync(userAddDto);
                if(result.ResultStatus==ResultStatus.Success)
                {
                    TempData["success"] =result.Message;
                    await _signInManager.SignOutAsync();
                    return Redirect("/Account/LogIn");
                }
                else
                {
                    TempData["warning"] = result.Message;
                }
                return View(userAddDto);
            }
            
        }

        #endregion
    }
}
