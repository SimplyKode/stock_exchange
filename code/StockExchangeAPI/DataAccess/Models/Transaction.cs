using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Transaction
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string StockSymbol { get; set; }
        [Required]
        public double Quantity { get; set; }
        [Required]
        public double UnitProce { get; set; }
        [Required]
        public int BrokerId { get; set; }

    }
}
