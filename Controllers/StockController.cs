using backend.Context;
using Microsoft.AspNetCore.Mvc;
using backend.Mappers;
using backend.DTOs.Stock;
using backend.Helpers.Querys;
using backend.Interfaces;
using backend.Helpers.StringMessages;
using backend.Interfaces.IRepo;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IStockRepository _stockRepository;

        public StockController(ApplicationContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stocks = await _stockRepository.GetAllAsync(query);

            var stockDto = stocks.Select(s => s.StockToDto()).ToList();

            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound(Messages.HttpMessages.NotFoundMessage);
            }

            return Ok(stock.StockToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = stockDto.StockToDtoPost();
            await _stockRepository.CreateAsync(stock);
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.StockToDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRepository.UpdateAsync(id, updateDto);

            if (stockModel == null)
            {
                return NotFound(Messages.HttpMessages.NotFoundMessage);
            }

            return Ok(stockModel.StockToDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRepository.DeleteAsync(id);

            if (stockModel == null)
            {
                return NotFound(Messages.HttpMessages.NotFoundMessage);
            }

            return NoContent();
        }
    }
}