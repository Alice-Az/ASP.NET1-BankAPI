using Bank.Data.Interfaces;
using Bank.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data.Repositories
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly BankAppDataContext _context;

        public TransactionRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateLoanTransaction(Transaction transaction)
        {
            try
            {
                Account? account = await _context.Accounts.SingleOrDefaultAsync(a => a.AccountId == transaction.AccountId);
                if (account != null)
                {
                    account.Balance += transaction.Amount;
                    _context.Accounts.Update(account);
                    transaction.Balance = account.Balance;
                    await _context.Transactions.AddAsync(transaction);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public async Task<bool> CreateTransfer(Transaction debitTransaction, Transaction creditTransaction)
        {
            try
            {
                await _context.Transactions.AddRangeAsync(debitTransaction, creditTransaction);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
    }
}
