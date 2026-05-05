using Adam_Ahmed_Web_Project.Dtos;

namespace Adam_Ahmed_Web_Project.Services
{
    public interface IMedicalRecordService
    {
        // Fetches all records with Patient and Doctor names
        Task<IEnumerable<MedicalRecordReadDto>> GetAllRecordsAsync();
        
        // Fetches records specifically for one patient
        Task<IEnumerable<MedicalRecordReadDto>> GetRecordsByPatientIdAsync(int patientId);
        
        // Supports the 'Add Record' form in React
        Task<MedicalRecordReadDto> CreateRecordAsync(MedicalRecordCreateDto createDto);
        
        // NEW: Required for the 'Edit' button to function
        Task<bool> UpdateRecordAsync(int id, MedicalRecordCreateDto updateDto);
        
        // Supports the 'Delete' icon in your table
        Task<bool> DeleteRecordAsync(int id);
    }
}