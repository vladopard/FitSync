namespace FitSync.DTOs
{
    // 1) Заједнички базни DTO (за Create и Update)
    public abstract class ExercisePlanItemBaseDTO
    {
        public required int ExercisePlanId { get; set; }
        public required int ExerciseId { get; set; }
        public required int Order { get; set; }
        public required int Sets { get; set; }
        public required int Reps { get; set; }
        public string? Note { get; set; }
    }

    public class ExercisePlanItemCreateDTO : ExercisePlanItemBaseDTO { }

    public class ExercisePlanItemUpdateDTO : ExercisePlanItemBaseDTO { }

    public class ExercisePlanItemPatchDTO
    {
        public int? ExercisePlanId { get; set; }
        public int? ExerciseId { get; set; }
        public int? Order { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public string? Note { get; set; }
    }

    public class ExercisePlanItemDTO : ExercisePlanItemBaseDTO
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; } = null!;
        public string PlanName { get; set; } = null!;

    }

    public class ExercisePlanItemOrderDTO
    {
        public int Id { get; set; }
        public int Order { get; set; }
    }
}
