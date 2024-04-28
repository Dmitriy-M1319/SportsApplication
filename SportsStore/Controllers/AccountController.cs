using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers;

public class AccountController: Controller
{
    private UserManager<IdentityUser> _userManager;
    private SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public ViewResult Login(string returnUrl)
    {
        return View(new LoginModel { Name = string.Empty, Password = string.Empty, ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginModel) {
        if (ModelState.IsValid) {
            IdentityUser? user =
                await _userManager.FindByNameAsync(loginModel.Name);
            if (user != null) {
                //await _signInManager.SignOutAsync();
                if ((await _signInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false)).Succeeded) {
                    Console.WriteLine(User.Identity.IsAuthenticated);
                    return Redirect(loginModel.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
        }
        return View(loginModel);
    } 

    [Authorize]
    public async Task<RedirectResult> Logout(string returnUrl = "/")
    {
        await _signInManager.SignOutAsync();
        return Redirect(returnUrl);
    }
}