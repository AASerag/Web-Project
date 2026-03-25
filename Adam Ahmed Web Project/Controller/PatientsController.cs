using Microsoft.AspNetCore.Mvc;
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.Services;
using Microsoft.AspNetCore.Authorization;

namespace Adam_Ahmed_Web_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: api/patients
        [HttpGet]
        [Authorize(Roles = "Admin,User")] // Both Admin and User can view
        public async Task<ActionResult<IEnumerable<PatientReadDto>>> GetAll()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        // GET: api/patients/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")] // Both Admin and User can view
        public async Task<ActionResult<PatientReadDto>> GetById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        // POST: api/patients
        [HttpPost]
        [Authorize(Roles = "Admin")] // ONLY Admin can create
        public async Task<ActionResult<PatientReadDto>> Create(PatientCreateDto createDto)
        {
            var result = await _patientService.CreatePatientAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/patients/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // ONLY Admin can update
        public async Task<IActionResult> Update(int id, PatientUpdateDto updateDto)
        {
            var success = await _patientService.UpdatePatientAsync(id, updateDto);
            if (!success) return NotFound();
            return NoContent();
        }

        // DELETE: api/patients/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // ONLY Admin can delete
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _patientService.DeletePatientAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}