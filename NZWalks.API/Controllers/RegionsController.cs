using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
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

		// GET ALL REGIONS
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

		// GET SINGLE REGION (Get Region By ID)
		[HttpGet]
		[Route("{id:Guid}")]
		public IActionResult Get([FromRoute] Guid id)
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

		// POST To Create New Region
		[HttpPost]
		public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto) 
		{
			// Map DTO to Domain Model
			var regionDomainModel = new Region
			{
				Code = addRegionRequestDto.Code,
				Name = addRegionRequestDto.Name,
				RegionImageUrl = addRegionRequestDto.RegionImageUrl
			};

			// Use Domain Model to create Region
			_dbContext.regions.Add(regionDomainModel);
			_dbContext.SaveChanges();

			// Map Domain Model back to DTO
			var regionDto = new RegionDto
			{
				Id=regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			return CreatedAtAction(nameof(Get), new { id = regionDto.Id }, regionDto);
		}

		// Update Region
		[HttpPut]
		[Route("{id:Guid}")]
		public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
			// Check if Region exists
			var regionDomainModel = _dbContext.regions.FirstOrDefault(region => region.Id == id);

			if (regionDomainModel == null)
			{
				return NotFound();
			}

			// Map DTO to Domain models
			regionDomainModel.Code = updateRegionRequestDto.Code;
			regionDomainModel.Name = updateRegionRequestDto.Name;
			regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

			_dbContext.SaveChanges();

			// Convert Domain Model to DTO
			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			return Ok(regionDto);
		}

		// Delete Region
		[HttpDelete]
		[Route("{id:Guid}")]
		public IActionResult Delete([FromRoute] Guid id)
		{
			var regionDomainModel = _dbContext.regions.FirstOrDefault(region => region.Id == id);

			if(regionDomainModel == null)
			{
				return NotFound(); 
			}

			// Delete Region
			_dbContext.regions.Remove(regionDomainModel);
			_dbContext.SaveChanges();

			// Return deleted Region back
			// Map Domain Model to DTO

			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			return Ok(regionDto);
		}
	}
}
