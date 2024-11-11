using AutoMapper;
using WorkNet.BLL.DTOs.CandidateDTOs;
using WorkNet.DAL.Models;

namespace WorkNet.BLL.MappingProfiles
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<Candidate, outCandidateDTO>();
            CreateMap<CandidateUpdateDTO, Candidate>();
        }
    }
}
