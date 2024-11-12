using AutoMapper;
using WorkNet.BLL.DTOs.JobApplicationDTOs;
using WorkNet.DAL.Models;

namespace WorkNet.BLL.MappingProfiles
{
    internal class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<JobApplication, outJobApplicationDTO>();
        }
    }
}
