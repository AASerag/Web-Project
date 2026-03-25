using Adam_Ahmed_Web_Project.DataBase;
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Adam_Ahmed_Web_Project.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly AppDbContext _context;

        public PrescriptionService(AppDbContext context) => _context = context;

        public async Task<IEnumerable<PrescriptionReadDto>> GetAllPrescriptionsAsync()
        {
            // OPTIMIZED: Projecting directly to the DTO
            return await _context.Prescriptions
                .Select(p => new PrescriptionReadDto
                {
                    Id = p.Id,
                    PatientName = p.Patient != null ? p.Patient.Name : "Unknown",
                    MedicationName = p.Medication != null ? p.Medication.Name : "Unknown",
                    DurationInDays = p.DurationInDays
                })
                .ToListAsync();
        }

        public async Task<PrescriptionReadDto> CreatePrescriptionAsync(PrescriptionCreateDto createDto)
        {
            var prescription = new Prescription
            {
                PatientId = createDto.PatientId,
                MedicationId = createDto.MedicationId,
                DurationInDays = createDto.DurationInDays
            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            // Use projection to return the fresh data with names
            return await _context.Prescriptions
                .AsNoTracking()
                .Where(p => p.Id == prescription.Id)
                .Select(p => new PrescriptionReadDto
                {
                    Id = p.Id,
                    PatientName = p.Patient != null ? p.Patient.Name : "N/A",
                    MedicationName = p.Medication != null ? p.Medication.Name : "N/A",
                    DurationInDays = p.DurationInDays
                })
                .FirstAsync();
        }
    }
}