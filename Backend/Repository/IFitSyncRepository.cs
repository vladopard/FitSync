using FitSync.Entities;

public interface IFitSyncRepository
{
    Task<IEnumerable<Exercise>> GetAllExercisesAsync();
    Task<Exercise?> GetExerciseByIdAsync(int id);
    Task AddExerciseAsync(Exercise exercise);
    void DeleteExercise(Exercise exercise);
    Task<bool> ExerciseExistsAsync(string name);
    void UpdateExercise(Exercise exercise);
    Task<bool> SaveChangesAsync();
}