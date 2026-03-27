using System;
using AutoServicesRegina.Data.Entities;
using AutoServicesRegina.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AutoServicesRegina.Data;

public class AutoServicesReginaDbContext : DbContext
{
    private const string DatabaseName = "AutoServicesReginaDb.sqlite";

    public DbSet<User> Users { get; set; }
    // edd service
    public DbSet<Service> Services { get; set; }
    // edd Donate
    public DbSet<DonationRecord> Donations { get; set; }

    public AutoServicesReginaDbContext()
    {
    }

    public AutoServicesReginaDbContext(DbContextOptions<AutoServicesReginaDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var databasePath = FileSystemHelper.GetDatabasePath(DatabaseName);
            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }
    }
}