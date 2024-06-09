using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repository
{
    public class SQLRegionRepository : IRegionRepository 
    {
        private readonly   NZWalkDbContext dbContext;
        


        public  SQLRegionRepository(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;  
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await  dbContext.Regions.AddAsync(region); 
            await dbContext.SaveChangesAsync(); 
            return region;  

        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);  

            if (existingRegion == null) {
                return null; 
                  
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion; 

         }

        public async Task<List<Region>> GetAllAsync()
        {
          return await dbContext.Regions.ToListAsync();
        }

        public Task<Region> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public  async Task<Region> UpdateAsync(Guid guid ,  Region region)
        { 
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == guid);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;


            await dbContext.SaveChangesAsync(); 
            return existingRegion; 
        }


        
    }
}
