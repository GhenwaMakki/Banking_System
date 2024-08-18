using UserService.Domain.Modules;
using CustomerManagmnet.Persistence;
using MediatR;

namespace CustomerManagment.Application.Commands;

    public class CreateUserHandler : IRequestHandler<CreateUserCommand, long>
    {
        private readonly UserContext _context;

        public CreateUserHandler(UserContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    
    }