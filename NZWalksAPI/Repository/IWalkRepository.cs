using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? sortBy, bool isAscending, string? filterOn = null ,string? filterQuery  = null
            ,int pageNumber =1 , int pageSize = 1000); 



        Task<Walk> GetByIdAsync(Guid id);

        Task<Walk> UpdateAsync(Guid id, Walk walk);


        Task<Walk> DeleteAsync(Guid id);
    }
}
