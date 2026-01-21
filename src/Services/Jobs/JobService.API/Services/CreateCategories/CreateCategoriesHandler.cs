namespace JobService.API.Services.CreateCategories;

public record CreateCategoryCommand(
    string Name,
    string Description,
    bool IsActive = true
) : ICommand<CreateCategoryResult>;

public record CreateCategoryResult(
    bool IsSuccess
);
public class CreateCategoryCommandHandlerValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandHandlerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters");

        RuleFor(x => x.Name)
            .Must(name => !name.Contains("  "))
            .WithMessage("Category name cannot contain multiple spaces");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");
    }
}

internal class CreateCategoriesCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResult>
{
    private readonly JobsDbContext _categoryDb;
    public CreateCategoriesCommandHandler(
        JobsDbContext categoryRepository
    )
    {
        _categoryDb = categoryRepository;
    }
    public async Task<CreateCategoryResult> Handle(
    CreateCategoryCommand request,
    CancellationToken cancellationToken)
    {
        var baseSlug = SlugGenerator.Generate(request.Name);
        var slug = baseSlug;
        var suffix = 1;

        // Ensure uniqueness
        while (await _categoryDb.Categories
            .AnyAsync(c =>
                c.Slug == slug,
                cancellationToken))
        {
            slug = $"{baseSlug}-{suffix++}";
        }

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Slug = slug,
            Description = request.Description,
            IsActive = request.IsActive
        };

        _categoryDb.Categories.Add(category);
        await _categoryDb.SaveChangesAsync(cancellationToken);

        return new CreateCategoryResult(true);
    }

}
