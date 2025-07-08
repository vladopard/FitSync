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
            // 1) креирај празан тренинг
            var workout = new Workout
            {
                UserId = userId,
                ExercisePlanId = dto.ExercisePlanId,
                Date = DateTime.UtcNow
            };
            await _repo.AddWorkoutAsync(workout);     

            // 2) ако постоји план → клонирај ставке
            if (dto.ExercisePlanId.HasValue)
            {
                var plan = await _repo.GetPlanAsync(dto.ExercisePlanId.Value)
                         ?? throw new KeyNotFoundException($"Plan {dto.ExercisePlanId} not found.");

                if (plan.UserId != userId)               // без туђих планова
                    throw new UnauthorizedAccessException("Plan does not belong to user.");

                var list = plan.Items.Select(i => new WorkoutExercise
                {
                    WorkoutId = workout.Id,
                    ExerciseId = i.ExerciseId,
                    Sets = i.Sets,
                    Reps = i.Reps,
                    Weight = 1,          
                    RestSeconds = 60,
                    OrderInWorkout = i.Order,
                    Notes = i.Note
                }).ToList();

                if (list.Any())
                    await _repo.AddWorkoutExercisesAsync(list);
            }

            // 3) учитај све детаље да фронт добије попуњен object
            var full = await _repo.GetWorkoutByIdAsync(workout.Id)!;
            return _mapper.Map<WorkoutDTO>(full);
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