using backend.Context;
using backend.DTOs.Stock;
using backend.Helpers.Querys;
using backend.Interfaces.IRepo;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationContext _context;

        public StockRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).ThenInclude(u => u.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsSortDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }
            
            var skip = (query.PageNumber - 1) * query.PageSize;
            
            return await stocks.Skip(skip).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stock)
        {
            var existingStock = await _context.Stocks.FindAsync(id);

            if (existingStock != null)
            {
                existingStock.Symbol = stock.Symbol;
                existingStock.CompanyName = stock.CompanyName;
                existingStock.Purchase = stock.Purchase;
                existingStock.LastDiv = stock.LastDiv;
                existingStock.Industry = stock.Industry;
                existingStock.MarketCap = stock.MarketCap;

                await _context.SaveChangesAsync();

                return existingStock;
            }
            return null;
        }
        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
                return stock;
            }
            return null;
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(e => e.Id == id);
        }
    }
}