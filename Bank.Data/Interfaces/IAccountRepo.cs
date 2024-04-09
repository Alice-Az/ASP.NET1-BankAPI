using Bank.Domain;

namespace Bank.Data.Interfaces
{
    public interface IAccountRepo
    {
        Task<List<Account>?> GetAccountsOverview(string userId);
        Task<List<Transaction>?> GetAccountDetails(int accountId, string userId);
        Task<int?> CreateAccount(Account account, string userId);
    }
}
