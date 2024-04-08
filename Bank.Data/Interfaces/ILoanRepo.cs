using Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Data.Interfaces
{
    public interface ILoanRepo
    {
        Task<bool> CreateLoan(Loan loan);
    }
}
