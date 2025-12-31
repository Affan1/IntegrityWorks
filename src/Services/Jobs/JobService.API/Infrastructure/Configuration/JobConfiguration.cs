using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobService.API.Infrastructure.Configuration;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("jobs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.Description)
               .IsRequired();

        builder.Property(x => x.CustomQuestionsJson)
               .HasColumnType("jsonb");

        builder.Property(x => x.ScamRiskScore)
               .HasColumnType("numeric(5,2)");

        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.PublishedAt);

        builder.HasMany(x => x.Skills)
               .WithOne(x => x.Job)
               .HasForeignKey(x => x.JobId);

        builder.HasMany(x => x.Categories)
               .WithOne(x => x.Job)
               .HasForeignKey(x => x.JobId);

        builder.HasMany(x => x.Attachments)
               .WithOne(x => x.Job)
               .HasForeignKey(x => x.JobId);
    }
}

