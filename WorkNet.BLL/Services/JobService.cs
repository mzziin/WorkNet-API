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
        public async Task<PaginatedResult<List<outJobDTO>>> SearchJobs(JobGetAllDTO jobGetAllDTO)
        {
            var jobs = await _jobRepository.GetAllJobs(
                jobGetAllDTO.JobTitle,
                jobGetAllDTO.JobRole,
                jobGetAllDTO.JobType,
                jobGetAllDTO.Location,
                jobGetAllDTO.PageNumber,
                jobGetAllDTO.PageSize
                );

            int totalJobApplications = await _jobRepository.GetTotalRecord();

            if (jobs == null || !jobs.Any())
                return new PaginatedResult<List<outJobDTO>>
                {
                    Data = [],
                    TotalRecords = totalJobApplications,
                };

            var outJobList = _mapper.Map<List<outJobDTO>>(jobs);

            return new PaginatedResult<List<outJobDTO>>
            {
                Data = outJobList,
                TotalRecords = totalJobApplications,
            };
        }
        public async Task<List<outJobDTO>> GetAllJobs(int eId)
        {
            var jobs = await _jobRepository.GetAllJobs(eId);

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
        public async Task<OperationResult> AddJob(int eId, JobAddDTO jobAddDTO)
        {
            if (jobAddDTO == null)
                throw new ArgumentNullException(nameof(jobAddDTO), "Job data cannot be null");

            var job = _mapper.Map<JobPosting>(jobAddDTO);
            job.EmployerId = eId;

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
    }
}
