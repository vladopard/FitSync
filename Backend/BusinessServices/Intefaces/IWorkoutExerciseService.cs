using FitSync.DTOs;

namespace FitSync.BusinessServices.Intefaces
{
    public interface IWorkoutExerciseService
    {
        Task<WorkoutExerciseDTO> CreateAsync(int workoutId, WorkoutExerciseCreateDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<WorkoutExerciseDTO>> GetAllByWorkoutAsync(int workoutId);
        Task<WorkoutExerciseDTO> GetByIdAsync(int id);
        Task UpdateAsync(int id, WorkoutExerciseUpdateDTO dto);
    }
}