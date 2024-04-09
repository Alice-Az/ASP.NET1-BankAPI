using AutoMapper;
using Bank.Domain.DTO.Transaction;

namespace Bank.Domain.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile() 
        { 
            CreateMap<Transaction, TransactionResponse>();
            CreateMap<LoanTransactionRequest, Transaction>();
        }
    }
}
