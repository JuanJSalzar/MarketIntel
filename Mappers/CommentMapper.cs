using backend.DTOs.Comment;
using backend.Models;

namespace backend.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto CommentToDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                CreatedBy = commentModel.AppUser.UserName,
                StockId = commentModel.StockId
            };
        }
        public static Comment CommentToDtoPost(this CreateCommentRequestDto commentDto, int StockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = StockId
            };
        }
    }
}
