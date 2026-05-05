using System.ComponentModel.DataAnnotations;

namespace Adam_Ahmed_Web_Project.Dtos
{
    public class DoctorCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Specialization { get; set; } = string.Empty;

        [Required]
        [Range(1, 50, ErrorMessage = "Experience must be between 1 and 50 years")]
        public int YearsOfExperience { get; set; }
    }

    public class DoctorUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Specialization { get; set; } = string.Empty;

        // Added this so editing a doctor doesn't accidentally wipe their experience data
        [Required]
        [Range(1, 50)]
        public int YearsOfExperience { get; set; }
    }

    public class DoctorReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
    }
}