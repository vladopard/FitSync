namespace FitSync.Entities;

public class ExercisePlanItem
{
    public int Id { get; set; }

    public int ExercisePlanId { get; set; }
    public ExercisePlan Plan { get; set; } = null!;

    public int ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;

    public int Order { get; set; }        // редослед у плану
    public int Sets { get; set; }
    public int Reps { get; set; }

    public string? Note { get; set; }     // нпр. "Zagrevaj lagano"
}
