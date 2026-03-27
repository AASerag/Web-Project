using Adam_Ahmed_Web_Project.DataBase;
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.Models;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly AppDbContext _context;
    public MedicalRecordService(AppDbContext context) => _context = context;

    public async Task<MedicalRecordReadDto> CreateAsync(MedicalRecordCreateDto dto)
    {
        var record = new MedicalRecord
        {
            BloodType = dto.BloodType,
            Allergies = dto.Allergies,
            PatientId = dto.PatientId
        };

        _context.MedicalRecords.Add(record);
        await _context.SaveChangesAsync(); // Async requirement met!

        return new MedicalRecordReadDto
        {
            Id = record.Id,
            BloodType = record.BloodType,
            Allergies = record.Allergies,
            PatientId = record.PatientId
        };
    }
}