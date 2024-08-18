namespace AccountService.Domain.Modules;

public class Account
{
    public long Id { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public long UserId { get; set; }  
}