using backend.Helpers.StringMessages;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = Messages.ValidationMessages.TitleMessageMinLength)]
        [MaxLength(500, ErrorMessage = Messages.ValidationMessages.TitleMessageMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = Messages.ValidationMessages.ContentMessageMinLength)]
        [MaxLength(500, ErrorMessage = Messages.ValidationMessages.ContentMessageMaxLength)]
        public string Content { get; set; } = string.Empty;
    }
}
