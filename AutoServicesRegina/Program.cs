using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using AutoServicesRegina.Data;
using Microsoft.EntityFrameworkCore;
using Stripe;


namespace AutoServicesRegina;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
       
        StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
         
        // Add services
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddDbContext<AutoServicesReginaDbContext>(options =>
        options.UseSqlite("Data Source=db/AutoServicesReginaDb.sqlite"));
        

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });
             // Add Authrization
             builder.Services.AddAuthorization();
             
        var app = builder.Build();
          // Seed Database
         var db = app.Services.CreateScope().ServiceProvider.GetRequiredService<AutoServicesReginaDbContext>();

          DatabaseSeed.Seed(db);
         
              

            // Configure pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();   // Important
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
        
    }
}