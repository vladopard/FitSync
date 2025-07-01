using FitSync.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitSync.FluentAPI;

public class WorkoutExerciseConfiguration : IEntityTypeConfiguration<WorkoutExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
    {
        builder.ToTable("WorkoutExercises", tb => tb
            .HasCheckConstraint(
                "CK_WorkoutExercises_PositiveValues",
                "\"Sets\" > 0 AND \"Reps\" > 0 AND \"Weight\" > 0 AND \"RestSeconds\" > 0 AND \"OrderInWorkout\" > 0"
            )
          );

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

        builder.HasIndex(we => new { we.WorkoutId, we.OrderInWorkout })
               .IsUnique()
               .HasDatabaseName("UX_WorkoutExercises_Workout_Order");
    }
}
