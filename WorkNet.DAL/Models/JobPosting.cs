using System;
using System.Collections.Generic;

namespace WorkNet.DAL.Models;

public partial class JobPosting
{
    public int JobId { get; set; }

    public int EmployerId { get; set; }

    public int CategoryId { get; set; }

    public string JobTitle { get; set; } = null!;

    public string JobDescription { get; set; } = null!;

    public string JobType { get; set; } = null!;

    public string JobRole { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int Openings { get; set; }

    public string? SalaryRange { get; set; }

    public DateOnly PostedDate { get; set; }

    public virtual JobCategory Category { get; set; } = null!;

    public virtual Employer Employer { get; set; } = null!;

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}
