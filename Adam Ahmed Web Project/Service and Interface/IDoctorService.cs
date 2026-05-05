using Adam_Ahmed_Web_Project.Dtos;

namespace Adam_Ahmed_Web_Project.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorReadDto>> GetAllDoctorsAsync();
        Task<DoctorReadDto> CreateDoctorAsync(DoctorCreateDto createDto);

        Task<bool> UpdateDoctorAsync(int id, DoctorUpdateDto updateDto);
        Task<bool> DeleteDoctorAsync(int id);
    }
}