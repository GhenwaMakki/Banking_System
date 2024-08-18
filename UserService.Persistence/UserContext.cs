using UserService.Domain.Modules;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagmnet.Persistence;

public class UserContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Branch> Branches { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options) { }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
} 