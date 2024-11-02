using Microsoft.AspNetCore.Mvc;
using WorkNet.BLL.DTOs;
using WorkNet.BLL.Services.IServices;

namespace WorkNetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _authService.Login(loginDTO);
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("employer/register")]
        public async Task<IActionResult> RegisterEmployer(EmployerRegisterDTO employerRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = await _authService.RegisterEmployer(employerRegisterDTO);
            if (status.IsSuccess)
            {
                return Ok(status.Message);
            }
            else
            {
                return BadRequest(status.Message);
            }
        }

        [HttpPost]
        [Route("candidate/register")]
        public async Task<IActionResult> RegisterCandidate(CandidateRegisterDTO candidateRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = await _authService.RegisterCandidate(candidateRegisterDTO);
            if (status.IsSuccess)
            {
                return Ok(status.Message);
            }
            else
            {
                return BadRequest(status.Message);
            }

        }
    }
}
