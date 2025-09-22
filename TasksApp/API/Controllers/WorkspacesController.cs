using Application.UserCQ.Commands;
using Application.WorkspaceCQ.Commands;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public static class WorkspacesController
    {
        public static void WorkspacesRoutes(this WebApplication app)
        {
            var group = app.MapGroup("Workspaces").WithTags("Workspaces");

            group.MapPost("create-workspace", () => CreateWorkspace).RequireAuthorization(); // mesmo que utilizar Autorize nas Controller
            group.MapPost("edit-workspace", () => EditWorkspace);
            group.MapPost("delete-workspace", () => DeleteWorkspace);
            group.MapPost("get-all-workspace", () => GetAllWorkspace);
            group.MapPost("get-workspace", () => GetWorkspace);

            //Delegates
        }

        public static async Task<IResult> CreateWorkspace(
            [FromServices] IMediator _mediator,
            [FromBody] CreateWorkspaceCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.ResponseInfo is null)
            {
                return Results.Ok(result.Value);
            }
            return Results.BadRequest(result.ResponseInfo);
        }
        public static async Task<IResult> EditWorkspace([FromServices] IMediator _mediator)
        {

        }
        public static async Task<IResult> DeleteWorkspace([FromServices] IMediator _mediator)
        {

        }
        public static async Task<IResult> GetAllWorkspace([FromServices] IMediator _mediator)
        {

        }
        public static async Task<IResult> GetWorkspace([FromServices] IMediator _mediator)
        {

        }
    }
}
