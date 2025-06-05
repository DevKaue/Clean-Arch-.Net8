using Application.UserCQ.ViewModels;
using MediatR;

namespace Application.UserCQ.Commands
{
    public record CreateUserCommand : IRequest<UserInfoViewModel>
    {
    }
}
