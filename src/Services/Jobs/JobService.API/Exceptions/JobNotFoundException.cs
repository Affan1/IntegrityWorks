using BuildingBlocks.Exceptions;

namespace JobService.API.Exceptions;

public class JobNotFoundException : NotFoundException
{
    public JobNotFoundException(Guid jobId)
        : base($"Job with id '{jobId}' was not found.")
    {
    } 
}
