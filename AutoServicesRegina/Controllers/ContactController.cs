
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace AutoServicesRegina.Controllers
{
    public class ContactController : Controller
    {
        private readonly IConfiguration _configuration;

        public ContactController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }

        // POST (send email)
        [HttpPost]
        public IActionResult SendMessage(string name, string email, string message)
        {var config = _configuration.GetSection("Mailtrap");

        var client = new SmtpClient(config["Host"], int.Parse(config["Port"]))
        {
            Credentials = new NetworkCredential(config["User"], config["Pass"]),
            EnableSsl = true
        };
        
            
                 
    

            var mail = new MailMessage
            {
                From = new MailAddress("test@mailtrap.io"),
                Subject = $"New message from {name}",
                Body = $"Name: {name}\nEmail: {email}\n\nMessage:\n{message}",
                IsBodyHtml = false
            };

            mail.To.Add("test@mailtrap.io");

            client.Send(mail);

            ViewBag.Success = "Message sent successfully!";
            return View("Index");
        }
    }
}