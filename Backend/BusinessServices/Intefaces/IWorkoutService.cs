using FitSync.DTOs;

namespace FitSync.BusinessServices.Intefaces
{
    public interface IWorkoutService
    {
        Task<WorkoutDTO> CreateAsync(string userId, WorkoutCreateDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<WorkoutDTO>> GetAllAsync();
        Task<IEnumerable<WorkoutDTO>> GetAllByUserAsync(string userId);
        Task<WorkoutDTO> GetByIdAsync(int id);
        Task UpdateAsync(int id, WorkoutUpdateDTO dto);
    }
}