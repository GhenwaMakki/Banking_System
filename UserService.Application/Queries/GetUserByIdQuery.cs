using UserService.Domain.Modules;
using MediatR;

namespace CustomerManagment.Application.Queries;

public class GetUserByIdQuery : IRequest<User>
{
    public long UserId { get; set; }
}