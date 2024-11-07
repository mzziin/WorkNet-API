using System.ComponentModel.DataAnnotations;
using WorkNet.DAL.Models;

namespace WorkNet.BLL.DTOs.CandidateDTOs
{
    public class CandidateRegisterDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required string FullName { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public required string ContactNumber { get; set; }

        [StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public string? Address { get; set; }
        public int Experience { get; set; }
        public string? ResumePath { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }
        public List<Skill> SkillNames { get; set; } = new List<Skill>();

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
