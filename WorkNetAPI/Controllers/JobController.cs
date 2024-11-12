using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkNet.BLL.DTOs.JobDTOs;
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
        public async Task<IActionResult> GetAllJobs()
        {
            var response = await _jobService.GetAllJobs();
            return Ok(new { status = true, data = new { Jobs = response } });
        }

        [HttpGet("{jobId}")]
        public async Task<IActionResult> GetJob(int jobId)
        {
            var job = await _jobService.GetJob(jobId);
            return Ok(new { status = true, data = new { Job = job } });
        }

        [HttpPost]
        [Authorize(Policy = "RequireEmployerRole")]
        public async Task<IActionResult> AddJob([FromBody] JobAddDTO jobAddDTO)
        {
            var response = await _jobService.AddJob(jobAddDTO);
            if (response.IsSuccess)
                return Ok(new { status = true, message = "Job added successfully" });
            else
                return BadRequest(new { status = false, message = response.Message });
        }

        [HttpPut("{jobId}")]
        [Authorize(Policy = "RequireEmployerRole")]
        public async Task<IActionResult> EditJob(int jobId, [FromBody] JobUpdateDTO jobUpdateDTO)
        {
            var response = await _jobService.UpdateJob(jobId, jobUpdateDTO);
            if (response == null)
                return BadRequest(new { status = false, message = "Something went wrong" });
            return Ok(new { status = true, data = new { Job = response } });
        }

        [HttpDelete("{jobId}")]
        [Authorize(Policy = "RequireEmployerRole")]
        public async Task<IActionResult> DeleteJob(int jobId)
        {
            var response = await _jobService.DeleteJob(jobId);
            if (response.IsSuccess)
                return Ok(new { status = true, message = "Job Deleted successfully" });
            else
                return BadRequest(new { status = false, message = response.Message });
        }
    }
}
