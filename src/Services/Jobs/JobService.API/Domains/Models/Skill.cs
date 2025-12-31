namespace JobService.API.Domains.Models;

public class Skill
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public bool IsActive { get; set; } = true;

    public ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
}

