namespace Adam_Ahmed_Web_Project.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int MedicationId { get; set; }
        public Medication? Medication { get; set; }

        
        public int DurationInDays { get; set; }
    }
}