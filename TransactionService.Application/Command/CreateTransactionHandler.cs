using System.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionService.Persistence;
using Transaction = TransactionService.Domain.Modules.Transaction;

//using TransactionService.Modules;

namespace TransactionService.Application.Command;


public class CreateTransactionCommand: IRequest<long>
{
    public long AccountId { get; set; }
    public string TransactionType { get; set; }
    public decimal Amount { get; set; }
}



    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, long>
    {
        private readonly TransactionContext _context;

        public CreateTransactionHandler(TransactionContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            // Find the account
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == request.AccountId, cancellationToken);

            if (account == null)
            {
                throw new Exception("Account not found");
            }
            

            var transaction = new Transaction()
            {
                AccountId = request.AccountId,
                TransactionType = request.TransactionType,
                Amount = request.Amount,
                CreatedAt = DateTime.UtcNow
            };
            

            if (transaction.TransactionType == "Deposit")
            {
                account.Balance += transaction.Amount;
            }
            else if (transaction.TransactionType == "Withdrawal")
            {
                if (account.Balance < transaction.Amount)
                {
                    throw new Exception("Insufficient funds for withdrawal");
                }
                account.Balance -= transaction.Amount;
            }

            // Save the transaction and update the account
            _context.Transactions.Add(transaction);
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync(cancellationToken);

            return transaction.Id;
        }
    }
