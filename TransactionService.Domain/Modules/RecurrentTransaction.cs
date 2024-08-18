namespace TransactionService.Domain.Modules;

public class RecurrentTransaction
{
    
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string TransactionType { get; set; } // 'Withdrawal' or 'Deposit'
        public decimal Amount { get; set; }
        public string Interval { get; set; } // 'Daily', 'Weekly', 'Monthly'
        public DateTime? NextExecutionDate { get; set; } // Add this property

        
        public Account Account { get; set; }
    

}