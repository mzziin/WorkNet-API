using WorkNet.BLL.DTOs;

namespace WorkNet.BLL.Services.IServices
{
    public interface IAuthService
    {
        public Task<UserDTO?> Login(LoginDTO loginDTO);
        public Task<OperationResult> RegisterEmployer(EmployerRegisterDTO employerRegisterDTO);
        public Task<OperationResult> RegisterCandidate(CandidateRegisterDTO candidateRegisterDTO);
    }
}