using GiftExchange.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftExchange.Web.Controllers;

[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IPlayerManagementService _playerManagementService;

    public PlayerController(IPlayerManagementService playerManagementService)
    {
        _playerManagementService = playerManagementService;
    }

}