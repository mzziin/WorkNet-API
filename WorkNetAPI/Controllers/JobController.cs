using Microsoft.AspNetCore.Mvc;
using WorkNet.BLL.Services.IServices;

namespace WorkNetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobs(
            [FromQuery] string? jobTitle = null,
            [FromQuery] string? jobRole = null,
            [FromQuery] string? jobType = null,
            [FromQuery] string? location = null
            )
        {
            var response = await _jobService.SearchJobs(jobTitle, jobRole, jobType, location);
            return Ok(new { status = true, data = new { Jobs = response } });
        }

        [HttpGet("{jobId}")]
        public async Task<IActionResult> GetJob(int jobId)
        {
            var job = await _jobService.GetJob(jobId);
            return Ok(new { status = true, data = new { Job = job } });
        }


    }
}
