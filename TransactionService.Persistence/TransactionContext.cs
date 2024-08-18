using Microsoft.EntityFrameworkCore;
using TransactionService.Domain.Modules;

namespace TransactionService.Persistence;

public class TransactionContext :DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options)
            : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RecurrentTransaction> RecurrentTransactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
