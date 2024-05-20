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
			return await _dBContext.walks.ToListAsync();
		}
	}
}
