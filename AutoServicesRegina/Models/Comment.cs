using System;

namespace AutoServicesRegina.Models;

    public class Comment
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Text { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int AutoServiceId { get; set; }
    public Service AutoService { get; set; }
}