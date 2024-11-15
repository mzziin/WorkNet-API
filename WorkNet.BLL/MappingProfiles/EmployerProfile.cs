using AutoMapper;
using WorkNet.BLL.DTOs.EmployerDTOs;
using WorkNet.DAL.Models;

namespace WorkNet.BLL.MappingProfiles
{
    public class EmployerProfile : Profile
    {
        public EmployerProfile()
        {
            CreateMap<Employer, OutEmployerDTO>();
            CreateMap<EmployerUpdateDTO, Employer>();
            CreateMap<EmployerRegisterDTO, Employer>();
        }
    }
}
