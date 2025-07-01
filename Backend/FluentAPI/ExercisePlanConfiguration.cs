using FitSync.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitSync.FluentAPI;

public class ExercisePlanConfiguration : IEntityTypeConfiguration<ExercisePlan>
{
    public void Configure(EntityTypeBuilder<ExercisePlan> builder)
    {
        builder.ToTable("ExercisePlans");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.HasIndex(p => new { p.UserId, p.Name })
            .IsUnique();

        builder.HasOne(p => p.User)
            .WithMany(u => u.ExercisePlans)
            .HasForeignKey(p => p.UserId)
            .IsRequired();

        builder.HasMany(p => p.Items)
            .WithOne(i => i.Plan)
            .HasForeignKey(i => i.ExercisePlanId);

        

    }
}
