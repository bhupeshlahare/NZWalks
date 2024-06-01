using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Collections.Generic;

namespace NZWalks.API.Repositories
{
	public class SQLWalkRepository : IWalkRepository
	{
		private readonly NZWalksDBContext _dBContext;

		public SQLWalkRepository(NZWalksDBContext dBContext)
        {
			_dBContext = dBContext;
		}
        public async Task<Walk> CreateAsync(Walk walk)
		{
			await _dBContext.walks.AddAsync(walk);
			await _dBContext.SaveChangesAsync();
			return walk;
		}

		public async Task<List<Walk>> GetAllAsync()
		{
			return await _dBContext.walks.Include("Difficulty").Include("Region").ToListAsync();
		}

		public async Task<Walk?> DeleteAsync(Guid id)
		{
			var existingWalk = _dBContext.walks.FirstOrDefault(x => x.Id == id);
			if(existingWalk == null)
			{
				return null;
			}
			_dBContext.Remove(existingWalk);
			await _dBContext.SaveChangesAsync();
			return existingWalk;
		}

		public async Task<Walk?> GetByIdAsync(Guid id)
		{
			return await _dBContext.walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
		{
			
			var existingWalk = await _dBContext.walks.FirstOrDefaultAsync(u => u.Id == id);
            if (existingWalk == null)
            {
				return null;
            }

			existingWalk.Name = walk.Name;
			existingWalk.Description = walk.Description;
			existingWalk.LengthInKm = walk.LengthInKm;
			existingWalk.WalkImageUrl = walk.WalkImageUrl;
			existingWalk.DifficultyId = walk.DifficultyId;
			existingWalk.RegionId = walk.RegionId;
			await _dBContext.SaveChangesAsync();
			return existingWalk;
        }
	}
}
