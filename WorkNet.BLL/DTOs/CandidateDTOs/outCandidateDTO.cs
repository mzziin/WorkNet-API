using WorkNet.DAL.Models;

namespace WorkNet.BLL.DTOs.CandidateDTOs
{
    public class outCandidateDTO
    {
        public int CandidateId { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string ContactNumber { get; set; } = null!;

        public string? Address { get; set; }

        public int? Experience { get; set; }

        public string? ResumePath { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
    }
}
