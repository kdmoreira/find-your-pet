using FindYourPet.Domain.Models;
using MongoDB.Driver;

namespace FindYourPet.Infra.Data.Interfaces
{
    public interface IFindYourPetContext
    {
        IMongoCollection<Sighting> Sightings { get; }
    }
}
