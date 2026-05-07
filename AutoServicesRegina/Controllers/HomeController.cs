using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AutoServicesRegina.Models;
using Microsoft.EntityFrameworkCore;
using AutoServicesRegina.Data;
namespace AutoServicesRegina.Controllers;

public class HomeController : Controller
{
    private readonly AutoServicesReginaDbContext _context;

    public HomeController(AutoServicesReginaDbContext context)
    {
        _context = context;
    }
 
     // Display homepage with top-rated services
    public IActionResult Index()
   
{
    var topServices = _context.Services
    .Include(s => s.Ratings)
    // Sort services by average rating
    .OrderByDescending(s => s.Ratings.Any() ? s.Ratings.Average(r => r.Value) : 0)
    .Take(3)
    .ToList();

    return View(topServices);
}
     
     // Privacy policy page
    public IActionResult Privacy()
    {
        return View();
    }

     // Display error page
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
