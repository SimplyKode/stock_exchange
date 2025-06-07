using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class StockBroker
    {
        public StockBroker()
        {
            StockTransactions = new HashSet<StockTransaction>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<StockTransaction> StockTransactions { get; set; }
    }
}
