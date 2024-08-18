using CustomerManagment.Domain.Modules;
using MediatR;

namespace CustomerManagment.Application.Queries;

public class GetUserQuery: IRequest<User>
{
    public long Id { get; set; }
}