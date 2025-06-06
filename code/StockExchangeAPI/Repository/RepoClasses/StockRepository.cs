using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public class StockRepository : IStockRepository
    {
        public Dictionary<string, double> GetStockPrice(int pageNumber, int pageSize) 
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            return result;
        }
        public double GetStockPrice(string stockSymbol) 
        {
            return 1.2;
        }
        public Dictionary<string, double> GetStockPrice(string[] stockSymbolList, int pageNumber, int pageSize) 
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            return result;
        }
    }
}
