
namespace JobService.API.Services.CreateCategories;

public record CreateCategoriesRequest(
    string Name,
    string Description,
    Guid? CategoryId = null,
    bool IsActive = true
);
public record CreateCategoryResponse(
    Guid Id
);
public class CreateCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/categories", async (CreateCategoriesRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateCategoryCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateCategoryResponse>();
            return Results.Created($"/categories/{response.Id}", response);
        })
            .Produces<CreateCategoryResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
