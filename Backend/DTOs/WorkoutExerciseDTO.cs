namespace FitSync.DTOs
{
    public abstract class WorkoutExerciseBaseDTO
    {
        public required int ExerciseId { get; set; }
        public required int Sets { get; set; }
        public required int Reps { get; set; }
        public required double Weight { get; set; }
        public required int RestSeconds { get; set; }
        public required int OrderInWorkout { get; set; }
        public string? Notes { get; set; }
    }

    public class WorkoutExerciseCreateDTO : WorkoutExerciseBaseDTO { }

    public class WorkoutExerciseUpdateDTO : WorkoutExerciseBaseDTO { }

    public class WorkoutExercisePatchDTO
    {
        public int? WorkoutId { get; set; }
        public int? ExerciseId { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public double? Weight { get; set; }
        public int? RestSeconds { get; set; }
        public int? OrderInWorkout { get; set; }
        public string? Notes { get; set; }
    }
    public class WorkoutExerciseDTO : WorkoutExerciseBaseDTO
    {
        public  int WorkoutId { get; set; }
        public int Id { get; set; }
        public string ExerciseName { get; set; } = null!;
    }
}
