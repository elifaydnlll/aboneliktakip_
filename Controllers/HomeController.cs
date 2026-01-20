using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using webproje.Models;
using webproje.Data;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;

namespace webproje.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var subs = await _context.Subscriptions.ToListAsync();
        
        // Kullanıcı Ayarlarını Çek
        var user = await _userManager.GetUserAsync(User);
        string userName = "Misafir";
        string currency = "₺";
        decimal budget = 5000;

        if (user != null)
        {
            var settings = await _context.UserSettings.FirstOrDefaultAsync(u => u.UserId == user.Id);
            if (settings != null)
            {
                userName = settings.UserName;
                currency = settings.Currency;
                budget = settings.MonthlyBudget;
            }
            else
            {
                userName = user.UserName.Split('@')[0];
            }
        }

        var model = new DashboardViewModel
        {
            UserName = userName, // Dinamik isim
            Currency = currency,
            TotalBudget = budget,
            TotalSubscriptions = subs.Count,
            TotalMonthlyCost = subs.Sum(s => s.Price),
            TopCategory = subs.GroupBy(s => s.Category)
                              .OrderByDescending(g => g.Count())
                              .Select(g => g.Key)
                              .FirstOrDefault() ?? "Yok",
            UpcomingRenewal = subs
                              .Where(s => s.RenewalDate >= DateTime.Today)
                              .OrderBy(s => s.RenewalDate)
                              .FirstOrDefault(),
            RecentSubscriptions = subs.OrderByDescending(s => s.Id).Take(3).ToList(),
            
            // Populate Chart Data
            CategorySpending = subs.GroupBy(s => s.Category)
                                   .ToDictionary(g => g.Key, g => g.Sum(s => s.Price)),

            // Mock Alerts (Bunu şimdilik sabit bırakıyoruz veya ileride dinamik yapabiliriz)
            Alerts = new List<string> 
            { 
                "Netflix zam yaptı! Planınız 20% arttı.", 
                "Amazon Prime deneme süreniz 3 gün sonra bitiyor.",
                "Harcamalarınız geçen aya göre %15 daha az. Harika!"
            }
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
