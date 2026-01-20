using Microsoft.AspNetCore.Mvc;
using webproje.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace webproje.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalendarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetEvents()
        {
            var events = new List<object>();

            var subscriptions = await _context.Subscriptions.ToListAsync();
            var bills = await _context.Bills.ToListAsync();

            // Abonelikleri ekle
            foreach (var sub in subscriptions)
            {
                // Gelecek 1 yıl içindeki tekrarları ekleyelim
                for (int i = 0; i < 12; i++)
                {
                    var date = sub.RenewalDate.AddMonths(i);
                    events.Add(new
                    {
                        title = $"{sub.Name} ({sub.Price}₺)",
                        start = date.ToString("yyyy-MM-dd"),
                        backgroundColor = "#6366f1", // Primary color
                        borderColor = "#4338ca",
                        url = $"/Subscription/Details/{sub.Id}"
                    });
                }
            }

            // Faturaları ekle
            foreach (var bill in bills)
            {
                events.Add(new
                {
                    title = $"{bill.Name} ({bill.Amount}₺)",
                    start = bill.DueDate.ToString("yyyy-MM-dd"),
                    backgroundColor = bill.IsPaid ? "#22c55e" : "#ef4444", // Green if paid, Red if unpaid
                    borderColor = bill.IsPaid ? "#15803d" : "#b91c1c",
                    url = $"/Bill/Edit/{bill.Id}"
                });
            }

            return Json(events);
        }
    }
}
