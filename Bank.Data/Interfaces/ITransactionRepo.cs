using Bank.Domain;

namespace Bank.Data.Interfaces
{
    public interface ITransactionRepo
    {
        Task<bool> CreateLoanTransaction(Transaction transaction);
        Task<bool> CreateTransfer(Transaction debitTransaction, Transaction creditTransaction);
    }
}
