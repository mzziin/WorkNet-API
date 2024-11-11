using Microsoft.AspNetCore.Http;
using WorkNet.BLL.DTOs;
using WorkNet.BLL.DTOs.CandidateDTOs;
using WorkNet.BLL.DTOs.EmployerDTOs;
using WorkNet.BLL.SecurityServices;
using WorkNet.BLL.Services.IServices;
using WorkNet.DAL.Models;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IEmployerRepository _employerRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthService(
            IUserRepository userRepository,
            ICandidateRepository candidateRepository,
            IEmployerRepository employerRepository,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _userRepository = userRepository;
            _candidateRepository = candidateRepository;
            _employerRepository = employerRepository;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<UserDTO?> Login(LoginDTO loginDTO)
        {
            if (loginDTO == null || string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
            {
                throw new ArgumentNullException(nameof(loginDTO));
            }

            var user = await _userRepository.GetUser(loginDTO.Email);
            if (user != null)
            {
                bool isPasswordMatch = HashingService.VerifyPassword(loginDTO.Password, user.PasswordHash);
                if (isPasswordMatch)
                    return new UserDTO { UserId = user.UserId, Email = user.Email, Role = user.Role };
            }
            return null;
        }

        public async Task<bool> RegisterEmployer(EmployerRegisterDTO employerRegisterDTO)
        {
            ArgumentNullException.ThrowIfNull(employerRegisterDTO);

            var user = await _userRepository.GetUser(employerRegisterDTO.Email);
            if (user != null)
                return false;

            int uId = await _userRepository.AddUser(new User
            {
                Email = employerRegisterDTO.Email,
                Role = "Employer",
                PasswordHash = HashingService.HashPassword(employerRegisterDTO.Password)!
            });

            return await _employerRepository.AddEmployer(new Employer
            {
                UserId = uId,
                CompanyName = employerRegisterDTO.CompanyName,
                Address = employerRegisterDTO.Address,
                ContactPerson = employerRegisterDTO.ContactPerson,
                Industry = employerRegisterDTO.Industry
            });
        }

        public async Task<bool> RegisterCandidate(CandidateRegisterDTO candidateRegisterDTO)
        {
            ArgumentNullException.ThrowIfNull(candidateRegisterDTO);

            var user = await _userRepository.GetUser(candidateRegisterDTO.Email);
            if (user != null)
                return false;

            int uId = await _userRepository.AddUser(new User
            {
                Email = candidateRegisterDTO.Email,
                Role = "Candidate",
                PasswordHash = HashingService.HashPassword(candidateRegisterDTO.Password)!
            });

            var skillList = new List<Skill>();
            var SkillsFromDb = await _candidateRepository.GetAllSkills();

            foreach (var skill in candidateRegisterDTO.Skills)
            {
                var existingSkill = SkillsFromDb.FirstOrDefault(c => c.SkillName.ToLower() == skill.ToLower());

                if (existingSkill != null)
                {
                    skillList.Add(existingSkill);
                }
                else
                {
                    skillList.Add(new Skill { SkillName = skill.Trim() });
                }
            }

            return await _candidateRepository.AddCandidate(new Candidate
            {
                UserId = uId,
                FullName = candidateRegisterDTO.FullName,
                Address = candidateRegisterDTO.Address,
                ContactNumber = candidateRegisterDTO.ContactNumber,
                Experience = candidateRegisterDTO.Experience,
                ResumePath = candidateRegisterDTO.ResumePath,
                Skills = skillList
            });
        }

        public bool CheckIsAuthorized(string claim, int id)
        {
            var claimValue = _contextAccessor.HttpContext.User.FindFirst(claim)?.Value;
            if (id != Convert.ToInt32(claimValue))
                return false;
            return true;
        }
    }
}