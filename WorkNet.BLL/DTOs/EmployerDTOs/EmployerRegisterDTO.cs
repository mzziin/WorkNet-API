using System.ComponentModel.DataAnnotations;

namespace WorkNet.BLL.DTOs.EmployerDTOs
{
    public class EmployerRegisterDTO
    {
        [Required]
        public required string CompanyName { get; set; }

        [Required]
        public required string ContactPerson { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public required string Address { get; set; }

        [Required]
        public required string Industry { get; set; }


        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public required string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }
    }
}
