using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepoClasses
{
    public class StockRepository : IStockRepository
    {
        private readonly DevelopmentContext _context;

        public StockRepository(DevelopmentContext context)
        {
            _context = context;
        }
        public async Task<decimal?> GetStockPrice(string stockSymbol)
        {
            try
            {
                var stock = await _context.StockPrices.Where(x => x.Symbol.ToLower() == stockSymbol.ToLower()).FirstOrDefaultAsync();
                return stock?.Price;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<StockPrice>> GetStockPrice(List<string> stockSymbolList)
        {
            List<StockPrice> result = new List<StockPrice>();

            try
            {
                foreach (var symbol in stockSymbolList)
                {
                    var stock = await _context.StockPrices.Where(x => x.Symbol.ToLower() == symbol.ToLower()).FirstOrDefaultAsync();
                    if (stock != null)
                    {
                        result.Add(stock);
                    }
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<List<StockPrice>> GetStockPrice(int pageNumber, int pageSize)
        {
            List<StockPrice> stockList = new List<StockPrice>();

            try
            {
                stockList = await _context.StockPrices
                    .OrderBy(s => s.Symbol)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }

            return stockList;
        }
    }
}
