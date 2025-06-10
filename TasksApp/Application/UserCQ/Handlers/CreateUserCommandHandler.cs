using Application.Response;
using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using AutoMapper;
using Domain.Entity;
using Infra.Persistence;
using MediatR;

namespace Application.UserCQ.Handlers
{
    public class CreateUserCommandHandler(TasksDbContext context, IMapper mapper) : IRequestHandler<CreateUserCommand, ResponseBase<UserInfoViewModel?>>
    {
        private readonly TasksDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseBase<UserInfoViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            _context.Users.Add(user);
            _context.SaveChanges();

            return new ResponseBase<UserInfoViewModel>
            {
                ResponseInfo = null,
                Value = _mapper.Map<UserInfoViewModel>(user)
            };
        }
    }
}
