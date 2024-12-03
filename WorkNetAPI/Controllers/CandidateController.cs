using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkNet.BLL.DTOs.CandidateDTOs;
using WorkNet.BLL.Services.IServices;

namespace WorkNetAPI.Controllers
{
    [Authorize(Policy = "RequireCandidateRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly IAuthService _authService;
        public CandidateController(ICandidateService candidateService, IAuthService authService)
        {
            _candidateService = candidateService;
            _authService = authService;
        }

        [HttpGet("{uId}")]
        public async Task<IActionResult> GetCandidate(int uId)
        {
            if (uId <= 0)
                return BadRequest(new { status = "fail", Message = "Invalid Id" });

            if (_authService.CheckIsAuthorized("UserId", uId))
            {
                var candidate = await _candidateService.GetByUserId(uId);
                if (candidate == null)
                    return NotFound(new { status = "fail", message = "Candidate not found" });

                return Ok(new { status = "success", data = new { candidate = candidate! } });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }

        [HttpPut("{cId}")]
        public async Task<IActionResult> Update(int cId, [FromBody] CandidateUpdateDTO candidateUpdateDTO)
        {
            if (candidateUpdateDTO == null || !ModelState.IsValid)
                return BadRequest(new { status = "fail", Message = "Candidate details are not valid", modelstate = ModelState });

            if (_authService.CheckIsAuthorized("CandidateId", cId))
            {
                var status = await _candidateService.UpdateCandidate(cId, candidateUpdateDTO);
                if (status.IsSuccess)
                    return Ok(new { status = "success", message = status.Message });

                return NotFound(new { status = "fail", message = status.Message });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }

        [HttpDelete("{cId}")]
        public async Task<IActionResult> DeleteCandidate(int cId)
        {
            if (cId <= 0)
                return BadRequest(new { status = "fail", Message = "Invalid Id" });

            if (_authService.CheckIsAuthorized("CandidateId", cId))
            {
                var status = await _candidateService.DeleteCandidate(cId);
                if (status.IsSuccess)
                    return Ok(new { status = "success", message = status.Message });

                return NotFound(new { status = "fail", message = status.Message });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }

        [HttpGet("{cId}/applications")]
        public async Task<IActionResult> GetApplicationsByCandidateId(int cId)
        {
            if (_authService.CheckIsAuthorized("CandidateId", cId))
            {
                var applications = await _candidateService.GetAllJobApplicationsByCandidateId(cId);
                if (applications != null)
                    return Ok(new { status = "success", data = new { JobApplications = applications } });
                else
                    return NotFound(new { status = "fail", message = "Application not found" });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }

        [HttpPost("{cId}/apply/{jobId}")]
        public async Task<IActionResult> ApplyJobApplication(int cId, int jobId)
        {
            if (_authService.CheckIsAuthorized("CandidateId", cId))
            {
                var response = await _candidateService.ApplyJobApplication(cId, jobId);
                if (response.IsSuccess)
                    return Ok(new { status = "success", message = "Applied successfully" });
                else
                    return BadRequest(new { status = "fail", message = response.Message });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }
    }
}
