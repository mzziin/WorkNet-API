using WorkNet.DAL.Models;

namespace WorkNet.DAL.Repositories.IRepositories
{
    public interface IApplicationRepository
    {
        Task<bool> Create(JobApplication application);
        Task<JobApplication?> GetByApplicationId(int applicationId);
        Task<List<JobApplication>> GetAllByJobId(int jobId);
        Task<List<JobApplication>> GetAllByCandidateId(int candidateId);
        Task<JobApplication?> GetByJobIdAndCandidateId(int jobId, int candidateId);
    }
}