using backend.Context;
using backend.DTOs.Comment;
using backend.Helpers.Querys;
using backend.Interfaces.IRepo;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationContext _context;
        public CommentRepository(ApplicationContext context) => _context = context;


        public async Task<List<Comment>> GetCommentsAsync(CommentQueryObject queryObject)
        {
            var comments = _context.Comments.Include(a => a.AppUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                comments = comments.Where(s => s.Stock.Symbol == queryObject.Symbol);
            };
            if (queryObject.IsDecsending == true)
            {
                comments = comments.OrderByDescending(c => c.CreatedOn);
            }
            return await comments.ToListAsync();
        }


        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment> CreateCommentAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }
        public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequestDto commentDto)
        {
            var comment = await _context.Comments.FindAsync(id);
            if(comment != null)
            {
                comment.Title = commentDto.Title;
                comment.Content = commentDto.Content;
                await _context.SaveChangesAsync();
                
                return comment;
            }

            return null;
        }
        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                return comment;
            }
            return null;
        }
    }
}

//How can i update the date to now when i update the comment?

