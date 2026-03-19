using System;

namespace AutoServicesRegina.Data.Entities;

public class User

{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}

