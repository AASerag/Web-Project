using System.ComponentModel.DataAnnotations;

namespace Adam_Ahmed_Web_Project.Dtos
{
    public class MedicationCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string DosageInstructions { get; set; } = string.Empty; // Added this!

        [Range(1, 1000)]
        public int DosageMg { get; set; }
    }

    public class MedicationReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DosageInstructions { get; set; } = string.Empty; // Added this!
    }
}