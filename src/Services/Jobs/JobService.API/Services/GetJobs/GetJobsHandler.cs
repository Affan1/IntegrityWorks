namespace JobService.API.Services.GetJobs;

public record GetJobsQuery(int? PageNumber = 1, int? PageSize = 10)
    : IQuery<GetJobsResult>;

public record GetJobsResult(IEnumerable<Job> Jobs);

public class GetJobsQueryHandler
    : IQueryHandler<GetJobsQuery, GetJobsResult>
{
    private readonly JobsDbContext _dbContext;

    public GetJobsQueryHandler(JobsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetJobsResult> Handle(
        GetJobsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Jobs
            .AsNoTracking()
            .OrderBy(j => j.Id); // ✅ Always order before Skip/Take

        if (request.PageNumber.HasValue && request.PageSize.HasValue)
        {
            int skip = (request.PageNumber.Value - 1) * request.PageSize.Value;
            query = (IOrderedQueryable<Job>)query.Skip(skip).Take(request.PageSize.Value);
        }

        var jobs = await query.ToListAsync(cancellationToken);
        return new GetJobsResult(jobs);
    }
}
