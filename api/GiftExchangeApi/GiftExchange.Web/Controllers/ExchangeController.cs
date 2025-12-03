using GiftExchange.Core.Models.Exchange;
using GiftExchange.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftExchange.Web.Controllers;

[ApiController]
public class ExchangeController : ControllerBase
{
    private readonly IExchangeManagementService _exchangeManagementService;
    private readonly IPlayerManagementService _playerManagementService;

    public ExchangeController(IPlayerManagementService playerManagementService,
        IExchangeManagementService exchangeManagementService)
    {
        _playerManagementService = playerManagementService;
        _exchangeManagementService = exchangeManagementService;
    }
    
    [HttpGet]
    [Route("exchange/{exchangeIdentifier}")]
    public async Task<IActionResult> GetExchange(Guid exchangeIdentifier)
    {
        var exchange = await _exchangeManagementService.GetExchange(exchangeIdentifier);
        return new OkObjectResult(exchange);
    }

    [HttpPost]
    [Route("exchange")]
    public async Task<IActionResult> CreateExchange(SaveExchangeRequest request)
    {
        var exchange = await _exchangeManagementService.CreateOrUpdateExchange(request);
        return new CreatedAtRouteResult(
            nameof(GetExchange), 
            new { exchangeIdentifier = exchange.ExchangeIdentifier.ToString() }, 
            exchange);
    }
    
    [HttpPut]
    [Route("exchange")]
    public async Task<IActionResult> UpdateExchange(SaveExchangeRequest request)
    {
        var exchange = await _exchangeManagementService.CreateOrUpdateExchange(request);
        return new NoContentResult();
    }

    [HttpGet]
    [Route("exchange/{exchangeIdentifier}/players")]
    public async Task<IActionResult> GetPlayersForExchange(Guid exchangeIdentifier)
    {
        throw new NotImplementedException();
    }
}