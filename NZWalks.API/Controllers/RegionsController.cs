using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDBContext _dbContext;
        public RegionsController(NZWalksDBContext dBContext)
        {
            _dbContext = dBContext;
        }

		[HttpGet]
        public IActionResult GetAll()
		{
			var regions = _dbContext.regions.ToList();
			return Ok(regions);
		}

		[HttpGet]
		[Route("id")]
		public IActionResult Get(Guid id)
		{
			var region = _dbContext.regions.FirstOrDefault(u => u.Id == id);
			if (region == null)
			{
				return NotFound();
			}
			return Ok(region);
		}
	}
}
