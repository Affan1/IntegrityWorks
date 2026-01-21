using JobService.API.Domains.Models;
using JobService.API.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace JobService.API.Infrastructure;

public class JobsDbContext : DbContext
{
    public JobsDbContext(DbContextOptions<JobsDbContext> options)
        : base(options)
    {
    }
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<JobSkill> JobSkills => Set<JobSkill>();
    public DbSet<JobCategory> JobCategories => Set<JobCategory>();
    public DbSet<JobAttachment> JobAttachments => Set<JobAttachment>();
    public DbSet<JobBookmark> JobApplications => Set<JobBookmark>();
    public DbSet<JobView> JobViews => Set<JobView>();
    public DbSet<JobStatusHistory> JobStatusHistories => Set<JobStatusHistory>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Skill> Skills => Set<Skill>(); 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(JobsDbContext).Assembly
        );
    }
}
