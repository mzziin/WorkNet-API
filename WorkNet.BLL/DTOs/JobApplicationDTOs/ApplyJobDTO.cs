using System.ComponentModel.DataAnnotations;

namespace WorkNet.BLL.DTOs.JobApplicationDTOs
{
    public class ApplyJobDTO
    {
        [Required]
        public int JobId { get; set; }

        [Required]
        public int CandidateId { get; set; }
    }
}
