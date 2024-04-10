using AutoMapper;
using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain;
using Bank.Domain.DTO.Customer;

namespace Bank.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepo _customerRepo;
        private readonly IAccountRepo _accountRepo;

        public CustomerService(IMapper mapper, ICustomerRepo customerRepo, IAccountRepo accountRepo)
        {
            _mapper = mapper;
            _customerRepo = customerRepo;
            _accountRepo = accountRepo;
        }

        public async Task<int?> CreateCustomer(CustomerRequest request, string userId)
        {
            Customer customer = _mapper.Map<Customer>(request);
            customer.UserId = userId;

            int? customerId = await _customerRepo.CreateCustomer(customer);

            Account account = new()
            {
                Frequency = "Monthly",
                Created = DateOnly.FromDateTime(DateTime.Now),
                Balance = 0,
                AccountTypesId = 1,
            };

            int? accountId = await _accountRepo.CreateAccount(account, customer);

            return customerId;
        }
    }
}
