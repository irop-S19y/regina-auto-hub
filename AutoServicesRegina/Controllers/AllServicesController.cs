using Microsoft.AspNetCore.Mvc;
using AutoServicesRegina.Models;
using System.Collections.Generic;
using AutoServicesRegina.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        .Include(s => s.Ratings)
        .ThenInclude(r => r.User)
        .ToList();

    foreach (var s in services)
    {
        s.Rating = s.Ratings.Any()
            ? s.Ratings.Average(r => r.Value)
            : 0;

        s.RatingCount = s.Ratings.Count;
    }

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
         [HttpPost]
      public IActionResult Rate([FromBody] RatingDto dto)
{
    var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (!int.TryParse(userIdString, out int userId))
        return Unauthorized();
    var existing = _context.Ratings
        .FirstOrDefault(r => r.ServiceId == dto.ServiceId && r.UserId == userId);

    if (existing != null)
    {
        existing.Value = dto.Value; // 
    }
    else
    {
        _context.Ratings.Add(new Rating
        {
            ServiceId = dto.ServiceId,
            Value = dto.Value,
            UserId = userId
        });
    }

            _context.SaveChanges();

            return Json(new { success = true });
        }
        public IActionResult Ratings(int id)
        {
            var service = _context.Services
                .Include(s => s.Ratings)
                .ThenInclude(r => r.User)
                .FirstOrDefault(s => s.Id == id);

            if (service == null)
                return NotFound();

            return View(service);
        }
   
   }
    
        }
           