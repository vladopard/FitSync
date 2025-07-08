using FitSync.DbContext;
using FitSync.DTOs;
using FitSync.Entities;
using Microsoft.EntityFrameworkCore;

public class FitSyncRepository : IFitSyncRepository
{
    private readonly AppDbContext _ctx;

    public FitSyncRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    // === EXERCISE ===
    public async Task<IEnumerable<Exercise>> GetAllExercisesAsync()
        => await _ctx.Exercises.AsNoTracking().ToListAsync();

    public async Task<Exercise?> GetExerciseByIdAsync(int id)
        => await _ctx.Exercises.FindAsync(id);

    public async Task AddExerciseAsync(Exercise exercise)
        => await _ctx.Exercises.AddAsync(exercise);

    public void DeleteExercise(Exercise exercise)
        => _ctx.Exercises.Remove(exercise);
    public void UpdateExercise(Exercise exercise)
        => _ctx.Exercises.Update(exercise);
    
    public async Task<bool> ExerciseExistsAsync(string name)
        => await _ctx.Exercises.AnyAsync(e => e.Name.ToLower() == name.ToLower().Trim());


    // === EXERCISE PLAN ===

    public async Task<IEnumerable<ExercisePlan>> GetAllPlansAsync()
    {
        var plans = await _ctx.ExercisePlans
            .Include(p => p.Items)
                .ThenInclude(i => i.Exercise)
            .AsNoTracking()
            .ToListAsync();

        // Сортирај сваки план по редоследу ставки
        foreach (var plan in plans)
        {
            plan.Items = plan.Items
                .OrderBy(i => i.Order)
                .ToList();
        }

        return plans;
    }

    public async Task<IEnumerable<ExercisePlan>> GetPlansByUserAsync(string userId)
    {
        var plans = await _ctx.ExercisePlans
            .Where(p => p.UserId == userId)
            .Include(p => p.Items)
                .ThenInclude(i => i.Exercise)
            .AsNoTracking()
            .ToListAsync();

        foreach (var plan in plans)
        {
            plan.Items = plan.Items
                .OrderBy(i => i.Order)
                .ToList();
        }

        return plans;
    }

    public async Task<ExercisePlan?> GetPlanAsync(int id)
    {
        var plan = await _ctx.ExercisePlans
            .Include(p => p.Items)
                .ThenInclude(i => i.Exercise)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (plan != null)
        {
            plan.Items = plan.Items
                .OrderBy(i => i.Order)
                .ToList();
        }

        return plan;
    }


    public async Task AddPlanAsync(ExercisePlan plan)
        => await _ctx.ExercisePlans.AddAsync(plan);

    public void UpdatePlan(ExercisePlan plan)
        => _ctx.ExercisePlans.Update(plan);

    public void DeletePlan(ExercisePlan plan)
        => _ctx.ExercisePlans.Remove(plan);



    // === EXERCISE PLAN ITEMS ===
    public async Task<IEnumerable<ExercisePlanItem>> GetItemsForPlanAsync(int planId)
        => await _ctx.ExercisePlanItems
                     .Where(i => i.ExercisePlanId == planId)
                     .Include(i => i.Exercise)
                     .Include(i => i.Plan)
                     .AsNoTracking()
                     .ToListAsync();

    public async Task<ExercisePlanItem?> GetPlanItemAsync(int id)
        => await _ctx.ExercisePlanItems
                     .Include(i => i.Exercise)
                     .Include(i => i.Plan)
                     .FirstOrDefaultAsync(i => i.Id == id);

    public async Task ReorderPlanItemsAsync(IEnumerable<ExercisePlanItemOrderDTO> items)
    {
        var list = items.ToList();
        var ids = list.Select(x => x.Id);

        var cases = string.Join("\n", list.Select(i =>
            $"WHEN {i.Id} THEN {i.Order}"
        ));

        var sql = $@"
      UPDATE ""ExercisePlanItems""
      SET ""Order"" = CASE ""Id""
        {cases}
      END
      WHERE ""Id"" IN ({string.Join(",", ids)});
    ";

        // Почни транзакцију
        await using var tx = await _ctx.Database.BeginTransactionAsync();
        // Одложи све дефејрабл констреинте (укупно или по имену)
        await _ctx.Database.ExecuteSqlRawAsync(
            @"SET CONSTRAINTS ALL DEFERRED;"
        );

        // Сада уради атомарни UPDATE
        await _ctx.Database.ExecuteSqlRawAsync(sql);

        // Комитуј
        await tx.CommitAsync();
    }


    public async Task AddPlanItemAsync(ExercisePlanItem item)
    {
        await _ctx.ExercisePlanItems.AddAsync(item);
        await _ctx.SaveChangesAsync();
        await _ctx.Entry(item)
            .Reference(i => i.Exercise)
            .LoadAsync();
        await _ctx.Entry(item)
            .Reference(i => i.Plan)
            .LoadAsync();
    }

