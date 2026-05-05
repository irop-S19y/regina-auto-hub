using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using AutoServicesRegina.Models;
using AutoServicesRegina.Data;


namespace AutoServicesRegina.Controllers
{
    public class DonationController : Controller
    {
       private readonly AutoServicesReginaDbContext _context;
       private readonly IConfiguration _configuration;

       public DonationController(AutoServicesReginaDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
                
        
         public IActionResult Index()
        {
            var totalCents = _context.Donations.Sum(d => d.Amount);

            decimal totalDollars = totalCents / 100m;

            ViewBag.Total = totalDollars;

            return View();
        }

        
        public IActionResult Donate(int amount)
        {
            var domain = _configuration["AppUrl"];
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },

                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "cad",
                            UnitAmount = amount * 100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Project Donation"
                            }
                        },
                        Quantity = 1
                    }
                },

                Mode = "payment",

                SuccessUrl = $"{domain}/Donation/success?sessionId={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{domain}/Donation/cancel"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        public IActionResult Success(string sessionId)
       {
        var service = new SessionService();
        var session = service.Get(sessionId);

        if (session.PaymentStatus != "paid")
        {
            return RedirectToAction("Cancel");
        }

        if (_context.Donations.Any(d => d.StripeSessionId == sessionId))
        {
            return View();
        }

        var donation = new DonationRecord
        {
            Amount = session.AmountTotal ?? 0,
            Date = DateTime.UtcNow,
            StripeSessionId = sessionId
        };

        _context.Donations.Add(donation);
        _context.SaveChanges();

        return View();
    
    }


    

        public IActionResult Cancel()
        {
            return View();
        }
   }
}
        

    

