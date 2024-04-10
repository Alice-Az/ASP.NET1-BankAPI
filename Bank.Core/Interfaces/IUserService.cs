using Bank.Domain.DTO.Customer;
using Bank.Domain.DTO.Login;

namespace Bank.Core.Interfaces
{
    public interface IUserService
    {
        Task<int?> CreateUser(CustomerRequest request);
        Task<string?> Login(LoginRequest request);
    }
}
