using System.ComponentModel.DataAnnotations;

namespace Bank.Domain.DTO.Loan
{
    public class LoanRequest
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int Duration { get; set; }

    }
}
