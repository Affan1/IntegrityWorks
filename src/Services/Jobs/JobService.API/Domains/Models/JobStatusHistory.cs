namespace JobService.API.Domains.Models;

public class JobStatusHistory
{
    public Guid Id { get; set; }

    public Guid JobId { get; set; }

    public string OldStatus { get; set; } = default!;
    public string NewStatus { get; set; } = default!;

    // External user reference
    public Guid ChangedBy { get; set; }

    public string? Reason { get; set; }

    public DateTime ChangedAt { get; set; }
}

