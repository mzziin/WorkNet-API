using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkNet.BLL.DTOs.JobApplicationDTOs;
using WorkNet.BLL.Services.IServices;

namespace WorkNetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        [HttpPost("apply")]
        [Authorize(Policy = "RequireCandidateRole")]
        public async Task<IActionResult> ApplyJobApplication(ApplyJobDTO applyJobDTO)
        {
            var response = await _applicationService.ApplyJobApplication(applyJobDTO);
            if (response.IsSuccess)
                return Ok(new { status = true, message = "Applied successfully" });
            else
                return BadRequest(new { status = false, message = response.Message });
        }

        [HttpGet("job/{jobId}")]
        [Authorize(Policy = "RequireEmployerRole")]
        public async Task<IActionResult> GetApplicationsByJobId(int jobId)
        {
            var applications = await _applicationService.GetJobApplicationsByJobId(jobId);
            if (applications != null)
                return Ok(new { status = true, data = new { JobApplications = applications } });
            else
                return NotFound(new { status = false, message = "Application not found" });
        }

        // GET: api/application/candidate/{candidateId}
        [HttpGet("candidate/{candidateId}")]
        public async Task<IActionResult> GetApplicationsByCandidateId(int candidateId)
        {
            var applications = await _applicationService.GetJobApplicationsByCandidateId(candidateId);
            if (applications != null)
                return Ok(new { status = true, data = new { JobApplications = applications } });
            else
                return NotFound(new { status = false, message = "Application not found" });
        }
    }
}
