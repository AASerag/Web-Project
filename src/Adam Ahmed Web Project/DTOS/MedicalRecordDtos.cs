namespace Adam_Ahmed_Web_Project.Dtos
{
    public class MedicalRecordCreateDto
    {
        public string BloodType { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;
        public int PatientId { get; set; } 
    }

    public class MedicalRecordReadDto
    {
        public int Id { get; set; }
        public string BloodType { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;
        public int PatientId { get; set; }
    }
}