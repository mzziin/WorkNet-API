using WorkNet.DAL.Models;

namespace WorkNet.DAL.Repositories.IRepositories
{
    public interface IJobRepository
    {
        Task<JobPosting?> GetJob(int jobId);
        Task<List<JobPosting>> GetAllJobs(string? jobTitle, string? jobRole, string? jobType, string? location, int pageNo, int pageSize);
        Task<List<JobPosting>> GetAllJobs(int eId);
        Task<bool> AddJob(JobPosting job);
        Task<bool> DeleteJob(JobPosting job);
        Task<bool> UpdateJob(JobPosting job);
        Task<bool> SubmitJobApplication(JobApplication jobApplication);
        Task<int> GetTotalRecord();
    }
}
