using backend.DTOs.FinancialMP;
using backend.DTOs.Stock;
using backend.Models;

namespace backend.Mappers;

public static class FinancialMapper
{
    public static Stock ToStockFromFMP(this FMPStock fmpStock)
    {
        return new Stock
        {
            Symbol = fmpStock.symbol,
            CompanyName = fmpStock.companyName,
            Purchase = (decimal)fmpStock.price,
            LastDiv = (decimal)fmpStock.lastDiv,
            Industry = fmpStock.industry,
            MarketCap = fmpStock.mktCap
        };
    }
}