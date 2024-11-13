using WorkNet.BLL.DTOs.JobApplicationDTOs;

namespace WorkNet.BLL.Services.IServices
{
    public interface IApplicationService
    {
        Task<List<outJobApplicationDTO>> GetJobApplicationsByJobId(int id);
        Task<bool> UpdateApplicationStatus(int applicationId, string status);
    }
}