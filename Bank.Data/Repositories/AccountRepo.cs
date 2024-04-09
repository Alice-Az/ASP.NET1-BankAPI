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

        public async Task<List<Account>?> GetAccountsOverview(string userId)
        {
            try
            {
                Customer? customer = await _context.Customers.Include(c => c.Dispositions)
                                               .ThenInclude(d => d.Account)
                                               .ThenInclude(a => a.AccountTypes)
                                               .FirstOrDefaultAsync(c => c.UserId == userId);

                if (customer != null)
                {
                    return customer.Dispositions.Select(d => d.Account).ToList();
                }
                return null;
            }
            catch { return null; }
        }

        public async Task<List<Transaction>?> GetAccountDetails(int accountId, string userId)
        {
            try
            {
                Customer? customer = await _context.Customers.Include(c => c.Dispositions)
                                                             .ThenInclude(d => d.Account)
                                                             .SingleOrDefaultAsync(c => c.UserId == userId);
                if (customer != null)
                {
                    bool accountExists = customer.Dispositions.Any(d => d.AccountId == accountId);
                    return accountExists ? await _context.Transactions.Where(t => t.AccountId == accountId).ToListAsync()
                                         : null;
                }
                return null;
            }
            catch { return null; }
        }

        public async Task<int?> CreateAccount(Account account, string userId)
        {
            try
            {
                Customer? customer = await _context.Customers.SingleOrDefaultAsync(c => c.UserId == userId);
                if (customer != null)
                {
                    await _context.Accounts.AddAsync(account);
                    await _context.Dispositions.AddAsync(new Disposition() { Account = account, Customer = customer, Type = "OWNER" });
                    await _context.SaveChangesAsync();
                    return account.AccountId;
                }
                return null;
            }
            catch { return null; }
        }
    }
}
