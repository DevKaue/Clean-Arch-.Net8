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
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

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
            return Ok(await _mediator.Send(command));
        }
    }
}
