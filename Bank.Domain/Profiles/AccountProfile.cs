using AutoMapper;
using Bank.Domain.DTO.Account;

namespace Bank.Domain.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile() 
        {
            CreateMap<Account, AccountOverviewResponse>();
        }
    }
}
