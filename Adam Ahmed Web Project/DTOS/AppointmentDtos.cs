using System;
using System.ComponentModel.DataAnnotations;

namespace Adam_Ahmed_Web_Project.Dtos
{
    // Used when booking a new appointment
    public class AppointmentCreateDto
    {
        [Required(ErrorMessage = "A valid appointment date is required")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Patient ID is required to book an appointment")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Doctor ID is required to book an appointment")]
        public int DoctorId { get; set; }
    }

    // Used for showing appointment details in the API
    public class AppointmentReadDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        
    }
}