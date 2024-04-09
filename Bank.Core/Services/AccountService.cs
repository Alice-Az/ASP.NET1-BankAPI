using AutoMapper;
using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain;
using Bank.Domain.DTO.Account;

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
    }
}
