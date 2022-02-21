using AutoMapper;
using FindYourPet.Domain.Dtos;
using FindYourPet.Domain.Interfaces.Repositories;
using FindYourPet.Domain.Interfaces.Services;
using FindYourPet.Domain.Queries;
using FindYourPet.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindYourPet.Service.Services
{
    public class SightingService : ISightingService
    {
        private readonly ISightingRepository _sightingRepository;
        private readonly IMapper _mapper;
        public SightingService(ISightingRepository sightingRepository, IMapper mapper)
        {
            _sightingRepository = sightingRepository;
            _mapper = mapper;
        }

        public async Task<DefaultResponseData<SightingDto>> PagedSightings(SightingQuery query, int page, int pageSize)
        {
            var (totalPages, readOnlyList) = await _sightingRepository.GetPagedSightings(query, page, pageSize);

            return new DefaultResponseData<SightingDto>()
            {
                TotalPages = totalPages,
                Data = _mapper.Map<List<SightingDto>>(readOnlyList),
                Count = readOnlyList.Count
            };
        }
    }
}
