
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
          public async Task<IActionResult> SendMessage(string name, string email, string message)
        {
          // Validate form fields
          if (string.IsNullOrWhiteSpace(name) ||
          string.IsNullOrWhiteSpace(email) ||
          string.IsNullOrWhiteSpace(message))
            {
                ViewBag.Error = "All fields are required";
                return View("Index");
            }
             // Basic email validation
            if (!email.Contains("@") || !email.Contains("."))
            {
                ViewBag.Error = "Invalid email";
                return View("Index");
            }
          // Load Mailtrap configuration
          var config = _configuration.GetSection("Mailtrap");

          int port = config.GetValue<int>("Port");

           using var client = new SmtpClient(config["Host"], port);

          client.Credentials = new NetworkCredential(config["User"], config["Pass"]);
          client.EnableSsl = true;

          var fromEmail = config["From"];
          
          var toEmail = config["To"];
           // Check email configuration
          if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(toEmail))
          {
             ViewBag.Error = "Email config missing";
             return View("Index");
          }
        
            // Create email message
             using var mail = new MailMessage
         {
            From = new MailAddress(fromEmail),
            Subject = $"New message from {name}",
            Body = $"Name: {name}\nEmail: {email}\n\nMessage:\n{message}",
            IsBodyHtml = false
         };

            mail.To.Add(toEmail);
                        
    

         
               try
            {
                 // Send email
                await client.SendMailAsync(mail);
                ViewBag.Success = "Message sent successfully!";
            }
             
             catch 
            {
               ViewBag.Error = "Failed to send message. Please try again later.";
            }

            return View("Index");
            

        
        }
    
    }
}