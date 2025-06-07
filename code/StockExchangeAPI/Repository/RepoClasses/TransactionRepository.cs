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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DevelopmentContext _context;

        public TransactionRepository(DevelopmentContext context)
        {
            _context = context;
        }

        public async Task<bool> ProcessTransaction(StockTransaction transaction)
        {
            try
            {
                await _context.StockTransactions.AddAsync(transaction);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
            
            return true;
        }        
    }
}
