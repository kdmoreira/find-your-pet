using AutoMapper;
using FindYourPet.Domain.Dtos;
using FindYourPet.Domain.Models;

namespace FindYourPet.CrossCutting.AutoMapper
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile()
        {
            CreateMap<Sighting, SightingDto>()
                .ForMember(x => x.Sex, opt =>
                opt.MapFrom(x => x.Sex.ToString()))
                .ForMember(x => x.TypeOfPet, opt =>
                opt.MapFrom(x => x.TypeOfPet.ToString()));
        }
    }
}
