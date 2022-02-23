using FindYourPet.Domain.Models;
using FindYourPet.Infra.Data.Entities;
using FindYourPet.Infra.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FindYourPet.Infra.Data
{
    public class FindYourPetContext : IFindYourPetContext
    {
        public IMongoCollection<SightingEntity> Sightings { get; }

        public FindYourPetContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Sightings = database.GetCollection<SightingEntity>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }
    }
}
