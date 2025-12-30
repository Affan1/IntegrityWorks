namespace JobService.API.Domains.Models;

public class JobAttachment
{
    public Guid Id { get; set; }

    public Guid JobId { get; set; }

    public string FileName { get; set; } = default!;
    public string FileUrl { get; set; } = default!;
    public string FileType { get; set; } = default!;
    public long FileSize { get; set; }

    public DateTime UploadedAt { get; set; }

    public Job Job { get; set; } = default!;
}

