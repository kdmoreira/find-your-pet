using FindYourPet.Domain.Models;
using FindYourPet.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindYourPet.Domain.Interfaces.Repositories
{
    public interface ISightingRepository
    {
        Task<IEnumerable<Sighting>> GetSightings();
        Task<Sighting> GetSighting(Guid id);
        Task CreateSighting(Sighting sighting);
        Task<bool> UpdateSighting(Sighting sighting);
        Task<(int totalPages, IReadOnlyList<Sighting> readOnlyList)> GetPagedSightings(SightingQuery query, int page, int pageSize);
    }
}
