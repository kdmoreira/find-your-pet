using FindYourPet.Domain.Dtos;
using FindYourPet.Domain.Interfaces.Services;
using FindYourPet.Domain.Queries;
using FindYourPet.Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FindYourPet.API.Controllers
{
    [Route("api/sightings")]
    [ApiController]
    public class SightingController : BaseApiController
    {
        private readonly ISightingService _sightingService;

        public SightingController(ISightingService sightingService)
        {
            _sightingService = sightingService;
        }

        /// <summary>
        /// Lists pet sightings ordered by last seen date.
        /// </summary>
        /// <param name="page">Page number starting from 1.</param>
        /// <param name="pageSize">Number of items to be displayed.</param>
        /// <param name="query">Optional filter options.</param>
        /// <returns>Returns pet sightings.</returns>
        /// <response code="200">Object containing sighting data, item count and total pages.</response>
        /// <response code="500">Internal Server Error.</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(DefaultResponseData<SightingDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DefaultErrorResponse), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetSightings(int page, int pageSize, [FromQuery] SightingQuery query)
        {
            return Ok(await _sightingService.PagedSightings(query, page, pageSize));
        }
    }
}
