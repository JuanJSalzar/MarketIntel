using backend.Models;

namespace backend.Interfaces.IPortfolio;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetUserPortfolio(AppUser user);
    Task<Portfolio> CreatePortfolio(Portfolio portfolio);
    Task<Portfolio> DeletePortfolio(AppUser user, string symbol);
}