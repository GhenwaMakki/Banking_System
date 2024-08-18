namespace TransactionService.Domain.Modules;

public class Transaction
{
    public long Id { get; set; }
    public long AccountId { get; set; }
    public string TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    
}