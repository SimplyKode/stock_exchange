using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class StockPrice
    {
        [Required]
        public string StockSymbol { get; set; }
        [Required]
        public double Price{ get; set; }        

    }
}
