using FitSync.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitSync.FluentAPI;

public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Date)
            .IsRequired();

        builder.HasOne(w => w.User)
            .WithMany(u => u.Workouts)
            .HasForeignKey(w => w.UserId)
            .IsRequired();

        builder.HasOne(w => w.ExercisePlan)
            .WithMany()
            .HasForeignKey(w => w.ExercisePlanId)
            .OnDelete(DeleteBehavior.SetNull); // ако план буде обрисан, остави null

        builder.HasMany(w => w.Exercises)
            .WithOne(we => we.Workout)
            .HasForeignKey(we => we.WorkoutId);

        builder.HasIndex(w => new { w.UserId, w.Date })
               .IsUnique()
               .HasDatabaseName("IX_Workouts_UserId_Date_Unique");
    }
}
