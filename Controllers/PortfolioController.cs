using backend.Extensions;
using backend.Interfaces;
using backend.Interfaces.IFinancialMP;
using backend.Interfaces.IPortfolio;
using backend.Interfaces.IRepo;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _stockRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IFinancialMPService _financialMPService;
    
    public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository,
        IPortfolioRepository portfolioRepository,IFinancialMPService financialMPService)
    {
        _userManager = userManager;
        _stockRepository = stockRepository;
        _portfolioRepository = portfolioRepository;
        _financialMPService = financialMPService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        try
        {
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPortfolio(string symbol)
    {
        try
        {
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);
            
            if (stock == null)
            {
                stock = await _financialMPService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _stockRepository.CreateAsync(stock);
                }
            }
            if (stock == null) return BadRequest("Stock not found");
            
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser); // get user portfolio
            
            if(userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Stock already exists in portfolio");
            
            var portfolioModel = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id
            };            
            
            await _portfolioRepository.CreatePortfolio(portfolioModel);

            if (portfolioModel == null)
            {
                return BadRequest("Failed to add stock to portfolio");
            }
            else
            {
                return Created();
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{symbol}")]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(string symbol)
    {
        try
        {
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var portfolioModel = await _portfolioRepository.GetUserPortfolio(appUser);
            var filteredPortfolio = portfolioModel.Where(e => e.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredPortfolio.Count == 1)
            {
                await _portfolioRepository.DeletePortfolio(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock not found in portfolio");
            }

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}