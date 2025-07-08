using FitSync.DTOs;
using FitSync.Entities;
using FitSync.Helpers;

public interface IFitSyncRepository
{
    //EXERCISE
    Task<IEnumerable<Exercise>> GetAllExercisesAsync();
    Task<PagedList<Exercise>> GetExercisesPagedAsync(ExerciseQueryParameters parameters);
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
    Task AddPlanWithItemsAsync(ExercisePlan plan, IEnumerable<ExercisePlanItem> items);
    void UpdatePlan(ExercisePlan plan);
    void DeletePlan(ExercisePlan plan);
    // EXERCISE PLAN ITEM
    Task<IEnumerable<ExercisePlanItem>> GetItemsForPlanAsync(int planId);
    Task<ExercisePlanItem?> GetPlanItemAsync(int id);
    Task AddPlanItemAsync(ExercisePlanItem item);
    Task ReorderPlanItemsAsync(IEnumerable<ExercisePlanItemOrderDTO> items);
    void UpdatePlanItem(ExercisePlanItem item);
    void DeletePlanItem(ExercisePlanItem item);
    // PERSONAL RECORD
    Task<IEnumerable<PersonalRecord>> GetAllPersonalRecordsByUserAsync(string userId);
    Task<PersonalRecord?> GetPersonalRecordByIdAsync(int id);
    Task AddPersonalRecordAsync(PersonalRecord record);
    void UpdatePersonalRecord(PersonalRecord record);
    void DeletePersonalRecord(PersonalRecord record);
    // WORKOUT
    Task<IEnumerable<Workout>> GetAllWorkoutsAsync();
    Task<IEnumerable<Workout>> GetAllWorkoutsByUserAsync(string userId);
    Task<Workout?> GetWorkoutByIdAsync(int id);
    Task AddWorkoutAsync(Workout workout);
    void UpdateWorkout(Workout workout);
    void DeleteWorkout(Workout workout);
    // WORKOUT EXERCISE
    Task<IEnumerable<WorkoutExercise>> GetAllWorkoutExercisesByWorkoutIdAsync(int workoutId);
    Task<WorkoutExercise?> GetWorkoutExerciseByIdAsync(int id);
    Task AddWorkoutExerciseAsync(WorkoutExercise we);
    Task AddWorkoutExercisesAsync(IEnumerable<WorkoutExercise> list);
    void UpdateWorkoutExercise(WorkoutExercise we);
    void DeleteWorkoutExercise(WorkoutExercise we);
    Task<double?> GetMaxRecordWeightAsync(string userId, int exerciseId);

    //HELPERS
    Task<bool> SaveChangesAsync();


}