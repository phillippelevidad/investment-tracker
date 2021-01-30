using Api.Data;
using AutoMapper;
using Core.Application.Assets.Commands.AddOrUpdateAsset;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Assets
{
    [ApiController]
    [Route("api/assets")]
    public class AssetsController : Controller
    {
        private readonly InvestmentDbContext db;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public AssetsController(InvestmentDbContext db, IMapper mapper, IMediator mediator)
        {
            this.db = db;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AssetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var asset = await db.Assets.FindAsync(id);
            if (asset == null) return NotFound();
            return Ok(mapper.Map<AssetDto>(asset));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(AssetDto[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAll()
        {
            var assets = await db.Assets.ToListAsync();
            var dtos = assets.Select(asset => mapper.Map<AssetDto>(asset));
            return Ok(dtos);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(AssetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsset(AddOrUpdateAssetCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await mediator.Send(command);
            if (result.IsFailure)
                return BadRequest(ModelState.AddErrors(result));

            var url = Url.Action(nameof(Get), new { id = command.Id });
            return Created(url, command);
        }

        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsset(Guid id, [FromBody] AddOrUpdateAssetCommand command)
        {
            var asset = await db.Assets.FindAsync(id);
            if (asset == null) return NotFound();

            var result = await mediator.Send(command);
            if (result.IsFailure)
                return BadRequest(ModelState.AddErrors(result));

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveAsset(Guid id)
        {
            var asset = await db.Assets.FindAsync(id);
            if (asset == null) return NotFound();

            db.Assets.Remove(asset);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}
