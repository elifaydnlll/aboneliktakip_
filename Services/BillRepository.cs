using System;
using System.Collections.Generic;
using webproje.Models;

namespace webproje.Services
{
    public static class BillRepository
    {
        public static List<Bill> Bills { get; } = new List<Bill>
        {
            new Bill { Id = 1, Name = "Elektrik Faturası", Amount = 450.50m, DueDate = DateTime.Now.AddDays(5), IsPaid = false, SubscriberNumber = "123456789" },
            new Bill { Id = 2, Name = "Su Faturası", Amount = 120.00m, DueDate = DateTime.Now.AddDays(-2), IsPaid = false, SubscriberNumber = "987654321" },
            new Bill { Id = 3, Name = "İnternet", Amount = 250.00m, DueDate = DateTime.Now.AddDays(10), IsPaid = true, SubscriberNumber = "5555" }
        };
    }
}
