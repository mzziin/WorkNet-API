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
        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet("{uId}")]
        public async Task<IActionResult> GetCandidate(int uId)
        {
            if (uId <= 0)
                return BadRequest(new { status = "fail", Message = "Invalid Id" });

            var userId = User.FindFirst("UserId")?.Value;
            if (uId != Convert.ToInt32(userId))
                return Unauthorized(new { status = "fail", message = "You are not authorized" });

            var candidate = await _candidateService.GetByUserId(uId);
            if (candidate == null)
                return NotFound(new { status = "fail", message = "Candidate not found" });

            return Ok(new { status = "success", data = new { candidate = candidate! } });
        }

        [HttpPut("{cId}")]
        public async Task<IActionResult> Update(int cId, CandidateUpdateDTO candidateUpdateDTO)
        {
            if (candidateUpdateDTO == null || !ModelState.IsValid)
                return BadRequest(new { status = "fail", Message = "Candidate details are not valid", modelstate = ModelState });

            var candidateId = User.FindFirst("CandidateId")?.Value;
            if (cId != Convert.ToInt32(candidateId))
                return Unauthorized(new { status = "fail", message = "You are not authorized" });

            var status = await _candidateService.UpdateCandidate(cId, candidateUpdateDTO);
            if (status.IsSuccess)
                return Ok(new { status = "success", message = status.Message });

            return NotFound(new { status = "fail", message = status.Message });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCandidate(int cId)
        {
            if (cId <= 0)
                return BadRequest(new { status = "fail", Message = "Invalid Id" });

            var candidateId = User.FindFirst("CandidateId")?.Value;
            if (cId != Convert.ToInt32(candidateId))
                return Unauthorized(new { status = "fail", message = "You are not authorized" });

            var status = await _candidateService.DeleteCandidate(cId);
            if (status.IsSuccess)
                return Ok(new { status = "success", message = status.Message });

            return NotFound(new { status = "fail", message = status.Message });
        }
    }
}
