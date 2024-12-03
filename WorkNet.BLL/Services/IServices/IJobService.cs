using WorkNet.BLL.DTOs.JobDTOs;

namespace WorkNet.BLL.Services.IServices
{
    public interface IJobService
    {
        Task<outJobDTO> GetJob(int jobId);
        Task<PaginatedResult<List<outJobDTO>>> SearchJobs(JobGetAllDTO jobGetAllDTO);
        Task<List<outJobDTO>> GetAllJobs(int eId);
        Task<OperationResult> AddJob(int eId, JobAddDTO jobAddDTO);
        Task<OperationResult> DeleteJob(int jobId);
        Task<outJobDTO> UpdateJob(int jobId, JobUpdateDTO jobUpdateDTO);
    }
}
