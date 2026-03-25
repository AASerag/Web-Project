using Microsoft.AspNetCore.Mvc;
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.Services;

namespace Adam_Ahmed_Web_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationsController : ControllerBase
    {
        private readonly IMedicationService _medicationService;

        public MedicationsController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationReadDto>>> GetAll()
        {
            return Ok(await _medicationService.GetAllMedicationsAsync());
        }

        [HttpPost]
        public async Task<ActionResult<MedicationReadDto>> Create(MedicationCreateDto createDto)
        {
            var result = await _medicationService.CreateMedicationAsync(createDto);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }
    }
}