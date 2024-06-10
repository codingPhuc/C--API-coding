using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        // CREATE Walk  
        // POST  :   
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalkController (IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper = mapper; 
            this.walkRepository = walkRepository;    
        }

        [HttpPost] 
        public async   Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel); 

            return Ok(mapper.Map<WalkDTo>(walkDomainModel));

        }
    }
}
