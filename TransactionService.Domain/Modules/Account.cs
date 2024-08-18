namespace TransactionService.Domain.Modules;


    public class Account
    {
        public long Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<RecurrentTransaction> RecurrentTransactions { get; set; }
    }
