using WorkNet.DAL.Data;
using WorkNet.DAL.Models;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.DAL.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly WorkNetDbContext _db;
        public CandidateRepository(WorkNetDbContext workNetDbContext)
        {
            _db = workNetDbContext;
        }
        public async Task<bool> AddCandidate(Candidate candidate)
        {
            try
            {
                await _db.Candidates.AddAsync(candidate);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // log 
                // throw new Exception(ex.Message, ex);
                return false;
            }
        }
    }
}
