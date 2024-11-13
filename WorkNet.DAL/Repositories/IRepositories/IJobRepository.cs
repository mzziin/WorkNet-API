using WorkNet.DAL.Models;

namespace WorkNet.DAL.Repositories.IRepositories
{
    public interface IJobRepository
    {
        Task<JobPosting?> GetJob(int jobId);
        Task<List<JobPosting>> GetAllJobs(string? jobTitle, string? jobRole, string? jobType, string? location);
        Task<List<JobPosting>> GetAllJobs(int eId);
        Task<bool> AddJob(JobPosting job);
        Task<bool> DeleteJob(JobPosting job);
        Task<bool> UpdateJob(JobPosting job);
        Task<bool> SubmitJobApplication(JobApplication jobApplication);
    }
}
