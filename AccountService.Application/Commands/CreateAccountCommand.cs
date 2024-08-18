using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AccountService.Domain.Modules;
using AccountService.Persistence;

namespace AccountService.Application.Handlers
{
    public class CreateAccountCommand : IRequest<long>
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public long UserId { get; set; }
    }

    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, long>
    {
        private readonly AccountContext _context;

        public CreateAccountHandler(AccountContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                AccountNumber = request.AccountNumber,
                Balance = request.Balance,
                UserId = request.UserId
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account.Id;
        }
    }
}