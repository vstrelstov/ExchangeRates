using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExchangeRates.Models
{
    public class ExchangeRate
    {
        [Key, Column(Order = 0)]
        public DateTime Date { get; set; }
        [Key, Column(Order = 1)]
        public string Currency { get; set; }
        public decimal Rate { get; set; }
    }
}