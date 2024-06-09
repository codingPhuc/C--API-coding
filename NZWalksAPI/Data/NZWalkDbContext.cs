using Microsoft.EntityFrameworkCore;

namespace NZWalksAPI.Data
{

    public class NZWalkDbContext : DbContext
    {
        public NZWalkDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { 


        } 

        public DbSet<NZWalksAPI.Models.Domain.Region> Regions { get; set; }
        public DbSet<NZWalksAPI.Models.Domain.Difficulty> Difficulties { get; set; }
        public DbSet<NZWalksAPI.Models.Domain.Walk> Walks { get; set; }


    
    }
}
