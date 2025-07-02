using AutoMapper;
using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using FitSync.Entities;

namespace FitSync.BusinessServices
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IFitSyncRepository _repo;
        private readonly IMapper _mapper;

        public WorkoutService(IFitSyncRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkoutDTO>> GetAllAsync()
        {
            var workouts = await _repo.GetAllWorkoutsAsync();
            return _mapper.Map<IEnumerable<WorkoutDTO>>(workouts);
        }
        public async Task<IEnumerable<WorkoutDTO>> GetAllByUserAsync(string userId)
        {
            var workouts = await _repo.GetAllWorkoutsByUserAsync(userId);
            return _mapper.Map<IEnumerable<WorkoutDTO>>(workouts);
        }

        public async Task<WorkoutDTO> GetByIdAsync(int id)
        {
            var workout = await GetWorkoutOrThrowAsync(id);
            return _mapper.Map<WorkoutDTO>(workout);
        }

        public async Task<WorkoutDTO> CreateAsync(string userId, WorkoutCreateDTO dto)
        {
            var workout = new Workout
            {
                UserId = userId,
                ExercisePlanId = dto.ExercisePlanId,
                Date = DateTime.UtcNow
            };

            await _repo.AddWorkoutAsync(workout);
            await _repo.SaveChangesAsync();

            return _mapper.Map<WorkoutDTO>(workout);
        }


        public async Task UpdateAsync(int id, WorkoutUpdateDTO dto)
        {
            var workout = await GetWorkoutOrThrowAsync(id);
            _mapper.Map(dto, workout);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var workout = await GetWorkoutOrThrowAsync(id);
            _repo.DeleteWorkout(workout);
            await _repo.SaveChangesAsync();
        }

        private async Task<Workout> GetWorkoutOrThrowAsync(int id)
            => await _repo.GetWorkoutByIdAsync(id)
               ?? throw new KeyNotFoundException($"Workout with id {id} not found.");
    }
}