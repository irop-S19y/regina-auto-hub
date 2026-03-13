using Microsoft.AspNetCore.Mvc;
using AutoServicesRegina.Models;
using System.Collections.Generic;

namespace AutoServicesRegina.Controllers
{
    public class AllServicesController : Controller
    {
        public static List<Service> services = new List<Service>();

        public IActionResult Index()
        {
            return View(services);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Service service)
        {
            services.Add(service);
            return RedirectToAction("Index");
        }
    }
}