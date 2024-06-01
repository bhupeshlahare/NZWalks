using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IWalkRepository _walkRepository;

		public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
			_mapper = mapper;
			_walkRepository = walkRepository;
		}
		//Create walk
		[HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			// Map DTO to Domain Model
			var walkDomainModel =  _mapper.Map<Walk>(addWalkRequestDto);

			await _walkRepository.CreateAsync(walkDomainModel);

			// Map Domain Model to DTO
			return Ok(_mapper.Map<WalkDto>(walkDomainModel));
		}

		//Get walks
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var walkDomainModel = await _walkRepository.GetAllAsync();

			// Map Domain Model to DTO
			return Ok(_mapper.Map<List<WalkDto>>(walkDomainModel));
		}

		//Get wak by id
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var walkDomainModel = await _walkRepository.GetByIdAsync(id);
			if (walkDomainModel == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<WalkDto>(walkDomainModel));
		}

		//Update walk by id
		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
		{
			//Map Dto to domain model
			var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);

			//use walk repository
			walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);
			if (walkDomainModel == null)
			{
				return NotFound();
			}
			//map domain model to Dto
			return Ok(_mapper.Map<WalkDto>(walkDomainModel));
		}

		//Delete walk
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var walkDomainModel = await _walkRepository.DeleteAsync(id);
			if(walkDomainModel == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<WalkDto>(walkDomainModel));
		}
	}
}
