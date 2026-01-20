using System;
using System.ComponentModel.DataAnnotations;

namespace webproje.Models
{
    public class Bill
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Fatura Adı")]
        public string Name { get; set; } = string.Empty; // e.g. Elektrik, Su, Doğalgaz

        [Required]
        [Display(Name = "Tutar")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "Son Ödeme Tarihi")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Display(Name = "Ödendi mi?")]
        public bool IsPaid { get; set; } = false;

        [Display(Name = "Abone No")]
        public string? SubscriberNumber { get; set; }
    }
}
