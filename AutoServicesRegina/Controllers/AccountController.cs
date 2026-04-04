using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using AutoServicesRegina.Data;
using AutoServicesRegina.Data.Entities;

namespace AutoServicesRegina.Controllers
{
    public class AccountController : Controller
    {
        private readonly AutoServicesReginaDbContext _context;
         
         
        
        
        public AccountController(AutoServicesReginaDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        
            [HttpPost]
            public async Task<IActionResult> Login(string email, string password)
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.EmailAddres == email);

                if (user == null)
                {
                    ViewBag.Error = "User not found";
                    return View();
                }

                var hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(user, user.PasswordHash, password);

                if (result == PasswordVerificationResult.Failed)
                {
                    ViewBag.Error = "Wrong password";
                    return View();
                }

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.EmailAddres),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Home");
}
         
         // GET
        public IActionResult Register()
        {
            return View();
        }

         
         // 🔹 REGISTER POST
        [HttpPost]
        public IActionResult Register(string firstName, string lastName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Fill all fields";
                return View();
            }

            var existingUser = _context.Users
                .FirstOrDefault(u => u.EmailAddres == email);

            if (existingUser != null)
            {
                ViewBag.Error = "User already exists";
                return View();
            }

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddres = email,
                Role = "User"
            };

            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        // 🔹 LOGOUT
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}