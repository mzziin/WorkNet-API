using Microsoft.EntityFrameworkCore;
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
        public async Task<Candidate?> GetByUserId(int uId)
        {
            if (uId <= 0)
                throw new ArgumentOutOfRangeException(nameof(uId), "User ID must be greater than zero.");

            return await _db.Candidates.FirstOrDefaultAsync(u => u.UserId == uId);
        }
        public async Task<Candidate?> GetByCandidateId(int cId)
        {
            if (cId <= 0)
                throw new ArgumentOutOfRangeException(nameof(cId), "Candidate ID must be greater than zero.");

            return await _db.Candidates.FindAsync(cId);
        }
        public async Task<bool> AddCandidate(Candidate candidate)
        {
            if (candidate == null)
                throw new ArgumentNullException(nameof(candidate), "Candidate cannot be null");

            await _db.Candidates.AddAsync(candidate);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCandidate(Candidate candidate)
        {
            if (candidate == null)
                throw new ArgumentNullException(nameof(candidate), "Candidate cannot be null");

            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Candidates.Remove(candidate);

                    var user = await _db.Users.FindAsync(candidate.UserId);
                    if (user != null)
                        _db.Users.Remove(user);

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception)
                {
                    //log
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<bool> UpdateCandidate(Candidate candidate)
        {
            ArgumentNullException.ThrowIfNull(candidate, nameof(candidate));

            var candidateFromDb = await _db.Candidates.FindAsync(candidate.CandidateId);
            if (candidateFromDb == null)
                return false;

            candidateFromDb.FullName = string.IsNullOrWhiteSpace(candidate.FullName) ? candidateFromDb.FullName : candidate.FullName;
            candidateFromDb.Address = string.IsNullOrWhiteSpace(candidate.Address) ? candidateFromDb.Address : candidate.Address;
            candidateFromDb.ContactNumber = string.IsNullOrWhiteSpace(candidate.ContactNumber) ? candidateFromDb.ContactNumber : candidate.ContactNumber;
            candidateFromDb.Experience = candidate.Experience.HasValue && candidate.Experience > 0 ? candidate.Experience : candidateFromDb.Experience;
            candidateFromDb.ResumePath = string.IsNullOrWhiteSpace(candidate.ResumePath) ? candidateFromDb.ResumePath : candidate.ResumePath;

            return await _db.SaveChangesAsync() > 0;
        }
    }
}
