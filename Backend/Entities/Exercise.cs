namespace FitSync.Entities
{
    public enum ExerciseType
    {
        Strength,
        Cardio,
        Stretching,
        Other
    }
    public enum MuscleGroup
    {
        Chest,
        Back,
        Legs,
        Shoulders,
        Arms,
        Core,
        Other
    }

    public class Exercise
    {
        public int Id { get; set; }

        // Required: the name of the exercise (e.g., "Bench Press")
        public required string Name { get; set; }

        // Optional: classify by group (e.g., Chest, Legs)
        public MuscleGroup MuscleGroup { get; set; }

        // Optional: type (e.g., Strength, Cardio, Stretching)
        public ExerciseType Type { get; set; }

        // Optional: short description or instructions
        public required string Description { get; set; }

        // Navigation
        public ICollection<ExercisePlanItem> PlanItems { get; set; } = new List<ExercisePlanItem>();
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
    }

}
