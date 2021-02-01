using Api.Queries.Assets.AssetExists;
using Api.Queries.Assets.GetAssetDetails;
using Api.Queries.Assets.ListAllAssets;
using AutoMapper;
using Core.Application.Assets.Commands.AddOrUpdateAsset;
using Core.Application.Assets.Commands.RemoveAsset;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Assets
{
    [ApiController]
    [Route("api/assets")]
    public class AssetsController : Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public AssetsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AssetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var dto = await mediator.Send(new GetAssetDetailsQuery(id));
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(AssetDto[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAll()
        {
            var dtos = await mediator.Send(new ListAllAssetsQuery());
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
            var exists = await mediator.Send(new AssetExistsQuery(id));
            if (!exists) return NotFound();

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
            var exists = await mediator.Send(new AssetExistsQuery(id));
            if (!exists) return NotFound();

            var result = await mediator.Send(new RemoveAssetCommand(id));
            if (result.IsFailure)
                return BadRequest(ModelState.AddErrors(result));

            return NoContent();
        }
    }
}
