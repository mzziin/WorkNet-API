﻿using Microsoft.EntityFrameworkCore;
using WorkNet.DAL.Data;
using WorkNet.DAL.Models;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.DAL.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly WorkNetDbContext _db;
        public JobRepository(WorkNetDbContext workNetDbContext)
        {
            _db = workNetDbContext;
        }
        public async Task<bool> AddJob(JobPosting job)
        {
            ArgumentNullException.ThrowIfNull(nameof(job));

            await _db.JobPostings.AddAsync(job);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteJob(JobPosting job)
        {
            ArgumentNullException.ThrowIfNull(nameof(job));

            _db.JobPostings.Remove(job);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<List<JobPosting>> GetAllJobs()
        {
            return await _db.JobPostings.ToListAsync();
        }

        public async Task<JobPosting?> GetJob(int jobId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(jobId);
            return await _db.JobPostings.FindAsync(jobId);
        }

        public async Task<bool> UpdateJob(JobPosting job)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(job));

            var jobFromDb = await _db.JobPostings.FindAsync(job.JobId);
            if (jobFromDb == null)
                return false;

            jobFromDb.JobTitle = string.IsNullOrWhiteSpace(job.JobTitle) ? jobFromDb.JobTitle : job.JobTitle;
            jobFromDb.JobDescription = string.IsNullOrWhiteSpace(job.JobDescription) ? jobFromDb.JobDescription : job.JobDescription;
            jobFromDb.Location = string.IsNullOrWhiteSpace(job.Location) ? jobFromDb.Location : job.Location;
            jobFromDb.JobRole = string.IsNullOrWhiteSpace(job.JobRole) ? jobFromDb.JobRole : job.JobRole;
            jobFromDb.Openings = job.Openings > 0 ? job.Openings : jobFromDb.Openings;
            jobFromDb.JobType = string.IsNullOrWhiteSpace(job.JobType) ? jobFromDb.JobType : job.JobType;
            jobFromDb.SalaryRange = string.IsNullOrWhiteSpace(job.SalaryRange) ? jobFromDb.SalaryRange : job.SalaryRange;

            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<JobApplication?> GetJobApplication(int jobId, int candidateId)
        {
            return await _db.JobApplications.FirstOrDefaultAsync(j => j.JobId == jobId && j.CandidateId == candidateId);
        }

        public async Task<bool> SubmitJobApplication(JobApplication jobApplication)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(jobApplication));

            await _db.JobApplications.AddAsync(jobApplication);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}