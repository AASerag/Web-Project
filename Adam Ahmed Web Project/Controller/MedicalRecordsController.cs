using Microsoft.AspNetCore.Mvc;
using Adam_Ahmed_Web_Project.Services;
using Adam_Ahmed_Web_Project.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Adam_Ahmed_Web_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] // Uncomment this if you want to restrict to logged-in users
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordsController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        // GET: api/MedicalRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecordReadDto>>> GetAll()
        {
            var records = await _medicalRecordService.GetAllRecordsAsync();
            return Ok(records);
        }

        // GET: api/MedicalRecords/patient/5
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<MedicalRecordReadDto>>> GetByPatient(int patientId)
        {
            var records = await _medicalRecordService.GetRecordsByPatientIdAsync(patientId);
            return Ok(records);
        }

        // POST: api/MedicalRecords
        [HttpPost]
        public async Task<ActionResult<MedicalRecordReadDto>> Create(MedicalRecordCreateDto createDto)
        {
            try
            {
                var createdRecord = await _medicalRecordService.CreateRecordAsync(createDto);
                
                // Returns 201 Created with the new record (including PatientName/DoctorName)
                return CreatedAtAction(nameof(GetAll), new { id = createdRecord.Id }, createdRecord);
            }
            catch (Exception ex)
            {
                // This catches the 1:1 relationship duplicate key error
                return BadRequest("Could not create record. Patient might already have one.");
            }
        }

        // DELETE: api/MedicalRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _medicalRecordService.DeleteRecordAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }

        // PUT: api/MedicalRecords/5 (Optional for Edit functionality)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MedicalRecordCreateDto updateDto)
        {
            // Note: You must add 'UpdateRecordAsync' to your IMedicalRecordService for this to work
            // If you haven't added it yet, you can skip this method
            var success = await _medicalRecordService.UpdateRecordAsync(id, updateDto);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}