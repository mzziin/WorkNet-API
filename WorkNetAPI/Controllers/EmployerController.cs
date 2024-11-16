using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkNet.BLL.DTOs.EmployerDTOs;
using WorkNet.BLL.DTOs.JobDTOs;
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
        private readonly IJobService _jobService;
        private readonly IApplicationService _applicationService;
        public EmployerController(
            IEmployerService employerService,
            IAuthService authService,
            IJobService jobService,
            IApplicationService applicationService
            )
        {
            _employerService = employerService;
            _authService = authService;
            _jobService = jobService;
            _applicationService = applicationService;
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
        public async Task<IActionResult> UpdateEmployer(int eId, [FromBody] EmployerUpdateDTO employerUpdateDTO)
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
        public async Task<IActionResult> DeleteEmployer(int eId)
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

        [HttpPost("{eId}/jobs/")]
        public async Task<IActionResult> AddJob(int eId, [FromBody] JobAddDTO jobAddDTO)
        {
            if (eId <= 0)
                return BadRequest(new { status = "fail", Message = "Invalid Id" });

            if (jobAddDTO == null || !ModelState.IsValid)
                return BadRequest(new { status = "fail", Message = "Job details are not valid", modelstate = ModelState });

            if (_authService.CheckIsAuthorized("EmployerId", eId))
            {
                var response = await _jobService.AddJob(eId, jobAddDTO);
                if (response.IsSuccess)
                    return Ok(new { status = true, message = "Job added successfully" });
                else
                    return BadRequest(new { status = false, message = response.Message });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }

        [HttpPut("{eId}/jobs/{jobId}")]
        public async Task<IActionResult> UpdateJob(int eId, int jobId, [FromBody] JobUpdateDTO jobUpdateDTO)
        {
            if (eId <= 0)
                return BadRequest(new { status = "fail", Message = "Invalid employer Id" });
            if (jobId <= 0)
                return BadRequest(new { status = "fail", Message = "Invalid job Id" });

            if (jobUpdateDTO == null || !ModelState.IsValid)
                return BadRequest(new { status = "fail", Message = "Employer details are not valid", modelstate = ModelState });

            if (_authService.CheckIsAuthorized("EmployerId", eId))
            {
                var response = await _jobService.UpdateJob(jobId, jobUpdateDTO);
                if (response == null)
                    return BadRequest(new { status = false, message = "Something went wrong" });
                return Ok(new { status = true, data = new { Job = response } });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }

        [HttpGet("{eId}/jobs")]
        public async Task<IActionResult> GetAllJobsByEmployer(int eId)
        {
            if (_authService.CheckIsAuthorized("EmployerId", eId))
            {
                var response = await _jobService.GetAllJobs(eId);
                return Ok(new { status = true, data = new { Jobs = response } });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }

        [HttpGet("{eId}/jobs/{jobId}/applications")]
        public async Task<IActionResult> GetAllApplicationsOfJob(int eId, int jobId)
        {
            if (_authService.CheckIsAuthorized("EmployerId", eId))
            {
                var applications = await _applicationService.GetJobApplicationsByJobId(jobId);
                if (applications != null)
                    return Ok(new { status = true, data = new { JobApplications = applications } });
                else
                    return NotFound(new { status = false, message = "No application found" });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }

        [HttpPut("{eId}/jobs/{jobId}/applications/{applicationId}/status")]
        public async Task<IActionResult> UpdateApplicationStatus(int eId, int jobId, int applicationId, [FromBody] string status)
        {
            var validStatuses = new[] { "pending", "approved", "rejected" };
            if (!validStatuses.Contains(status.ToLower()))
                return BadRequest(new { status = false, message = "status should be 'pending', 'approved' or 'rejected'." });

            //todo check weather the application belong to this job


            if (_authService.CheckIsAuthorized("EmployerId", eId))
            {
                var result = await _applicationService.UpdateApplicationStatus(applicationId, status);
                if (result)
                    return Ok(new { status = true, message = "application status updated successfully" });
                return BadRequest(new { status = false, message = "something went wrong" });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }

        [HttpDelete("{eId}/jobs/{jobId}")]
        public async Task<IActionResult> DeleteJob(int eId, int jobId)
        {
            if (_authService.CheckIsAuthorized("EmployerId", eId))
            {
                var response = await _jobService.DeleteJob(jobId);
                if (response.IsSuccess)
                    return Ok(new { status = true, message = "Job Deleted successfully" });
                else
                    return BadRequest(new { status = false, message = response.Message });
            }
            return Unauthorized(new { status = "fail", message = "You are not authorized" });
        }
    }
}
