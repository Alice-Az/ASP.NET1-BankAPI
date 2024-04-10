using Bank.Domain;

namespace Bank.Data.Interfaces
{
    public interface ICustomerRepo
    {
        Task<int?> CreateCustomer(Customer customer);
        Task<Customer?> GetCustomerByUserId(string userId);
    }
}
