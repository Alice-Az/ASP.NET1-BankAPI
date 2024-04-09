using System.ComponentModel.DataAnnotations;

namespace Bank.Domain.DTO.Transaction
{
    public class LoanTransactionRequest
    {

        [Required]
        public int AccountId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public string Type { get; set; } = null!;

        [Required]
        public string Operation { get; set; } = null!;

        [Required]
        public decimal Amount { get; set; }
    }
}
