using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class StockPriceDto
    {
        public string StockSymbol { get; set; } = string.Empty;
        public decimal? AveragePrice { get; set; }
    }
}
