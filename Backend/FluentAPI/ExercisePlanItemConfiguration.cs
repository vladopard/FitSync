using FitSync.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitSync.FluentAPI;

public class ExercisePlanItemConfiguration : IEntityTypeConfiguration<ExercisePlanItem>
{
    public void Configure(EntityTypeBuilder<ExercisePlanItem> builder)
    {
        builder.ToTable("ExercisePlanItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Order)
            .IsRequired();

        builder.Property(i => i.Sets)
            .IsRequired();

        builder.Property(i => i.Reps)
            .IsRequired();

        builder.Property(i => i.Note)
            .HasMaxLength(500);

        builder.HasOne(i => i.Plan)
            .WithMany(p => p.Items)
            .HasForeignKey(i => i.ExercisePlanId)
            .IsRequired();

        builder.HasOne(i => i.Exercise)
            .WithMany(e => e.PlanItems)
            .HasForeignKey(i => i.ExerciseId)
            .IsRequired();
    }
}
