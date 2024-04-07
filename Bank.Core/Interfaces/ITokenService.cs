using Bank.Domain.DTO.Login;
using Bank.Domain.Identity;

namespace Bank.Core.Interfaces
{
    public interface ITokenService
    {
        string? GetToken(User user, IList<string> roles);
    }
}
