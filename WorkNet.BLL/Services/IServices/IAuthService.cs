using WorkNet.BLL.DTOs;
using WorkNet.BLL.DTOs.CandidateDTOs;
using WorkNet.BLL.DTOs.EmployerDTOs;

namespace WorkNet.BLL.Services.IServices
{
    public interface IAuthService
    {
        public Task<UserDTO?> Login(LoginDTO loginDTO);
        public Task<bool> RegisterEmployer(EmployerRegisterDTO employerRegisterDTO);
        public Task<bool> RegisterCandidate(CandidateRegisterDTO candidateRegisterDTO);
    }
}