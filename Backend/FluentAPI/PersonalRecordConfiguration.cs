using FitSync.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitSync.FluentAPI;

public class PersonalRecordConfiguration : IEntityTypeConfiguration<PersonalRecord>
{
    public void Configure(EntityTypeBuilder<PersonalRecord> builder)
    {
        builder.ToTable("PersonalRecords", tb => tb
            .HasCheckConstraint(
               "CK_PersonalRecords_PositiveValuesAndDate",
               "\"MaxWeight\" > 0 AND \"Reps\" > 0"
            )
          );

        builder.HasKey(pr => pr.Id);

        builder.Property(pr => pr.MaxWeight).IsRequired();
        builder.Property(pr => pr.Reps).IsRequired();
        builder.Property(pr => pr.AchievedAt).IsRequired();

        builder.HasOne(pr => pr.User)
            .WithMany(u => u.PersonalRecords)
            .HasForeignKey(pr => pr.UserId);

        builder.HasOne(pr => pr.Exercise)
            .WithMany(e => e.PersonalRecords)
            .HasForeignKey(pr => pr.ExerciseId);

        builder.HasIndex(pr => new { pr.ExerciseId, pr.UserId })
            .IsUnique()
            .HasDatabaseName("UX_PersonalRecord_Exercise_User");

    }
}
