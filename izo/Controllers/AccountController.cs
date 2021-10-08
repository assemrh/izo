using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using izo.Data;
using izo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace izo.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataDbContext _context;

        public AccountController(DataDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            ReturnUrl = string.IsNullOrWhiteSpace(ReturnUrl) ? "/" : ReturnUrl;
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect(ReturnUrl);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login, string ReturnUrl)
        {
            Task<User> _task;
            await (_task = _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password));
            if (_task.IsCompleted && _task.Result is not null)
            {
                User _user = _task.Result;
                var clamais = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,_user.Id.ToString() ),
                    new Claim(ClaimTypes.Name, _user.Username),
                    new Claim(ClaimTypes.Email, _user.Email),
                    new Claim(ClaimTypes.Role, _user.Role),
                };

                var userIdentity1 = new ClaimsIdentity(clamais, CookieAuthenticationDefaults.AuthenticationScheme);
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity1 });


                await HttpContext.SignInAsync(userPrincipal);

                if (!string.IsNullOrWhiteSpace(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            ModelState.AddModelError("", "Email veya Şifre yanlış girdiniz");
            return View(login);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("login", "Account");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
    }
}
