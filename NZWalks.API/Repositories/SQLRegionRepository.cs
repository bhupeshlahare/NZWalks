using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
	public class SQLRegionRepository : IRegionRepository
	{
		private readonly NZWalksDBContext dBContext;

		public SQLRegionRepository(NZWalksDBContext dBContext)
        {
			this.dBContext = dBContext;
		}

        public async Task<List<Region>> GetAllAsync()
		{
			return await dBContext.regions.ToListAsync();
		}
	}
}
