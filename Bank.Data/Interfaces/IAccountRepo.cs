using Bank.Domain;

namespace Bank.Data.Interfaces
{
    public interface IAccountRepo
    {
        Task<List<Account>?> GetAccountsOverview(string userId);
    }
}
