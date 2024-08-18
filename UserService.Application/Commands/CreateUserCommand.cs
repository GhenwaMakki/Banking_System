using UserService.Domain.Modules;
using MediatR;

namespace CustomerManagment.Application.Commands;

public class CreateUserCommand: IRequest<long>
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public long BranchId { get; set; }
}