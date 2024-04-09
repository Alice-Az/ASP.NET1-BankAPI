using AutoMapper;
using Bank.Domain.DTO.AccountType;

namespace Bank.Domain.Profiles
{
    public class AccountTypeProfile : Profile
    {
        public AccountTypeProfile() 
        { 
            CreateMap<AccountType, AccountTypeResponse>(); 
        }
    }
}
