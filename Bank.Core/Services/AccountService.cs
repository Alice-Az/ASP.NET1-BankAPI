using AutoMapper;
using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain;
using Bank.Domain.DTO.Account;
using Bank.Domain.DTO.Transaction;

namespace Bank.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;
        private readonly ICustomerRepo _customerRepo;

        public AccountService(IAccountRepo accountRepo, IMapper mapper, ICustomerRepo customerRepo)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
            _customerRepo = customerRepo;
        }

        public async Task<List<AccountOverviewResponse>?> GetAccountsOverview(string userId)
        {
            Customer? customer = await _customerRepo.GetCustomerByUserId(userId);

            List<Account>? result = customer?.Dispositions.Select(d => d.Account).ToList();
            if (result != null)
            {
                return _mapper.Map<List<AccountOverviewResponse>>(result);
            }
            return null;
        }

        public async Task<List<TransactionResponse>?> GetAccountDetails(int accountId, string userId)
        {
            Customer? customer = await _customerRepo.GetCustomerByUserId(userId);
            Account? account = customer?.Dispositions.Select(d => d.Account).SingleOrDefault(d => d.AccountId == accountId);

            if (account != null)
            {
                List<Transaction>? result = await _accountRepo.GetAccountDetails(account.AccountId);
                return result != null ? _mapper.Map<List<TransactionResponse>>(result) : null;
            }
            return null;
        }

        public async Task<int?> CreateAccount(NewAccountRequest request, string userId)
        {
            Customer? customer = await _customerRepo.GetCustomerByUserId(userId);
            Account account = _mapper.Map<Account>(request);
            account.Created = DateOnly.FromDateTime(DateTime.Now);
            account.Balance = 0;
            return customer != null ? await _accountRepo.CreateAccount(account, customer) : null;
        }
    }
}
