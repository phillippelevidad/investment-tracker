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
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var asset = await db.Assets.FindAsync(id);
            if (asset == null) return NotFound();
            return Ok(asset);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(AssetDto[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAllAsync()
        {
            var assets = await db.Assets.ToListAsync();
            var dtos = assets.Select(asset => mapper.Map<AssetDto>(asset));
            return Ok(assets);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(AssetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAssetAsync(AssetDto dto)
        {
            Asset asset;

            try
            {
                asset = new Asset(dto.Id, dto.Name, dto.Broker, dto.Category, dto.Currency);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            await db.Assets.AddAsync(asset);
            await db.SaveChangesAsync();

            var url = Url.Action(nameof(GetAsync), new { id = dto.Id });
            return Created(url, dto);
        }

        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAssetAsync(Guid id, [FromBody] AssetDto dto)
        {
            var asset = await db.Assets.FindAsync(id);
            if (asset == null) return NotFound();

            try
            {
                asset.Name = dto.Name;
                asset.Broker = dto.Broker;
                asset.Category = dto.Category;
                asset.Currency = dto.Currency;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            db.Assets.Update(asset);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}
