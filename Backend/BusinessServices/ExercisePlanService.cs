using AutoMapper;
using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using FitSync.Entities;

namespace FitSync.BusinessServices
{
    public class ExercisePlanService : IExercisePlanService
    {
        private readonly IFitSyncRepository _repo;
        private readonly IMapper _mapper;

        public ExercisePlanService(IFitSyncRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ExercisePlanDTO>> GetAll()
        {
            var entities = await _repo.GetAllPlansAsync();
            return _mapper.Map<IEnumerable<ExercisePlanDTO>>(entities);
        }

        public async Task<IEnumerable<ExercisePlanDTO>> GetAllByUserAsync(string userId)
        {
            var entities = await _repo.GetPlansByUserAsync(userId);
            return _mapper.Map<IEnumerable<ExercisePlanDTO>>(entities);
        }

        public async Task<ExercisePlanDTO> GetByIdAsync(int id)
        {
            var entity = await GetPlanOrThrowAsync(id);
            return _mapper.Map<ExercisePlanDTO>(entity);
        }

        public async Task<ExercisePlanDTO> CreateAsync(ExercisePlanCreateDTO dto)
        {
            var entity = _mapper.Map<ExercisePlan>(dto);
            await _repo.AddPlanAsync(entity);
            await _repo.SaveChangesAsync();
            return _mapper.Map<ExercisePlanDTO>(entity);
        }

        public async Task<ExercisePlanDTO> CopyAsync(int id, string userId)
        {
            var source = await GetPlanOrThrowAsync(id);

            var newPlan = new ExercisePlan
            {
                Name = source.Name,
                Description = source.Description,
                UserId = userId
            };

            var items = source.Items
                .OrderBy(i => i.Order)
                .Select(i => new ExercisePlanItem
                {
                    ExerciseId = i.ExerciseId,
                    Order = i.Order,
                    Sets = i.Sets,
                    Reps = i.Reps,
                    Note = i.Note
                })
                .ToList();

            await _repo.AddPlanWithItemsAsync(newPlan, items);

            // ако ти треба „свеже“ учитане навигације
            var full = await _repo.GetPlanAsync(newPlan.Id) ?? newPlan;
            return _mapper.Map<ExercisePlanDTO>(full);
        }


        public async Task UpdateAsync(int id, ExercisePlanUpdateDTO dto)
        {
            var entity = await GetPlanOrThrowAsync(id);
            _mapper.Map(dto, entity);
            await _repo.SaveChangesAsync();
        }
       
        public async Task PatchAsync(int id, ExercisePlanPatchDTO dto)
        {
            var entity = await GetPlanOrThrowAsync(id);
            _mapper.Map(dto, entity);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetPlanOrThrowAsync(id);
            _repo.DeletePlan(entity);
            await _repo.SaveChangesAsync();
        }

        private async Task<ExercisePlan> GetPlanOrThrowAsync(int id)
            => await _repo.GetPlanAsync(id)
               ?? throw new KeyNotFoundException($"ExercisePlan with id {id} not found.");
    }
}

