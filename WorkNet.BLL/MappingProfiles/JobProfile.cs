using AutoMapper;
using WorkNet.BLL.DTOs.JobDTOs;
using WorkNet.DAL.Models;

namespace WorkNet.BLL.MappingProfiles
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<JobPosting, outJobDTO>();
            CreateMap<JobAddDTO, JobPosting>();
            CreateMap<JobUpdateDTO, JobPosting>()
                .ForAllMembers(dest => dest.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
