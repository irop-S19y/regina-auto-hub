using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using AutoServicesRegina.Models;
using AutoServicesRegina.Data;


namespace AutoServicesRegina.Controllers
{
    public class DonationController : Controller
    {
        private readonly AutoServicesReginaDbContext _context;

        public DonationController(AutoServicesReginaDbContext context)
        {
            _context = context;
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

                SuccessUrl = $"http://localhost:5143/Donation/success?amount={amount}",
                CancelUrl = "http://localhost:5143/Donation/cancel"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        public IActionResult Success(int amount)
{
    if (TempData["PaymentSaved"] != null)
    {
        return View();
    }

    var donation = new DonationRecord
    {
        Amount = amount * 100,
        Date = DateTime.Now
    };

    _context.Donations.Add(donation);
    _context.SaveChanges();

    TempData["PaymentSaved"] = true;

    return View();
}
    

        public IActionResult Cancel()
        {
            return View();
        }
        }
}
        

    

