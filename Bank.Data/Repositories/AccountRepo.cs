using Bank.Data.Interfaces;
using Bank.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly BankAppDataContext _context;

        public AccountRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>?> GetAccountDetails(int accountId)
        {
            try
            {
                return await _context.Transactions.Where(t => t.AccountId == accountId).ToListAsync();
            }
            catch { return null; }
        }

        public async Task<int?> CreateAccount(Account account, Customer customer)
        {
            try
            {
                await _context.Accounts.AddAsync(account);
                await _context.Dispositions.AddAsync(new Disposition() { Account = account, Customer = customer, Type = "OWNER" });
                await _context.SaveChangesAsync();
                return account.AccountId;
            }
            catch { return null; }
        }

        public async Task<Account?> GetAccountById(int accountId)
        {
            try
            {
                return await _context.Accounts.SingleOrDefaultAsync(a => a.AccountId == accountId);
            }
            catch { return null; }
        }

        public async void UpdateAccount(Account account)
        {
            await _context.SaveChangesAsync();
        }
    }
}
