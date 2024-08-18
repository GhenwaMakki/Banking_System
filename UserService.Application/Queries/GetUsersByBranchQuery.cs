using UserService.Domain.Modules;
using MediatR;
using UserService.Domain.Modules;

namespace CustomerManagment.Application.Queries;

public class GetUsersByBranchQuery : IRequest<List<User>>
{
    public long BranchId { get; set; }
}