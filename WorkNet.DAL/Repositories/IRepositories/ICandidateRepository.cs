using WorkNet.DAL.Models;

namespace WorkNet.DAL.Repositories.IRepositories
{
    public interface ICandidateRepository
    {
        Task<bool> AddCandidate(Candidate candidate);
    }
}
