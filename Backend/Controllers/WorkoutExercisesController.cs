using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitSync.Controllers
{
    [ApiController]
    [Route("api/workouts/{workoutId}/exercises")]
    public class WorkoutExercisesController : ControllerBase
    {
        private readonly IWorkoutExerciseService _service;

        public WorkoutExercisesController(IWorkoutExerciseService service)
        {
            _service = service;
        }

        // GET: api/workouts/{workoutId}/exercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutExerciseDTO>>> GetAll(int workoutId)
        {
            var list = await _service.GetAllByWorkoutAsync(workoutId);
            return Ok(list);
        }

        // GET: api/workouts/{workoutId}/exercises/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutExerciseDTO>> GetById(int workoutId, int id)
        {
            // Optionally, you can verify the workoutId matches the entity’s WorkoutId.
            var dto = await _service.GetByIdAsync(id);
            return Ok(dto);
        }

        // POST: api/workouts/{workoutId}/exercises
        [HttpPost]
        public async Task<ActionResult<WorkoutExerciseDTO>> Create(
            int workoutId,
            [FromBody] WorkoutExerciseCreateDTO createDto)
        {
            var created = await _service.CreateAsync(workoutId, createDto);
            return CreatedAtAction(
                nameof(GetById),
                new { workoutId = workoutId, id = created.Id },
                created);
        }

        // PUT: api/workouts/{workoutId}/exercises/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int workoutId,
            int id,
            [FromBody] WorkoutExerciseUpdateDTO updateDto)
        {
            // Optionally, you can verify the DTO’s WorkoutId matches the route if you include it.
            await _service.UpdateAsync(id, updateDto);
            return NoContent();
        }

        // DELETE: api/workouts/{workoutId}/exercises/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int workoutId, int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
