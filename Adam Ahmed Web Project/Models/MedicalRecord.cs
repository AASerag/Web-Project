using System;

namespace Adam_Ahmed_Web_Project.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Prescription { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        

        
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        
        public int? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
    }
}