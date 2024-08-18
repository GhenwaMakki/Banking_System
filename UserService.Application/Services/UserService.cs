/*using CustomerManagment.Application.Commands;
using CustomerManagment.Application.Queries;
using MediatR;

namespace CustomerManagment.Application.Services;


    public class UserService 
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var query = new GetUserQuery { Id = request.Id };
            var user = await _mediator.Send(query);

            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role.RoleName,
                Branch = user.Branch?.Name
            };
        }

        public override async Task<UserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var command = new CreateUserCommand
            {
                Username = request.Username,
                PasswordHash = request.PasswordHash,
                RoleId = request.RoleId,
                BranchId = request.BranchId
            };

            var user = await _mediator.Send(command);

            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role.RoleName,
                Branch = user.Branch?.Name
            };
        }
    }

}*/