using FitSync.DbContext;
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
        => await _ctx.ExercisePlans
                      .Include(p => p.Items)
                        .ThenInclude(i => i.Exercise)
                      .AsNoTracking()
                      .ToListAsync();
    public async Task<IEnumerable<ExercisePlan>> GetPlansByUserAsync(string userId)
        => await _ctx.ExercisePlans
                     .Where(p => p.UserId == userId)
                     .Include(p => p.Items)
                        .ThenInclude(i => i.Exercise)
                     .AsNoTracking()
                     .ToListAsync();

    public async Task<ExercisePlan?> GetPlanAsync(int id)
        => await _ctx.ExercisePlans
                     .Include(p => p.Items)
                        .ThenInclude(i => i.Exercise)
                     .FirstOrDefaultAsync(p => p.Id == id);

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

    //HELPERS
    public async Task<bool> SaveChangesAsync()
        => await _ctx.SaveChangesAsync() > 0;
}
