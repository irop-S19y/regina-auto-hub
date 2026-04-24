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

    // 🔥 знайти існуючий рейтинг
    var rating = _context.Ratings
        .FirstOrDefault(r => r.ServiceId == dto.ServiceId && r.UserId == userId);

    if (rating != null)
    {
        // оновити рейтинг
        rating.Value = dto.Value;

        // 🔥 знайти коментар через RatingId
        var comment = _context.Comments
            .FirstOrDefault(c => c.RatingId == rating.Id);

        if (comment != null)
        {
            comment.Text = dto.Text;
        }
        else
        {
            _context.Comments.Add(new Comment
            {
                ServiceId = dto.ServiceId,
                UserId = userId,
                Text = dto.Text,
                RatingId = rating.Id
            });
        }
    }
    else
    {
        // 🔥 створити рейтинг
        var newRating = new Rating
        {
            ServiceId = dto.ServiceId,
            UserId = userId,
            Value = dto.Value
        };

        _context.Ratings.Add(newRating);
        _context.SaveChanges(); // отримати Id

        // 🔥 створити коментар і прив’язати
        var newComment = new Comment
        {
            ServiceId = dto.ServiceId,
            UserId = userId,
            Text = dto.Text,
            RatingId = newRating.Id
        };

        _context.Comments.Add(newComment);
    }

    _context.SaveChanges();

    return Ok();
}
    }
}