using AutoMapper;
using Bank.Domain.DTO.Customer;

namespace Bank.Domain.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<CustomerRequest, Customer>();
        }

    }
}
