namespace JobService.API.Domains.Models;

public class JobBookmark
{
    public Guid Id { get; set; }

    public Guid JobId { get; set; }
    public Guid UserId { get; set; }

    public DateTime BookmarkedAt { get; set; }
}
