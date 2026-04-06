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

        
        {
            var services = new List<Service>
            {
                new Service
                {
                    Name = "Minute Muffler & Brake Regina",
                    Address = "240 Victoria Ave E, Regina, SK S4N 0N4",
                    City = "Regina",
                    Phone = "+13065652044",
                    Website = "https://minutemuffler.com",
                    Description = "Brake repair, exhaust systems and general auto repair.",
                    WorkingHours = "Mon-Fri 8:00-17:30",
                    Rating = 5
                },
                new Service
                {
                    Name = "Kal Tire Regina",
                    Address = "804 Albert St, Regina, SK S4R 2P5",
                    City = "Regina",
                    Phone = "+13067752283",
                    Website = "https://kaltire.com",
                    Description = "Tire replacement and balancing.",
                    WorkingHours = "Mon-Sat 8:00-18:00",
                    Rating = 5
                },
                new Service
                {
                    Name = "Great Canadian Oil Change",
                    Address = "907 Albert St, Regina, SK S4R 2P6",
                    City = "Regina",
                    Phone = "+13065464773",
                    Website = "https://gcoc.ca",
                    Description = "Quick oil change service.",
                    WorkingHours = "Mon-Sat 8:00-18:00",
                    Rating = 4
                },
                new Service
                {
                    Name = "Regina Auto Body Shop",
                    Address = "1800 Angus St, Regina, SK S4T 1Z4",
                    City = "Regina",
                    Phone = "+13067576683",
                    Website = "http://www.reginaautobody.ca/",
                    Description = "Collision repair and body painting.",
                    WorkingHours = "Mon-Fri 8:00-17:00",
                    Rating = 4
                },
                new Service
                {
                    Name = "Canadian Tire Auto Service",
                    Address = "655 Albert St, Regina, SK S4R 2P4",
                    City = "Regina",
                    Phone = "+13067578608",
                    Website = "https://www.canadiantire.ca/en/store-details/sk/regina-north-albert-sk-275.html?utm_source=google&utm_medium=lss&utm_content=275",
                    Description = "Auto repair and tire service.",
                    WorkingHours = "Mon-Sat 8:00-20:00",
                    Rating = 4
                },
                new Service
                {
                    Name = "Capital Ford Service Center",
                    Address = "1201 Pasqua St N, Regina, SK S4X 4P7",
                    City = "Regina",
                    Phone = "+13065435410",
                    Website = "http://www.capitalfordlincoln.com/?utm_source=google&utm_medium=organic&utm_campaign=googlemybusiness",
                    Description = "Authorized Ford maintenance.",
                    WorkingHours = "Mon-Fri 7:30-17:30",
                    Rating = 5
                },
                new Service
                {
                    Name = "Midas Auto Service Regina",
                    Address = "149 Albert St N, Regina, SK S4R 2N3",
                    City = "Regina",
                    Phone = "+13065439191",
                    Website = "https://www.midas.com/regina/store.aspx?shopnum=9803&dmanum=723",
                    Description = "Brake repair and suspension service.",
                    WorkingHours = "Mon-Fri 8:00-17:30",
                    Rating = 4
                },
                new Service
                {
                    Name = "Speedy Glass Regina",
                    Address = "4525 Albert St, Regina, SK S4S 6B6",
                    City = "Regina",
                    Phone = "+13063371020",
                    Website = "https://www.speedyglass.ca/en/service-centre/sk/regina/speedy-glass-regina-south?utm_source=google&utm_medium=local&utm_campaign=Speedy%20Glass%20Regina%20South",
                    Description = "Windshield repair and replacement.",
                    WorkingHours = "Mon-Fri 8:00-17:00",
                    Rating = 5
                },
                new Service
                {
                    Name = "Driven Automotive Regina",
                    Address = "555 Broad St, Regina, SK S4R 1X5",
                    City = "Regina",
                    Phone = "+13065692886",
                    Website = "http://www.drivenautomotive.ca/",
                    Description = "Full mechanical service.",
                    WorkingHours = "Mon-Fri 8:00-17:00",
                    Rating = 4
                },
                new Service
                {
                    Name = "A1 Automotive Repair",
                    Address = "1750 Reynolds St, Regina, SK S4N 5P1",
                    City = "Regina",
                    Phone = "+13065651221",
                    Website = "",
                    Description = "General auto repair.",
                    WorkingHours = "Mon-Fri 8:00-17:00",
                    Rating = 4
                }
            };

                        foreach (var newService in services)
            {
                var existing = context.Services
                    .FirstOrDefault(s => s.Name == newService.Name);

                if (existing == null)
                {
                    context.Services.Add(newService);
                }
                else
                {
                    existing.Address = newService.Address;
                    existing.Phone = newService.Phone;
                    existing.Website = newService.Website;
                    existing.Description = newService.Description;
                    existing.WorkingHours = newService.WorkingHours;
                    existing.City = newService.City;
                }
            }

                 context.SaveChanges();
        
                 }
                   
                   // ⭐ Seed Ratings
            if (!context.Ratings.Any())
            {
                context.Ratings.AddRange(
                    new Rating { ServiceId = 1, UserId = 1, Value = 5 },
                    new Rating { ServiceId = 1, UserId = 2, Value = 4 },
                    new Rating { ServiceId = 1, UserId = 3, Value = 5 },

                    new Rating { ServiceId = 2, UserId = 4, Value = 3 },
                    new Rating { ServiceId = 2, UserId = 5, Value = 4 },

                    new Rating { ServiceId = 3, UserId = 6, Value = 5 },
                    new Rating { ServiceId = 3, UserId = 7, Value = 5 }
                );

                context.SaveChanges();
            }
                
                            // 💬 Seed Comments
                if (!context.Comments.Any())
                {
                    context.Comments.AddRange(
                        new Comment { ServiceId = 1, UserId = 1, Text = "Amazing service!" },
                        new Comment { ServiceId = 1, UserId = 2, Text = "Very fast and friendly" },
                        new Comment { ServiceId = 1, UserId = 3, Text = "Highly recommend!" },

                        new Comment { ServiceId = 2, UserId = 4, Text = "Good but немного дорого" },
                        new Comment { ServiceId = 2, UserId = 5, Text = "Okay service" },

                        new Comment { ServiceId = 3, UserId = 6, Text = "Best oil change ever" }
                    );

                    context.SaveChanges();
        }
               
               
              
    }
}
