using Microsoft.AspNetCore.Mvc;
using AutoServicesRegina.Data;
using AutoServicesRegina.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AutoServicesRegina.Controllers
{    
    [Authorize] 
    public class ReviewsController : Controller
    {
        private readonly AutoServicesReginaDbContext _context;

        public ReviewsController(AutoServicesReginaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Add([FromBody] ReviewDto dto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out int userId))
                return Unauthorized();

            // ⭐ Rating
            var rating = _context.Ratings
                .FirstOrDefault(r => r.ServiceId == dto.ServiceId && r.UserId == userId);

            if (rating != null)
                rating.Value = dto.Value;
            else
                _context.Ratings.Add(new Rating
                {
                    ServiceId = dto.ServiceId,
                    UserId = userId,
                    Value = dto.Value
                });

            // 💬 Comment
            var comment = _context.Comments
                .FirstOrDefault(c => c.ServiceId == dto.ServiceId && c.UserId == userId);

            if (comment != null)
                comment.Text = dto.Text;
            else
                _context.Comments.Add(new Comment
                {
                    ServiceId = dto.ServiceId,
                    UserId = userId,
                    Text = dto.Text
                });

            _context.SaveChanges();

            return Ok();
        }
    }
}