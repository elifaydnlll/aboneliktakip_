using System.Collections.Generic;

namespace webproje.Services
{
    public static class CategoryRepository
    {
        public static List<string> Categories { get; } = new List<string>
        {
            "Eğlence",
            "Müzik",
            "İş",
            "Alışveriş",
            "Eğitim",
            "Spor",
            "Ulaşım",
            "Yemek",
            "Ev",
            "Teknoloji"
        };
    }
}
