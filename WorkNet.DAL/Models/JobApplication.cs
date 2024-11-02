using System;
using System.Collections.Generic;

namespace WorkNet.DAL.Models;

public partial class JobApplication
{
    public int ApplicationId { get; set; }

    public int JobId { get; set; }

    public int CandidateId { get; set; }

    public DateTime? ApplicationDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Candidate Candidate { get; set; } = null!;

    public virtual JobPosting Job { get; set; } = null!;
}
