using Bank.Data.Interfaces;
using Bank.Domain;

namespace Bank.Data.Repositories
{
    public class LoanRepo : ILoanRepo
    {
        private readonly BankAppDataContext _context;

        public LoanRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateLoan(Loan loan)
        {
            try
            {
                await _context.Loans.AddAsync(loan);
                return await _context.SaveChangesAsync() > 0;
            }
            catch 
            {
                return false;
            }
        }
    }
}
