using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface ITransactionRepository
    {
        public void ProcessTransaction(Transaction transaction);
    }
}
