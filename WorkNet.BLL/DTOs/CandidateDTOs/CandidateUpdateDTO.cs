using System.ComponentModel.DataAnnotations;
using WorkNet.DAL.Models;

namespace WorkNet.BLL.DTOs.CandidateDTOs
{
    public class CandidateUpdateDTO
    {
        public string? FullName { get; set; }

        [Phone]
        public string? ContactNumber { get; set; }

        [StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public string? Address { get; set; }
        public int? Experience { get; set; }
        public string? ResumePath { get; set; }
        public List<Skill>? Skills { get; set; }
    }
}
