using Microsoft.AspNetCore.Mvc;
using AutoServicesRegina.Models;
using System.Collections.Generic;
using AutoServicesRegina.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoServicesRegina.Controllers
{
     public class AllServicesController : Controller
     {
        private readonly AutoServicesReginaDbContext _context;

       public AllServicesController(AutoServicesReginaDbContext context)
{
         _context = context;
}
        public static List<Service> services = new List<Service>();

        public IActionResult Index()
        {
           var services = _context.Services
            .Include(s => s.Comments)
            .ToList();
            return View(services);
        }
        
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Add(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}