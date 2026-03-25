using System.Collections.Generic;

namespace Adam_Ahmed_Web_Project.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;

       
        public List<Appointment> Appointments { get; set; } = new();
    }
}