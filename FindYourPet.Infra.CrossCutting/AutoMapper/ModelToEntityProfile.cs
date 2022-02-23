using AutoMapper;
using FindYourPet.Domain.Models;
using FindYourPet.Infra.Data.Entities;


namespace FindYourPet.Infra.CrossCutting.AutoMapper
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<Sighting, SightingEntity>();
        }
    }
}
