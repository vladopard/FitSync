namespace FitSync.DTOs
{
    // 1) Заједничка база (користе Create и Update DTO)
    public abstract class ExerciseBaseDTO
    {
        public required string Name { get; set; }
        public required string MuscleGroup { get; set; }
        public required string Type { get; set; }
        public required string Description { get; set; }
    }

    // 2) DTO за креирање
    public class ExerciseCreateDTO : ExerciseBaseDTO { }

    // 3) DTO за ажурирање
    public class ExerciseUpdateDTO : ExerciseBaseDTO { }

    // 4) DTO за делимично ажурирање (PATCH)
    public class ExercisePatchDTO
    {
        public string? Name { get; set; }
        public string? MuscleGroup { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
    }

    // 5) DTO за приказ клијенту
    public class ExerciseDTO : ExerciseBaseDTO
    {
        public int Id { get; set; }
    }
}
