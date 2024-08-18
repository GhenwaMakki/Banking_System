using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionService.Application.Command;
using TransactionService.Application.Queries;

namespace TransactionServices.API.Controllers;


    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Employee, Customer")]

        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
        {
            var transactionId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTransaction), new { id = transactionId }, new { id = transactionId });
        }
        
        [HttpPost("recurrent")]
        [Authorize(Roles = "Employee")]

        public async Task<IActionResult> CreateRecurrentTransaction([FromBody] CreateRecurrentTransactionCommand command)
        {
            var recurrentTransactionId = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateRecurrentTransaction), new { id = recurrentTransactionId }, new { id = recurrentTransactionId });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Employee, Customer")]

        public async Task<IActionResult> GetTransaction(long id)
        {
            var query = new GetTransactionQuery { TransactionId = id };
            var transaction = await _mediator.Send(query);
            
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }
        
}