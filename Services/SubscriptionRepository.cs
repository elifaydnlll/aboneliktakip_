using System;
using System.Collections.Generic;
using System.Linq;
using webproje.Models;

namespace webproje.Services
{
    public static class SubscriptionRepository
    {
        public static List<Subscription> Subscriptions { get; } = new List<Subscription>
        {
            new Subscription { Id = 1, Name = "Netflix", Price = 199.99m, Category = "Eğlence", StartDate = DateTime.Now.AddMonths(-5), RenewalDate = DateTime.Now.AddDays(10), Description = "Standart Plan" },
            new Subscription { Id = 2, Name = "Spotify", Price = 59.99m, Category = "Müzik", StartDate = DateTime.Now.AddMonths(-12), RenewalDate = DateTime.Now.AddDays(5), Description = "Öğrenci" },
            new Subscription { Id = 3, Name = "Adobe Creative Cloud", Price = 350.00m, Category = "İş", StartDate = DateTime.Now.AddMonths(-2), RenewalDate = DateTime.Now.AddDays(20), Description = "Tüm Uygulamalar" },
            new Subscription { Id = 4, Name = "Amazon Prime", Price = 39.00m, Category = "Alışveriş", StartDate = DateTime.Now.AddMonths(-6), RenewalDate = DateTime.Now.AddDays(2), Description = "Prime Video dahil" }
        };
    }
}
