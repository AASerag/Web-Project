using Adam_Ahmed_Web_Project.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adam_Ahmed_Web_Project.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientReadDto>> GetAllPatientsAsync();
        Task<PatientReadDto?> GetPatientByIdAsync(int id);
        Task<PatientReadDto> CreatePatientAsync(PatientCreateDto createDto);

        Task<bool> UpdatePatientAsync(int id, PatientUpdateDto updateDto);
        Task<bool> DeletePatientAsync(int id);
    }
}