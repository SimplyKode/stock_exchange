using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using StockExchangeAPI.Interfaces;

namespace StockExchangeAPI.Interfaces
{
    public interface IStockService
    {
        public Task<decimal?> GetStockPrice(string stockSymbol);

        public Task<Dictionary<string, decimal?>> GetStockPrice(List<string> stockSymbolList);

        public Task<Dictionary<string, decimal?>> GetStockPrice(int pageNumber, int pageSize);
    }
}
