using System.Text.Json;

namespace JobService.API.Services.UpdateJob;

public record UpdateJobCommand(
    Guid Id,
    string Title,
    string Description,
    Guid ClientId,
    string Status,
    decimal? BudgetMin,
    decimal? BudgetMax,
    string BudgetType,
    string ExperienceLevel,
    DateTime? Deadline,
    string LocationType,
    string? Location,
    string? CustomQuestionsJson,
    bool IsFeatured
) : ICommand<UpdateJobResult>;

public record UpdateJobResult(bool IsSuccess);
public class UpdateJobCommandHandlerValidator : AbstractValidator<UpdateJobCommand>
{
    public UpdateJobCommandHandlerValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(5000).WithMessage("Description is too long");

        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("Client ID is required")
            .NotEqual(Guid.Empty).WithMessage("Client ID cannot be empty GUID");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status value");

        RuleFor(x => x.BudgetMin)
            .GreaterThanOrEqualTo(0).WithMessage("Minimum budget cannot be negative")
            .LessThanOrEqualTo(x => x.BudgetMax).When(x => x.BudgetMax.HasValue)
            .WithMessage("Minimum budget must be less than or equal to maximum budget");

        RuleFor(x => x.BudgetMax)
            .GreaterThanOrEqualTo(0).When(x => x.BudgetMax.HasValue)
            .WithMessage("Maximum budget cannot be negative");

        RuleFor(x => x.BudgetType)
            .IsInEnum().WithMessage("Invalid budget type");

        RuleFor(x => x.ExperienceLevel)
            .IsInEnum().WithMessage("Invalid experience level");

        RuleFor(x => x.Deadline)
            .GreaterThan(DateTime.UtcNow).When(x => x.Deadline.HasValue)
            .WithMessage("Deadline must be in the future");

        RuleFor(x => x.LocationType)
            .IsInEnum().WithMessage("Invalid location type");

        RuleFor(x => x.Location)
            .MaximumLength(500).WithMessage("Location is too long")
            .NotEmpty()
            .WithMessage("Location is required for on-site jobs");

        RuleFor(x => x.CustomQuestionsJson)
            .Must(BeValidJson).When(x => !string.IsNullOrEmpty(x.CustomQuestionsJson))
            .WithMessage("Custom questions must be valid JSON");
    }
    
    private bool BeValidJson(string json)
    {
        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
internal class UpdateJobCommandHandler : ICommandHandler<UpdateJobCommand, UpdateJobResult>
{
    private readonly JobsDbContext _dbContext;
    public UpdateJobCommandHandler(JobsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<UpdateJobResult> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var Job = await _dbContext.Jobs
            .AsNoTracking()
            .FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken);

        if (Job == null)
        {
             throw new JobNotFoundException(request.Id);
        }

        Job.Title = request.Title;
        Job.Description = request.Description;
        Job.ClientId = request.ClientId;
        Job.Status = request.Status;
        Job.BudgetMin = request.BudgetMin;
        Job.BudgetMax = request.BudgetMax;
        Job.BudgetType = request.BudgetType;
        Job.ExperienceLevel = request.ExperienceLevel;
        Job.Deadline = request.Deadline.HasValue
            ? DateTime.SpecifyKind(request.Deadline.Value, DateTimeKind.Utc)
            : null;
        Job.LocationType = request.LocationType;
        Job.Location = request.Location;
        Job.CustomQuestionsJson = request.CustomQuestionsJson;
        Job.IsFeatured = request.IsFeatured;
        Job.UpdatedAt = DateTime.UtcNow;
        _dbContext.Jobs.Update(Job);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateJobResult(true);
    }
}