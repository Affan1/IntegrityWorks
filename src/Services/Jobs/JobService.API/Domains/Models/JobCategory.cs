namespace JobService.API.Domains.Models;

public class JobCategory
{
    public Guid JobId { get; set; }
    public Guid CategoryId { get; set; }

    public DateTime AssignedAt { get; set; }

    public Job Job { get; set; } = default!;
    public Category Category { get; set; } = default!;
}

