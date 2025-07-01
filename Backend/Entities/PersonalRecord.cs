namespace FitSync.Entities;

public class PersonalRecord
{
    public int Id { get; set; }

    // Веза ка кориснику
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    // Веза ка вежби
    public int ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;

    // Рекорд: тежина и број понављања
    public double MaxWeight { get; set; }  // нпр. 120.0
    public int Reps { get; set; }          // нпр. 1 → 1RM

    private DateTime _achievedAt;
    public DateTime AchievedAt
    {
        get => _achievedAt;
        set => _achievedAt = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
}
