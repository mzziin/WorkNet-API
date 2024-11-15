using AutoMapper;
using WorkNet.BLL.DTOs.EmployerDTOs;
using WorkNet.BLL.Services.IServices;
using WorkNet.DAL.Models;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.BLL.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly IMapper _mapper;
        public EmployerService(IEmployerRepository employerRepository, IMapper mapper)
        {
            _employerRepository = employerRepository;
            _mapper = mapper;
        }

        public async Task<OutEmployerDTO?> GetByUserId(int uId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(uId);

            var user = await _employerRepository.GetByUserId(uId);
            if (user == null)
                return null;

            return _mapper.Map<OutEmployerDTO>(user);
        }
        public async Task<OutEmployerDTO?> GetByEmployerId(int eId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(eId);

            var user = await _employerRepository.GetByEmployerId(eId);
            if (user == null)
                return null;

            return _mapper.Map<OutEmployerDTO>(user);
        }

        public async Task<OperationResult> UpdateEmployer(int eId, EmployerUpdateDTO employerUpdateDTO)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(eId);

            var employer = _mapper.Map<Employer>(employerUpdateDTO);
            employer.EmployerId = eId;

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
