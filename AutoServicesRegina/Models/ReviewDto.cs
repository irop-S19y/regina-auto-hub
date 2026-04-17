using System;

namespace AutoServicesRegina.Models;

public class ReviewDto
{
    public int ServiceId { get; set; }
    public int Value { get; set; }
    public string Text { get; set; } = "";
}
