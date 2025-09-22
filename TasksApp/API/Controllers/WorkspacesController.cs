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
            group.MapPost("create-workspace", () => CreateWorkspace);
            group.MapPost("edit-workspace", () => EditWorkspace);
            group.MapPost("delete-workspace", () => DeleteWorkspace);
            group.MapPost("get-all-workspace", () => GetAllWorkspace);
            group.MapPost("get-workspace", () => GetWorkspace);

            //Delegates
        }

        public static Results<Ok, BadRequest> CreateWorkspace([FromServices] IMediator _mediator
            , [FromBody] CreateWorkspaceCommand command)
        {
            
        }
        public static Results<Ok, BadRequest> EditWorkspace([FromServices] IMediator _mediator)
        {

        }
        public static Results<Ok, BadRequest> DeleteWorkspace([FromServices] IMediator _mediator)
        {

        }
        public static Results<Ok, BadRequest> GetAllWorkspace([FromServices] IMediator _mediator)
        {

        }
        public static Results<Ok, BadRequest> GetWorkspace([FromServices] IMediator _mediator)
        {

        }
    }
}
