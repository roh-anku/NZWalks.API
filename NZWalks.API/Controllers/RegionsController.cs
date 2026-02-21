using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Database;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        //HTTP METHOD: GET
        //API url:https://localhost:7053/api/regions
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Inside get all regions API");
            List<Region> regionsDomain = await _regionRepository.GetAllRegionsAsync();

            _logger.LogInformation("finished executing get all regions API");
            return Ok(_mapper.Map<List<RegionDto>>(regionsDomain));
        }

        //HTTP METHOD: GET
        //API url:https://localhost:7053/api/regions/{id} -> id=Guid 
        [HttpGet]
        [Route("{id:Guid}")] //we can exclude :Guid, but it adds extra layer of security. i.e. user can not enter any other than Guid
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = _dbContext.Regions.Find(id);
            Region? regionDomain = await _regionRepository.GetRegionById(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }

        //HTTP POST: to create a new region
        //API url:https://localhost:7053/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomain = _mapper.Map<Region>(addRegionRequestDto);

            //use dbcontext to save domain model to database and call SaveChanges() om dbcontext
            regionDomain = await _regionRepository.AddRegionAsync(regionDomain);

            return CreatedAtAction(nameof(GetById), new { id = regionDomain.Id }, _mapper.Map<RegionDto>(regionDomain));
        }

        //HTTP PUT: update a region
        //API url: https://localhost:7053/api/regions/{id}

        [HttpPut]
        [Route("{id:Guid}")] //type safe
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            var regionDomain = _mapper.Map<Region>(updateRegionRequestDto);

            //check if region exists
            regionDomain = await _regionRepository.UpdateRegionAsync(id, regionDomain);

            if (regionDomain == null)
                return NotFound();

            var regionDto = _mapper.Map<RegionDto>(regionDomain);
            return Ok(regionDto);
        }

        //HTTP DELETE: remove region from database
        //API url: https://localhost:7053/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.DeleteAsync(id);

            if (regionDomain == null)
                return NotFound();

            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }
    }
}
