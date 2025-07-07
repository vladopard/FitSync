using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitSync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisePlanItemsController : ControllerBase
    {
        private readonly IExercisePlanItemService _service;

        public ExercisePlanItemsController(IExercisePlanItemService service)
        {
            _service = service;
        }

        // GET: api/exerciseplanitems/plan/{planId}
        [HttpGet("plan/{planId}")]
        public async Task<ActionResult<IEnumerable<ExercisePlanItemDTO>>> GetByPlan(int planId)
        {
            var items = await _service.GetAllByPlanAsync(planId);
            return Ok(items);
        }

        // GET: api/exerciseplanitems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ExercisePlanItemDTO>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return Ok(dto);
        }

        // POST: api/exerciseplanitems
        [HttpPost]
        public async Task<ActionResult<ExercisePlanItemDTO>> Create([FromBody] ExercisePlanItemCreateDTO createDto)
        {
            var created = await _service.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/exerciseplanitems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExercisePlanItemUpdateDTO updateDto)
        {
            await _service.UpdateAsync(id, updateDto);
            return NoContent();
        }

        // DELETE: api/exerciseplanitems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // PUT: api/exerciseplanitems/reorder
        [HttpPut("reorder")]
        public async Task<IActionResult> Reorder([FromBody] IEnumerable<ExercisePlanItemOrderDTO> items)
        {
            await _service.ReorderAsync(items);
            return NoContent();
        }
    }
}
