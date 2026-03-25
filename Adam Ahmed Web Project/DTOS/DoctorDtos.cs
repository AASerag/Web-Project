using System.ComponentModel.DataAnnotations;

namespace Adam_Ahmed_Web_Project.Dtos
{
    public class DoctorCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Specialization { get; set; } = string.Empty;

        [Range(1, 50, ErrorMessage = "Experience must be between 1 and 50 years")]
        public int YearsOfExperience { get; set; }
    }

    // New: Satisfies the "Create DTOs for Update operations" requirement
    public class DoctorUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Specialization { get; set; } = string.Empty;
    }

    public class DoctorReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
    }
}