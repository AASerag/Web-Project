using Adam_Ahmed_Web_Project.Dtos;

namespace Adam_Ahmed_Web_Project.Services
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<PrescriptionReadDto>> GetAllPrescriptionsAsync();
        Task<PrescriptionReadDto> CreatePrescriptionAsync(PrescriptionCreateDto createDto);
    }
}