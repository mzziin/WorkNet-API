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
        private readonly ICandidateService _candidateService;
        private readonly IEmployerService _employerService;
        private readonly JWTService _jwtService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(
            IAuthService authService,
            JWTService jwtService,
            ILogger<AuthController> logger,
            ICandidateService candidateService,
            IEmployerService employerService
            )
        {
            _authService = authService;
            _candidateService = candidateService;
            _employerService = employerService;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.Login(loginDTO);
            if (user == null)
                return Unauthorized(new { status = "fail", Message = "Invalid username or password" });

            int? candidateId = null;
            int? employerId = null;

            // Get CandidateId or EmployerId based on role without modifying UserDTO
            if (user.Role == "Candidate")
            {
                var candidate = await _candidateService.GetByUserId(user.UserId);
                if (candidate == null)
                    return Unauthorized(new { status = "fail", message = "Candidate not found" });

                candidateId = candidate.CandidateId;
            }
            else if (user.Role == "Employer")
            {
                var employer = await _employerService.GetByUserId(user.UserId);
                if (employer == null)
                    return Unauthorized(new { status = "fail", message = "Employer not found" });

                employerId = employer.EmployerId;
            }

            var token = _jwtService.GenerateJwtToken(user, candidateId, employerId);

            _logger.LogInformation(5, "{Email} Logged in successfully", loginDTO.Email);
            return Ok(new { status = "success", data = new { User = user, Token = token } });
        }

        [HttpPost]
        [Route("employer/register")]
        public async Task<IActionResult> RegisterEmployer([FromBody] EmployerRegisterDTO employerRegisterDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var uId = await _authService.RegisterEmployer(employerRegisterDTO);
            if (uId != 0)
                return CreatedAtAction(
                    actionName: nameof(EmployerController.GetEmployer),
                    controllerName: "Employer",
                    routeValues: new { uId = uId },
                    new { status = "success", Message = "Employer registered successfully"! });
            else
                return Conflict(new { status = "fail", Message = "Employer already exists" });
        }

        [HttpPost]
        [Route("candidate/register")]
        public async Task<IActionResult> RegisterCandidate([FromBody] CandidateRegisterDTO candidateRegisterDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var uId = await _authService.RegisterCandidate(candidateRegisterDTO);
            if (uId != 0)
                return CreatedAtAction(
                    actionName: nameof(CandidateController.GetCandidate),
                    controllerName: "Candidate",
                    routeValues: new { uId = uId },
                    new { status = "success", Message = "Candidate registered successfully" });
            else
                return Conflict(new { status = "fail", Message = "Candidate already exists" });
        }
    }
}
