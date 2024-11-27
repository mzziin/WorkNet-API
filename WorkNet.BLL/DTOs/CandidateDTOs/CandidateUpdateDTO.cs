using System.ComponentModel.DataAnnotations;

namespace WorkNet.BLL.DTOs.CandidateDTOs
{
    public class CandidateUpdateDTO
    {
        [StringLength(100, MinimumLength = 3)]
        public string? FullName { get; set; }

        [Phone]
        public string? ContactNumber { get; set; }

        [StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public string? Address { get; set; }
        public int? Experience { get; set; }
        public string? ResumePath { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
    }
}
