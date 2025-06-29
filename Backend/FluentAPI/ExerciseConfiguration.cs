using FitSync.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitSync.FluentAPI
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.ToTable("Exercises");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.MuscleGroup)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(e => e.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.HasMany(e => e.PlanItems)
                .WithOne(p => p.Exercise)
                .HasForeignKey(p => p.ExerciseId);

            builder.HasMany(e => e.WorkoutExercises)
                .WithOne(w => w.Exercise)
                .HasForeignKey(w => w.ExerciseId);
        }
    }
}
