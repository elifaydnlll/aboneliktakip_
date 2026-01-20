using Microsoft.AspNetCore.Mvc;
using webproje.Models;
using webproje.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;

namespace webproje.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string category)
        {
            var subscriptions = from s in _context.Subscriptions
                                select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                subscriptions = subscriptions.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                subscriptions = subscriptions.Where(s => s.Category == category);
            }

            // Kategorileri dropdown için viewbag'e atalım
            ViewBag.Categories = Services.CategoryRepository.Categories;
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentCategory = category;

            return View(await subscriptions.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Category,RenewalDate,Description")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subscription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subscription);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var sub = await _context.Subscriptions.FirstOrDefaultAsync(m => m.Id == id);
            if (sub == null) return NotFound();

            return View(sub);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sub = await _context.Subscriptions.FindAsync(id);
            if (sub == null) return NotFound();
            return View(sub);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Category,RenewalDate,Description")] Subscription subscription)
        {
            if (id != subscription.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Subscriptions.Any(e => e.Id == subscription.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subscription);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var sub = await _context.Subscriptions.FirstOrDefaultAsync(m => m.Id == id);
            if (sub == null) return NotFound();

            return View(sub);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sub = await _context.Subscriptions.FindAsync(id);
            if (sub != null)
            {
                _context.Subscriptions.Remove(sub);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
