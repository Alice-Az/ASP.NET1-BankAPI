using AutoMapper;
using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain;
using Bank.Domain.DTO.Loan;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bank.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepo _loanRepo;
        private readonly ITransactionRepo _transactionRepo;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepo loanRepo, IMapper mapper, ITransactionRepo transactionRepo)
        {
            _loanRepo = loanRepo;
            _mapper = mapper;
            _transactionRepo = transactionRepo;
        }

        public async Task<bool> CreateLoan(LoanRequest request)
        {
            Loan loan = _mapper.Map<Loan>(request);
            loan.Date = DateOnly.FromDateTime(DateTime.Now);
            loan.Payments = loan.Amount / loan.Duration;
            loan.Status = "Running";

            bool loanCreated = await _loanRepo.CreateLoan(loan);
            if (loanCreated)
            {
                Transaction loanTransfer = new()
                {
                    AccountId = loan.AccountId,
                    Date = loan.Date,
                    Type = "Credit",
                    Operation = "Loan",
                    Amount = loan.Amount
                };
                return loanCreated && await _transactionRepo.CreateLoanTransaction(loanTransfer);
            }
            return false;
        }
    }
}
