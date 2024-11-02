using System;
using System.Collections.Generic;

namespace WorkNet.DAL.Models;

public partial class JobCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<JobPosting> JobPostings { get; set; } = new List<JobPosting>();
}
