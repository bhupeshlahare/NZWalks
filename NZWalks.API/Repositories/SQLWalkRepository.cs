using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

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
	}
}
