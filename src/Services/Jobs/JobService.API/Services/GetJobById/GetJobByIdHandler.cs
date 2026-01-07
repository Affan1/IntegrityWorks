namespace JobService.API.Services.GetJobById;

public record GetJobByIdQuery(Guid JobId) : IRequest<GetJobByIdResult>;
public record GetJobByIdResult(Job Job);
internal class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, GetJobByIdResult>
{
    private readonly JobsDbContext _dbContext;
    public GetJobByIdQueryHandler(JobsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<GetJobByIdResult> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var job = await _dbContext.Jobs
            .AsNoTracking()
            .FirstOrDefaultAsync(j => j.Id == request.JobId, cancellationToken);
        if (job == null) {
            throw new JobNotFoundException(request.JobId);
        }

        return new GetJobByIdResult(job);
    }
}
