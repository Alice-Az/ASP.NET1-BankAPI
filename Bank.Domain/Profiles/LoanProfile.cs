using AutoMapper;
using Bank.Domain.DTO.Loan;

namespace Bank.Domain.Profiles
{
    internal class LoanProfile : Profile
    {
        public LoanProfile() 
        {
            CreateMap<LoanRequest, Loan>();
        }
    }
}
