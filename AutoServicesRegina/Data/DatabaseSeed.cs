using System;
using AutoServicesRegina.Data.Entities;

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
    }
}