using Godel.AutoPartsStore.BL.Interfaces;
using Godel.AutoPartsStore.Common.Authorization;
using Godel.AutoPartsStore.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Godel.AutoPartsStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        public AccountController(IUserService userService, IRoleService roleService)
        {
            _userService = (userService ?? throw new ArgumentNullException(nameof(userService)));
            _roleService = (roleService ?? throw new ArgumentNullException(nameof(roleService)));
        }

        [Authorize]
        public IActionResult Index()
        {
            // return Content(User.Identity.Name);
            if (User.Identity.IsAuthenticated)
            {
                return Content(User.Identity.Name);
            }
            return Content("User is not authenticated");
        }

        public IActionResult About()
        {
            return Content("Authorized");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user =  _userService.GetUserByEmailAndPassword(model.Email,model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username or password");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user =  _userService.GetUserByEmail(model.Email);

                if (user == null)
                {
                    // добавляем пользователя в бд
                    var newUser = new User();
                    newUser.Email = model.Email;
                    newUser.Password = model.Password;

                    Role userRole = _roleService.GetRoleByName("user");
                    if (userRole != null)
                        newUser.Role = userRole;
                    _userService.CreateUser(newUser);
                    await Authenticate(newUser); // аутентификация
                    return RedirectToAction("Parts", "Home");
                }
                else 
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return View(model);
        }

    private async Task Authenticate(User user)
    {
        // создаем один claim
        var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };
        // создаем объект ClaimsIdentity
        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        // установка аутентификационных куки
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
