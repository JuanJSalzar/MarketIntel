using backend.Interfaces;
using backend.Mappers;
using backend.DTOs.Comment;
using backend.Extensions;
using backend.Helpers.Querys;
using Microsoft.AspNetCore.Mvc;
using backend.Helpers.StringMessages;
using backend.Interfaces.IFinancialMP;
using backend.Interfaces.IRepo;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace backend.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFinancialMPService _financialMPService;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, 
            UserManager<AppUser> userManager, IFinancialMPService financialMPService)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
            _financialMPService = financialMPService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetComments([FromQuery] CommentQueryObject queryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepository.GetCommentsAsync(queryObject);
            var commentDto = comments.Select(c => c.CommentToDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepository.GetCommentByIdAsync(id);
            if (comments != null)
            {
                return Ok(comments.CommentToDto());
            }
            return NotFound(Messages.HttpMessages.NotFoundMessage);
        }

        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, CreateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                stock = await _financialMPService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _stockRepository.CreateAsync(stock);
                }
            }

            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            
            
            var comment = commentDto.CommentToDtoPost(stock.Id);
            comment.AppUserId = appUser.Id;
            await _commentRepository.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment.CommentToDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null) return NotFound(Messages.HttpMessages.NotFoundMessage);
            
            var updatedComment = await _commentRepository.UpdateCommentAsync(id, commentDto);
            return Ok(updatedComment.CommentToDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null) return NotFound(Messages.HttpMessages.NotFoundMessage);
            
            await _commentRepository.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
