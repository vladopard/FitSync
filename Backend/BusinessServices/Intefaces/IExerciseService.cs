using FitSync.DTOs;
using FitSync.Helpers;

namespace FitSync.BusinessServices.Intefaces
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseDTO>> GetAllAsync();
        Task<PagedList<ExerciseDTO>> GetAllAsync(ExerciseQueryParameters parameters);
        Task<ExerciseDTO> GetByIdAsync(int id);
        Task<ExerciseDTO> CreateAsync(ExerciseCreateDTO dto);
        Task UpdateAsync(int id, ExerciseUpdateDTO dto);
        Task PatchAsync(int id, ExercisePatchDTO dto);
        Task DeleteAsync(int id);
    }
}