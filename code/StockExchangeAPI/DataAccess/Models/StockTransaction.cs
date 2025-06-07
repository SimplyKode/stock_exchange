using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class StockTransaction
    {
        public int Id { get; set; }
        public string? Stocksymbol { get; set; }
        public int? Quantity { get; set; }
        public decimal? Unitprice { get; set; }
        public int? Brokerid { get; set; }

        public virtual StockBroker? Broker { get; set; }
        public virtual Stock? StocksymbolNavigation { get; set; }
    }
}
