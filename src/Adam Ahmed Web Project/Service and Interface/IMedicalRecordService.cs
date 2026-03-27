using Adam_Ahmed_Web_Project.Dtos;

public interface IMedicalRecordService
{
    Task<MedicalRecordReadDto> CreateAsync(MedicalRecordCreateDto dto);
}