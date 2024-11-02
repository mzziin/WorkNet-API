namespace WorkNet.DAL.Models;

public partial class Employer
{
    public int EmployerId { get; set; }

    public int UserId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string ContactPerson { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Industry { get; set; } = null!;

    public virtual ICollection<JobPosting> JobPostings { get; set; } = new List<JobPosting>();

    public virtual User User { get; set; } = null!;
}
