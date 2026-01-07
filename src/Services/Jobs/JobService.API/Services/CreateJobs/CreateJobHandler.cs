namespace JobService.API.Services.CreateJobs;

public record CreateJobCommand(
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
    bool IsFeatured,

    IReadOnlyList<JobCategoryRequest>? Categories,
    IReadOnlyList<JobSkillRequest>? Skills,
    IReadOnlyList<JobAttachmentRequest>? Attachments
) : ICommand<CreateJobResult>;
public record CreateJobResult(Guid Id);

// Supporting records for nested collections
public record JobCategoryRequest(
    Guid CategoryId,
    string? Name); // Name might be denormalized for display purposes

public record JobSkillRequest(
    Guid SkillId,
    string? Name, // Denormalized skill name
    int? RequiredLevel);

public record JobAttachmentRequest(
    string FileName,
    string FileUrl,
    string FileType,
    long FileSize);


internal class CreateJobCommandHandler : ICommandHandler<CreateJobCommand, CreateJobResult>
{
    private readonly JobsDbContext _dbContext;
    public CreateJobCommandHandler(JobsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<CreateJobResult> Handle(
    CreateJobCommand request,
    CancellationToken cancellationToken)
    {
        var job = new Job
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
            Deadline = request.Deadline.HasValue
            ? DateTime.SpecifyKind(request.Deadline.Value, DateTimeKind.Utc)
            : null,
            LocationType = request.LocationType,
            Location = request.Location,
            CustomQuestionsJson = request.CustomQuestionsJson,
            IsFeatured = request.IsFeatured,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // 🔹 Categories (optional)
        if (request.Categories?.Any() == true)
        {
            foreach (var category in request.Categories)
            {
                job.Categories.Add(new JobCategory
                {
                    JobId = job.Id,
                    CategoryId = category.CategoryId,
                    AssignedAt = DateTime.UtcNow
                });
            }
        }

        // 🔹 Skills (optional)
        if (request.Skills?.Any() == true)
        {
            foreach (var skill in request.Skills)
            {
                job.Skills.Add(new JobSkill
                {
                    JobId = job.Id,
                    SkillId = skill.SkillId,
                    AssignedAt = DateTime.UtcNow
                });
            }
        }

        // 🔹 Attachments (optional)
        if (request.Attachments?.Any() == true)
        {
            foreach (var attachment in request.Attachments)
            {
                job.Attachments.Add(new JobAttachment
                {
                    Id = Guid.NewGuid(),
                    JobId = job.Id,
                    FileName = attachment.FileName,
                    FileUrl = attachment.FileUrl,
                    FileType = attachment.FileType,
                    FileSize = attachment.FileSize,
                    UploadedAt = DateTime.UtcNow
                });
            }
        }
        _dbContext.Jobs.Add(job);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateJobResult(job.Id);
    }

}
