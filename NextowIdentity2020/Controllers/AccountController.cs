using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NextowIdentity2020.Models.VeiwModels;

namespace NextowIdentity2020.Controllers
{
    #region Configration
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> _signInManager,
            RoleManager<IdentityRole> _roleManager
            )
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }
        #endregion
        #region User
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Phone

                };
                var r = await userManager.CreateAsync(user, model.Password!);
                if (r.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                foreach (var err in r.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email!, model.Password!, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "invalsed");
                return View(model);
            }

            return View(model);
        }
        #endregion
        #region Roles
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole()
        {

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateRole(CreteRoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);

        }
        [Authorize(Roles = "Admin")]
        public IActionResult RolesList()
        {
            return View(roleManager.Roles);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion

    }
}
