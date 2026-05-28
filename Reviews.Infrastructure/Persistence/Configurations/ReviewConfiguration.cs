using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reviews.Domain.Entities;

namespace Reviews.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.CourseId)
            .IsRequired();

        builder.HasIndex(r => new { r.CourseId, r.UserId })
            .IsUnique();

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.Text)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        builder.Property(r => r.UpdatedAt)
            .IsRequired(false);

        builder.Property(r => r.Rating)
            .HasConversion(
                rating => rating.Value,
                value => Rating.Create(value)
            )
            .IsRequired();

        builder.HasIndex(r => r.CourseId);
    }
}