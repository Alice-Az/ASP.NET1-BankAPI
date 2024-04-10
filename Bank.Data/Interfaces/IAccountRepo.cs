using Bank.Domain;

namespace Bank.Data.Interfaces
{
    public interface IAccountRepo
    {
        Task<List<Transaction>?> GetAccountDetails(int accountId);
        Task<int?> CreateAccount(Account account, Customer customer);
        Task<Account?> GetAccountById(int accountId);
        void UpdateAccount(Account account);
    }
}
