using WorkNet.BLL.DTOs;
using WorkNet.BLL.DTOs.CandidateDTOs;
using WorkNet.BLL.DTOs.EmployerDTOs;

namespace WorkNet.BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<UserDTO?> Login(LoginDTO loginDTO);
        Task<int> RegisterEmployer(EmployerRegisterDTO employerRegisterDTO);
        Task<int> RegisterCandidate(CandidateRegisterDTO candidateRegisterDTO);
        bool CheckIsAuthorized(string claim, int id);
    }
}