using AutoMapper;
using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using FitSync.Entities;

namespace FitSync.BusinessServices
{

    public class ExercisePlanItemService : IExercisePlanItemService
    {
        private readonly IFitSyncRepository _repo;
        private readonly IMapper _mapper;

        public ExercisePlanItemService(IFitSyncRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExercisePlanItemDTO>> GetAllByPlanAsync(int planId)
        {
            var entities = await _repo.GetItemsForPlanAsync(planId);
            return _mapper.Map<IEnumerable<ExercisePlanItemDTO>>(entities);
        }

        public async Task<ExercisePlanItemDTO> GetByIdAsync(int id)
        {
            var entity = await GetPlanItemOrThrowAsync(id);
            return _mapper.Map<ExercisePlanItemDTO>(entity);
        }

        public async Task<ExercisePlanItemDTO> CreateAsync(ExercisePlanItemCreateDTO dto)
        {
            var entity = _mapper.Map<ExercisePlanItem>(dto);
            await _repo.AddPlanItemAsync(entity);
            await _repo.SaveChangesAsync();
            return _mapper.Map<ExercisePlanItemDTO>(entity);
        }

        public async Task UpdateAsync(int id, ExercisePlanItemUpdateDTO dto)
        {
            var entity = await GetPlanItemOrThrowAsync(id);
            _mapper.Map(dto, entity);
            await _repo.SaveChangesAsync();
        }

        public async Task ReorderAsync(IEnumerable<ExercisePlanItemOrderDTO> items)
        {
            await _repo.ReorderPlanItemsAsync(items);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetPlanItemOrThrowAsync(id);
            _repo.DeletePlanItem(entity);
            await _repo.SaveChangesAsync();
        }

        private async Task<ExercisePlanItem> GetPlanItemOrThrowAsync(int id)
            => await _repo.GetPlanItemAsync(id)
               ?? throw new KeyNotFoundException($"ExercisePlanItem with id {id} not found.");
    }
}
