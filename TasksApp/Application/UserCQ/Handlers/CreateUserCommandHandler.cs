using Application.Response;
using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entity;
using Domain.Enum;
using Infra.Repository.Repositories;
using MediatR;

namespace Application.UserCQ.Handlers
{
    public class CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService) : IRequestHandler<CreateUserCommand, ResponseBase<RefreshTokenViewModel?>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IAuthService _authService = authService;

        public async Task<ResponseBase<RefreshTokenViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var isUniqueEmailAndUsername = _authService.UniqueEmailAndUsername(request.Email!, request.Username!);

            if(isUniqueEmailAndUsername is ValidationFieldsUserEnum.EmailUnavailable)
            {
                return new ResponseBase<RefreshTokenViewModel>
                {
                    ResponseInfo = new()
                    {
                        Title = "Email indisponível.",
                        ErrorDescription = "O email apresentado já está sendo utilizado. Tente outro.",
                        HTTPStatus = 400
                    },
                    Value = null,
                };
            }

            if (isUniqueEmailAndUsername is ValidationFieldsUserEnum.UserNameUnavailable)
            {
                return new ResponseBase<RefreshTokenViewModel>
                {
                    ResponseInfo = new()
                    {
                        Title = "Username indisponível.",
                        ErrorDescription = "O username apresentado já está sendo utilizado. Tente outro.",
                        HTTPStatus = 400
                    },
                    Value = null,
                };
            }

            if (isUniqueEmailAndUsername is ValidationFieldsUserEnum.UsernameAndEmailUnavailable)
            {
                return new ResponseBase<RefreshTokenViewModel>
                {
                    ResponseInfo = new()
                    {
                        Title = "Username e Email indisponíveis.",
                        ErrorDescription = "O username e o email apresentados já está sendo utilizados. Tente outros.",
                        HTTPStatus = 400
                    },
                    Value = null,
                };
            }

            var user = _mapper.Map<User>(request);
            user.RefreshToken = _authService.GenerateRefreshToken();
            user.PasswordHash = _authService.HashingPassword(request.Password!);

            await _unitOfWork.UserRepository.Create(user);
            _unitOfWork.Commit();
           
            var refreshTokenVM = _mapper.Map<RefreshTokenViewModel>(user);
            refreshTokenVM.TokenJWT = _authService.GenerateJWT(user.Email!, user.UserName!);

            return new ResponseBase<RefreshTokenViewModel>
            {
                ResponseInfo = null,
                Value = refreshTokenVM
            };
        }
    }
}
