using WorkNet.BLL.DTOs.CandidateDTOs;
using WorkNet.BLL.Services.IServices;
using WorkNet.DAL.Models;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.BLL.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
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

        public async Task<outCandidateDTO?> GetByCandidateId(int cId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cId);

            var candidate = await _candidateRepository.GetByCandidateId(cId);
            return new outCandidateDTO
            {
                CandidateId = candidate!.CandidateId,
                UserId = candidate.UserId,
                FullName = candidate.FullName,
                Address = candidate.Address,
                ContactNumber = candidate.ContactNumber,
                Experience = candidate.Experience,
                ResumePath = candidate.ResumePath,
            };
        }

        public async Task<outCandidateDTO?> GetCandidate(int uId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(uId);

            var candidate = await _candidateRepository.GetByUserId(uId);
            return new outCandidateDTO
            {
                CandidateId = candidate!.CandidateId,
                UserId = candidate.UserId,
                FullName = candidate.FullName,
                Address = candidate.Address,
                ContactNumber = candidate.ContactNumber,
                Experience = candidate.Experience,
                ResumePath = candidate.ResumePath,
            };
        }

        public async Task<OperationResult> UpdateCandidate(int cId, CandidateUpdateDTO candidateUpdateDTO)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cId);

            var candidate = new Candidate
            {
                CandidateId = cId,
                FullName = candidateUpdateDTO.FullName,
                Address = candidateUpdateDTO.Address,
                ContactNumber = candidateUpdateDTO.ContactNumber,
                Experience = candidateUpdateDTO.Experience,
                ResumePath = candidateUpdateDTO.ResumePath,
            };

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
    }
}
