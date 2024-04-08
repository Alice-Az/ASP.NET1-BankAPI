using Bank.Domain.DTO.Loan;

namespace Bank.Core.Interfaces
{
    public interface ILoanService
    {
        Task<bool> CreateLoan(LoanRequest request);
    }
}
