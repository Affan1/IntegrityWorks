using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobService.API.Infrastructure.Configuration;

public class JobAttachmentConfiguration : IEntityTypeConfiguration<JobAttachment>
{
    public void Configure(EntityTypeBuilder<JobAttachment> builder)
    {
        builder.ToTable("job_attachments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.FileUrl)
            .IsRequired();

        builder.Property(a => a.FileSize)
            .HasColumnType("bigint");

        builder.HasOne(a => a.Job)
            .WithMany(j => j.Attachments)
            .HasForeignKey(a => a.JobId);
    }
}
