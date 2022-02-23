using AutoMapper;
using FindYourPet.Domain.Dtos;
using FindYourPet.Domain.Interfaces.Repositories;
using FindYourPet.Domain.Interfaces.Services;
using FindYourPet.Domain.Models;
using FindYourPet.Domain.Queries;
using FindYourPet.Domain.Responses;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
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

        private byte[] ProcessImage(IFormFile file)
        {
            var width = 267;
            var height = 374;

            using var image = Image.Load(file.OpenReadStream());
            image.Mutate(x => x.Resize(width, height));

            using var memoryStream = new MemoryStream();
            var imageEncoder = image.GetConfiguration().ImageFormatsManager.FindEncoder(PngFormat.Instance);
            image.Save(memoryStream, imageEncoder);
            return memoryStream.ToArray();
        }
    }
}
