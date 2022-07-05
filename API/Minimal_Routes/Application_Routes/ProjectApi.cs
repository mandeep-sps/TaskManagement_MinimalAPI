using BusinessLogic.Common;
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
            //GetProjects


            app.MapGet("api/getprojects", async ([FromServices] IProjectService projectService) =>
            {
                var response = await projectService.GetAllProject();
                var apiResponse = new ApiResponseModel(response.HasValidationError ? System.Net.HttpStatusCode.Conflict : System.Net.HttpStatusCode.OK, response.Message, response.Exception, response.Data);
                return Results.Json(apiResponse);
            })
                .RequireAuthorization()
              .WithTags("Project");

            //GetManagedProjects

            app.MapGet("api/getmanagedprojects", async ([FromServices] IProjectService projectService, int Id) =>
            {
                var response = await projectService.GetManagedProjects(Id);
                var apiResponse = new ApiResponseModel(response.HasValidationError ? System.Net.HttpStatusCode.Conflict : System.Net.HttpStatusCode.OK, response.Message, response.Exception, response.Data);
                return Results.Json(apiResponse);
            })
             .RequireAuthorization()
             .WithTags("Project");


            //GetUserProjects

            app.MapGet("api/getprojectsbyuser", async ([FromServices] IProjectService projectService, int UserId) =>
            {
                var response = await projectService.GetProjectsByUser(UserId);
                var apiResponse = new ApiResponseModel(response.HasValidationError ? System.Net.HttpStatusCode.Conflict : System.Net.HttpStatusCode.OK, response.Message, response.Exception, response.Data);
                return Results.Json(apiResponse);
            })
             .RequireAuthorization()
             .WithTags("Project");
        }
    }
}
