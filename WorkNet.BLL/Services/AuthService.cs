using WorkNet.BLL.DTOs;
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
        public AuthService(IUserRepository userRepository, ICandidateRepository candidateRepository, IEmployerRepository employerRepository)
        {
            _userRepository = userRepository;
            _candidateRepository = candidateRepository;
            _employerRepository = employerRepository;

        }

        public async Task<UserDTO?> Login(LoginDTO loginDTO)
        {
            if (loginDTO == null || string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
            {
                //log a argument null exception
                return null;
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

        public async Task<OperationResult> RegisterEmployer(EmployerRegisterDTO employerRegisterDTO)
        {
            if (employerRegisterDTO == null)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Invalid registration details."
                };
            }

            var user = await _userRepository.GetUser(employerRegisterDTO.Email);
            if (user != null)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "User already exists"
                };
            }

            int uId = await _userRepository.AddUser(new User
            {
                Email = employerRegisterDTO.Email,
                Role = employerRegisterDTO.Role,
                PasswordHash = HashingService.HashPassword(employerRegisterDTO.Password)!
            });

            bool status = await _employerRepository.AddEmployer(new Employer
            {
                UserId = uId,
                CompanyName = employerRegisterDTO.CompanyName,
                Address = employerRegisterDTO.Address,
                ContactPerson = employerRegisterDTO.ContactPerson,
                Industry = employerRegisterDTO.Industry
            });
            if (status)
            {
                return new OperationResult
                {
                    IsSuccess = true,
                    Message = "Employer registered successfully"
                };
            }
            else
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Something went wrong while adding to database"
                };
            }
        }

        public async Task<OperationResult> RegisterCandidate(CandidateRegisterDTO candidateRegisterDTO)
        {
            if (candidateRegisterDTO == null)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Invalid registration details."
                };
            }

            var user = await _userRepository.GetUser(candidateRegisterDTO.Email);
            if (user != null)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "User already exists"
                };
            }

            int uId = await _userRepository.AddUser(new User
            {
                Email = candidateRegisterDTO.Email,
                Role = candidateRegisterDTO.Role,
                PasswordHash = HashingService.HashPassword(candidateRegisterDTO.Password)!
            });

            bool status = await _candidateRepository.AddCandidate(new Candidate
            {
                UserId = uId,
                FullName = candidateRegisterDTO.FullName,
                Address = candidateRegisterDTO.Address,
                ContactNumber = candidateRegisterDTO.ContactNumber,
                Experience = candidateRegisterDTO.Experience,
                ResumePath = candidateRegisterDTO.ResumePath,
                Skills = null
            });

            if (status)
            {
                return new OperationResult
                {
                    IsSuccess = true,
                    Message = "Candidate registered successfully"
                };
            }
            else
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Something went wrong while adding to database"
                };
            }
        }
    }
}