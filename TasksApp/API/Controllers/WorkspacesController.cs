namespace API.Controllers
{
    public static class WorkspacesController
    {
        public static void WorkspacesRoutes(this WebApplication app)
        {
            var group = app.MapGroup("Workspaces").WithTags("Workspaces");
            group.MapGet("hello-world", () => "HELLO WORLD!");
        }
    }
}
