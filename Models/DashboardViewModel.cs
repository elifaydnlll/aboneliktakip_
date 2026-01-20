using System.Collections.Generic;

namespace webproje.Models
{
    public class DashboardViewModel
    {
        public decimal TotalMonthlyCost { get; set; }
        public int TotalSubscriptions { get; set; }
        public string TopCategory { get; set; } = "Yok";
        public Subscription? UpcomingRenewal { get; set; }
        public List<Subscription> RecentSubscriptions { get; set; } = new List<Subscription>();
        
        // New Features for rich UI
        public Dictionary<string, decimal> CategorySpending { get; set; } = new Dictionary<string, decimal>();
        public decimal TotalBudget { get; set; } = 2500.00m; // Mock budget
        public List<string> Alerts { get; set; } = new List<string>();
        
        public string UserName { get; set; } = "Kullanıcı";
        public string Currency { get; set; } = "₺";
    }
}
