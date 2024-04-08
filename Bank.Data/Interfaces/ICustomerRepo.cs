using Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Data.Interfaces
{
    public interface ICustomerRepo
    {
        Task<int?> CreateCustomer(Customer customer);
    }
}
