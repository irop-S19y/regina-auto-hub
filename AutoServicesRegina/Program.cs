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
         
        // Add MVC services
        builder.Services.AddControllersWithViews();
        
        // Configure SQLite database
        builder.Services.AddDbContext<AutoServicesReginaDbContext>(options =>
        options.UseSqlite("Data Source=db/AutoServicesReginaDb.sqlite"));
        
        // Configure cookie authentication
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
       {
            options.LoginPath = "/Account/Login";
             
             // Redirect unauthorized users to login page
            options.Events.OnRedirectToLogin = context =>
            {
                var returnUrl = context.Request.Path + context.Request.QueryString;

                context.Response.Redirect($"/Account/Login?returnUrl={returnUrl}&message=login_required");
                return Task.CompletedTask;
            };
        });
             // Add Authrization
             builder.Services.AddAuthorization();
             
        var app = builder.Build();
          // Seed Database
         var db = app.Services.CreateScope().ServiceProvider.GetRequiredService<AutoServicesReginaDbContext>();

          DatabaseSeed.Seed(db);
         
              

            // Configure middleware pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
         
        // Enable authentication and authorization
        app.UseAuthentication();   // Important
        app.UseAuthorization();
        
         // Configure default route
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
        
    }
}