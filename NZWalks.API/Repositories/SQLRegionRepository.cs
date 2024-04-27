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

		public async Task<Region> CreateAsync(Region region)
		{
			await dBContext.regions.AddAsync(region);
			await dBContext.SaveChangesAsync();
			return region;
		}

		public async Task<Region?> DeleteAsync(Guid id)
		{
			var exixtingRegion = await dBContext.regions.FirstOrDefaultAsync(u => u.Id == id);
			if (exixtingRegion == null)
			{
				return null;
			}
			dBContext.Remove(exixtingRegion);
			await dBContext.SaveChangesAsync();
			return exixtingRegion;
		}

		public async Task<List<Region>> GetAllAsync()
		{
			return await dBContext.regions.ToListAsync();
		}

		public async Task<Region?> GetByIdAsync(Guid id)
		{
			return await dBContext.regions.FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task<Region?> UpdateAsync(Guid id, Region region)
		{
			var existingRegion = await dBContext.regions.FirstOrDefaultAsync(u => u.Id == id);
			if (existingRegion == null)
			{
				return null;
			}

			existingRegion.Code = region.Code;
			existingRegion.Name = region.Name;
			existingRegion.RegionImageUrl = region.RegionImageUrl;

			await dBContext.SaveChangesAsync();
			return existingRegion;
		}
	}
}
