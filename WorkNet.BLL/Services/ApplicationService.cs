using AutoMapper;
using WorkNet.BLL.DTOs.JobApplicationDTOs;
using WorkNet.BLL.Services.IServices;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.BLL.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;
        public ApplicationService(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }
        public async Task<List<outJobApplicationDTO>> GetJobApplicationsByJobId(int id)
        {
            var response = await _applicationRepository.GetAllByJobId(id);
            return _mapper.Map<List<outJobApplicationDTO>>(response);
        }

        public async Task<bool> UpdateApplicationStatus(int applicationId, string status)
        {
            return await _applicationRepository.UpdateStatus(applicationId, status);
        }
    }
}
