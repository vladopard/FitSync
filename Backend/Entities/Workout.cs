namespace FitSync.Entities;

public class Workout
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public int? ExercisePlanId { get; set; }           // опционално
    public ExercisePlan? ExercisePlan { get; set; }

    public ICollection<WorkoutExercise> Exercises { get; set; } = new List<WorkoutExercise>();
}
