using WorkNet.BLL.DTOs.CandidateDTOs;
using WorkNet.BLL.DTOs.JobApplicationDTOs;

namespace WorkNet.BLL.Services.IServices
{
    public interface ICandidateService
    {
        Task<outCandidateDTO?> GetByUserId(int uId);
        Task<outCandidateDTO?> GetByCandidateId(int cId);
        Task<OperationResult> UpdateCandidate(int cId, CandidateUpdateDTO candidateUpdateDTO);
        Task<OperationResult> DeleteCandidate(int uId);
        Task<List<outJobApplicationDTO>> GetAllJobApplicationsByCandidateId(int cId);
        Task<OperationResult> ApplyJobApplication(int candidateId, int jobId);
    }
}