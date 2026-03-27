using System;
using AutoServicesRegina.Data.Entities;
using AutoServicesRegina.Models;
using Microsoft.EntityFrameworkCore;


namespace AutoServicesRegina.Data;

public static class DatabaseSeed
{
    public static void Seed(AutoServicesReginaDbContext context)
    {
        if (!context.Users.Any())
        {
            var users = new List<User>
            {
                new User
                {
                    FirstName = "John",
                    LastName = "Smith",
                    EmailAddres = "john.smith@test.com",
                    DateOfBirth = new DateTime(1990, 1, 1)
                },
                new User
                {
                    FirstName = "Anna",
                    LastName = "Brown",
                    EmailAddres = "anna.brown@test.com",
                    DateOfBirth = new DateTime(1992, 5, 10)
                },
                new User
                {
                    FirstName = "Mike",
                    LastName = "Johnson",
                    EmailAddres = "mike.johnson@test.com",
                    DateOfBirth = new DateTime(1988, 3, 15)
                },
                new User
                {
                    FirstName = "Sara",
                    LastName = "Wilson",
                    EmailAddres = "sara.wilson@test.com",
                    DateOfBirth = new DateTime(1995, 7, 20)
                },
                new User
                {
                    FirstName = "David",
                    LastName = "Taylor",
                    EmailAddres = "david.taylor@test.com",
                    DateOfBirth = new DateTime(1987, 9, 9)
                },
                new User
                {
                    FirstName = "Emma",
                    LastName = "Anderson",
                    EmailAddres = "emma.anderson@test.com",
                    DateOfBirth = new DateTime(1993, 2, 11)
                },
                new User
                {
                    FirstName = "Daniel",
                    LastName = "Thomas",
                    EmailAddres = "daniel.thomas@test.com",
                    DateOfBirth = new DateTime(1991, 4, 4)
                },
                new User
                {
                    FirstName = "Olivia",
                    LastName = "Moore",
                    EmailAddres = "olivia.moore@test.com",
                    DateOfBirth = new DateTime(1996, 6, 6)
                },
                new User
                {
                    FirstName = "Liam",
                    LastName = "Martin",
                    EmailAddres = "liam.martin@test.com",
                    DateOfBirth = new DateTime(1994, 8, 8)
                },
                new User
                {
                    FirstName = "Sophia",
                    LastName = "Lee",
                    EmailAddres = "sophia.lee@test.com",
                    DateOfBirth = new DateTime(1997, 12, 12)
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        if (!context.Services.Any())
        {
            var services = new List<Service>
            {
                new Service
                {
                    Name = "Minute Muffler & Brake Regina",
                    Address = "1300 Broad St",
                    City = "Regina",
                    Phone = "306-525-2121",
                    Website = "https://minutemuffler.com",
                    Description = "Brake repair, exhaust systems and general auto repair.",
                    WorkingHours = "Mon-Fri 8:00-17:30",
                    Rating = 5
                },
                new Service
                {
                    Name = "Kal Tire Regina",
                    Address = "1775 Albert St",
                    City = "Regina",
                    Phone = "306-757-6060",
                    Website = "https://kaltire.com",
                    Description = "Tire replacement and balancing.",
                    WorkingHours = "Mon-Sat 8:00-18:00",
                    Rating = 5
                },
                new Service
                {
                    Name = "Great Canadian Oil Change",
                    Address = "440 Broad St",
                    City = "Regina",
                    Phone = "306-522-5823",
                    Website = "https://gcoc.ca",
                    Description = "Quick oil change service.",
                    WorkingHours = "Mon-Sat 8:00-18:00",
                    Rating = 4
                },
                new Service
                {
                    Name = "Regina Auto Body Shop",
                    Address = "1601 Winnipeg St",
                    City = "Regina",
                    Phone = "306-569-2244",
                    Website = "https://reginaautobody.ca",
                    Description = "Collision repair and body painting.",
                    WorkingHours = "Mon-Fri 8:00-17:00",
                    Rating = 4
                },
                new Service
                {
                    Name = "Canadian Tire Auto Service",
                    Address = "2225 Prince of Wales Dr",
                    City = "Regina",
                    Phone = "306-522-8473",
                    Website = "https://canadiantire.ca",
                    Description = "Auto repair and tire service.",
                    WorkingHours = "Mon-Sat 8:00-20:00",
                    Rating = 4
                },
                new Service
                {
                    Name = "Capital Ford Service Center",
                    Address = "1201 Pasqua St",
                    City = "Regina",
                    Phone = "306-543-5410",
                    Website = "https://capitalfordregina.ca",
                    Description = "Authorized Ford maintenance.",
                    WorkingHours = "Mon-Fri 7:30-17:30",
                    Rating = 5
                },
                new Service
                {
                    Name = "Midas Auto Service Regina",
                    Address = "200 Albert St",
                    City = "Regina",
                    Phone = "306-522-6500",
                    Website = "https://midas.com",
                    Description = "Brake repair and suspension service.",
                    WorkingHours = "Mon-Fri 8:00-17:30",
                    Rating = 4
                },
                new Service
                {
                    Name = "Speedy Glass Regina",
                    Address = "1800 Victoria Ave",
                    City = "Regina",
                    Phone = "306-525-8880",
                    Website = "https://speedyglass.ca",
                    Description = "Windshield repair and replacement.",
                    WorkingHours = "Mon-Fri 8:00-17:00",
                    Rating = 5
                },
                new Service
                {
                    Name = "Driven Automotive Regina",
                    Address = "100 Dewdney Ave",
                    City = "Regina",
                    Phone = "306-565-5500",
                    Website = "https://drivenauto.ca",
                    Description = "Full mechanical service.",
                    WorkingHours = "Mon-Fri 8:00-17:00",
                    Rating = 4
                },
                new Service
                {
                    Name = "A1 Automotive Repair",
                    Address = "925 Winnipeg St",
                    City = "Regina",
                    Phone = "306-522-1234",
                    Website = "https://a1autorepair.ca",
                    Description = "General auto repair.",
                    WorkingHours = "Mon-Fri 8:00-17:00",
                    Rating = 4
                }
            };

            context.Services.AddRange(services);
            context.SaveChanges();
        }
    }
}
