using WorkNet.BLL.DTOs.EmployerDTOs;
using WorkNet.BLL.Services.IServices;
using WorkNet.DAL.Models;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.BLL.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository _employerRepository;
        public EmployerService(IEmployerRepository employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public async Task<OutEmployerDTO?> GetEmployer(int uId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(uId);

            var user = await _employerRepository.GetByUserId(uId);
            if (user == null)
                return null;

            return new OutEmployerDTO
            {
                EmployerId = user.EmployerId,
                CompanyName = user.CompanyName,
                Address = user.Address,
                Industry = user.Industry,
                ContactPerson = user.ContactPerson,
            };
        }
        public async Task<OutEmployerDTO?> GetByEmployerId(int eId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(eId);

            var user = await _employerRepository.GetByEmployerId(eId);
            if (user == null)
                return null;

            return new OutEmployerDTO
            {
                EmployerId = user.EmployerId,
                UserId = user.UserId,
                CompanyName = user.CompanyName,
                Address = user.Address,
                Industry = user.Industry,
                ContactPerson = user.ContactPerson,
            };
        }

        public async Task<OperationResult> UpdateEmployer(int eId, EmployerUpdateDTO employerUpdateDTO)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(eId);

            var employer = new Employer
            {
                CompanyName = employerUpdateDTO.CompanyName,
                Address = employerUpdateDTO.Address,
                ContactPerson = employerUpdateDTO.ContactPerson,
                Industry = employerUpdateDTO.Industry,
                EmployerId = eId,
            };
            var result = await _employerRepository.UpdateEmployer(employer);
            if (result == false)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Something went wrong while updating"
                };
            }
            return new OperationResult
            {
                IsSuccess = true,
                Message = "Employer updated successfully"
            };
        }
        public async Task<OperationResult> DeleteEmployer(int eId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(eId);

            var emp = await _employerRepository.GetByEmployerId(eId);
            if (emp == null)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Employer not found"
                };
            }

            await _employerRepository.DeleteEmployer(emp);
            return new OperationResult
            {
                IsSuccess = true,
                Message = "Employer deleted successfully"
            };
        }
    }
}
