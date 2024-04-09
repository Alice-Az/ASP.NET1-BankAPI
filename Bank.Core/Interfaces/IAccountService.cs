using Bank.Domain.DTO.Account;
using Bank.Domain.DTO.Transaction;

namespace Bank.Core.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountOverviewResponse>?> GetAccountsOverview(string userId);
        Task<List<TransactionResponse>?> GetAccountDetails(int accountId, string userId);
        Task<int?> CreateAccount(NewAccountRequest request, string userId);
    }
}
