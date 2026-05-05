using Microsoft.AspNetCore.Mvc;
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.Services;

namespace Adam_Ahmed_Web_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetAll()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        [HttpPost]
        public async Task<ActionResult<DoctorReadDto>> Create(DoctorCreateDto createDto)
        {
            var result = await _doctorService.CreateDoctorAsync(createDto);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DoctorUpdateDto updateDto)
        {
            var result = await _doctorService.UpdateDoctorAsync(id, updateDto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorService.DeleteDoctorAsync(id);
            if (!result) return NotFound();
            return Ok();
        }
    }
}