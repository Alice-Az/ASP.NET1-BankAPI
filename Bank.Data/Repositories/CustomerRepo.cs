using Bank.Data.Interfaces;
using Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Account account = new Account() 
                {
                    Frequency = "Monthly",
                    Created = DateOnly.FromDateTime(DateTime.Now),
                    Balance = 0,
                    AccountTypesId = 1,
                };
                await _context.Accounts.AddAsync(account);
                await _context.Dispositions.AddAsync(new Disposition() { Customer = customer, Account = account, Type = "OWNER" });
                await _context.SaveChangesAsync();
                return customer.CustomerId;
            }
            catch { return null; }
        }
    }
}
