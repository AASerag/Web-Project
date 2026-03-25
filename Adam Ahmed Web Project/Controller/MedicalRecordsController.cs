using Adam_Ahmed_Web_Project.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class MedicalRecordsController : ControllerBase
{
    private readonly IMedicalRecordService _medicalService;
    public MedicalRecordsController(IMedicalRecordService medicalService) => _medicalService = medicalService;

    [HttpPost]
    public async Task<IActionResult> Create(MedicalRecordCreateDto dto)
    {
        var result = await _medicalService.CreateAsync(dto);
        return Ok(result);
    }
}