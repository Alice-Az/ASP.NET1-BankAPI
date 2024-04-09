using Bank.Domain.DTO.Account;

namespace Bank.Core.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountOverviewResponse>?> GetAccountsOverview(string userId);
    }
}