    public void UpdatePlanItem(ExercisePlanItem item)
        => _ctx.ExercisePlanItems.Update(item);

    public void DeletePlanItem(ExercisePlanItem item)
        => _ctx.ExercisePlanItems.Remove(item);

    // === PERSONAL RECORDS ===
    public async Task<IEnumerable<PersonalRecord>> GetAllPersonalRecordsByUserAsync(string userId)
        => await _ctx.PersonalRecords
                     .Where(pr => pr.UserId == userId)
                     .Include(pr => pr.Exercise)
                     .AsNoTracking()
                     .ToListAsync();

    public async Task<PersonalRecord?> GetPersonalRecordByIdAsync(int id)
        => await _ctx.PersonalRecords
                     .Include(pr => pr.Exercise)
                     .FirstOrDefaultAsync(pr => pr.Id == id);

    public async Task AddPersonalRecordAsync(PersonalRecord record)
    {
        await _ctx.PersonalRecords.AddAsync(record);
        await _ctx.SaveChangesAsync();

        await _ctx.Entry(record)
            .Reference(r => r.Exercise)
            .LoadAsync();

        await _ctx.Entry(record)
            .Reference(r => r.User)
            .LoadAsync(); 
    }

    public void UpdatePersonalRecord(PersonalRecord record)
        => _ctx.PersonalRecords.Update(record);

    public void DeletePersonalRecord(PersonalRecord record)
        => _ctx.PersonalRecords.Remove(record);

    // === WORKOUT ===
    public async Task<IEnumerable<Workout>> GetAllWorkoutsAsync()
        => await _ctx.Workouts
                .Include(w => w.ExercisePlan)
                .Include(w => w.Exercises)
                    .ThenInclude(e => e.Exercise)
                .AsNoTracking()
                .ToListAsync();
    public async Task<IEnumerable<Workout>> GetAllWorkoutsByUserAsync(string userId)
        => await _ctx.Workouts
                .Where(w => w.UserId == userId)
                .Include(w => w.ExercisePlan)
                .Include(w => w.Exercises)
                    .ThenInclude(e => e.Exercise)
                .AsNoTracking()
                .ToListAsync();
    public async Task<Workout?> GetWorkoutByIdAsync(int id)
        => await _ctx.Workouts
                     .Include(w => w.ExercisePlan)
                     .Include(w => w.Exercises)
                       .ThenInclude(e => e.Exercise)
                     .FirstOrDefaultAsync(w => w.Id == id);

    public async Task AddWorkoutAsync(Workout workout)
    {
        await _ctx.Workouts.AddAsync(workout);
        await _ctx.SaveChangesAsync();

        // immediately load the plan nav
        await _ctx.Entry(workout)
                  .Reference(w => w.ExercisePlan)
                  .LoadAsync();
    }

    public void UpdateWorkout(Workout workout)
        => _ctx.Workouts.Update(workout);

    public void DeleteWorkout(Workout workout)
        => _ctx.Workouts.Remove(workout);

    // === WORKOUT EXERCISE ===

    public async Task<IEnumerable<WorkoutExercise>> GetAllWorkoutExercisesByWorkoutIdAsync(int workoutId)
        => await _ctx.WorkoutExercises
                     .Where(we => we.WorkoutId == workoutId)
                     .Include(we => we.Exercise)
                     .AsNoTracking()
                     .ToListAsync();

    public async Task<WorkoutExercise?> GetWorkoutExerciseByIdAsync(int id)
        => await _ctx.WorkoutExercises
                     .Include(we => we.Exercise)
                     .FirstOrDefaultAsync(we => we.Id == id);

    public async Task AddWorkoutExerciseAsync(WorkoutExercise we)
    {
        await _ctx.WorkoutExercises.AddAsync(we);

        await _ctx.SaveChangesAsync();

        await _ctx.Entry(we)
                  .Reference(x => x.Exercise)
                  .LoadAsync();
    }

    public async Task AddWorkoutExercisesAsync(IEnumerable<WorkoutExercise> list)
    {
        await _ctx.WorkoutExercises.AddRangeAsync(list);
        await _ctx.SaveChangesAsync();

        foreach (var we in list)
            await _ctx.Entry(we)
                      .Reference(x => x.Exercise)
                      .LoadAsync();
    }

    public void UpdateWorkoutExercise(WorkoutExercise we)
        => _ctx.WorkoutExercises.Update(we);

    public void DeleteWorkoutExercise(WorkoutExercise we)
        => _ctx.WorkoutExercises.Remove(we);

    //HELPERS
    public async Task<bool> SaveChangesAsync()
        => await _ctx.SaveChangesAsync() > 0;
}
