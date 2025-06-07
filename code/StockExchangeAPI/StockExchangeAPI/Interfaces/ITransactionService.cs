using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using StockExchangeAPI.Interfaces;

namespace StockExchangeAPI.Interfaces
{
    public interface ITransactionService
    {
        public Task<bool> ProcessTransaction(StockTransaction transaction);
    }
}
