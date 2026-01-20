using System;
using System.ComponentModel.DataAnnotations;

namespace webproje.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Abonelik Adı")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Fiyat")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Kategori")]
        public string Category { get; set; } = "Genel";

        [Display(Name = "Başlangıç Tarihi")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = "Yenilenme Tarihi")]
        [DataType(DataType.Date)]
        public DateTime RenewalDate { get; set; }

        public string BillingCycle { get; set; } = "Aylık";

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
    }
}
