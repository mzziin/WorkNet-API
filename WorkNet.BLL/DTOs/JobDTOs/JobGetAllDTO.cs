namespace WorkNet.BLL.DTOs.JobDTOs
{
    public class JobGetAllDTO
    {
        public string? JobTitle { get; set; }
        public string? JobRole { get; set; }
        public string? JobType { get; set; }
        public string? Location { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
