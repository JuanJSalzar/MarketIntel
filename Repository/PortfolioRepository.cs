using backend.Context;
using backend.Interfaces.IPortfolio;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class PortfolioRepository :  IPortfolioRepository
{
    private readonly ApplicationContext _context;
    public PortfolioRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<List<Stock>> GetUserPortfolio(AppUser user)
    {
        return await _context.Portfolios.Where(p => p.AppUserId == user.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
    }

    public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
    {
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        return portfolio;
    }

    public async Task<Portfolio> DeletePortfolio(AppUser user, string symbol)
    {
        var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

        if (portfolioModel == null)
        {
            return null;
        }
        
        _context.Portfolios.Remove(portfolioModel);
        await _context.SaveChangesAsync();
        return portfolioModel;
    }
}