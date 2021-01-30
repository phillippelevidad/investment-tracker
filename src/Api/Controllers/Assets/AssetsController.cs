using Api.Core.Application.Assets.Commands.AddAsset;
using Api.Core.Domain;
using Api.Core.Domain.Assets;
using Api.Data;
using AutoMapper;
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

        public AssetsController(InvestmentDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
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
        public async Task<IActionResult> AddAsset(AddAssetCommand dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Asset asset;

            try
            {
                asset = new Asset(dto.Id.Value, dto.Name, dto.Broker, dto.Category, new Currency(dto.Currency));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            await db.Assets.AddAsync(asset);
            await db.SaveChangesAsync();

            var url = Url.Action(nameof(Get), new { id = dto.Id });
            return Created(url, dto);
        }

        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsset(Guid id, [FromBody] UpdateAssetCommand dto)
        {
            var asset = await db.Assets.FindAsync(id);
            if (asset == null) return NotFound();

            try
            {
                asset.Name = dto.Name;
                asset.Broker = dto.Broker;
                asset.Category = dto.Category;
                asset.Currency = new Currency(dto.Currency);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            db.Assets.Update(asset);
            await db.SaveChangesAsync();

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
