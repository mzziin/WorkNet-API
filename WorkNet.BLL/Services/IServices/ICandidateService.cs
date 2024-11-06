using WorkNet.BLL.DTOs.CandidateDTOs;

namespace WorkNet.BLL.Services.IServices
{
    public interface ICandidateService
    {
        Task<outCandidateDTO?> GetCandidate(int uId);
        Task<outCandidateDTO?> GetByCandidateId(int cId);
        Task<OperationResult> UpdateCandidate(int cId, CandidateUpdateDTO candidateUpdateDTO);
        Task<OperationResult> DeleteCandidate(int uId);
    }
}