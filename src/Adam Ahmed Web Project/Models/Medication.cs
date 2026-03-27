using System.Collections.Generic;

namespace Adam_Ahmed_Web_Project.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DosageInstructions { get; set; } = string.Empty;

        
        public List<Prescription> Prescriptions { get; set; } = new();
    }
}