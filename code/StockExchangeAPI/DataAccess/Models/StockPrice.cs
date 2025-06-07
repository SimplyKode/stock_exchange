using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class StockPrice
    {
        public string Symbol { get; set; } = null!;
        public decimal? Price { get; set; }

        public virtual Stock SymbolNavigation { get; set; } = null!;
    }
}
