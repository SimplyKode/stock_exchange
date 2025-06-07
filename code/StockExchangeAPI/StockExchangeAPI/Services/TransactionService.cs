using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using StockExchangeAPI.Interfaces;

namespace StockExchangeAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepo;

        public TransactionService(ITransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }     

        public async Task<bool> ProcessTransaction(StockTransaction transaction)
        {
            try
            {
                return await _transactionRepo.ProcessTransaction(transaction);
            }
            catch
            {
                throw;
            }
        }
    }
}
