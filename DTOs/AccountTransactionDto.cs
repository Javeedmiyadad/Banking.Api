namespace BankingService.API.DTOs
{
    public class AccountTransactionDto
    {
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public Decimal Amount { get; set; }
        public string TransactionType { get; set; } = null!;
        public DateTime TransactionDate { get; set; }
        public Decimal Balance { get; set; }

    }
}
