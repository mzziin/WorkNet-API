namespace WorkNet.BLL.DTOs.JobDTOs
{
    public class JobUpdateDTO
    {
        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public string JobType { get; set; }

        public string JobRole { get; set; }

        public string Location { get; set; }

        public int Openings { get; set; }

        public string? SalaryRange { get; set; }

    }
}
