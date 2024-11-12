namespace WorkNet.BLL.DTOs.JobApplicationDTOs
{
    public class outJobApplicationDTO
    {
        public int ApplicationId { get; set; }

        public int JobId { get; set; }

        public int CandidateId { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public string Status { get; set; }
    }
}
