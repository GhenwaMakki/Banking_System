using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionService.Domain.Modules;
using TransactionService.Persistence;

namespace TransactionService.Application.Command;

public class CreateRecurrentTransactionCommand : IRequest<long>
    {
        public long AccountId { get; set; }
        public string TransactionType { get; set; }  
        public decimal Amount { get; set; }
        public string Interval { get; set; }  
    }


public class CreateRecurrentTransactionHandler : IRequestHandler<CreateRecurrentTransactionCommand, long>
{
    private readonly TransactionContext _context;

    public CreateRecurrentTransactionHandler(TransactionContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateRecurrentTransactionCommand request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == request.AccountId, cancellationToken);

        if (account == null)
        {
            throw new Exception("Account not found");
        }

        var recurrentTransaction = new RecurrentTransaction
        {
            AccountId = request.AccountId,
            TransactionType = request.TransactionType,
            Amount = request.Amount,
            Interval = request.Interval
        };

        _context.RecurrentTransactions.Add(recurrentTransaction);
        await _context.SaveChangesAsync(cancellationToken);

        return recurrentTransaction.Id;
    }

}