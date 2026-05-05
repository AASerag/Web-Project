using Adam_Ahmed_Web_Project.Dtos;

namespace Adam_Ahmed_Web_Project.Services
{

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentReadDto>> GetAllAppointmentsAsync();
    Task<AppointmentReadDto> CreateAppointmentAsync(AppointmentCreateDto createDto);
    Task<bool> DeleteAppointmentAsync(int id);
}
}