using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            //return DTO regions

            var regions = await regionRepository.GetAllAsync();
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region() 
            //    { 
            //         Id=region.Id,
            //         Code= region.Code,
            //         Name=region.Name,
            //         Area=region.Area,
            //         Lat=region.Lat,
            //         Long=region.Long,
            //         Population=region.Population,
            //    };

            //    regionsDTO.Add(regionDTO);
            //});

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName(nameof(GetRegionAsync))]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // Request(DTO) to domain
            var region = new Models.Domain.Region()
            {
                Code= addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat= addRegionRequest.Lat,
                Long= addRegionRequest.Long,
                Population= addRegionRequest.Population,

            };

            //Pass details to repository 

            region =  await regionRepository.AddAsync(region);
            //Convert data back to DTO

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Name= region.Name,
                Code= region.Code,
                Area= region.Area,
                Lat= region.Lat,
                Long= region.Long,
                Population= region.Population,

            };
            return CreatedAtAction(nameof(GetRegionAsync),new { id=regionDTO.Id},regionDTO);

                
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //get the region from db
            var region = await regionRepository.DeleteAsync(id);

            //if null return notfound

            if (region == null)
                return NotFound();

            //convert to DTO model


            //var regionDTO = new Models.DTO.Region()
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name=region.Name,
            //    Area=region.Area,
            //    Lat=region.Lat,
            //    Long=region.Long,
            //    Population=region.Population

            //};
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            //return Ok responce 
            return Ok(regionDTO);
        }
    }
}
