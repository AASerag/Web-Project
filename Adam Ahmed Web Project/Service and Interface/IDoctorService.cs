using Adam_Ahmed_Web_Project.Dtos;

namespace Adam_Ahmed_Web_Project.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorReadDto>> GetAllDoctorsAsync();
        Task<DoctorReadDto> CreateDoctorAsync(DoctorCreateDto createDto);
    }
}