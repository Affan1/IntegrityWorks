namespace JobService.API.Domains.Models;

public class JobView
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }
    public Guid? UserId { get; set; }

    public DateTime ViewedAt { get; set; }
    public string? IpAddress { get; set; }

    public Job Job { get; set; } = default!;
}


