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

    // 2) DTO за креирање нове ставке
    public class ExercisePlanItemCreateDTO : ExercisePlanItemBaseDTO { }

    // 3) DTO за потпуно ажурирање (PUT)
    public class ExercisePlanItemUpdateDTO : ExercisePlanItemBaseDTO { }

    // 4) DTO за делимично ажурирање (PATCH)
    public class ExercisePlanItemPatchDTO
    {
        public int? ExercisePlanId { get; set; }
        public int? ExerciseId { get; set; }
        public int? Order { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public string? Note { get; set; }
    }

    // 5) DTO за приказ (Read)
    public class ExercisePlanItemDTO : ExercisePlanItemBaseDTO
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; } = null!;
        public string PlanName { get; set; } = null!;
    }

    // 6) DTO за промену редоследа
    public class ExercisePlanItemOrderDTO
    {
        public int Id { get; set; }
        public int Order { get; set; }
    }
}
