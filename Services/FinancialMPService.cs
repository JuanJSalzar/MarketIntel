using backend.DTOs.FinancialMP;
using backend.Interfaces.IFinancialMP;
using backend.Mappers;
using backend.Models;
using Newtonsoft.Json;

namespace backend.Services;

public class FinancialMPService : IFinancialMPService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public FinancialMPService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    public async Task<Stock> FindStockBySymbolAsync(string symbol)
    {
        try
        {
            var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_configuration["FMPKey"]}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<FMPStock[]>(content);
                var stocks = task[0];
                if (stocks != null)
                {
                    return stocks.ToStockFromFMP();
                }

                return null;
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

        return null;
    }
}