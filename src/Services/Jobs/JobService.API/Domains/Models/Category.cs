namespace JobService.API.Domains.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public Guid? ParentId { get; set; }
    public bool IsActive { get; set; }

    public Category? Parent { get; set; }
    public ICollection<Category> Children { get; set; } = new List<Category>();
    public ICollection<JobCategory> JobCategories { get; set; } = new List<JobCategory>();
}
