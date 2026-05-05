using Adam_Ahmed_Web_Project.DataBase;
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Adam_Ahmed_Web_Project.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;

        // This constructor fixes the "red" _context error
        public AppointmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppointmentReadDto>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                .Select(a => new AppointmentReadDto
                {
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    Reason = a.Reason
                })
                .ToListAsync();
        }

        public async Task<AppointmentReadDto> CreateAppointmentAsync(AppointmentCreateDto dto)
        {
            var appointment = new Appointment
            {
                AppointmentDate = dto.AppointmentDate,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                Reason = dto.Reason
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return new AppointmentReadDto 
            { 
                Id = appointment.Id, 
                AppointmentDate = appointment.AppointmentDate,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                Reason = appointment.Reason 
            };
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}