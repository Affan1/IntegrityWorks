
using Microsoft.AspNetCore.Mvc;

namespace JobService.API.Services.CreateJobs;

public record CreateJobRequest(
    string Title,
    string Description,
    Guid ClientId,
    string Status = "Draft",
    decimal? BudgetMin = null,
    decimal? BudgetMax = null,
    string BudgetType = "Fixed", // or "Hourly", "Project"
    string ExperienceLevel = "Intermediate",
    DateTime? Deadline = null,
    string LocationType = "Remote", // or "OnSite", "Hybrid"
    string? Location = null,
    string? CustomQuestionsJson = null,
    bool IsFeatured = false);

public record CreateJobResponse(Guid Id);
public class CreateJobsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/jobs", async (CreateJobRequest request, ISender sender) =>
        {
            var job = new JobService.API.Domains.Models.Job
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                ClientId = request.ClientId,
                Status = request.Status,
                BudgetMin = request.BudgetMin,
                BudgetMax = request.BudgetMax,
                BudgetType = request.BudgetType,
                ExperienceLevel = request.ExperienceLevel,
                Deadline = request.Deadline,
                LocationType = request.LocationType,
                Location = request.Location,
                CustomQuestionsJson = request.CustomQuestionsJson,
                IsFeatured = request.IsFeatured,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            var command = request.Adapt<CreateJobCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateJobResponse>();

            return Results.Created($"/jobs/{response.Id}", response);
        })
        .Produces<CreateJobResult>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("CreateJob")
        .WithTags("Jobs");
    }
}
