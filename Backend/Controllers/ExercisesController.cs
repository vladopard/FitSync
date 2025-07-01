using FitSync.BusinessServices;
using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitSync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _service;

        public ExercisesController(IExerciseService service)
        {
            _service = service;
        }

        // GET: api/exercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        // GET: api/exercises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDTO>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return Ok(dto);
        }

        // POST: api/exercises
        [HttpPost]
        public async Task<ActionResult<ExerciseDTO>> Create([FromBody] ExerciseCreateDTO createDto)
        {
            var created = await _service.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/exercises/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExerciseUpdateDTO updateDto)
        {
            await _service.UpdateAsync(id, updateDto);
            return NoContent();
        }

        // DELETE: api/exercises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
