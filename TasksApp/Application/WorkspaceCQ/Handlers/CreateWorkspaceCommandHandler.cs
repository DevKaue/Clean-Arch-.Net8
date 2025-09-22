using Application.Response;
using Application.WorkspaceCQ.Commands;
using Application.WorkspaceCQ.ViewModels;
using MediatR;

namespace Application.WorkspaceCQ.Handlers
{
    public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, ResponseBase<CreateWorkspaceViewModel>>
    {
        public Task<ResponseBase<CreateWorkspaceViewModel>> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
