
using JobService.API.Services.GetJobs;

namespace JobService.API.Services.GetJobById;

public record GetJobByIdRequest(Guid JobId);
public record GetJobByIdResponse(Job Job);
public class GetJobByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/jobs/{jobId}", async (Guid jobId, ISender sender) =>
        {
            var query = new GetJobByIdQuery(jobId);
            var result = await sender.Send(query);
            if (result is null)
            {
                return Results.NotFound();
            }
            var response = result.Adapt<GetJobByIdResponse>();
            return Results.Ok(response);
        })
            .Produces<GetJobResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("Get Job By ID")
            .WithDescription("Get Job By ID")
            .WithTags("GetJobID");
    }
}
