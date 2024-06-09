using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repository;
using System.Runtime.InteropServices;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalkDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly Mapper mapper; 

        public RegionController(NZWalkDbContext dbContext , IRegionRepository regionRepository, Mapper  mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {

            var regionDomain = await regionRepository.GetAllAsync();

            //var regionsDTO = new List<RegionDTO>();

            //foreach (var region in regionDomain)
            //{

            //    regionsDTO.Add(new RegionDTO()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl,


            //    }
            //    );
            //}
         
            return Ok(mapper.Map<List<RegionDTO>>(regionDomain));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult>  GetRegion([FromRoute] Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id); 



            if (region == null)
            {
                return NotFound();
            }

            // Map/Convert Region Domain Model to Region DTO 

            return Ok(mapper.Map<RegionDTO>(region));
        }


        [HttpPost]
        public  async Task <IActionResult> Create([FromBody]AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);  

            regionDomainModel =  await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDTO>(regionDomainModel); 
            return CreatedAtAction(nameof(GetRegion) , new {id = regionDomainModel.Id} , regionDto) ; 


        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDTo updateRegionRequestDTo )
        {
            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionRequestDTo.Code,
            //    Name = updateRegionRequestDTo.Name,
            //    RegionImageUrl = updateRegionRequestDTo.RegionImageUrl



            //}; 

            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTo); 

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel); 



            if(regionDomainModel == null)
            {
                return NotFound(); 
            }

          
           /*  await dbContext.SaveChangesAsync();*/

            //var regionDto = new RegionDTO()
            //{
            //    Id = regionDomainModel.Id,

            //    Code = regionDomainModel.Code,

            //    Name = regionDomainModel.Name,

            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //}; 

            

            return Ok(mapper.Map<RegionDTO>(regionDomainModel)); 
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id); 
            if(regionDomainModel == null)
            {
                return NotFound(); 
            }



            //var regionDto = new RegionDTO { 
                
            //    Id= regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //}; 

            return Ok(mapper.Map<RegionDTO>(regionDomainModel));

        }
    }
}
