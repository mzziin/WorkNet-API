using WorkNet.BLL.DTOs.EmployerDTOs;

namespace WorkNet.BLL.Services.IServices
{
    public interface IEmployerService
    {
        Task<OutEmployerDTO?> GetEmployer(int uId);
        Task<OutEmployerDTO?> GetByEmployerId(int eId);
        Task<OperationResult> UpdateEmployer(int eId, EmployerUpdateDTO EmployerUpdateDTO);
        Task<OperationResult> DeleteEmployer(int uId);
    }
}