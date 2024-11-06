using Microsoft.AspNetCore.Mvc;
using WorkNet.BLL.DTOs;
using WorkNet.BLL.DTOs.CandidateDTOs;
using WorkNet.BLL.DTOs.EmployerDTOs;
using WorkNet.BLL.SecurityServices;
using WorkNet.BLL.Services.IServices;

namespace WorkNetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JWTService _jwtService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, JWTService jwtService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.Login(loginDTO);
            if (user == null)
                return Unauthorized(new { Success = false, Message = "Invalid username or password" });

            var token = _jwtService.GenerateJwtToken(user);

            _logger.LogInformation(5, "{Email} Logged in successfully", loginDTO.Email);
            return Ok(new { Success = true, Message = "Login successfull", User = user, Token = token });
        }

        [HttpPost]
        [Route("employer/register")]
        public async Task<IActionResult> RegisterEmployer(EmployerRegisterDTO employerRegisterDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var status = await _authService.RegisterEmployer(employerRegisterDTO);
            if (status)
                return Ok(new { Success = true, Message = "Employer registered successfully"! });
            else
                return BadRequest(new { Success = false, Message = "Employer already exists" });
        }

        [HttpPost]
        [Route("candidate/register")]
        public async Task<IActionResult> RegisterCandidate(CandidateRegisterDTO candidateRegisterDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var status = await _authService.RegisterCandidate(candidateRegisterDTO);

            if (status)
                return Ok(new { Success = true, Message = "Candidate registered successfully"! });
            else
                return BadRequest(new { Success = false, Message = "Candidate already exists" });
        }
    }
}
