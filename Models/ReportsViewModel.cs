using System;
using System.Collections.Generic;

namespace webproje.Models
{
    public class ReportsViewModel
    {
        public decimal TotalYearlySpend { get; set; }
        public decimal AverageMonthlySpend { get; set; }
        public string TopCategoryName { get; set; }
        public decimal TopCategoryAmount { get; set; }
        
        // Data for charts
        public List<string> Months { get; set; } = new List<string>();
        public List<decimal> MonthlySpends { get; set; } = new List<decimal>();
        
        public Dictionary<string, decimal> CategorySpends { get; set; } = new Dictionary<string, decimal>();
    }
}
