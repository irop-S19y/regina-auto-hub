using System;

namespace AutoServicesRegina.Models;

public class Rating
{
    public int Id { get; set; }

    public int Value { get; set; } // 1–5 stars

    public int ServiceId { get; set; }
    public Service? Service { get; set; }

    public string UserId { get; set; } //  1 reting 1 people
}