/*using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using AccountService.Application.Handlers;
using AccountService.Protos;

namespace AccountService.Application.Services
{
    public class AccountServiceImpl : AccountService.AccountServiceBase
    {
        private readonly IMediator _mediator;

        public AccountServiceImpl(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetAccountResponse> GetAccount(GetAccountRequest request, ServerCallContext context)
        {
            var account = await _mediator.Send(new GetAccountQuery { AccountId = request.AccountId });

            if (account == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Account not found"));

            return new GetAccountResponse
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                Balance = (double)account.Balance,
                UserId = account.UserId
            };
        }

        public override async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request, ServerCallContext context)
        {
            var accountId = await _mediator.Send(new CreateAccountCommand
            {
                AccountNumber = request.AccountNumber,
                Balance = (decimal)request.Balance,
                UserId = request.UserId
            });

            return new CreateAccountResponse
            {
                Id = accountId
            };
        }
    }
}*/