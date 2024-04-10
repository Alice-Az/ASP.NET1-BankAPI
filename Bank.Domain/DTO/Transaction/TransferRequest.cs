using System.ComponentModel.DataAnnotations;

namespace Bank.Domain.DTO.Transaction
{
    public class TransferRequest
    {
        [Required]
        public int SenderAccountId { get; set; }

        [Required]
        public int ReceiverAccountId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
