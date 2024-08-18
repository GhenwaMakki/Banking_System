using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TransactionService.Domain.Modules;

namespace TransactionService.Persistence.BackgroundJob
{
    public class RecurrentTransactionService : IHostedService, IDisposable
    {
        private readonly ILogger<RecurrentTransactionService> _logger;
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;

        public RecurrentTransactionService(IServiceProvider serviceProvider, ILogger<RecurrentTransactionService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RecurrentTransactionService is starting.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("RecurrentTransactionService is working.");

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<TransactionContext>();
                    var recurrentTransactions = dbContext.RecurrentTransactions.ToList();

                    foreach (var recurrentTransaction in recurrentTransactions)
                    {
                        _logger.LogInformation($"Processing recurrent transaction {recurrentTransaction.Id}.");
                        ProcessRecurrentTransaction(recurrentTransaction, dbContext);
                    }

                    dbContext.SaveChanges();
                    _logger.LogInformation("Recurrent transactions processed successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing recurrent transactions.");
            }
        }

        private void ProcessRecurrentTransaction(RecurrentTransaction recurrentTransaction, TransactionContext dbContext)
        {
            var transactionAmount = recurrentTransaction.TransactionType == "Withdrawal"
                ? -recurrentTransaction.Amount
                : recurrentTransaction.Amount;
            
            var transaction = new Transaction
            {
                AccountId = recurrentTransaction.AccountId,
                Amount = transactionAmount
            };
            
            dbContext.Transactions.Add(transaction);
            
            recurrentTransaction.NextExecutionDate = CalculateNextExecutionDate(recurrentTransaction.Interval, DateTime.UtcNow);
            
            dbContext.RecurrentTransactions.Update(recurrentTransaction);
            
            dbContext.SaveChanges();

            _logger.LogInformation($"Processed recurrent transaction {recurrentTransaction.Id} as a {recurrentTransaction.TransactionType}. Next execution date is {recurrentTransaction.NextExecutionDate}.");
        }
        
        private DateTime CalculateNextExecutionDate(string interval, DateTime currentExecutionDate)
        {
            return interval switch
            {
                "Daily" => currentExecutionDate.AddDays(1),
                "Weekly" => currentExecutionDate.AddDays(7),
                "Monthly" => currentExecutionDate.AddMonths(1),
                _ => throw new ArgumentException("Invalid interval specified.")
            };
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RecurrentTransactionService is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
