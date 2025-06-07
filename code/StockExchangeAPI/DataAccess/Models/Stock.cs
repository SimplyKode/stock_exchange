using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Stock
    {
        public Stock()
        {
            StockTransactions = new HashSet<StockTransaction>();
        }

        public string Symbol { get; set; } = null!;
        public string? Name { get; set; }

        public virtual StockPrice StockPrice { get; set; } = null!;
        public virtual ICollection<StockTransaction> StockTransactions { get; set; }
    }
}
