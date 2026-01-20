using Microsoft.AspNetCore.Mvc;
using webproje.Models;
using webproje.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace webproje.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ReportsViewModel();
            var subscriptions = await _context.Subscriptions.ToListAsync();
            var bills = await _context.Bills.ToListAsync();

            // 1. Calculate Category Spends
            var catSpends = subscriptions
                .GroupBy(s => s.Category)
                .ToDictionary(g => g.Key, g => g.Sum(s => s.Price));
            
            model.CategorySpends = catSpends;

            if (catSpends.Any())
            {
                var topCat = catSpends.OrderByDescending(x => x.Value).First();
                model.TopCategoryName = topCat.Key;
                model.TopCategoryAmount = topCat.Value;
            }

            // 2. Calculate Monthly Spends (Simulation for the last 6 months)
            var now = DateTime.Now;
            for (int i = 5; i >= 0; i--)
            {
                var monthDate = now.AddMonths(-i);
                var monthName = monthDate.ToString("MMMM", new CultureInfo("tr-TR"));
                model.Months.Add(monthName);

                decimal totalForMonth = subscriptions
                    .Where(s => s.StartDate <= monthDate) 
                    .Sum(s => s.Price);
                
                totalForMonth += bills.Where(b => b.DueDate.Month == monthDate.Month).Sum(b => b.Amount);

                model.MonthlySpends.Add(totalForMonth);
            }

            model.AverageMonthlySpend = model.MonthlySpends.Any() ? model.MonthlySpends.Average() : 0;
            model.TotalYearlySpend = model.AverageMonthlySpend * 12;

            return View(model);
        }
    }
}
