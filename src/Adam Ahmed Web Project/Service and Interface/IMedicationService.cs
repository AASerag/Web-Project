using Adam_Ahmed_Web_Project.Dtos;

namespace Adam_Ahmed_Web_Project.Services
{
    public interface IMedicationService
    {
        Task<IEnumerable<MedicationReadDto>> GetAllMedicationsAsync();
        Task<MedicationReadDto> CreateMedicationAsync(MedicationCreateDto createDto);
    }
}