using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AccountService.Domain.Modules;
using AccountService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Application.Handlers
{
    public class GetAccountQuery : IRequest<Account>
    {
        public long AccountId { get; set; }
    }

    public class GetAccountHandler : IRequestHandler<GetAccountQuery, Account>
    {
        private readonly AccountContext _context;

        public GetAccountHandler(AccountContext context)
        {
            _context = context;
        }

        public async Task<Account> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.FindAsync(request.AccountId);
            return account;
        }
    }
}