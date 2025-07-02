namespace FitSync.DTOs
{
    public abstract class WorkoutBaseDTO
    {
        public int? ExercisePlanId { get; set; }
    }
    public class WorkoutCreateDTO : WorkoutBaseDTO { }

    public class WorkoutUpdateDTO : WorkoutBaseDTO { }

    public class WorkoutPatchDTO
    {
        public int? ExercisePlanId { get; set; }
    }
    public class WorkoutDTO : WorkoutBaseDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; } = null!;
        public string? PlanName { get; set; }
        public List<WorkoutExerciseDTO> Exercises { get; set; } = new();
    }
}
