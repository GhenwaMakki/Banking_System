using AccountService.Application.Handlers;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace AccountManagment.API.Controllers;


    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
        {
            var accountId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAccount), new { id = accountId }, accountId);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Employee, Customer")]

        public async Task<IActionResult> GetAccount(long id)
        {
            var query = new GetAccountQuery { AccountId = id };
            var account = await _mediator.Send(query);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }
    
}