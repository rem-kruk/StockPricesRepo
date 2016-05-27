using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PricesV2.Models
{
    public class InvestmentAndStock
    {
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public DateTime DateTo { get; set; }

        
        [Required(ErrorMessage = "Pole jest wymagane")]
        public float Amount { get; set; }

        public float Percentage { get; set; }

    }
}