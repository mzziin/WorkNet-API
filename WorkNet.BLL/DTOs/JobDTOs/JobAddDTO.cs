using System.ComponentModel.DataAnnotations;

namespace WorkNet.BLL.DTOs.JobDTOs
{
    public class JobAddDTO
    {
        [Required]
        public required int CategoryId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required string JobTitle { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public required string JobDescription { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string JobType { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required string JobRole { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required string Location { get; set; }

        [Required]
        public required int Openings { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string? SalaryRange { get; set; }
    }
}
