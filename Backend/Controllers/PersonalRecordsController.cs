using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitSync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalRecordsController : ControllerBase
    {
        private readonly IPersonalRecordService _service;

        public PersonalRecordsController(IPersonalRecordService service)
        {
            _service = service;
        }

        // GET: api/personalrecords/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<PersonalRecordDTO>>> GetAllByUser(string userId)
        {
            var list = await _service.GetAllByUserAsync(userId);
            return Ok(list);
        }

        // GET: api/personalrecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalRecordDTO>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return Ok(dto);
        }

        // POST: api/personalrecords/user/{userId}
        [HttpPost("user/{userId}")]
        public async Task<ActionResult<PersonalRecordDTO>> Create(
            string userId,
            [FromBody] PersonalRecordCreateDTO createDto)
        {
            var created = await _service.CreateAsync(userId, createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/personalrecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] PersonalRecordUpdateDTO updateDto)
        {
            await _service.UpdateAsync(id, updateDto);
            return NoContent();
        }

        // DELETE: api/personalrecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
