namespace WorkNet.BLL.DTOs.EmployerDTOs
{
    public class OutEmployerDTO
    {
        public int EmployerId { get; set; }

        public int UserId { get; set; }

        public string CompanyName { get; set; } = null!;

        public string ContactPerson { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Industry { get; set; } = null!;
    }
}
