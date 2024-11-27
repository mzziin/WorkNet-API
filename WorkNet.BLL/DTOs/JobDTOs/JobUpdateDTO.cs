using System.ComponentModel.DataAnnotations;

namespace WorkNet.BLL.DTOs.JobDTOs
{
    public class JobUpdateDTO
    {
        [StringLength(100, MinimumLength = 3)]
        public string? JobTitle { get; set; }

        [StringLength(500, MinimumLength = 50)]
        public string? JobDescription { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string? JobType { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string? JobRole { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string? Location { get; set; }

        public int? Openings { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string? SalaryRange { get; set; }

    }
}
