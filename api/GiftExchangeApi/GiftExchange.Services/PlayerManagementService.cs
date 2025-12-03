using GiftExchange.Core.Exceptions;
using GiftExchange.Core.Models.Player;
using GiftExchange.Data.GiftExchangeDb;
using GiftExchange.Data.GiftExchangeDb.Schema;
using Microsoft.EntityFrameworkCore;

namespace GiftExchange.Services;

public interface IPlayerManagementService
{
    Task<List<PlayerResponse>> GetPlayersForExchange(Guid exchangeIdentifier);
    Task<PlayerResponse> GetPlayer(Guid playerIdentifier);
    Task<PlayerResponse> SaveOrUpdatePlayer(SavePlayerRequest request);

    Task RemovePlayer(Guid playerIdentifier);
}

public class PlayerManagementService : IPlayerManagementService
{
    private readonly IGiftExchangeRepository _giftExchangeRepository;

    public PlayerManagementService(IGiftExchangeRepository giftExchangeRepository)
    {
        _giftExchangeRepository = giftExchangeRepository;
    }
    
    public async Task<List<PlayerResponse>> GetPlayersForExchange(Guid exchangeIdentifier)
    {
        return await _giftExchangeRepository
            .Query<Player>(a => a.Exchange.ExchangeIdentifier == exchangeIdentifier)
            .Include(a => a.Exchange)
            .Select(a => MapPlayerResponse(a)).ToListAsync();
    }

    public async Task<PlayerResponse> GetPlayer(Guid playerIdentifier)
    {
        var player = await _giftExchangeRepository.Query<Player>(
                a => a.PlayerIdentifier == playerIdentifier)
            .SingleOrDefaultAsync();

        if (player == null)
        {
            throw new NotFoundException($"Unable to find player with identifier {playerIdentifier}");
        }
        return MapPlayerResponse(player);
    }

    public async Task<PlayerResponse> SaveOrUpdatePlayer(SavePlayerRequest request)
    {
        var playerToSave = await _giftExchangeRepository.Query<Player>(
                a => a.PlayerIdentifier == request.PlayerIdentifier)
            .SingleOrDefaultAsync();

        if (playerToSave == null)
        {
            var exchange = await _giftExchangeRepository
                .Query<Exchange>(a => a.ExchangeIdentifier == request.ExchangeIdentifier)
                .SingleOrDefaultAsync();

            if (exchange == null)
            {
                throw new NotFoundException($"Unable to find exchange with identifier {request.ExchangeIdentifier}");
            }

            playerToSave = new Player
            {
                Name = request.Name,
                PictureUrl = request.PictureUrl,
                Exchange = exchange,
                CreatedBy = "TBD",
                CreatedDate = default,
                ModifiedBy = "TBD",
                ModifiedDate = default
            };
            
            _giftExchangeRepository.Add(playerToSave);
        }
        else
        {
            playerToSave.Name = request.Name;
            playerToSave.PictureUrl = request.PictureUrl;
        }

        await _giftExchangeRepository.SaveChangesAsync();

        return MapPlayerResponse(playerToSave);
    }

    public async Task RemovePlayer(Guid playerIdentifier)
    {
        var player = await _giftExchangeRepository.Query<Player>(
                a => a.PlayerIdentifier == playerIdentifier)
            .SingleOrDefaultAsync();

        if (player == null)
        {
            throw new NotFoundException($"Unable to locate player with identifier {playerIdentifier}");
        }
        
        _giftExchangeRepository.Remove(player);
        await _giftExchangeRepository.SaveChangesAsync();
    }
    
    public static PlayerResponse MapPlayerResponse(Player entity)
    {
        return new PlayerResponse()
        {
            ExchangeIdentifier = entity.Exchange.ExchangeIdentifier,
            PlayerIdentifier = entity.PlayerIdentifier,
            PictureUrl = entity.PictureUrl,
            Name = entity.Name
        };
    }
}