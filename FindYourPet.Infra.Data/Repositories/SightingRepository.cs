using FindYourPet.Domain.Interfaces.Repositories;
using FindYourPet.Domain.Models;
using FindYourPet.Domain.Queries;
using FindYourPet.Infra.Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindYourPet.Infra.Data.Repositories
{
    public class SightingRepository : ISightingRepository
    {
        private readonly IFindYourPetContext _context;

        public SightingRepository(IFindYourPetContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateSighting(Sighting sighting)
        {
            await _context.Sightings.InsertOneAsync(sighting);
        }

        public async Task<Sighting> GetSighting(Guid id)
        {
            FilterDefinition<Sighting> filter = Builders<Sighting>
                .Filter.Eq(x => x.Id, id);

            return await _context.Sightings
                .Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Sighting>> GetSightings()
        {
            return await _context.Sightings
                .Find(x => true).ToListAsync();
        }

        public async Task<(int totalPages, IReadOnlyList<Sighting> readOnlyList)> GetPagedSightings
            (SightingQuery query, int page, int pageSize)
        {
            var countFacet = AggregateFacet.Create("count",
                PipelineDefinition<Sighting, AggregateCountResult>.Create(new[]
                {
                    PipelineStageDefinitionBuilder.Count<Sighting>()
                }));

            var dataFacet = AggregateFacet.Create("data",
                PipelineDefinition<Sighting, Sighting>.Create(new[]
                {
                    PipelineStageDefinitionBuilder.Sort(Builders<Sighting>.Sort.Descending(x => x.Date)),
                    PipelineStageDefinitionBuilder.Skip<Sighting>((page - 1) * pageSize),
                    PipelineStageDefinitionBuilder.Limit<Sighting>(pageSize),
                }));

            var municipalityFilter = query.Municipality == null ?
                Builders<Sighting>.Filter.Empty : Builders<Sighting>.Filter.Eq(x => x.Municipality, query.Municipality);
            var zipCodeFilter = query.ZipCode == null ?
                Builders<Sighting>.Filter.Empty : Builders<Sighting>.Filter.Eq(x => x.ZipCode, query.ZipCode);
            var typeOfPetFilter = query.TypeOfPet == 0 ?
                Builders<Sighting>.Filter.Empty : Builders<Sighting>.Filter.Eq(x => x.TypeOfPet, query.TypeOfPet);

            var combineFilters = Builders<Sighting>.Filter.And(municipalityFilter, zipCodeFilter, typeOfPetFilter);

            var aggregation = await _context.Sightings.Aggregate()
                .Match(combineFilters)
                .Facet(countFacet, dataFacet)
                .ToListAsync();

            var count = aggregation.First()
                .Facets.First(x => x.Name == "count")
                .Output<AggregateCountResult>()
                ?.FirstOrDefault()
                ?.Count ?? 0;

            var totalPages = (int)Math.Ceiling((double)count / pageSize);

            var data = aggregation.First()
                .Facets.First(x => x.Name == "data")
                .Output<Sighting>();

            return (totalPages, data);
        }

        public async Task<bool> UpdateSighting(Sighting sighting)
        {
            ReplaceOneResult updateResult =
                await _context.Sightings
                .ReplaceOneAsync(filter: x => x.Id == sighting.Id,
                replacement: sighting);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
    }
}
