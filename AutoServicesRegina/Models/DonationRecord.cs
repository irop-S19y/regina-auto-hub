using System;

namespace AutoServicesRegina.Models;

public class DonationRecord
{
    public int Id { get; set; }

    public long Amount { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public string? StripePaymentId { get; set; }
}