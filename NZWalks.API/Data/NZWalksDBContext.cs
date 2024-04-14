using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
	public class NZWalksDBContext:DbContext
	{
        public NZWalksDBContext(DbContextOptions dbContextOptions):base(dbContextOptions) 
        {
            
        }

        public DbSet<Difficulty> difficulties { get; set; }
        public DbSet<Region> regions { get; set; }
        public DbSet<Walk> walks { get; set; }

    }
}
