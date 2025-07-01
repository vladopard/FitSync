using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitSync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisePlansController : ControllerBase
    {
        private readonly IExercisePlanService _service;

        public ExercisePlansController(IExercisePlanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExercisePlanDTO>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        // GET: api/exerciseplans/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ExercisePlanDTO>>> GetByUser(string userId)
        {
            var list = await _service.GetAllByUserAsync(userId);
            return Ok(list);
        }

        // GET: api/exerciseplans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExercisePlanDTO>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return Ok(dto);
        }

        // POST: api/exerciseplans
        [HttpPost]
        public async Task<ActionResult<ExercisePlanDTO>> Create([FromBody] ExercisePlanCreateDTO createDto)
        {
            var created = await _service.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/exerciseplans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExercisePlanUpdateDTO updateDto)
        {
            await _service.UpdateAsync(id, updateDto);
            return NoContent();
        }


        // DELETE: api/exerciseplans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
