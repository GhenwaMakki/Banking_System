using CustomerManagment.Application.Commands;
using CustomerManagment.Application.Queries;
using UserService.Domain.Modules;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Modules;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Employee")]

    public async Task<ActionResult<User>> GetUserById(long id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery { UserId = id });

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet("branch/{branchId}")]
    [Authorize(Roles = "Admin, Employee")]

    public async Task<ActionResult<List<User>>> GetUsersByBranch(long branchId)
    {
        var users = await _mediator.Send(new GetUsersByBranchQuery { BranchId = branchId });

        return Ok(users);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Employee")]

    public async Task<ActionResult<long>> CreateUser([FromBody] CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetUserById), new { id = userId }, userId);
    }

}