namespace WorkNet.DAL.Models;

public partial class Candidate
{
    public int CandidateId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public string? Address { get; set; }

    public int? Experience { get; set; }

    public string? ResumePath { get; set; }

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
