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
    }
}
