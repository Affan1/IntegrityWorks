
namespace JobService.API.Services.CreateJobs;

public record CreateJobRequest
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public decimal Salary { get; init; }
    public List<string> Skills { get; init; } = new();
    public List<Guid> CategoryIds { get; init; } = new();
}
public class CreateJobsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        
    }
}
