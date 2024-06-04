using backend.Helpers.StringMessages;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = Messages.ValidationMessages.SymbolMessage)]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(30, ErrorMessage = Messages.ValidationMessages.CompanyNameMessage)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1, 10000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 10000)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = Messages.ValidationMessages.IndustryMessage)]
        public string Industry { get; set; } = string.Empty;

        [Range(1, 500000000000000)]
        public long MarketCap { get; set; }
    }
}