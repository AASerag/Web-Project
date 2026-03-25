using System;

namespace Adam_Ahmed_Web_Project.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        // Link to Patient
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}