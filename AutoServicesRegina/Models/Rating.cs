using System;
using AutoServicesRegina.Data.Entities;
namespace AutoServicesRegina.Models;

public class Rating
{
    public int Id { get; set; }

    public int Value { get; set; } // 1–5 stars

    public int ServiceId { get; set; }
    public Service? Service { get; set; }

    public int UserId { get; set; } //  1 reting 1 people
    public User? User { get; set; }

 }
