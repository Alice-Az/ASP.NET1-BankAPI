using Bank.Domain.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<int?> CreateCustomer(CustomerRequest request, string userId);
    }
}
