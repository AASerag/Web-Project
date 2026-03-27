using System.ComponentModel.DataAnnotations;

namespace Adam_Ahmed_Web_Project.Dtos
{
   
    public class PatientCreateDto
    {
        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Valid email is required")]
        public string Email { get; set; } = string.Empty;
    }

 
    public class PatientUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    
    public class PatientReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        
        public string BloodType { get; set; } = string.Empty;

        
        public string Allergies { get; set; } = string.Empty;
    }
}