using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
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
            //Validate the request
            //if (!ValidateAddRegionAsync(addRegionRequest))
            //    return BadRequest(ModelState);


            // Request(DTO) to domain
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,

            };

            //Pass details to repository 
            region = await regionRepository.AddAsync(region);

            //Convert data back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,

            };
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
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
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            //return Ok responce 
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateRegionAsync
            ([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //Validate the incoming request
            if (!ValidateUpdateRegionAsync(updateRegionRequest))
                return BadRequest(ModelState);

            //convert dto to domain model
            var regionDomain = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population,

            };

            //update region using repository
            var updatedRegion = await regionRepository.UpdateASync(id, regionDomain);

            //if null = not found
            if (updatedRegion == null) return NotFound();


            //convert back to dto
            var regionDTO = mapper.Map<Models.DTO.Region>(updatedRegion);

            //return ok 
            return Ok(regionDTO);
        }

        #region Private methods

        private bool ValidateAddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
         {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), $"Add Region Data is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code),
                    $"{nameof(addRegionRequest.Code)} cannot be null or empty or whitespaces");

            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name),
                    $"{nameof(addRegionRequest.Name)} cannot be null or empty or whitespaces");

            }
            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area),
                    $"{nameof(addRegionRequest.Area)} cannot be less or equal to zero.");
            }
            if (addRegionRequest.Lat == 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Lat),
                    $"{nameof(addRegionRequest.Lat)} cannot equal to zero.");
            }
            if (addRegionRequest.Long == 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Long),
                    $"{nameof(addRegionRequest.Long)} cannot equal to zero.");
            }
            if (addRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population),
                    $"{nameof(addRegionRequest.Population)} cannot be less than zero.");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateUpdateRegionAsync(Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest), $"Add Region Data is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code),
                    $"{nameof(updateRegionRequest.Code)} cannot be null or empty or whitespaces");

            }
            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name),
                    $"{nameof(updateRegionRequest.Name)} cannot be null or empty or whitespaces");

            }
            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area),
                    $"{nameof(updateRegionRequest.Area)} cannot be less or equal to zero.");
            }
            if (updateRegionRequest.Lat == 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Lat),
                    $"{nameof(updateRegionRequest.Lat)} cannot equal to zero.");
            }
            if (updateRegionRequest.Long == 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Long),
                    $"{nameof(updateRegionRequest.Long)} cannot equal to zero.");
            }
            if (updateRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population),
                    $"{nameof(updateRegionRequest.Population)} cannot be less than zero.");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
