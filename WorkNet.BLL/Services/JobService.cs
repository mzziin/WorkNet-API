using AutoMapper;
using WorkNet.BLL.DTOs.JobDTOs;
using WorkNet.BLL.Services.IServices;
using WorkNet.DAL.Models;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.BLL.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public JobService(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }
        public async Task<List<outJobDTO>> GetAllJobs()
        {
            var jobs = await _jobRepository.GetAllJobs();

            if (jobs == null || !jobs.Any())
                return new List<outJobDTO>();

            return _mapper.Map<List<outJobDTO>>(jobs);
        }

        public async Task<outJobDTO> GetJob(int jobId)
        {
            if (jobId <= 0)
                throw new ArgumentException("Invalid job ID", nameof(jobId));

            var job = await _jobRepository.GetJob(jobId);
            if (job == null)
                throw new KeyNotFoundException($"Job not found");

            return _mapper.Map<outJobDTO>(job);
        }
        public async Task<OperationResult> AddJob(JobAddDTO jobAddDTO)
        {
            if (jobAddDTO == null)
                throw new ArgumentNullException(nameof(jobAddDTO), "Job data cannot be null");

            var job = _mapper.Map<JobPosting>(jobAddDTO);

            var response = await _jobRepository.AddJob(job);

            if (response)
                return new OperationResult { IsSuccess = true, Message = "Job posted successfully" };
            else
                return new OperationResult { IsSuccess = false, Message = "Something went wrong" };
        }

        public async Task<OperationResult> DeleteJob(int jobId)
        {
            if (jobId <= 0)
                throw new ArgumentException("Invalid job ID", nameof(jobId));


            var job = await _jobRepository.GetJob(jobId);
            if (job == null)
                return new OperationResult { IsSuccess = false, Message = "Job not found" };

            var status = await _jobRepository.DeleteJob(job);
            if (status)
                return new OperationResult { IsSuccess = true, Message = "Job deleted successfully" };

            return new OperationResult { IsSuccess = false, Message = "Something went wrong" };
        }
        public async Task<outJobDTO> UpdateJob(int jobId, JobUpdateDTO jobUpdateDTO)
        {
            if (jobUpdateDTO == null)
                throw new ArgumentNullException(nameof(jobUpdateDTO), "Job update data cannot be null");

            if (jobId <= 0)
                throw new ArgumentException("Invalid job ID", nameof(jobId));

            var job = await _jobRepository.GetJob(jobId);
            if (job == null)
                throw new KeyNotFoundException("Job not found");

            _mapper.Map(jobUpdateDTO, job);

            var status = await _jobRepository.UpdateJob(job);
            if (!status)
                throw new InvalidOperationException("Failed to update job");

            return _mapper.Map<outJobDTO>(job);
        }


        public async Task<OperationResult> SubmitJobApplication(int jobId, int candidateId)
        {
            if (candidateId <= 0)
                throw new ArgumentException("Invalid candidate ID", nameof(candidateId));

            if (jobId <= 0)
                throw new ArgumentException("Invalid job ID", nameof(jobId));

            var job = await _jobRepository.GetJob(jobId);
            if (job == null)
                return new OperationResult { IsSuccess = false, Message = "Job not found" };

            // check wheather candidate already applied or not 
            var response = await _jobRepository.GetJobApplication(jobId, candidateId);
            if (response != null)
                return new OperationResult { IsSuccess = false, Message = "You already applied for this job" };

            var jobApplication = new JobApplication
            {
                CandidateId = candidateId,
                JobId = jobId,
                Status = "Applied"
            };

            var status = await _jobRepository.SubmitJobApplication(jobApplication);
            if (!status)
                return new OperationResult { IsSuccess = false, Message = "Failed to submit application" };
            else
                return new OperationResult { IsSuccess = true, Message = "Application submitted successfully" };
        }
    }
}
