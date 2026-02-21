using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            //region table mappings
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            //walk table mappings
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<WalkDto, Walk>().ReverseMap();
            CreateMap<UpdateWalksRequestDto, Walk>().ReverseMap();
            CreateMap<DifficultyDto, Difficulty>().ReverseMap();
        }
    }
}
