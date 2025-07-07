using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitSync.Helpers
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext http)
        {
            try
            {
                await _next(http);
            }
            catch (DbUpdateException dbEx)
                when (dbEx.InnerException?.Message.Contains("duplicate key value") == true)
            {
                var message = dbEx.InnerException?.Message ?? "";

                // Check your PostgreSQL constraint names
                var detail = message.Contains("IX_ExercisePlanItems_ExercisePlanId_ExerciseId")
                    ? "ExerciseAlreadyExists"
                    : message.Contains("IX_ExercisePlanItems_ExercisePlanId_Order")
                        ? "OrderAlreadyExists"
                        : "DuplicatePlanName";

                var title = detail switch
                {
                    "ExerciseAlreadyExists" => "This exercise is already part of the plan.",
                    "OrderAlreadyExists" => "This order number is already used in the plan.",
                    _ => "Conflict"
                };

                var problem = new ProblemDetails
                {
                    Type = "https://httpstatuses.com/409",
                    Title = title,
                    Status = StatusCodes.Status409Conflict,
                    Detail = detail,
                    Instance = http.Request.Path
                };

                await WriteProblemAsync(http, problem);
            }
            catch (KeyNotFoundException knf)
            {
                var problem = new ProblemDetails
                {
                    Type = "https://httpstatuses.com/404",
                    Title = "Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = knf.Message,
                    Instance = http.Request.Path
                };
                await WriteProblemAsync(http, problem);
            }
            catch (InvalidOperationException ioe)
            {
                var problem = new ProblemDetails
                {
                    Type = "https://httpstatuses.com/409",
                    Title = "Conflict",
                    Status = StatusCodes.Status409Conflict,
                    Detail = ioe.Message,
                    Instance = http.Request.Path
                };
                await WriteProblemAsync(http, problem);
            }
            catch (Exception)
            {
                var problem = new ProblemDetails
                {
                    Type = "https://httpstatuses.com/500",
                    Title = "Internal Server Error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "An unexpected error occurred. Please try again later.",
                    Instance = http.Request.Path
                };
                await WriteProblemAsync(http, problem);
            }
        }

        private static async Task WriteProblemAsync(HttpContext http, ProblemDetails problem)
        {
            http.Response.StatusCode = problem.Status!.Value;
            http.Response.ContentType = "application/problem+json";
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(problem, options);
            await http.Response.WriteAsync(json);
        }
    }
}
