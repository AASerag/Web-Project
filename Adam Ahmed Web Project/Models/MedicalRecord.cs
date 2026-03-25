namespace Adam_Ahmed_Web_Project.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public string BloodType { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;

        
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}