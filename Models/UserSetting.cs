using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace webproje.Models
{
    public class UserSetting
    {
        public int Id { get; set; }

        // Hangi kullanıcıya ait olduğu (IdentityUser ile ilişki)
        [Required]
        public string UserId { get; set; }

        public string UserName { get; set; } = "Kullanıcı";
        public decimal MonthlyBudget { get; set; } = 5000;
        public string Currency { get; set; } = "₺";
        public bool DarkMode { get; set; } = true;
    }
}
