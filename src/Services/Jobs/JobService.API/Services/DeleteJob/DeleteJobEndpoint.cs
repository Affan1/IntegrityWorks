namespace JobService.API.Services.DeleteJob;

public record DeleteJobQuery(Guid Id);
public record DeleteJobResponse(bool IsSuccess);
public class DeleteJobEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/jobs/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteJobCommand(id));
            var response = result.Adapt<DeleteJobResponse>();
            return Results.Ok(response);
        })
            .WithDescription("Deleting Job")
            .WithName("Delete Job")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<DeleteJobResponse>(StatusCodes.Status200OK);
    }
}
