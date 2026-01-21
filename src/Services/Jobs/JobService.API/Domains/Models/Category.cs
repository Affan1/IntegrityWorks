namespace JobService.API.Domains.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<JobCategory> JobCategories { get; set; } = new List<JobCategory>();
}
