using System.Collections.Generic;

namespace Adam_Ahmed_Web_Project.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Navigation for 1:1 relationship
        public MedicalRecord? MedicalRecord { get; set; }

        // Navigation for 1:N relationship
        public List<Appointment> Appointments { get; set; } = new();

        // Navigation for Many-to-Many relationship
        public List<Prescription> Prescriptions { get; set; } = new();
    }
}