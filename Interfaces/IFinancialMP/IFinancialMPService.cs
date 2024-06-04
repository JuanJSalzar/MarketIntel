using backend.Models;

namespace backend.Interfaces.IFinancialMP;

public interface IFinancialMPService
{
    Task<Stock> FindStockBySymbolAsync(string symbol);
}