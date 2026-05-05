using Microsoft.AspNetCore.Mvc;
using AutoServicesRegina.Models;
using System.Collections.Generic;
using AutoServicesRegina.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

        public IActionResult Index(string search)
      {
        var query = _context.Services
        .Include(s => s.Comments)
        .Include(s => s.Ratings)
        .ThenInclude(r => r.User)
        .AsQueryable();

            // 🔍 name 
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.Name.Contains(search));
            }

        var services = query.ToList();

        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (int.TryParse(userIdString, out int userId))
{
        foreach (var s in services)
        {
            var myRating = s.Ratings
                .FirstOrDefault(r => r.UserId == userId);

            s.MyRating = myRating?.Value ?? 0;
        }
    }

    foreach (var s in services)
    {
        s.Rating = s.Ratings.Any()
            ? s.Ratings.Average(r => r.Value)
            : 0;

        s.RatingCount = s.Ratings.Count;
    }

    return View(services);
}
        // GET
       [Authorize(Roles = "Admin")]
       public IActionResult Edit(int id)
       {
        var service = _context.Services.Find(id);

            if (service == null)
                return NotFound();

            return View(service);
        }

         [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Service service)
        {
            _context.Services.Update(service);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
          
          //GET
         [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var service = _context.Services.Find(id);

            if (service == null)
                return NotFound();

            return View(service);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var service = _context.Services.Find(id);

            if (service == null)
                return NotFound();

            _context.Services.Remove(service);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

         [HttpPost]
            [Authorize(Roles = "Admin")]
            public IActionResult Add(Service service)
            {
                if (!ModelState.IsValid)
                {
                    return View(service);
                }

                _context.Services.Add(service);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
         
         [HttpPost]
         [Authorize]
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
        public IActionResult Ratings(int id, int? star)
        
        {
    
            var service = _context.Services
                .Include(s => s.Ratings)
                .ThenInclude(r => r.User)
                
                .Include(s => s.Comments)
                .ThenInclude(c => c.User)
                
                .FirstOrDefault(s => s.Id == id);

            if (service == null)
                return NotFound();

            // ВСІ рейтинги (для статистики)
            var allRatings = service.Ratings;

            // статистика (з усіх)
            ViewBag.Avg = allRatings.Any() ? allRatings.Average(r => r.Value) : 0;
            ViewBag.Count = allRatings.Count;

            ViewBag.Five = allRatings.Count(r => r.Value == 5);
            ViewBag.Four = allRatings.Count(r => r.Value == 4);
            ViewBag.Three = allRatings.Count(r => r.Value == 3);
            ViewBag.Two = allRatings.Count(r => r.Value == 2);
            ViewBag.One = allRatings.Count(r => r.Value == 1);

            ViewBag.SelectedStar = star;

            // ФІЛЬТР (тільки для списку)
            if (star.HasValue)
            {
                service.Ratings = allRatings
                    .Where(r => r.Value == star.Value)
                    .ToList();
            }
            else
            {
                service.Ratings = allRatings;
            }

             var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

             if (int.TryParse(userIdString, out int userId))
           
            {
                var myRating = service.Ratings
                    .FirstOrDefault(r => r.UserId == userId);

                ViewBag.MyRating = myRating?.Value ?? 0;
            }

            return View(service);
             
        }
     
    }               
       
}
            
    
                