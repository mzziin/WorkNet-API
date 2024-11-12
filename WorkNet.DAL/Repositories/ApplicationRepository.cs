using Microsoft.EntityFrameworkCore;
using WorkNet.DAL.Data;
using WorkNet.DAL.Models;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.DAL.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly WorkNetDbContext _db;

        public ApplicationRepository(WorkNetDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(JobApplication application)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(application));

            await _db.JobApplications.AddAsync(application);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<List<JobApplication>> GetAllByCandidateId(int candidateId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(candidateId, "candidateId cannot be less than 1");

            return await _db.JobApplications
                .AsNoTracking()
                .Where(j => j.CandidateId == candidateId)
                .ToListAsync();
        }

        public async Task<List<JobApplication>> GetAllByJobId(int jobId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(jobId, "jobId cannot be less than 1");

            return await _db.JobApplications
                .AsNoTracking()
                .Where(j => j.JobId == jobId)
                .ToListAsync();
        }

        public async Task<JobApplication?> GetByApplicationId(int applicationId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(applicationId, "applicationId cannot be less than 1");

            return await _db.JobApplications
                .FindAsync(applicationId);
        }
        public async Task<JobApplication?> GetByJobIdAndCandidateId(int jobId, int candidateId)
        {
            return await _db.JobApplications
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.JobId == jobId && j.CandidateId == candidateId);
        }
    }
}