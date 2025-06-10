using Application.Response;
using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entity;
using Infra.Persistence;
using MediatR;

namespace Application.UserCQ.Handlers
{
    public class CreateUserCommandHandler(TasksDbContext context, IMapper mapper, IAuthService authService) : IRequestHandler<CreateUserCommand, ResponseBase<UserInfoViewModel?>>
    {
        private readonly TasksDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IAuthService _authService;

        public async Task<ResponseBase<UserInfoViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            user.RefreshToken = _authService.GenerateRefreshToken();

            _context.Users.Add(user);
            _context.SaveChanges();

            var userInfoVm = _mapper.Map<UserInfoViewModel>(user);
            userInfoVm.TokenJWT = _authService.GenerateJWT(user.Email!, user.UserName!);

            return new ResponseBase<UserInfoViewModel>
            {
                ResponseInfo = null,
                Value = userInfoVm
            };
        }
    }
}
