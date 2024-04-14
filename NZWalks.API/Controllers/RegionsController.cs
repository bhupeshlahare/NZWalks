using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;

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

		//GET ALL REGIONS
		[HttpGet]
        public IActionResult GetAll()
		{
			// Get Data From Database - Domain models
			var regionsDomain = _dbContext.regions.ToList();

			// Map Domain Models to DTOs
			var regionDto = new List<RegionDto>();
			foreach (var regionDomain in regionsDomain)
			{
				regionDto.Add(new RegionDto()
				{
					Id = regionDomain.Id,
					Code = regionDomain.Code,
					Name = regionDomain.Name,
					RegionImageUrl = regionDomain.RegionImageUrl
				});
			}

			//Return DTOs
			return Ok(regionDto);
		}

		//GET SINGLE REGION (Get Region By ID)
		[HttpGet]
		[Route("id")]
		public IActionResult Get(Guid id)
		{
			//Get Region Domain Model From Database
			var regionDomain = _dbContext.regions.FirstOrDefault(u => u.Id == id);
			if (regionDomain == null)
			{
				return NotFound();
			}

			//Map Region Domain Model to Region DTO
			var regionDto = new RegionDto
			{
				Id = regionDomain.Id,
				Code = regionDomain.Code,
				Name = regionDomain.Name,
				RegionImageUrl = regionDomain.RegionImageUrl
			};

			//Return DTO
			return Ok(regionDto);
		}
	}
}
