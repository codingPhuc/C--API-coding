using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilter;
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

        [HttpGet]
        [Route("{id:Guid}")]
        public async  Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await  walkRepository.GetByIdAsync(id);

            if(walkDomainModel ==null)
            {
                return NotFound(); 
            }

            return Ok(mapper.Map<WalkDTo> (walkDomainModel));
        }

        //  GET :  /api/walks?filterOn = Name?filterQuery=Track  
        [HttpGet]
        public async Task<IActionResult> GetALl([FromQuery] string? sortBy, [FromQuery] bool? isAscending , 
            [FromQuery] string? filterQuery , [FromQuery] string? filterOn ,
            [FromQuery] int pageNumber = 1  , [FromQuery] int pageSize = 1000)
        {

            var walkDomainModels = await walkRepository.GetAllAsync(sortBy, isAscending ?? true , filterQuery , filterOn, pageNumber , pageSize );
            return Ok(mapper.Map<List<WalkDTo>>(walkDomainModels));
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
           
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);


            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel); 
            if(walkDomainModel == null)
            {
                return NotFound();  
            }

            return Ok(mapper.Map<WalkDTo>(walkDomainModel)); 
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async  Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalk = await walkRepository.DeleteAsync(id); 
            if (deletedWalk == null)
            {
                return NotFound(); 
            }

            return Ok(mapper.Map<WalkDTo>(deletedWalk)); 
        }
    }
}
