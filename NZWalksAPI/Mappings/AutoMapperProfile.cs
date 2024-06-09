using AutoMapper;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Mappings
{
    public class AutoMapperProfile :Profile
    {
        public  AutoMapperProfile() { 
            
               CreateMap<Region, RegionDTO>().ReverseMap(); 
               CreateMap<AddRegionRequestDto , Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTo, Region>().ReverseMap();



        }
    }
}
