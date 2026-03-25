using Adam_Ahmed_Web_Project.DataBase;
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Adam_Ahmed_Web_Project.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _context;

        public DoctorService(AppDbContext context) => _context = context;

        public async Task<IEnumerable<DoctorReadDto>> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return doctors.Select(d => new DoctorReadDto { Id = d.Id, Name = d.Name, Specialization = d.Specialization });
        }

        public async Task<DoctorReadDto> CreateDoctorAsync(DoctorCreateDto createDto)
        {
            var doctor = new Doctor { Name = createDto.Name, Specialization = createDto.Specialization };
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return new DoctorReadDto { Id = doctor.Id, Name = doctor.Name, Specialization = doctor.Specialization };
        }
    }
}