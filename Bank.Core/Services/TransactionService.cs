using AutoMapper;
using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain;
using Bank.Domain.DTO.Transaction;

namespace Bank.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepo _transactionRepo;

        public TransactionService(IMapper mapper, ITransactionRepo transactionRepo)
        {
            _mapper = mapper;
            _transactionRepo = transactionRepo;
        }

        public async Task<int> CreateTransaction(LoanTransactionRequest request)
        {
            return 1;

        }
    }
}
