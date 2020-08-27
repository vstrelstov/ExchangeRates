using System;
using System.ComponentModel.DataAnnotations;
using ExchangeRates.Attributes;

namespace ExchangeRates.Models
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [DateTimeRange(ErrorMessage = "Invalid start date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Finish date is required")]
        [DataType(DataType.Date)]
        [DateTimeRange(ErrorMessage = "Invalid finish date")]
        public DateTime FinishDate { get; set; }

        public string Currency { get; set; }
    }
}