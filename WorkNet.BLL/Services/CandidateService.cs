using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using WorkNet.BLL.DTOs.CandidateDTOs;
using WorkNet.BLL.DTOs.JobApplicationDTOs;
using WorkNet.BLL.Services.IServices;
using WorkNet.DAL.Models;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.BLL.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;
        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper, IJobRepository jobRepository, IApplicationRepository applicationRepository)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
            _jobRepository = jobRepository;
            _applicationRepository = applicationRepository;
        }

        public async Task<outCandidateDTO?> GetByCandidateId(int cId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cId);

            var candidate = await _candidateRepository.GetByCandidateId(cId);

            if (candidate == null)
                return null;

            var skills = candidate.Skills?.Select(skill => skill.SkillName).ToList() ?? new List<string>();

            var outCandidate = _mapper.Map<outCandidateDTO>(candidate);
            outCandidate.Skills = skills;

            return outCandidate;
        }

        public async Task<outCandidateDTO?> GetByUserId(int uId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(uId);

            var candidate = await _candidateRepository.GetByUserId(uId);

            if (candidate == null)
                return null;

            var skills = candidate.Skills?.Select(skill => skill.SkillName).ToList() ?? new List<string>();

            var outCandidate = _mapper.Map<outCandidateDTO>(candidate);
            outCandidate.Skills = skills;

            return outCandidate;
        }

        public async Task<OperationResult> UpdateCandidate(int cId, CandidateUpdateDTO candidateUpdateDTO)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cId);

            var skills = new List<Skill>();

            if (!candidateUpdateDTO.Skills.IsNullOrEmpty())
            {
                foreach (var name in candidateUpdateDTO.Skills)
                {
                    skills.Add(new Skill { SkillName = name });
                }
            }
            var candidate = _mapper.Map<Candidate>(candidateUpdateDTO);
            candidate.Skills = skills;
            candidate.CandidateId = cId;

            var status = await _candidateRepository.UpdateCandidate(candidate);

            if (status == false)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Candidate not found to update"
                };
            }
            return new OperationResult
            {
                IsSuccess = true,
                Message = "Candidate updated successfully"
            };
        }
        public async Task<OperationResult> DeleteCandidate(int cId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cId);

            var candidate = await _candidateRepository.GetByCandidateId(cId);
            if (candidate == null)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Candidate not found"
                };
            }
            await _candidateRepository.DeleteCandidate(candidate);
            return new OperationResult
            {
                IsSuccess = true,
                Message = "Candidate removed successfully"
            };
        }

        public async Task<List<outJobApplicationDTO>> GetAllJobApplicationsByCandidateId(int cId)
        {
            var response = await _candidateRepository.GetAllApplications(cId);
            return _mapper.Map<List<outJobApplicationDTO>>(response);
        }

        public async Task<OperationResult> ApplyJobApplication(int candidateId, int jobId)
        {
            if (candidateId <= 0)
                throw new ArgumentException("Invalid candidate ID", nameof(candidateId));

            if (jobId <= 0)
                throw new ArgumentException("Invalid job ID", nameof(jobId));

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
    }
}
