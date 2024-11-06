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

        [HttpGet("{cId}")]
        public async Task<IActionResult> GetCandidate(int cId)
        {
            var candidate = await _candidateService.GetCandidate(cId);
            if (candidate == null)
                return NotFound(new { success = false, message = "Candidate not found" });

            return Ok(new { success = true, message = "Candidate retrieved successfully", candidate });
        }

        [HttpPut("{cId}")]
        public async Task<IActionResult> Update(int cId, CandidateUpdateDTO candidateUpdateDTO)
        {
            var status = await _candidateService.UpdateCandidate(cId, candidateUpdateDTO);
            if (status.IsSuccess)
                return Ok(new { success = true, message = status.Message });

            return BadRequest(new { success = false, message = status.Message });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCandidate(int cId)
        {
            var status = await _candidateService.DeleteCandidate(cId);
            if (status.IsSuccess)
                return Ok(new { success = true, message = status.Message });

            return NotFound(new { success = false, message = status.Message });
        }
    }
}
