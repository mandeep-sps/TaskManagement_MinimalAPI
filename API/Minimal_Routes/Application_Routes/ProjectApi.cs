using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Minimal_Routes.Application_Routes
{
    public static class ProjectApi
    {
        public static void MapProjectRoutes(this IEndpointRouteBuilder app)
        {
            // Post
            app.MapPost("api/addproject", async ([FromServices] IProjectService projectService, ProjectRequest projectRequest) =>
            {
                return Results.Created("api/employees", await projectService.AddProject(projectRequest));
            })
                .RequireAuthorization()
                .WithDisplayName("Project Post")
                .WithTags("Project");

            // Edit 
            app.MapPut("api/updateproject", async ([FromServices] IProjectService projectService, ProjectUpdateRequest projectUpdateRequest) =>
            {
                return Results.Ok(await projectService.UpdateProject(projectUpdateRequest));
            })
                .RequireAuthorization()
                .WithTags("Project");

            // Delete
            app.MapDelete("api/deleteproject/{Id}", async ([FromServices] IProjectService projectService , int Id) =>
            {
                return Results.Ok(await projectService.DeleteProject(Id));
            })
                .RequireAuthorization()
                .WithTags("Project");
        }
    }
}
