using System;
using AutoServicesRegina.Models;
using AutoServicesRegina.Data.Entities;
namespace AutoServicesRegina.Models;

    public class Comment
{
    public int Id { get; set; }

    public int UserId { get; set; }
     public User? User { get; set; }

    public string Text { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int ServiceId { get; set; }
    public Service? Service { get; set; }
    
    public int? RatingId { get; set; }
    public Rating? Rating { get; set; }

}
