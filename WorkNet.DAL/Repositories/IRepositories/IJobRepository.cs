using WorkNet.DAL.Models;

namespace WorkNet.DAL.Repositories.IRepositories
{
    public interface IJobRepository
    {
        Task<JobPosting?> GetJob(int jobId);
        Task<List<JobPosting>> GetAllJobs();
        Task<bool> AddJob(JobPosting job);
        Task<bool> DeleteJob(JobPosting job);
        Task<bool> UpdateJob(JobPosting job);
        Task<bool> SubmitJobApplication(JobApplication jobApplication);
        Task<JobApplication?> GetJobApplication(int jobId, int candidateId);
    }
}
