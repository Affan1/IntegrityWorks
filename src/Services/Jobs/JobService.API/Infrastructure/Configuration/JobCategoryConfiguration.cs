using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobService.API.Infrastructure.Configuration;

public class JobCategoryConfiguration : IEntityTypeConfiguration<JobCategory>
{
    public void Configure(EntityTypeBuilder<JobCategory> builder)
    {
        builder.ToTable("job_categories");

        builder.HasKey(x => new { x.JobId, x.CategoryId });

        builder.Property(x => x.AssignedAt)
               .HasDefaultValueSql("now()");

        builder.HasOne(x => x.Job)
               .WithMany(x => x.Categories)
               .HasForeignKey(x => x.JobId);

        builder.HasOne(x => x.Category)
               .WithMany(x => x.JobCategories)
               .HasForeignKey(x => x.CategoryId);

        builder.HasIndex(x => x.CategoryId);
    }
}

