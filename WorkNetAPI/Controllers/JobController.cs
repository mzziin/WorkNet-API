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
        public async Task<IActionResult> GetAllJobs([FromQuery] JobGetAllDTO jobGetAllDTO)
        {
            if (jobGetAllDTO.PageNumber < 1 || jobGetAllDTO.PageSize < 1)
            {
                return BadRequest(new { status = false, message = "Invalid page number or page size." });
            }
            var response = await _jobService.SearchJobs(jobGetAllDTO);
            return Ok(new
            {
                status = true,
                pageNumber = jobGetAllDTO.PageNumber,
                pageSize = jobGetAllDTO.PageSize,
                totalRecords = response.TotalRecords,
                totalPages = (int)Math.Ceiling((double)response.TotalRecords / jobGetAllDTO.PageSize),
                data = new { Jobs = response.Data }
            });
        }

        [HttpGet("{jobId}")]
        public async Task<IActionResult> GetJob(int jobId)
        {
            var job = await _jobService.GetJob(jobId);
            return Ok(new { status = true, data = new { Job = job } });
        }
    }
}
