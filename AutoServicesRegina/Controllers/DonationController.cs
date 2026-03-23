using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace AutoServicesRegina.Controllers
{
    public class DonationController : Controller
    {
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

                SuccessUrl = "https://localhost:5001/donation/success",
                CancelUrl = "https://localhost:5001/donation/cancel"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }
    }
}
        

    

