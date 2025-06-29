namespace FitSync.Entities;

public class Exercise
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string MuscleGroup { get; set; }
    public required string Type { get; set; }
    public required string Description { get; set; }

    public ICollection<ExercisePlanItem> PlanItems { get; set; } = new List<ExercisePlanItem>();
    public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
    public ICollection<PersonalRecord> PersonalRecords { get; set; } = new List<PersonalRecord>();
}
