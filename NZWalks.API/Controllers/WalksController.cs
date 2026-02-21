using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories.Interfaces;

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                var walkDomain = _mapper.Map<Walk>(addWalkRequestDto);

                walkDomain = await _walkRepository.Create(walkDomain);

                return CreatedAtAction(nameof(GetWalkById), new { id = walkDomain.Id }, _mapper.Map<WalkDto>(walkDomain));
            }
            else
            {
                return BadRequest();
            }
        }

        //API url: api/walks?filterOn=Name&filterQuery=Test&sortBy=Name&isAscending=true&pageNo=1&pageSize=5
        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomain = await _walkRepository.GetAllWalks(filterOn, filterQuery, sortBy, isAscending, pageNo, pageSize);

            var walksDtos = _mapper.Map<List<WalkDto>>(walksDomain);

            return Ok(walksDtos);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepository.GetWalkById(id);

            if (walkDomain == null)
                return NotFound();

            return Ok(_mapper.Map<WalkDto>(walkDomain));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalksRequestDto updateWalksRequestDto)
        {
            var walkDomain = _mapper.Map<Walk>(updateWalksRequestDto);
            walkDomain = await _walkRepository.Update(id, walkDomain);

            if (walkDomain == null)
                return NotFound();

            return Ok(walkDomain);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepository.Delete(id);

            if (walkDomain == null)
                return NotFound();

            return Ok(_mapper.Map<WalkDto>(walkDomain));
        }
    }
}
