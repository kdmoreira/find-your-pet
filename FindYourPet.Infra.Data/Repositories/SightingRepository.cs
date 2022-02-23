using AutoMapper;
using FindYourPet.Domain.Interfaces.Repositories;
using FindYourPet.Domain.Models;
using FindYourPet.Domain.Queries;
using FindYourPet.Infra.Data.Entities;
using FindYourPet.Infra.Data.Interfaces;
using MongoDB.Bson;
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
        private readonly IMapper _mapper;

        public SightingRepository(IFindYourPetContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateSighting(Sighting sighting)
        {
            var entity = _mapper.Map<SightingEntity>(sighting);
            await _context.Sightings.InsertOneAsync(entity);            
        }

        public async Task<Sighting> GetSighting(Guid id)
        {
            FilterDefinition<SightingEntity> filter = Builders<SightingEntity>
                .Filter.Eq("_id", new ObjectId(id.ToString()));

            var entity = await _context.Sightings
                .Find(filter).FirstOrDefaultAsync();

            return _mapper.Map<Sighting>(entity);
        }

        public async Task<IEnumerable<Sighting>> GetSightings()
        {
            return _mapper.Map<List<Sighting>>(await _context.Sightings
                .Find(x => true).ToListAsync());
        }

        public async Task<(int totalPages, IReadOnlyList<Sighting> readOnlyList)> GetPagedSightings
            (SightingQuery query, int page, int pageSize)
        {
            var countFacet = AggregateFacet.Create("count",
                PipelineDefinition<SightingEntity, AggregateCountResult>.Create(new[]
                {
                    PipelineStageDefinitionBuilder.Count<SightingEntity>()
                }));

            var dataFacet = AggregateFacet.Create("data",
                PipelineDefinition<SightingEntity, SightingEntity>.Create(new[]
                {
                    PipelineStageDefinitionBuilder.Sort(Builders<SightingEntity>.Sort.Descending(x => x.Date)),
                    PipelineStageDefinitionBuilder.Skip<SightingEntity>((page - 1) * pageSize),
                    PipelineStageDefinitionBuilder.Limit<SightingEntity>(pageSize),
                }));

            var municipalityFilter = query.Municipality == null ?
                Builders<SightingEntity>.Filter.Empty : Builders<SightingEntity>.Filter.Eq(x => x.Municipality, query.Municipality);
            var zipCodeFilter = query.ZipCode == null ?
                Builders<SightingEntity>.Filter.Empty : Builders<SightingEntity>.Filter.Eq(x => x.ZipCode, query.ZipCode);
            var typeOfPetFilter = query.TypeOfPet == 0 ?
                Builders<SightingEntity>.Filter.Empty : Builders<SightingEntity>.Filter.Eq(x => x.TypeOfPet, query.TypeOfPet);

            var combineFilters = Builders<SightingEntity>.Filter.And(municipalityFilter, zipCodeFilter, typeOfPetFilter);

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
                .Output<SightingEntity>();

            return (totalPages, _mapper.Map<List<Sighting>>(data));
        }

        public async Task<bool> UpdateSighting(Sighting sighting)
        {
            var entity = _mapper.Map<SightingEntity>(sighting);
            ReplaceOneResult updateResult =
                await _context.Sightings
                .ReplaceOneAsync(filter: x => x.Id == entity.Id,
                replacement: entity);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
    }
}
