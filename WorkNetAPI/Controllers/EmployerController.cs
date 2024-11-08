using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkNet.BLL.DTOs.EmployerDTOs;
using WorkNet.BLL.Services.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkNetAPI.Controllers
{
    [Authorize(Policy = "RequireEmployerRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService _employerService;
        private readonly IAuthService _authService;
        public EmployerController(IEmployerService employerService, IAuthService authService)
        {
            _employerService = employerService;
            _authService = authService;
        }

        [HttpGet("{uId}")]
        public async Task<IActionResult> GetEmployer(int uId)
        {
            if (uId <= 0)
                return BadRequest(new { status = "fail", Message = "Invalid Id" });

            if (_authService.CheckIsAuthorized("UserId", uId))
            {
                var employer = await _employerService.GetByUserId(uId);
                if (employer == null)
                    return NotFound(new { status = "fail", Message = "Employer Not Found" });

                return Ok(new { status = "success", data = new { employer = employer! } });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }


        [HttpPut("{eId}")]
        public async Task<IActionResult> Update(int eId, EmployerUpdateDTO employerUpdateDTO)
        {
            if (employerUpdateDTO == null || !ModelState.IsValid)
                return BadRequest(new { status = "fail", Message = "Employer details are not valid", modelstate = ModelState });

            if (_authService.CheckIsAuthorized("EmployerId", eId))
            {
                var result = await _employerService.UpdateEmployer(eId, employerUpdateDTO);
                if (result.IsSuccess)
                    return Ok(new { status = "success", message = result.Message });

                return BadRequest(new { status = "fail", message = result.Message });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }

        [HttpDelete("{eId}")]
        public async Task<IActionResult> Delete(int eId)
        {
            if (eId <= 0)
                return BadRequest(new { status = "fail", Message = "Invalid Id" });

            if (_authService.CheckIsAuthorized("EmployerId", eId))
            {
                var result = await _employerService.DeleteEmployer(eId);
                if (result.IsSuccess)
                    return Ok(new { status = "success", message = result.Message });

                return NotFound(new { status = "fail", message = result.Message });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }
    }
}
