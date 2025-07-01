using FitSync.Entities;

public interface IFitSyncRepository
{
    //EXERCISE
    Task<IEnumerable<Exercise>> GetAllExercisesAsync();
    Task<Exercise?> GetExerciseByIdAsync(int id);
    Task AddExerciseAsync(Exercise exercise);
    void DeleteExercise(Exercise exercise);
    Task<bool> ExerciseExistsAsync(string name);
    void UpdateExercise(Exercise exercise);
    //EXERCISE PLAN
    Task<IEnumerable<ExercisePlan>> GetAllPlansAsync();
    Task<IEnumerable<ExercisePlan>> GetPlansByUserAsync(string userId);
    Task<ExercisePlan?> GetPlanAsync(int id);
    Task AddPlanAsync(ExercisePlan plan);
    void UpdatePlan(ExercisePlan plan);
    void DeletePlan(ExercisePlan plan);
    // EXERCISE PLAN ITEM
    Task<IEnumerable<ExercisePlanItem>> GetItemsForPlanAsync(int planId);
    Task<ExercisePlanItem?> GetPlanItemAsync(int id);
    Task AddPlanItemAsync(ExercisePlanItem item);
    void UpdatePlanItem(ExercisePlanItem item);
    void DeletePlanItem(ExercisePlanItem item);


    //HELPERS
    Task<bool> SaveChangesAsync();


}