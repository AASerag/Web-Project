using System;
using System.ComponentModel.DataAnnotations;

namespace Adam_Ahmed_Web_Project.Dtos
{
    public class MedicalRecordCreateDto
    {
        [Required(ErrorMessage = "Diagnosis is required")]
        public string Diagnosis { get; set; } = string.Empty;
        
        public string Prescription { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        [Required]
        public int PatientId { get; set; }
        
        [Required]
        public int DoctorId { get; set; }
        
        public int? AppointmentId { get; set; }
    }

    public class MedicalRecordReadDto
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Prescription { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
        
        public int PatientId { get; set; }
        // NEW: Property to hold the patient's name for the UI
        public string PatientName { get; set; } = string.Empty;

        public int DoctorId { get; set; }
        // NEW: Property to hold the doctor's name for the UI
        public string DoctorName { get; set; } = string.Empty;

        public int? AppointmentId { get; set; }
    }
}