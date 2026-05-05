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

        public async Task<bool> UpdateDoctorAsync(int id, DoctorUpdateDto updateDto)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return false;

            // FIX: Added YearsOfExperience to the Update mapping
            doctor.Name = updateDto.Name;
            doctor.Specialization = updateDto.Specialization;
            doctor.YearsOfExperience = updateDto.YearsOfExperience;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DoctorReadDto>> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors.ToListAsync();
            
            return doctors.Select(d => new DoctorReadDto
            {
                Id = d.Id,
                Name = d.Name,
                Specialization = d.Specialization,
                YearsOfExperience = d.YearsOfExperience 
            });
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return false;

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DoctorReadDto> CreateDoctorAsync(DoctorCreateDto createDto)
        {
            // FIX: Added YearsOfExperience to the Model creation
            var doctor = new Doctor 
            { 
                Name = createDto.Name, 
                Specialization = createDto.Specialization,
                YearsOfExperience = createDto.YearsOfExperience 
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            // FIX: Added YearsOfExperience to the returned ReadDto
            return new DoctorReadDto 
            { 
                Id = doctor.Id, 
                Name = doctor.Name, 
                Specialization = doctor.Specialization,
                YearsOfExperience = doctor.YearsOfExperience
            };
        }
    }
}