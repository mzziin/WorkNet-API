using System.ComponentModel.DataAnnotations;

namespace WorkNet.BLL.DTOs.EmployerDTOs
{
    public class EmployerUpdateDTO
    {
        [StringLength(100, MinimumLength = 3)]
        public string? CompanyName { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string? ContactPerson { get; set; }

        [StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public string? Address { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string? Industry { get; set; }
    }
}
