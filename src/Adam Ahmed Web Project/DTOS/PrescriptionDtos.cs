using System.ComponentModel.DataAnnotations;

namespace Adam_Ahmed_Web_Project.Dtos
{
    public class PrescriptionCreateDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public int MedicationId { get; set; }

        [Required]
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days")]
        public int DurationInDays { get; set; }
    }

    public class PrescriptionReadDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string MedicationName { get; set; } = string.Empty;
        public int DurationInDays { get; set; }
    }
}