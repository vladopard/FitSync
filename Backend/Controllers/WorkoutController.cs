using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitSync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _service;

        public WorkoutsController(IWorkoutService service)
        {
            _service = service;
        }

        //GET: api/workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        // GET: api/workouts/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetAllByUser(string userId)
        {
            var list = await _service.GetAllByUserAsync(userId);
            return Ok(list);
        }

        // GET: api/workouts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDTO>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return Ok(dto);
        }

        // POST: api/workouts/user/{userId}
        [HttpPost("user/{userId}")]
        public async Task<ActionResult<WorkoutDTO>> Create(string userId, [FromBody] WorkoutCreateDTO createDto)
        {
            var created = await _service.CreateAsync(userId, createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/workouts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkoutUpdateDTO updateDto)
        {
            await _service.UpdateAsync(id, updateDto);
            return NoContent();
        }

        // DELETE: api/workouts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
