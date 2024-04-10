using Bank.Data.Interfaces;
using Bank.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly BankAppDataContext _context;

        public CustomerRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<int?> CreateCustomer(Customer customer)
        {
            try
            {
                await _context.Customers.AddAsync(customer);
                //await _context.Accounts.AddAsync(account);
                //await _context.Dispositions.AddAsync(new Disposition() { Customer = customer, Account = account, Type = "OWNER" });
                await _context.SaveChangesAsync();
                return customer.CustomerId;
            }
            catch { return null; }
        }

        public async Task<Customer?> GetCustomerByUserId(string userId)
        {
            try
            {
                return await _context.Customers.Include(c => c.Dispositions)
                                               .ThenInclude(d => d.Account)
                                               .ThenInclude(a => a.AccountTypes)
                                               .FirstOrDefaultAsync(c => c.UserId == userId);
            }
            catch { return null; }
        }
    }
}
