using FitSync.DTOs;

namespace FitSync.BusinessServices.Intefaces
{
    public interface IExercisePlanService
    {
        Task<ExercisePlanDTO> CreateAsync(ExercisePlanCreateDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ExercisePlanDTO>> GetAll();
        Task<IEnumerable<ExercisePlanDTO>> GetAllByUserAsync(string userId);
        Task<ExercisePlanDTO> GetByIdAsync(int id);
        Task PatchAsync(int id, ExercisePlanPatchDTO dto);
        Task UpdateAsync(int id, ExercisePlanUpdateDTO dto);

    }
}