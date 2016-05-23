using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prices.Models
{
    public class StockPrice
    {
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime date { get; set; }
        public float price { get; set; }
    }
}