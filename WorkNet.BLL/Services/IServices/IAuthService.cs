using WorkNet.BLL.DTOs;
using WorkNet.BLL.DTOs.CandidateDTOs;
using WorkNet.BLL.DTOs.EmployerDTOs;

namespace WorkNet.BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<UserDTO?> Login(LoginDTO loginDTO);
        Task<bool> RegisterEmployer(EmployerRegisterDTO employerRegisterDTO);
        Task<bool> RegisterCandidate(CandidateRegisterDTO candidateRegisterDTO);
        bool CheckIsAuthorized(string claim, int id);
    }
}