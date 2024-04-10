using Bank.Domain.DTO.Transaction;

namespace Bank.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> CreateTransfer(TransferRequest request, string userId);
    }
}
