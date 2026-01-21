
namespace JobService.API.Services.UpdateJob;

public record UpdateJobRequest(
    Guid Id,
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
    bool IsFeatured = false
    );
public record UpdateJobResponse(bool IsSuccess);

public class UpdateJobEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/jobs", async (UpdateJobRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateJobCommand>();
            var result = await sender.Send(command);
            var response = new UpdateJobResponse(result.IsSuccess);
            return Results.Ok(response);
        })
            .Produces<UpdateJobResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("UpdateJob")
            .WithTags("Jobs")
            .ProducesProblem(StatusCodes.Status404NotFound);
            
    }
}
