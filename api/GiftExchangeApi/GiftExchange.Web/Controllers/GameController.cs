using GiftExchange.Core.Models.Game;
using GiftExchange.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftExchange.Web.Controllers;

[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameManagementService _gameManagementService;

    public GameController(IGameManagementService gameManagementService)
    {
        _gameManagementService = gameManagementService;
    }

    [HttpGet]
    [Route("game/{exchangeIdentifier}")]
    public async Task<IActionResult> GetCurrentGameStatus(Guid exchangeIdentifier)
    {
        var gameObject = await _gameManagementService.GetCurrentGameState(exchangeIdentifier);

        return new OkObjectResult(gameObject);
    }
}