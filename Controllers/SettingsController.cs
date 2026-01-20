using Microsoft.AspNetCore.Mvc;
using webproje.Models;
using webproje.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace webproje.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SettingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            // Kullanıcının ayarlarını DB'den bul
            var settings = await _context.UserSettings.FirstOrDefaultAsync(u => u.UserId == user.Id);

            // Eğer ayar yoksa (ilk kez giriyorsa) varsayılan oluştur
            if (settings == null)
            {
                settings = new UserSetting
                {
                    UserId = user.Id,
                    UserName = user.UserName.Split('@')[0], // Email'den isim türet
                    MonthlyBudget = 5000,
                    Currency = "₺",
                    DarkMode = true
                };
                _context.UserSettings.Add(settings);
                await _context.SaveChangesAsync();
            }

            var model = new SettingsViewModel
            {
                UserName = settings.UserName,
                MonthlyBudget = settings.MonthlyBudget,
                Currency = settings.Currency,
                DarkMode = settings.DarkMode
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SettingsViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var settings = await _context.UserSettings.FirstOrDefaultAsync(u => u.UserId == user.Id);

            if (settings != null)
            {
                settings.UserName = model.UserName;
                settings.MonthlyBudget = model.MonthlyBudget;
                settings.Currency = model.Currency;
                settings.DarkMode = model.DarkMode;

                _context.Update(settings);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }
    }
}
