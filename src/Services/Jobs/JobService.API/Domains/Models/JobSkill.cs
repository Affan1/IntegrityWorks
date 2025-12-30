namespace JobService.API.Domains.Models;

public class JobSkill
{
    public Guid Id { get; set; }

    public Guid JobId { get; set; }

    // References Skills service or Skills table
    public Guid SkillId { get; set; }

    public string ProficiencyLevel { get; set; } = default!;

    public DateTime AssignedAt { get; set; }

    public Job Job { get; set; } = default!;
}

