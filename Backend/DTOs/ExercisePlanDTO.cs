namespace FitSync.DTOs
{
    public abstract class ExercisePlanBaseDTO
    {
        public required string Name { get; set; }           // нпр. "Chest Day"
        public string? Description { get; set; }            // опционално
    }

    // 2) DTO за креирање (треба нам и UserId)
    public class ExercisePlanCreateDTO : ExercisePlanBaseDTO
    {
        public required string UserId { get; set; }
    }

    // 3) DTO за потпуно ажурирање (PUT) — не мењамо UserId
    public class ExercisePlanUpdateDTO : ExercisePlanBaseDTO { }

    // 4) DTO за делимично ажурирање (PATCH)
    public class ExercisePlanPatchDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    // 5) DTO за приказ (Read)
    public class ExercisePlanDTO : ExercisePlanBaseDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;

        // Списак ставки у плану
        public List<ExercisePlanItemDTO> Items { get; set; } = new();
    }

    
}
