using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobService.API.Infrastructure.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Slug)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.Slug).IsUnique();

        builder.HasOne(x => x.Parent)
               .WithMany(x => x.Children)
               .HasForeignKey(x => x.ParentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
    private static void SeedCategories(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Web Development",
                Slug = "web-development",
                ParentId = null,
                IsActive = true
            },
            new Category
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Mobile Development",
                Slug = "mobile-development",
                ParentId = null,
                IsActive = true
            },
            new Category
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Graphic Design",
                Slug = "graphic-design",
                ParentId = null,
                IsActive = true
            },
            new Category
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Digital Marketing",
                Slug = "digital-marketing",
                ParentId = null,
                IsActive = true
            },
            new Category
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Name = "Content Writing",
                Slug = "content-writing",
                ParentId = null,
                IsActive = true
            },
            new Category
            {
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                Name = "Data Science",
                Slug = "data-science",
                ParentId = null,
                IsActive = true
            },
            new Category
            {
                Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                Name = "DevOps & Cloud",
                Slug = "devops-cloud",
                ParentId = null,
                IsActive = true
            },
            new Category
            {
                Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                Name = "Project Management",
                Slug = "project-management",
                ParentId = null,
                IsActive = true
            },
            new Category
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Name = "Customer Support",
                Slug = "customer-support",
                ParentId = null,
                IsActive = true
            },
            new Category
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Sales & Business",
                Slug = "sales-business",
                ParentId = null,
                IsActive = true
            }
        );
    }
}

