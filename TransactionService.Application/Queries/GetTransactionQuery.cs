using MediatR;
using TransactionService.Domain.Modules;
using TransactionService.Persistence;

namespace TransactionService.Application.Queries;

    public class GetTransactionQuery : IRequest<Transaction>
    {
        public long TransactionId { get; set; }
    }

    public class GetTransactionHandler : IRequestHandler<GetTransactionQuery, Transaction>
    {
        private readonly TransactionContext _context;

        public GetTransactionHandler(TransactionContext context)
        {
            _context = context;
        }

        public async Task<Transaction> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            return await _context.Transactions.FindAsync(request.TransactionId);
        }
    }
    

