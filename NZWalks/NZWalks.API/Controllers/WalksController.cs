using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllWalksAsync()
        {
            //fetch data from database - domain walks
            var walks = await walkRepository.GetAllAsync();

            //convert domain into dto
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);
            //return responce

            return Ok(walksDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            //Get walk domain from database
            var walk = await walkRepository.GetAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            //convert to DTO
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            //return responce
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(Models.DTO.AddWalkRequest)
        {
            /////////////////////123
        }
    }
}
