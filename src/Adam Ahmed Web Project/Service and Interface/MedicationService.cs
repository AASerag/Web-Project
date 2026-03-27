using Adam_Ahmed_Web_Project.DataBase;
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Adam_Ahmed_Web_Project.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly AppDbContext _context;

        public MedicationService(AppDbContext context) => _context = context;

        public async Task<IEnumerable<MedicationReadDto>> GetAllMedicationsAsync()
        {
            var meds = await _context.Medications.ToListAsync();
            return meds.Select(m => new MedicationReadDto
            {
                Id = m.Id,
                Name = m.Name,
                DosageInstructions = m.DosageInstructions
            });
        }

        public async Task<MedicationReadDto> CreateMedicationAsync(MedicationCreateDto createDto)
        {
            var medication = new Medication
            {
                Name = createDto.Name,
                DosageInstructions = createDto.DosageInstructions
            };

            _context.Medications.Add(medication);
            await _context.SaveChangesAsync();

            return new MedicationReadDto
            {
                Id = medication.Id,
                Name = medication.Name,
                DosageInstructions = medication.DosageInstructions
            };
        }
    }
}