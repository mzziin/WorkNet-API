namespace WorkNet.BLL.DTOs.JobDTOs
{
    public class JobAddDTO
    {
        public required int CategoryId { get; set; }
        public required string JobTitle { get; set; }
        public required string JobDescription { get; set; }
        public required string JobType { get; set; }
        public required string JobRole { get; set; }
        public required string Location { get; set; }
        public required int Openings { get; set; }
        public required string? SalaryRange { get; set; }
    }
}
