using FindYourPet.Domain.Models;
using FindYourPet.Infra.Data.Entities;
using MongoDB.Driver;

namespace FindYourPet.Infra.Data.Interfaces
{
    public interface IFindYourPetContext
    {
        IMongoCollection<SightingEntity> Sightings { get; }
    }
}
