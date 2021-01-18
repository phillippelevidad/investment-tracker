using Api.Controllers.Assets.Models;
using Api.Core.Application.Assets.Commands.AddAsset;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers.Assets
{
    [ApiController]
    [Route("api/assets")]
    public class AssetsController : Controller
    {
        private readonly IMediator mediator;

        public AssetsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("")]
        public IActionResult ListAllAsync()
        {
            return Ok(new List<Asset>
            {
                new Asset
                {
                    Id = Guid.NewGuid(),
                    Name = "NuConta",
                    Broker = "NuBank",
                    Category = "Savings",
                    Currency = "BRL"
                },
                new Asset
                {
                    Id = Guid.NewGuid(),
                    Name = "Bova 11",
                    Broker = "Clear",
                    Category = "Stocks",
                    Currency = "BRL"
                },
                new Asset
                {
                    Id = Guid.NewGuid(),
                    Name = "FI Cambial",
                    Broker = "Órama",
                    Category = "Hedge Funds",
                    Currency = "USD"
                }
            });
        }

        [HttpPost("")]
        public async Task<IActionResult> AddAssetAsync()
        {
            var command = new AddAssetCommand();
            var result = await mediator.Send(command);

            if (result.IsSuccess)
                return Created(Url.Action(nameof(GetAsync), new { id = command.Id }), null);

            else return BadRequest(result.Error);
        }
    }
}
