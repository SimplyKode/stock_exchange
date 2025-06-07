using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IStockRepository
    {
        public Task<decimal?> GetStockPrice(string stockSymbol);
        public Task<List<StockPrice>> GetStockPrice(List<string> stockSymbolList);
        public Task<List<StockPrice>> GetStockPrice(int pageNumber, int pageSize);
    }
}
