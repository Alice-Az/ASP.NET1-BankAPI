using Bank.Domain.DTO.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<int> CreateTransaction(LoanTransactionRequest request);
    }
}
