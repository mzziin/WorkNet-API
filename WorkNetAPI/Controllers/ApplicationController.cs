using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
