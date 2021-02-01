using Api.Data;
using AutoMapper;
using Core.Application.Categories.Commands.AddOrUpdateCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Categories
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        private readonly InvestmentDbContext db;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public CategoriesController(InvestmentDbContext db, IMapper mapper, IMediator mediator)
        {
            this.db = db;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var category = await db.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return Ok(mapper.Map<CategoryDto>(category));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(CategoryDto[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAll()
        {
            var categories = await db.Categories.ToListAsync();
            var dtos = categories.Select(category => mapper.Map<CategoryDto>(category));
            return Ok(dtos);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategory(AddOrUpdateCategoryCommand command)
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
        public async Task<IActionResult> UpdateAsset(Guid id, [FromBody] AddOrUpdateCategoryCommand command)
        {
            var category = await db.Categories.FindAsync(id);
            if (category == null) return NotFound();

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
            var categories = await db.Categories.FindAsync(id);
            if (categories == null) return NotFound();

            db.Categories.Remove(categories);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}
