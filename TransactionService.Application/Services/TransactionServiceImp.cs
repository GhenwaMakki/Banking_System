/*using System.Threading.Tasks;
using Grpc.Core;
using TransactionService.Protos;
using MediatR;
using TransactionService.Application.Command;
using TransactionService.Application.Queries;

namespace TransactionService.Application.Services;

    public class TransactionServiceImpl : Protos.TransactionService.TransactionServiceBase
    {
        private readonly IMediator _mediator;

        public TransactionServiceImpl(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request, ServerCallContext context)
        {
            var command = new CreateTransactionCommand
            {
                AccountNumber = request.AccountNumber,
                Amount = request.Amount
            };

            var transactionId = await _mediator.Send(command);
            return new CreateTransactionResponse
            {
                TransactionId = transactionId
            };
        }

        public override async Task<GetTransactionResponse> GetTransaction(GetTransactionRequest request, ServerCallContext context)
        {
            var query = new GetTransactionQuery
            {
                TransactionId = request.TransactionId
            };

            var transaction = await _mediator.Send(query);

            if (transaction == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Transaction not found"));

            return new GetTransactionResponse
            {
                TransactionId = transaction.Id.ToString(),
                AccountNumber = transaction.AccountNumber,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate.ToString("o")
            };
        }

        public override async Task<CreateRecurrentTransactionResponse> CreateRecurrentTransaction(CreateRecurrentTransactionRequest request, ServerCallContext context)
        {
            var command = new CreateRecurrentTransactionCommand
            {
                AccountNumber = request.AccountNumber,
                Amount = request.Amount,
                RecurrencePattern = request.RecurrencePattern
            };

            var recurrentTransactionId = await _mediator.Send(command);
            return new CreateRecurrentTransactionResponse
            {
                RecurrentTransactionId = recurrentTransactionId
            };
        }

        public override async Task<GetRecurrentTransactionResponse> GetRecurrentTransaction(GetRecurrentTransactionRequest request, ServerCallContext context)
        {
            var query = new GetRecurrentTransactionQuery
            {
                RecurrentTransactionId = request.RecurrentTransactionId
            };

            var recurrentTransaction = await _mediator.Send(query);

            if (recurrentTransaction == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Recurrent transaction not found"));

            return new GetRecurrentTransactionResponse
            {
                RecurrentTransactionId = recurrentTransaction.Id.ToString(),
                AccountNumber = recurrentTransaction.AccountNumber,
                Amount = recurrentTransaction.Amount,
                RecurrencePattern = recurrentTransaction.RecurrencePattern
            };
        }
    }*/
