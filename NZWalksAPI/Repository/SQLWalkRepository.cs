using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repository
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalkDbContext dbContext; 

        public  SQLWalkRepository (NZWalkDbContext dbContext 
            )
        {

        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
             await  dbContext.SaveChangesAsync();    
            await dbContext.SaveChangesAsync();  
            return walk ; 

        }
    }
}
