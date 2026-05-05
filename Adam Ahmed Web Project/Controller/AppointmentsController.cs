using Microsoft.AspNetCore.Mvc;
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.Services;

namespace Adam_Ahmed_Web_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentReadDto>>> GetAll()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentReadDto>> Create(AppointmentCreateDto createDto)
        {
            var result = await _appointmentService.CreateAppointmentAsync(createDto);
            // Links back to the GetAll method for the location header
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appointmentService.DeleteAppointmentAsync(id);
            if (!result) return NotFound();
            return Ok();
        }
    }
}