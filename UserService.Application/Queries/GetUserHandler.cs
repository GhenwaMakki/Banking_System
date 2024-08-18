using UserService.Domain.Modules;
using CustomerManagmnet.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Modules;

namespace CustomerManagment.Application.Queries;

    public class GetUserHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly UserContext _context;

        public GetUserHandler(UserContext context)
        {
            _context = context;
        }
        
            public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                return await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
            }
        
}