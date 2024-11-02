using System.ComponentModel.DataAnnotations;

namespace WorkNet.BLL.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Not a correct email format")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 32 characters long.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
