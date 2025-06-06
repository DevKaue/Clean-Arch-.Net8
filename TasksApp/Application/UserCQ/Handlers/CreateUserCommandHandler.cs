using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using Domain.Entity;
using Infra.Persistence;
using MediatR;

namespace Application.UserCQ.Handlers
{
    public class CreateUserCommandHandler(TasksDbContext context) : IRequestHandler<CreateUserCommand, UserInfoViewModel>
    {
        private readonly TasksDbContext _context = context;
        public async Task<UserInfoViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PasswordHash = request.Password,
                UserName = request.Username,
                RefreshToken = Guid.NewGuid().ToString(),
                RefreshTokenExpirationTime = DateTime.Now.AddDays(5)
            };

           _context.Users.Add(user); 
            _context.SaveChanges();

            var userInfo = new UserInfoViewModel()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.UserName,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpirationTime = user.RefreshTokenExpirationTime,
                TokenJWT = Guid.NewGuid().ToString(),
            };
            return userInfo;
        }
    }
}
