using Microsoft.AspNetCore.Mvc;
using AutoServicesRegina.Data;
using AutoServicesRegina.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AutoServicesRegina.Controllers
{    
     // Only authenticated users can add reviews
    [Authorize] 
    public class ReviewsController : Controller
    {
        private readonly AutoServicesReginaDbContext _context;

        public ReviewsController(AutoServicesReginaDbContext context)
        {
            _context = context;
        }

         [HttpPost]
    
      // Add or update review and rating
    public IActionResult Add([FromBody] ReviewDto dto)
  {
    var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
     
     // Check if user is authenticated
    if (!int.TryParse(userIdString, out int userId))
        return Unauthorized();

     // Find existing rating for this service
    var rating = _context.Ratings
        .FirstOrDefault(r => r.ServiceId == dto.ServiceId && r.UserId == userId);

    if (rating != null)
    {
       // Update existing rating
        rating.Value = dto.Value;

       // Find related comment
        var comment = _context.Comments
            .FirstOrDefault(c => c.RatingId == rating.Id);

        if (comment != null)
        {
            // Update existing comment
            comment.Text = dto.Text;
        }
        else
        {
             // Create new comment
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
        // Create new rating
        var newRating = new Rating
        {
            ServiceId = dto.ServiceId,
            UserId = userId,
            Value = dto.Value
        };

        _context.Ratings.Add(newRating);
        _context.SaveChanges(); // Save to generate Rating ID

        // Create comment linked to rating
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