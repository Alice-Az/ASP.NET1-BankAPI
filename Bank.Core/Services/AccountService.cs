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

        public AccountService(IAccountRepo accountRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
        }

        public async Task<List<AccountOverviewResponse>?> GetAccountsOverview(string userId)
        {
            List<Account>? result = await _accountRepo.GetAccountsOverview(userId);
            if (result != null)
            {
                return _mapper.Map<List<AccountOverviewResponse>>(result);
            }
            return null;
        }

        public async Task<List<TransactionResponse>?> GetAccountDetails(int accountId, string userId)
        {
            List<Transaction>? result = await _accountRepo.GetAccountDetails(accountId, userId);
            if (result != null )
            {
                return _mapper.Map<List<TransactionResponse>>(result);
            }
            return null;
        }

        public async Task<int?> CreateAccount(NewAccountRequest request, string userId)
        {
            Account account = _mapper.Map<Account>(request);
            account.Created = DateOnly.FromDateTime(DateTime.Now);
            account.Balance = 0;
            return await _accountRepo.CreateAccount(account, userId);
        }
    }
}
