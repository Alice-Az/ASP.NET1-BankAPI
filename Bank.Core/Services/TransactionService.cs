using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain;
using Bank.Domain.DTO.Transaction;

namespace Bank.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly IAccountRepo _accountRepo;

        public TransactionService(ITransactionRepo transactionRepo, ICustomerRepo customerRepo, IAccountRepo accountRepo)
        {
            _transactionRepo = transactionRepo;
            _customerRepo = customerRepo;
            _accountRepo = accountRepo;
        }

        public async Task<bool> CreateTransfer(TransferRequest request, string userId)
        {
            Transaction debitTransaction = new()
            {
                AccountId = request.SenderAccountId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Debit",
                Operation = "Transfer",
                Amount = -request.Amount
            };

            Transaction creditTransaction = new()
            {
                AccountId = request.ReceiverAccountId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Credit",
                Operation = "Transfer",
                Amount = request.Amount
            };

            Customer? customer = await _customerRepo.GetCustomerByUserId(userId);
            if (customer != null)
            {
                bool senderAccountExists = customer.Dispositions.Any(d => d.AccountId == debitTransaction.AccountId);

                Account? senderAccount = await _accountRepo.GetAccountById(debitTransaction.AccountId);
                Account? receiverAccount = await _accountRepo.GetAccountById(creditTransaction.AccountId);

                if (senderAccountExists && senderAccount != null && receiverAccount != null && senderAccount.Balance >= request.Amount)
                {
                    senderAccount.Balance += debitTransaction.Amount;
                    receiverAccount.Balance += creditTransaction.Amount;
                    //_accountRepo.UpdateAccount(senderAccount);
                    //_accountRepo.UpdateAccount(receiverAccount);
                    debitTransaction.Balance = senderAccount.Balance;
                    creditTransaction.Balance = receiverAccount.Balance;
                    return await _transactionRepo.CreateTransfer(debitTransaction, creditTransaction);
                }
            }
            return false;
        }
    }
}
