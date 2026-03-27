using Microsoft.AspNetCore.Mvc;
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.Services;

namespace Adam_Ahmed_Web_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _service;
        public PrescriptionsController(IPrescriptionService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionReadDto>>> GetAll() => Ok(await _service.GetAllPrescriptionsAsync());

        [HttpPost]
        public async Task<ActionResult<PrescriptionReadDto>> Create(PrescriptionCreateDto dto)
        {
            var result = await _service.CreatePrescriptionAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }
    }
}