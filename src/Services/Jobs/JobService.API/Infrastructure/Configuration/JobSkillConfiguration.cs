using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobService.API.Infrastructure.Configuration;

public class JobSkillConfiguration : IEntityTypeConfiguration<JobSkill>
{
    public void Configure(EntityTypeBuilder<JobSkill> builder)
    {
        builder.ToTable("job_skills");

        // ✅ Composite PK
        builder.HasKey(js => new { js.JobId, js.SkillId });

        builder.Property(js => js.AssignedAt)
               .HasDefaultValueSql("now()");

        builder.HasOne(js => js.Job)
               .WithMany(j => j.Skills)
               .HasForeignKey(js => js.JobId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(js => js.Skill)
               .WithMany(s => s.JobSkills)
               .HasForeignKey(js => js.SkillId)
               .OnDelete(DeleteBehavior.Cascade);

        // ✅ Fast skill search
        builder.HasIndex(js => js.SkillId);
    }
}

