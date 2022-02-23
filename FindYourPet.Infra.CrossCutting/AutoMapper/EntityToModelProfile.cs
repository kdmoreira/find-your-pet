using AutoMapper;
using FindYourPet.Domain.Models;
using FindYourPet.Infra.Data.Entities;

namespace FindYourPet.Infra.CrossCutting.AutoMapper
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<SightingEntity, Sighting>();
        }
    }
}
