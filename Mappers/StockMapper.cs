using backend.DTOs.Stock;
using backend.Models;

namespace backend.Mappers
{
    public static class StockMapper
    {
        public static StockDto StockToDto(this Stock stockModel) //What this method does is take a Stock object and return a StockDto object with the same properties.
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.CommentToDto()).ToList()
            };
        }
        public static Stock StockToDtoPost(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}
