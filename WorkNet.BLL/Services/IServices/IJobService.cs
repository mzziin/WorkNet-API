using WorkNet.BLL.DTOs.JobDTOs;

namespace WorkNet.BLL.Services.IServices
{
    public interface IJobService
    {
        Task<outJobDTO> GetJob(int jobId);
        Task<List<outJobDTO>> GetAllJobs();
        Task<OperationResult> AddJob(JobAddDTO jobAddDTO);
        Task<OperationResult> DeleteJob(int jobId);
        Task<outJobDTO> UpdateJob(int jobId, JobUpdateDTO jobUpdateDTO);
    }
}
