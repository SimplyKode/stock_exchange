using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface IStockRepository
    {
        public Dictionary<string, double> GetStockPrice(int pageNumber, int pageSize);
        public double GetStockPrice(string stockSymbol);
        public Dictionary<string, double> GetStockPrice(string[] stockSymbolList, int pageNumber, int pageSize);
    }
}
