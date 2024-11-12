using WorkNet.BLL.DTOs.JobApplicationDTOs;

namespace WorkNet.BLL.Services.IServices
{
    public interface IApplicationService
    {
        Task<List<outJobApplicationDTO>> GetJobApplicationsByJobId(int id);
        Task<List<outJobApplicationDTO>> GetJobApplicationsByCandidateId(int id);
        Task<OperationResult> ApplyJobApplication(ApplyJobDTO applyJobDTO);
    }
}