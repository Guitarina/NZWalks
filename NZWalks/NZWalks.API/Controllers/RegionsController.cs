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

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            //return DTO regions

            var regions = regionRepository.GetAll();
            var regionsDTO = new List<Models.DTO.Region>();
            regions.ToList().ForEach(region =>
            {
                var regionDTO = new Models.DTO.Region() 
                { 
                    Id=region.Id,
                     Code= region.Code,
                     Name=region.Name,
                     Area=region.Area,
                     Lat=region.Lat,
                     Long=region.Long,
                     Population=region.Population,
                };

                regionsDTO.Add(regionDTO);
            });
            return Ok(regionsDTO);
        }
    }
}
