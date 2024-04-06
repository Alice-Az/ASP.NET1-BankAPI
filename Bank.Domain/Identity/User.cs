using Microsoft.AspNetCore.Identity;

namespace Bank.Domain.Identity
{
    public class User:IdentityUser
    {
        public virtual Customer? Customer { get; set; }
    }
}
