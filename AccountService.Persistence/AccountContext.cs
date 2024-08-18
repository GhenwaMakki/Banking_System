using AccountService.Domain.Modules;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Persistence;

public class AccountContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<User> Users { get; set; }  

    public AccountContext(DbContextOptions<AccountContext> options) : base(options)
    {
    }
    

}
