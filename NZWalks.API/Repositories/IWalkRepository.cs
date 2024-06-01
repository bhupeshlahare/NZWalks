using NZWalks.API.Models.Domain;
using System.Collections.Generic;

namespace NZWalks.API.Repositories
{
	public interface IWalkRepository
	{
		Task<Walk> CreateAsync(Walk walk);
		Task<List<Walk>> GetAllAsync();
		Task<Walk?> GetByIdAsync(Guid id);
	}
}
