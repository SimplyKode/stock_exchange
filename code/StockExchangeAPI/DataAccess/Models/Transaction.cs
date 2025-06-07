using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Transaction
    {
        public string Stocksymbol { get; set; }
        public int Quantity { get; set; }
        public decimal Unitprice { get; set; }
        public int Brokerid { get; set; }
    }
}
