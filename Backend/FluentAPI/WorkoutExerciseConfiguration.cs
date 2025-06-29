using FitSync.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitSync.FluentAPI;

public class WorkoutExerciseConfiguration : IEntityTypeConfiguration<WorkoutExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
    {
        builder.ToTable("WorkoutExercises");

        builder.HasKey(we => we.Id);

        builder.Property(we => we.Sets)
            .IsRequired();

        builder.Property(we => we.Reps)
            .IsRequired();

        builder.Property(we => we.Weight)
            .IsRequired();

        builder.Property(we => we.RestSeconds)
            .IsRequired();

        builder.Property(we => we.OrderInWorkout)
            .IsRequired();

        builder.Property(we => we.Notes)
            .HasMaxLength(500);

        builder.HasOne(we => we.Workout)
            .WithMany(w => w.Exercises)
            .HasForeignKey(we => we.WorkoutId)
            .IsRequired();

        builder.HasOne(we => we.Exercise)
            .WithMany(e => e.WorkoutExercises)
            .HasForeignKey(we => we.ExerciseId)
            .IsRequired();
    }
}
