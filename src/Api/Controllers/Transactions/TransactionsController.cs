using Api.Data;
using AutoMapper;
using Core.Application.Transactions.Commands.AddTransaction;
using Core.Domain;
using Core.Domain.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Transactions
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : Controller
    {
        private readonly InvestmentDbContext db;
        private readonly IMapper mapper;

        public TransactionsController(InvestmentDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var transaction = await db.Transactions
                .Include(t => t.Asset)
                .SingleOrDefaultAsync(t => t.Id == id);
            if (transaction == null) return NotFound();
            return Ok(mapper.Map<TransactionDto>(transaction));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(TransactionDto[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAll()
        {
            var transactions = await db.Transactions
                .Include(t => t.Asset)
                .ToListAsync();
            var dtos = transactions.Select(tran => mapper.Map<TransactionDto>(tran));
            return Ok(dtos);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTransaction(AddTransactionCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var asset = await db.Assets.FindAsync(command.AssetId);
            if (asset == null)
                return BadRequest($"Asset id {command.AssetId} does not exist.");

            Transaction transaction;

            try
            {
                transaction = new Transaction(
                    command.Id.Value,
                    asset,
                    Operation.FromId(command.Operation),
                    command.Quantity.Value,
                    new Money(command.UnitPrice.Value, (Currency)command.Currency),
                    command.DateTime.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            await db.Transactions.AddAsync(transaction);
            await db.SaveChangesAsync();

            var url = Url.Action(nameof(Get), new { id = command.Id });
            return Created(url, command);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveTransaction(Guid id)
        {
            var transaction = await db.Transactions.FindAsync(id);
            if (transaction == null) return NotFound();

            db.Transactions.Remove(transaction);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}
