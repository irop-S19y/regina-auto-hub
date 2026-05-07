using Microsoft.AspNetCore.Mvc;
using AutoServicesRegina.Data;
using Microsoft.AspNetCore.Authorization;

namespace AutoServicesRegina.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AutoServicesReginaDbContext _context;

        public AdminController(AutoServicesReginaDbContext context)
        {
            _context = context;
        }

        // 🔥 DELETE A COMMENT
        [HttpPost]
        public IActionResult DeleteComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);

            if (comment == null)
                return NotFound();

            int serviceId = comment.ServiceId;

            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return RedirectToAction("Ratings", "AllServices", new { id = serviceId });
        }

        // 🔥 DELETE RATING  and related comment
        [HttpPost]
        public IActionResult DeleteRating(int id)
        {
            var rating = _context.Ratings.FirstOrDefault(r => r.Id == id);

            if (rating == null)
                return NotFound();

            int serviceId = rating.ServiceId;

            // Find comment from the same user for this service
            var comment = _context.Comments
                .FirstOrDefault(c => c.UserId == rating.UserId && c.ServiceId == rating.ServiceId);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            _context.Ratings.Remove(rating);
            _context.SaveChanges();

            return RedirectToAction("Ratings", "AllServices", new { id = serviceId });
        }
            
            }
        }