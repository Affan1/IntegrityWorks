using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobService.API.Infrastructure.Configuration;

public class JobViewConfiguration : IEntityTypeConfiguration<JobView>
{
    public void Configure(EntityTypeBuilder<JobView> builder)
    {
        builder.ToTable("job_views");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ViewedAt)
               .HasDefaultValueSql("now()");

        builder.HasOne(x => x.Job)
               .WithMany()
               .HasForeignKey(x => x.JobId);

        builder.HasIndex(x => x.JobId);
        builder.HasIndex(x => x.ViewedAt);
    }
}
