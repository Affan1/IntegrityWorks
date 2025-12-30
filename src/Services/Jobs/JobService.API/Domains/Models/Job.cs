namespace JobService.API.Domains.Models;

public class Job
{
    public Guid Id { get; set; }

    // External reference (Users service)
    public Guid ClientId { get; set; }

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    public string Status { get; set; } = default!;

    public decimal? BudgetMin { get; set; }
    public decimal? BudgetMax { get; set; }
    public string BudgetType { get; set; } = default!;

    public string ExperienceLevel { get; set; } = default!;

    public DateTime? Deadline { get; set; }

    public string LocationType { get; set; } = default!;
    public string? Location { get; set; }

    // Flexible job-specific questions
    public string? CustomQuestionsJson { get; set; }

    // Denormalized counter (updated via events)
    public int ProposalCount { get; set; }

    public bool IsFeatured { get; set; }

    public decimal? ScamRiskScore { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? ClosedAt { get; set; }

    // Navigation
    public ICollection<JobCategory> Categories { get; set; } = new List<JobCategory>();
    public ICollection<JobSkill> Skills { get; set; } = new List<JobSkill>();
    public ICollection<JobAttachment> Attachments { get; set; } = new List<JobAttachment>();
}

