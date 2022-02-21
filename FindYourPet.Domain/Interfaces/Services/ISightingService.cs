using FindYourPet.Domain.Dtos;
using FindYourPet.Domain.Queries;
using FindYourPet.Domain.Responses;
using System.Threading.Tasks;

namespace FindYourPet.Domain.Interfaces.Services
{
    public interface ISightingService
    {
        Task<DefaultResponseData<SightingDto>> PagedSightings(SightingQuery query, int page, int pageSize);
    }
}
