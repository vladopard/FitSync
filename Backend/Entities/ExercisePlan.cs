namespace FitSync.Entities;

public class ExercisePlan
{
    public int Id { get; set; }

    public required string Name { get; set; }        // нпр. "Ponedeljak – Grudi"
    public string? Description { get; set; }

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public ICollection<ExercisePlanItem> Items { get; set; } = new List<ExercisePlanItem>();
}
