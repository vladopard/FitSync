using FitSync.DTOs;

namespace FitSync.BusinessServices.Intefaces
{
    public interface IExercisePlanItemService
    {
        Task<ExercisePlanItemDTO> CreateAsync(ExercisePlanItemCreateDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ExercisePlanItemDTO>> GetAllByPlanAsync(int planId);
        Task<ExercisePlanItemDTO> GetByIdAsync(int id);
        Task UpdateAsync(int id, ExercisePlanItemUpdateDTO dto);
        Task ReorderAsync(IEnumerable<ExercisePlanItemOrderDTO> items);
    }
}