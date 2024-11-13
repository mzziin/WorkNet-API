using WorkNet.DAL.Models;

namespace WorkNet.DAL.Repositories.IRepositories
{
    public interface IApplicationRepository
    {
        Task<bool> Create(JobApplication application);
        Task<List<JobApplication>> GetAllByJobId(int jobId);
        Task<JobApplication?> GetByApplicationId(int applicationId);
        //Task<List<JobApplication>> GetAllByCandidateId(int candidateId);
        Task<JobApplication?> GetByJobIdAndCandidateId(int jobId, int candidateId);
        Task<bool> UpdateStatus(int applicationId, string status);
    }
}