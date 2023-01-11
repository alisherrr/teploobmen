using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Claims;
using TeploobmenLibrary;
using TeploobmenWeb.Data;
using TeploobmenWeb.Models;

namespace TeploobmenWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        private readonly ApplicationContext _context;

        public LoginController(ILogger<LoginController> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _context = applicationContext;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);

            if(user != null)
            {
                var claims = new List<Claim> {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, "user"),
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateDefaultUser()
        {
            var user = new User { Email = "admin@mail.ru", Password = "12345", Name = "Admin" };
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }
    }
}