using Adam_Ahmed_Web_Project.DataBase;
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Adam_Ahmed_Web_Project.Services
{
    public class PatientService : IPatientService
    {
        private readonly AppDbContext _context;

        public PatientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PatientReadDto>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .AsNoTracking()
                .Select(p => new PatientReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Email = p.Email
                })
                .ToListAsync(); 
        }

        
        public async Task<PatientReadDto?> GetPatientByIdAsync(int id)
        {
            return await _context.Patients
                .AsNoTracking()
                .Include(p => p.MedicalRecord) 
                .Where(p => p.Id == id)
                .Select(p => new PatientReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Email = p.Email,
                    BloodType = p.MedicalRecord != null ? p.MedicalRecord.BloodType : "N/A",
                    Allergies = p.MedicalRecord != null ? p.MedicalRecord.Allergies : "None"
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdatePatientAsync(int id, PatientUpdateDto updateDto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;

            patient.Name = updateDto.Name;
            patient.Email = updateDto.Email;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PatientReadDto> CreatePatientAsync(PatientCreateDto createDto)
        {
            var patient = new Patient
            {
                Name = createDto.Name,
                Email = createDto.Email
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return new PatientReadDto
            {
                Id = patient.Id,
                Name = patient.Name,
                Email = patient.Email
            };
        }
    }
}