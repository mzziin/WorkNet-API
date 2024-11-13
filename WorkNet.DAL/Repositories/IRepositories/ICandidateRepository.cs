using WorkNet.DAL.Models;

namespace WorkNet.DAL.Repositories.IRepositories
{
    public interface ICandidateRepository
    {
        Task<bool> AddCandidate(Candidate candidate);
        Task<Candidate?> GetByUserId(int uId);
        Task<Candidate?> GetByCandidateId(int cId);
        Task<bool> UpdateCandidate(Candidate candidate);
        Task<bool> DeleteCandidate(Candidate candidate);
        Task<List<Skill>> GetAllSkills();
        Task<List<JobApplication>> GetAllApplications(int candidateId);

    }
}