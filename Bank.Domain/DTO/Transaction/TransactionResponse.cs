namespace Bank.Domain.DTO.Transaction
{
    public class TransactionResponse
    {
        public int TransactionId { get; set; }

        public int AccountId { get; set; }

        public DateOnly Date { get; set; }

        public string Type { get; set; } = null!;

        public string Operation { get; set; } = null!;

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

    }
}
