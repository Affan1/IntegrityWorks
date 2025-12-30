using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobService.API.Infrastructure.Configuration;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("jobs");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(j => j.Description)
            .IsRequired();

        builder.Property(j => j.CustomQuestionsJson)
            .HasColumnType("jsonb");

        builder.Property(j => j.ScamRiskScore)
            .HasColumnType("numeric(5,2)");

        builder.HasIndex(j => j.Status);
        builder.HasIndex(j => j.PublishedAt);
    }
}

