
using Microsoft.IdentityModel.Tokens;

namespace JobService.API.Services.DeleteJob;

public record DeleteJobCommand(Guid Id) : ICommand<DeleteJobResult>;
public record DeleteJobResult(bool IsSuccess);
internal class DeleteJobCommandHandler : ICommandHandler<DeleteJobCommand, DeleteJobResult>
{
    private readonly JobsDbContext _dbContext;
    public DeleteJobCommandHandler(JobsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<DeleteJobResult> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _dbContext.Jobs.FindAsync(new object?[] { request.Id }, cancellationToken);
        if (job is null)
        {
            return new DeleteJobResult(IsSuccess: false);
        }
        _dbContext.Jobs.Remove(job);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteJobResult(IsSuccess: true);
    }
}
