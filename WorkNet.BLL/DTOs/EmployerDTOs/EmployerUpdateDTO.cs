using System.ComponentModel.DataAnnotations;

namespace WorkNet.BLL.DTOs.EmployerDTOs
{
    public class EmployerUpdateDTO
    {
        public string? CompanyName { get; set; }
        public string? ContactPerson { get; set; }

        [StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public string? Address { get; set; }
        public string? Industry { get; set; }
    }
}
