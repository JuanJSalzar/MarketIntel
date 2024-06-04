using backend.DTOs.Comment;
using backend.Helpers.Querys;
using backend.Models;

namespace backend.Interfaces.IRepo
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsAsync(CommentQueryObject queryObject);
        Task<Comment> GetCommentByIdAsync(int id);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment> UpdateCommentAsync(int id, UpdateCommentRequestDto commentDto);
        Task<Comment> DeleteCommentAsync(int id);
    }
}
