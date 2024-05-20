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
		

	}
}
