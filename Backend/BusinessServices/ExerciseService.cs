using AutoMapper;
using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using FitSync.Entities;

namespace FitSync.BusinessServices
{
    public class ExerciseService : IExerciseService
    {
        private readonly IFitSyncRepository _repo;
        private readonly IMapper _mapper;

        public ExerciseService(IFitSyncRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /* ─── READ ─────────────────────────────────────────── */

        public async Task<IEnumerable<ExerciseDTO>> GetAllAsync()
        {
            var entities = await _repo.GetAllExercisesAsync();
            return _mapper.Map<IEnumerable<ExerciseDTO>>(entities);
        }

        public async Task<ExerciseDTO> GetByIdAsync(int id)
        {
            var entity = await GetExerciseOrThrowAsync(id);
            return _mapper.Map<ExerciseDTO>(entity);
        }

        /* ─── CREATE ───────────────────────────────────────── */

        public async Task<ExerciseDTO> CreateAsync(ExerciseCreateDTO dto)
        {
            var entity = _mapper.Map<Exercise>(dto);
            await _repo.AddExerciseAsync(entity);
            await _repo.SaveChangesAsync();
            return _mapper.Map<ExerciseDTO>(entity);
        }

        /* ─── UPDATE (PUT) NIJE ODRADJEN ─────────────────────────────────── */

        public async Task UpdateAsync(int id, ExerciseUpdateDTO dto)
        {
            var entity = await GetExerciseOrThrowAsync(id);
            _mapper.Map(dto, entity);
            await _repo.SaveChangesAsync();
        }

        /* ─── PATCH NIJE ODRADJEN ────────────────────────────────────────── */

        public async Task PatchAsync(int id, ExercisePatchDTO dto)
        {
            var entity = await GetExerciseOrThrowAsync(id);
            _mapper.Map(dto, entity);
            await _repo.SaveChangesAsync();
        }

        /* ─── DELETE ───────────────────────────────────────── */

        public async Task DeleteAsync(int id)
        {
            var entity = await GetExerciseOrThrowAsync(id);
            _repo.DeleteExercise(entity);
            await _repo.SaveChangesAsync();
        }

        /* ─── HELPERS ──────────────────────────────────────── */

        private async Task<Exercise> GetExerciseOrThrowAsync(int id)
            => await _repo.GetExerciseByIdAsync(id)
               ?? throw new KeyNotFoundException($"Exercise with id {id} not found.");
    }
}
