using Adam_Ahmed_Web_Project.DataBase; // Using your project's database namespace
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Adam_Ahmed_Web_Project.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly AppDbContext _context;

        public MedicalRecordService(AppDbContext context)
        {
            _context = context;
        }

        // 1. Get All Records with Names
        public async Task<IEnumerable<MedicalRecordReadDto>> GetAllRecordsAsync()
        {
            return await _context.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Select(m => new MedicalRecordReadDto
                {
                    Id = m.Id,
                    Diagnosis = m.Diagnosis,
                    Prescription = m.Prescription,
                    Notes = m.Notes,
                    DateAdded = m.DateAdded,
                    PatientId = m.PatientId,
                    PatientName = m.Patient != null ? m.Patient.Name : "Unknown",
                    DoctorId = m.DoctorId,
                    DoctorName = m.Doctor != null ? m.Doctor.Name : "Unknown",
                    AppointmentId = m.AppointmentId
                })
                .ToListAsync();
        }

        // 2. Get Records for a Specific Patient
        public async Task<IEnumerable<MedicalRecordReadDto>> GetRecordsByPatientIdAsync(int patientId)
        {
            return await _context.MedicalRecords
                .Where(m => m.PatientId == patientId)
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Select(m => new MedicalRecordReadDto
                {
                    Id = m.Id,
                    Diagnosis = m.Diagnosis,
                    Prescription = m.Prescription,
                    Notes = m.Notes,
                    DateAdded = m.DateAdded,
                    PatientId = m.PatientId,
                    PatientName = m.Patient != null ? m.Patient.Name : "Unknown",
                    DoctorId = m.DoctorId,
                    DoctorName = m.Doctor != null ? m.Doctor.Name : "Unknown",
                    AppointmentId = m.AppointmentId
                })
                .ToListAsync();
        }

        // 3. Create a New Record
        public async Task<MedicalRecordReadDto> CreateRecordAsync(MedicalRecordCreateDto dto)
        {
            var record = new MedicalRecord
            {
                Diagnosis = dto.Diagnosis,
                Prescription = dto.Prescription,
                Notes = dto.Notes,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentId = dto.AppointmentId,
                DateAdded = DateTime.UtcNow 
            };

            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync();

            // Fetch again to get the Patient and Doctor names for the return object
            var savedRecord = await _context.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .FirstAsync(m => m.Id == record.Id);

            return new MedicalRecordReadDto
            {
                Id = savedRecord.Id,
                Diagnosis = savedRecord.Diagnosis,
                Prescription = savedRecord.Prescription,
                Notes = savedRecord.Notes,
                DateAdded = savedRecord.DateAdded,
                PatientId = savedRecord.PatientId,
                PatientName = savedRecord.Patient?.Name ?? "Unknown",
                DoctorId = savedRecord.DoctorId,
                DoctorName = savedRecord.Doctor?.Name ?? "Unknown",
                AppointmentId = savedRecord.AppointmentId
            };
        }

        // 4. Update an Existing Record (FIX FOR EDIT BUTTON)
        public async Task<bool> UpdateRecordAsync(int id, MedicalRecordCreateDto dto)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record == null) return false;

            record.Diagnosis = dto.Diagnosis;
            record.Prescription = dto.Prescription;
            record.Notes = dto.Notes;
            record.DoctorId = dto.DoctorId; 
            record.AppointmentId = dto.AppointmentId;

            await _context.SaveChangesAsync();
            return true;
        }

        // 5. Delete a Record
        public async Task<bool> DeleteRecordAsync(int id)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record == null) return false;

            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}