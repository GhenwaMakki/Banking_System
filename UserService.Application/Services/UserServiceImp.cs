/*using System.Linq;
using UserService.Domain.Modules;
using System.Threading.Tasks;
using CustomerManagmnet.Persistence;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace UserManagmentService.Application.Services
{
    public class UserServiceImpl 

    {
    private readonly UserContext _context;

    public UserServiceImpl(UserContext context)
    {
        _context = context;
    }

    public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        var user = await _context.Users.FindAsync(request.UserId);

        if (user == null)
            throw new RpcException(new Status(StatusCode.NotFound, "User not found"));

        return new GetUserResponse
        {
            Id = user.Id,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
            BranchId = user.BranchId
        };
    }

    public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var user = new User
        {
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Role = request.Role,
            BranchId = request.BranchId
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new CreateUserResponse
        {
            UserId = user.Id
        };
    }

    public override async Task<GetUsersByBranchResponse> GetUsersByBranch(GetUsersByBranchRequest request,
        ServerCallContext context)
    {
        var users = await _context.Users.Where(u => u.BranchId == request.BranchId).ToListAsync();

        var response = new GetUsersByBranchResponse();
        response.Users.AddRange(users.Select(u => new GetUserResponse
        {
            Id = u.Id,
            Username = u.Username,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            Role = u.Role,
            BranchId = u.BranchId
        }));

        return response;
    }
    }
}*/
