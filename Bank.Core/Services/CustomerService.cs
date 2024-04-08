using AutoMapper;
using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain;
using Bank.Domain.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(IMapper mapper, ICustomerRepo customerRepo)
        {
            _mapper = mapper;
            _customerRepo = customerRepo;
        }

        public async Task<int?> CreateCustomer(CustomerRequest request, string userId)
        {
            Customer customer = _mapper.Map<Customer>(request);
            customer.UserId = userId;
            return await _customerRepo.CreateCustomer(customer);
        }
    }
}
