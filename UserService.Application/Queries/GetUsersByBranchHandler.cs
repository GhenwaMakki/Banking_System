using UserService.Domain.Modules;
using CustomerManagmnet.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagment.Application.Queries;

public class GetUsersByBranchHandler : IRequestHandler<GetUsersByBranchQuery, List<User>>
{
    private readonly UserContext _context;

    public GetUsersByBranchHandler(UserContext context)
    {
        _context = context;
    }

    public async Task<List<User>> Handle(GetUsersByBranchQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(u => u.BranchId == request.BranchId)
            .ToListAsync(cancellationToken);
    }

    
}