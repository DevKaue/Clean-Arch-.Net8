using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator, IConfiguration configuration) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Rota responsável pela criação de usuário
        /// </summary>
        /// <param name="command">
        /// Um Objeto CreateUserCommand
        /// </param>
        /// <returns> Os dados do usuário criado.</returns>
        /// <remarks>
        /// Exemplo de request:
        /// ```
        /// POST /User/Create-User
        /// {
        ///   "name": "teste",
        ///   "surname": "teste",
        ///   "username": "teste",
        ///   "email": "teste@mail.com",
        ///   "password": "1234"
        /// }
        /// ``` 
        /// </remarks>
        /// <response code="200">Retorna os dados do novo usuário registrado</response>
        /// <response code="400">Caso ocorra um erro de digitação referente a alguma informação</response>
        [HttpPost("Create-User")]
        public async Task<ActionResult<UserInfoViewModel>> CreateUser(CreateUserCommand command)
        {
            var request = await _mediator.Send(command);

            if(request.ResponseInfo is null)
            {
                var userInfo = request.Value;

                if (userInfo is not null) 
                {
                    var cookieOptionsToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(2)
                    };

                    _ = int.TryParse(_configuration["JWT:RefreshTokenExpirationTimeInDays"], out int refreshTokenExpirationTimeInDays);

                    var cookieOptionsRefreshToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(refreshTokenExpirationTimeInDays)
                    };

                    Response.Cookies.Append("jwt",request!.Value!.TokenJWT!, cookieOptionsToken);
                    Response.Cookies.Append("refreshToken", request!.Value!.RefreshToken!, cookieOptionsRefreshToken);

                    return Ok(request);
                }
            }

            return BadRequest(request);
        }
    }
}
