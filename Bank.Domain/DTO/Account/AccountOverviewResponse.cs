using Bank.Domain.DTO.AccountType;

namespace Bank.Domain.DTO.Account
{
    public class AccountOverviewResponse
    {
        public int AccountId { get; set; }

        public decimal Balance { get; set; }

        public AccountTypeResponse? AccountTypes { get; set; }
    }
}
