using backend.DTOs.Stock;
using backend.Helpers.Querys;
using backend.Models;

namespace backend.Interfaces.IRepo
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stock);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
