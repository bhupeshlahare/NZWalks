using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDBContext _dbContext;
		private readonly IRegionRepository _regionRepository;
		private readonly IMapper _mapper;

		public RegionsController(NZWalksDBContext dBContext, IRegionRepository regionRepository, IMapper mapper)
        {
            _dbContext = dBContext;
			_regionRepository = regionRepository;
			_mapper = mapper;
		}

		// GET ALL REGIONS
		[HttpGet]
        public async Task<IActionResult> GetAll()
		{
			// Get Data From Database - Domain models
			//var regionsDomain = await _dbContext.regions.ToListAsync();

			// Get data from region repository
			var regionsDomain = await _regionRepository.GetAllAsync();

			// Map Domain Models to DTOs
			//var regionDto = new List<RegionDto>();
			//foreach (var regionDomain in regionsDomain)
			//{
			//	regionDto.Add(new RegionDto()
			//	{
			//		Id = regionDomain.Id,
			//		Code = regionDomain.Code,
			//		Name = regionDomain.Name,
			//		RegionImageUrl = regionDomain.RegionImageUrl
			//	});
			//}

			//Auto Mapper - Map domain model to Dto
			var regionDto = _mapper.Map<List<RegionDto>>(regionsDomain);

			//Return DTOs
			return Ok(regionDto);
		}

		// GET SINGLE REGION (Get Region By ID)
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Get([FromRoute] Guid id)
		{
			//Get Region Domain Model From Database
			//var regionDomain = await _dbContext.regions.FirstOrDefaultAsync(u => u.Id == id);

			//Get data from region repository

			var regionDomain = await _regionRepository .GetByIdAsync(id);

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
		public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) 
		{
			// Map DTO to Domain Model
			var regionDomainModel = new Region
			{
				Code = addRegionRequestDto.Code,
				Name = addRegionRequestDto.Name,
				RegionImageUrl = addRegionRequestDto.RegionImageUrl
			};

			// Use Domain Model to create Region
			//await _dbContext.regions.AddAsync(regionDomainModel);

			//Use Region Repository
			regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

			//await _dbContext.SaveChangesAsync();

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
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
			// Check if Region exists
			//var regionDomainModel = await _dbContext.regions.FirstOrDefaultAsync(region => region.Id == id);

			

			// Map DTO to Domain models
			var regionDomainModel = new Region
			{
				Code = updateRegionRequestDto.Code,
				Name = updateRegionRequestDto.Name,
				RegionImageUrl = updateRegionRequestDto.RegionImageUrl
			};

			//Use Region Repository
			regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);
			if (regionDomainModel == null)
			{
				return NotFound();
			}

			// Map DTO to Domain models
			//regionDomainModel.Code = updateRegionRequestDto.Code;
			//regionDomainModel.Name = updateRegionRequestDto.Name;
			//regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

			//await _dbContext.SaveChangesAsync();

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
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			//var regionDomainModel = await _dbContext.regions.FirstOrDefaultAsync(region => region.Id == id);

			// Use Region Repository
			var regionDomainModel = await _regionRepository.DeleteAsync(id);

			if (regionDomainModel == null)
			{
				return NotFound(); 
			}

			// Delete Region
			//_dbContext.regions.Remove(regionDomainModel);
			//await _dbContext.SaveChangesAsync();

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
