using BusinessLogic.Common;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Minimal_Routes.Application_Routes
{
    public static class ApplicationUserAPI
    {
        public static void MapApplicationUserRoutes(this IEndpointRouteBuilder app, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            // Signup Post
            app.MapPost("api/register", async ([FromServices] IUserService userService, RegisterUser registerUser ) =>
            {
                var response = await userService.RegisterUser(registerUser);

                var apiResponse = new ApiResponseModel(response.HasValidationError ?
                    System.Net.HttpStatusCode.Conflict : System.Net.HttpStatusCode.OK, response.Message,
                    response.Exception, response.Data);

                return Results.Json(apiResponse);
            })
                .AllowAnonymous()
                .WithTags("Application User");

            //// Signup Post
            //app.MapMethods("api/accounts", new[] { "PATCH" }, async ([FromServices] IAccount accountService) =>
            //  {
            //      var response = await accountService.Accounts();

            //      var apiResponse = new ApiResponseModel(response.HasValidationError ?
            //          System.Net.HttpStatusCode.Conflict : System.Net.HttpStatusCode.OK,
            //          response.Message, response.Exception, response.Data);

            //      return Results.Json(apiResponse);
            //  })
            //    .RequireAuthorization()
            //    .WithTags("Application User");

     

            // login Post
            app.MapPost("api/login", async ([FromServices] IUserService userService, LoginRequest loginRequest) =>
            {
                var response = await userService.LogIn(loginRequest);
                var apiResponse = new ApiResponseModel(response.HasValidationError ? System.Net.HttpStatusCode.Conflict : System.Net.HttpStatusCode.OK, response.Message, response.Exception, response.Data);
                return Results.Json(apiResponse);
            })
                .AllowAnonymous()
                .WithTags("Application User");

            // GetUsers

            app.MapGet("api/getusers", async([FromServices] IUserService userService ) =>
            {
                var response = await userService.GetUsers();
                var apiResponse = new ApiResponseModel(response.HasValidationError ? System.Net.HttpStatusCode.Conflict : System.Net.HttpStatusCode.OK, response.Message, response.Exception, response.Data);
                return Results.Json(apiResponse);
            })
                .RequireAuthorization()
                .WithTags("Application User");


            // Edit 
            app.MapPut("api/updateuser", async ([FromServices] IUserService userService, RegisterUser registerUser) =>
            {
                return Results.Ok(await userService.UpdateUser(registerUser));
            })
                .RequireAuthorization()
                .WithTags("Application User");
        }



    }
}