namespace JobService.API.Services.GetJobs;

public record GetJobRequest(int? PageNumber = 1 , int? PageSize = 10);
public record GetJobResponse(IEnumerable<Job> Jobs);

public class GetJobEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/jobs", async ([AsParameters] GetJobRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetJobsQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetJobResponse>();
            return Results.Ok(response);
        })
        .Produces<GetJobResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("GetJobs")
        .WithTags("Jobs");
    }
}
