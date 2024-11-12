using AutoMapper;
using WorkNet.BLL.DTOs.JobApplicationDTOs;
using WorkNet.BLL.Services.IServices;
using WorkNet.DAL.Models;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.BLL.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public ApplicationService(IApplicationRepository applicationRepository, IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }
        public async Task<OperationResult> ApplyJobApplication(ApplyJobDTO applyJobDTO)
        {
            int jobId = applyJobDTO.JobId;
            int candidateId = applyJobDTO.CandidateId;

            if (candidateId <= 0)
                throw new ArgumentException("Invalid candidate ID", nameof(applyJobDTO.CandidateId));

            if (jobId <= 0)
                throw new ArgumentException("Invalid job ID", nameof(applyJobDTO.JobId));

            var job = await _jobRepository.GetJob(jobId);
            if (job == null)
                return new OperationResult { IsSuccess = false, Message = "Job not found" };

            // check wheather candidate already applied or not 
            var response = await _applicationRepository.GetByJobIdAndCandidateId(jobId, candidateId);
            if (response != null)
                return new OperationResult { IsSuccess = false, Message = "You already applied for this job" };

            var jobApplication = new JobApplication
            {
                CandidateId = candidateId,
                JobId = jobId,
                Status = "Applied"
            };

            var status = await _applicationRepository.Create(jobApplication);
            if (!status)
                return new OperationResult { IsSuccess = false, Message = "Failed to submit application" };
            else
                return new OperationResult { IsSuccess = true, Message = "Application submitted successfully" };
        }

        public async Task<List<outJobApplicationDTO>> GetJobApplicationsByCandidateId(int id)
        {
            var response = await _applicationRepository.GetAllByCandidateId(id);
            return _mapper.Map<List<outJobApplicationDTO>>(response);
        }

        public async Task<List<outJobApplicationDTO>> GetJobApplicationsByJobId(int id)
        {
            var response = await _applicationRepository.GetAllByJobId(id);
            return _mapper.Map<List<outJobApplicationDTO>>(response);
        }
    }
}
