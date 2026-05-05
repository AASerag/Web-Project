using System;
using System.ComponentModel.DataAnnotations;

namespace Adam_Ahmed_Web_Project.Dtos
{
    // Used when booking a new appointment
    public class AppointmentCreateDto
{
    [Required]
    public DateTime AppointmentDate { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    // Add this to match your Model!
    public string Reason { get; set; } = string.Empty;
}

public class AppointmentReadDto
{
    public int Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public string Reason { get; set; } = string.Empty;
    
    
    public string PatientName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
}
}