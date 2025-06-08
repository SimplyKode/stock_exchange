using DataAccess.Models;

namespace StockExchangeAPI.Interfaces
{
    public interface ITransactionService
    {
        public Task<bool> ProcessTransaction(StockTransaction transaction);
    }
}
