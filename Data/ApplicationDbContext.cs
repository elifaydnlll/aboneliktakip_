using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webproje.Models;

namespace webproje.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        // Ayarları da DB'de tutmak için bir entity lazım, şimdilik statik kullanıp
        // DB'den yüklenen bir yapı kuracağız veya basitçe Setting entity'si ekleyebiliriz.
        // Ama şimdilik en kritik olan Abonelik ve Faturalar.
    }
}
