using AutoMapper;
using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using FitSync.Entities;

namespace FitSync.BusinessServices
{
    public class WorkoutExerciseService : IWorkoutExerciseService
    {
        private readonly IFitSyncRepository _repo;
        private readonly IMapper _mapper;

        public WorkoutExerciseService(IFitSyncRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkoutExerciseDTO>> GetAllByWorkoutAsync(int workoutId)
        {
            // Ensure the workout exists
            var workout = await _repo.GetWorkoutByIdAsync(workoutId)
                ?? throw new KeyNotFoundException($"Workout with id {workoutId} not found.");

            var list = await _repo.GetAllWorkoutExercisesByWorkoutIdAsync(workoutId);
            return _mapper.Map<IEnumerable<WorkoutExerciseDTO>>(list);
        }

        public async Task<WorkoutExerciseDTO> GetByIdAsync(int id)
        {
            var we = await GetOrThrowAsync(id);
            return _mapper.Map<WorkoutExerciseDTO>(we);
        }

        public async Task<WorkoutExerciseDTO> CreateAsync(int workoutId, WorkoutExerciseCreateDTO dto)
        {
            var entity = _mapper.Map<WorkoutExercise>(dto);
            entity.WorkoutId = workoutId;

            await _repo.AddWorkoutExerciseAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<WorkoutExerciseDTO>(entity);
        }

        public async Task UpdateAsync(int id, WorkoutExerciseUpdateDTO dto)
        {
            var entity = await GetOrThrowAsync(id);
            _mapper.Map(dto, entity);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetOrThrowAsync(id);
            _repo.DeleteWorkoutExercise(entity);
            await _repo.SaveChangesAsync();
        }

        private async Task<WorkoutExercise> GetOrThrowAsync(int id)
            => await _repo.GetWorkoutExerciseByIdAsync(id)
               ?? throw new KeyNotFoundException($"WorkoutExercise with id {id} not found.");
    }
}
