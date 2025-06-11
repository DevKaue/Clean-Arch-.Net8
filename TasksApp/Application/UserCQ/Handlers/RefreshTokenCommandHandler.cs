using Application.Response;
using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using AutoMapper;
using Domain.Abstractions;
using Infra.Persistence;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.UserCQ.Handlers
{
    public class RefreshTokenCommandHandler(TasksDbContext context, IAuthService authService, IConfiguration configuration, IMapper mapper) : IRequestHandler<RefreshTokenCommand, ResponseBase<RefreshTokenViewModel>>
    {
        private readonly TasksDbContext _context = context;
        private readonly IAuthService _authService = authService;
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;
        public async Task<ResponseBase<RefreshTokenViewModel>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == request.Username);
            if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpirationTime < DateTime.Now)
            {
                return new ResponseBase<RefreshTokenViewModel>
                {
                    ResponseInfo = new()
                    {
                        Title = "Token inválido",
                        ErrorDescription = $"Refresh Token inválido ou expirado. Faça login novamente.",
                        HTTPStatus = 404
                    },
                    Value = null
                };
            }
            _ = int.TryParse(_configuration["JWT:RefreshTokenExpirationTimeInDays"], out int refreshTokenExpirationTimeInDays);
            user.RefreshToken = _authService.GenerateRefreshToken();
            user.RefreshTokenExpirationTime = DateTime.Now.AddDays(refreshTokenExpirationTimeInDays);
            _context.SaveChanges();

            RefreshTokenViewModel refreshTokenVM = _mapper.Map<RefreshTokenViewModel>(user);
            refreshTokenVM.TokenJWT = _authService.GenerateJWT(user.Email!, user.UserName!);

            return new ResponseBase<RefreshTokenViewModel>()
            {
                ResponseInfo = null,
                Value = refreshTokenVM
            };

        }
    }
}
