namespace webproje.Models
{
    // Simulating a database for user settings
    public static class UserSettings
    {
        public static string UserName { get; set; } = "Elif";
        public static decimal MonthlyBudget { get; set; } = 5000m;
        public static string Currency { get; set; } = "₺";
        public static bool DarkMode { get; set; } = true;
    }
}
