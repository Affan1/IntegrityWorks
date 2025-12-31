namespace JobService.API.Domains.Models;

public class JobSkill
{
    public Guid JobId { get; set; }
    public Job Job { get; set; } = default!;

    public Guid SkillId { get; set; }
    public Skill Skill { get; set; } = default!;

    public DateTime AssignedAt { get; set; }
}

