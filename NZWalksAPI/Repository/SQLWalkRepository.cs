using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repository
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalkDbContext dbContext; 

        public  SQLWalkRepository (NZWalkDbContext dbContext )
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
           await dbContext.Walks.AddAsync( walk );
            await dbContext.SaveChangesAsync();   
            return walk ; 
        }

        public async Task<List<Walk>> GetAllAsync( string? sortBy, bool isAscending , string? filterOn = null, string? filterQuery = null
            ,int pageNumber = 1, int pageSize = 1000)
        {
          
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) ==false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name == filterQuery);
                }
            }
            // Sorting  
            if(string.IsNullOrWhiteSpace(sortBy) ==false)
            {
                if(sortBy.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);  
                }    
                else if(sortBy.Equals("Lenght", StringComparison.OrdinalIgnoreCase) )
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);

                }
            }
            // Pagination 
            var skipResults = (pageNumber - 1) * pageSize;



            return await walks.Skip(skipResults).Take(pageSize).ToListAsync() ;  

        }

     
        public async Task<Walk?> GetByIdAsync(Guid id)
        {
          return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x =>x.Id ==id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null;

            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId; 
            existingWalk.RegionId  = walk.RegionId;

            await dbContext.SaveChangesAsync(); 

            return existingWalk;
        }
        public async  Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id); 

            if (existingWalk == null)
            {
                return null; 
            }
            dbContext.Walks.Remove(existingWalk);

            await dbContext.SaveChangesAsync(); 

            return existingWalk; 
        }

      
    }
}
