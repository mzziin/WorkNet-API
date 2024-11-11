namespace WorkNet.BLL.DTOs.JobDTOs
{
    public class JobAddDTO
    {
        public int EmployerId { get; set; }

        public int CategoryId { get; set; }

        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public string JobType { get; set; }

        public string JobRole { get; set; }

        public string Location { get; set; }

        public int Openings { get; set; }

        public string? SalaryRange { get; set; }
    }
}
