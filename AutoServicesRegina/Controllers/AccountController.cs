using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using AutoServicesRegina.Data;
using AutoServicesRegina.Data.Entities;
using System.Net;
using System.Net.Mail;

namespace AutoServicesRegina.Controllers
{
    public class AccountController : Controller
    {
        private readonly AutoServicesReginaDbContext _context;
         
         
         public AccountController(AutoServicesReginaDbContext context)
        {
            _context = context;
        }
         

        private void SendEmail(string to, string link)
         {
        var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential("8d83724222e434", "dbf7308c281888"),
            EnableSsl = true
        };

        var message = new MailMessage("no-reply@autoservices.com", to)
        {
            Subject = "Reset your password",
            Body = $"<p>Click the link below to reset your password:</p><a href='{link}'>Reset Password</a>",
            IsBodyHtml = true
        };

        client.Send(message);
        }
        
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.EmailAddres == email);

            if (user == null)
            {
                return View("ForgotPasswordConfirmation");
            }

            // генеруємо токен
            user.ResetToken = Guid.NewGuid().ToString();
            user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);

            _context.SaveChanges();

            var link = $"http://localhost:5143/Account/ResetPassword?token={user.ResetToken}";

            SendEmail(email, link);

            return View("ForgotPasswordConfirmation");
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
        
            // 🔹 RESET PASSWORD (GET)
            public IActionResult ResetPassword(string token)
            {
                var user = _context.Users.FirstOrDefault(u => u.ResetToken == token);

               if (user == null || user.ResetTokenExpiry == null || user.ResetTokenExpiry < DateTime.UtcNow)
                {
                    return Content("Invalid or expired token");
                }

                return View();
            }

            // 🔹 RESET PASSWORD (POST)
                [HttpPost]
                public IActionResult ResetPassword(string token, string newPassword)
            {
                var user = _context.Users.FirstOrDefault(u => u.ResetToken == token);

                if (user == null || user.ResetTokenExpiry == null || user.ResetTokenExpiry < DateTime.UtcNow)
                {
                    return Content("Invalid or expired token");
                }

                var hasher = new PasswordHasher<User>();
                user.PasswordHash = hasher.HashPassword(user, newPassword);

                user.ResetToken = null;
                user.ResetTokenExpiry = null;

                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            
            
            
            }
        }