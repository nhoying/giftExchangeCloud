using GiftExchange.Core.Enums;
using GiftExchange.Core.Exceptions;
using GiftExchange.Core.Extensions;
using GiftExchange.Core.Models.Game;
using GiftExchange.Data.GiftExchangeDb;
using GiftExchange.Data.GiftExchangeDb.Schema;
using Microsoft.EntityFrameworkCore;

namespace GiftExchange.Services;

public interface IGameManagementService
{
    Task<GameResponse> GetCurrentGameState(Guid exchangeIdentifier);

    Task InitializeGame(Guid exchangeIdentifier);

    Task UndoTurnAction(Guid turnActionIdentifier);

    Task SwipeGift(SwipeRequest request);

    Task OpenGift(OpenGiftRequest request);

}

public class GameManagementService : IGameManagementService
{
    private readonly IGiftExchangeRepository _giftExchangeRepository;

    public GameManagementService(IGiftExchangeRepository giftExchangeRepository)
    {
        _giftExchangeRepository = giftExchangeRepository;
    }
    
    public async Task<GameResponse> GetCurrentGameState(Guid exchangeIdentifier)
    {
        var gameQuery = await _giftExchangeRepository.Query<Exchange>(
                a => a.ExchangeIdentifier == exchangeIdentifier)
            .Include(a => a.Players)
            .Include(a => a.Gifts)
            .Include(a => a.Turns)
            .ThenInclude(a => a.StartingPlayer)
            .Include(a => a.Turns)
            .ThenInclude(a => a.TurnActions)
            .ThenInclude(a => a.TurnActionType)
            .Include(a => a.Turns)
            .ThenInclude(a => a.TurnActions)
            .ThenInclude(a=> a.Player)
            .SingleOrDefaultAsync();

        var turns = gameQuery.Turns.Select(t => new TurnResponse
        {
            TurnIdentifier = t.TurnIdentifier,
            TurnNumber = t.TurnNumber,
            InitiatingPlayer = PlayerManagementService.MapPlayerResponse(t.StartingPlayer),
            TurnStatus = (TurnStatusEnum)t.TurnStatusId
        }).ToList();

        var currentTurn = 
            gameQuery.Turns.SingleOrDefault(a => a.TurnStatusId == (int)TurnStatusEnum.InProgress);

        var currentTurnAction =
            currentTurn.TurnActions.SingleOrDefault(a =>
                a.TurnActionId == currentTurn.TurnActions.Max(a => a.TurnActionId));
        
        var nextTurn = currentTurn != null ? 
            gameQuery.Turns.SingleOrDefault(a => a.TurnNumber == currentTurn.TurnNumber + 1) :
            null;

        var giftList = from g in gameQuery.Gifts
            join ta in _giftExchangeRepository.Query<TurnAction>(a =>
                    a.Turn.Exchange.ExchangeIdentifier == exchangeIdentifier)
                on g.GiftId equals ta.GiftId
            group ta by g
            into grp
            let lastTurn = (from g2 in grp select g2.Turn.TurnNumber).Max()
            let lastTurnObject = grp.FirstOrDefault(a => a.Turn.TurnNumber == lastTurn)
            let numberOfTimesSwiped = grp.Count(a => a.TurnActionTypeId == (int)TurnActionTypeEnum.Trade)
            select new GiftResponse
            {
                GiftIdentifier = grp.Key.GiftIdentifier,
                GiftName = grp.Key.Description,
                HoldingPlayer = PlayerManagementService.MapPlayerResponse(lastTurnObject.Player),
                HasLotteryTickets = grp.Key.HasLotteryTickets,
                NumberOfTimesSwiped = numberOfTimesSwiped,
                ShowAsAvailable = gameQuery.MaxSwipes != 0 && numberOfTimesSwiped <= gameQuery.MaxSwipes
            };
            
        
        var response = new GameResponse()
        {
            ExchangeIdentifier = gameQuery.ExchangeIdentifier,
            ExchangeName = gameQuery.Name,
            MaxSwipes = gameQuery.MaxSwipes,
            MaxTurnActions = gameQuery.MaxTurnActions,
            TotalTurnNumber = gameQuery.Turns.Count,
            IsGameOver = currentTurn == null,
            Gifts = giftList.ToList(),
            Turns = turns,
            CurrentTurnNumber = currentTurn.TurnNumber,
            OnDeckPlayer = nextTurn != null ? 
                PlayerManagementService.MapPlayerResponse(nextTurn.StartingPlayer) : null,
            ActivePlayer = currentTurnAction != null ? 
                PlayerManagementService.MapPlayerResponse(currentTurnAction.Player) :
                PlayerManagementService.MapPlayerResponse(currentTurn.StartingPlayer)
                
        };

        return response;
    }

    public async Task InitializeGame(Guid exchangeIdentifier)
    {
        var exchange = await _giftExchangeRepository.Query<Exchange>(a => a.ExchangeIdentifier == exchangeIdentifier)
            .Include(a => a.Turns)
            .Include(a => a.Players)
            .SingleOrDefaultAsync();

        if (exchange.Players == null || !exchange.Players.Any())
        {
            throw new BadRequestException($"No players for the exchange {exchangeIdentifier}");
            
        }

        if (exchange.Turns !=null && exchange.Turns.Any())
        {
            throw new BadRequestException($"Exchange {exchangeIdentifier} has already been initialized.");
        }

        if (exchange.Turns == null)
        {
            exchange.Turns = new List<Turn>();
        }

        Random rng = new Random();
        var randomizedPlayers = exchange.Players.Shuffle(rng).ToList();

        int turnNumber = 1;
        foreach (var player in randomizedPlayers)
        {
            var turn = new Turn()
            {
                TurnNumber = turnNumber,
                StartingPlayerId = player.PlayerId,
                TurnStatusId = turnNumber == 1 ? (int)TurnStatusEnum.InProgress : (int)TurnStatusEnum.NotPlayed
            };
            exchange.Turns.Add(turn);
        }

        await _giftExchangeRepository.SaveChangesAsync();
    }

    public async Task UndoTurnAction(Guid turnActionIdentifier)
    {
        var action = await _giftExchangeRepository.Query<TurnAction>(
                a => a.TurnActionIdentifier == turnActionIdentifier)
            .Include(t => t.Turn)
            .SingleOrDefaultAsync();

        if (action == null)
        {
            throw new NotFoundException($"Unable to find Turn Action {turnActionIdentifier}");
        }

        if (action.Turn.TurnStatusId != (int)TurnStatusEnum.InProgress)
        {
            throw new BadRequestException("Unable to execute as this turn is not in progress");
        }
        
        _giftExchangeRepository.Remove(action);
        await _giftExchangeRepository.SaveChangesAsync();

    }

    public async Task SwipeGift(SwipeRequest request)
    {
        var exchange = await _giftExchangeRepository
            .Query<Exchange>(a => a.ExchangeIdentifier == request.ExchangeIdentifier)
            .SingleOrDefaultAsync();

        if (exchange.MaxSwipes == 0)
        {
            throw new BadRequestException("No Swiping is allowed on this gift");
        }

        var turnActions = await _giftExchangeRepository.Query<TurnAction>(
                a => a.Gift.GiftIdentifier == request.GiftIdentifier
                     && a.TurnActionTypeId == (int)TurnActionTypeEnum.Trade)
            .ToListAsync();

        int trades = turnActions.Count();

        if (trades >= exchange.MaxSwipes)
        {
            throw new BadRequestException("No more swiping is allowed on this gift");
        }

        var currentTurn = await _giftExchangeRepository.Query<Turn>(a => a.TurnStatusId == (int)TurnStatusEnum.InProgress)
            .SingleOrDefaultAsync();

        var previousTurnAction = currentTurn.TurnActions.MaxBy(a => a.TurnId);

        var gift = await _giftExchangeRepository.Query<Gift>(
                a => a.GiftIdentifier == request.GiftIdentifier)
            .SingleOrDefaultAsync();

        var player = await _giftExchangeRepository.Query<Player>(
                a => a.PlayerIdentifier == request.SwipingPlayerIdentifier)
            .SingleOrDefaultAsync();
        
        currentTurn.TurnActions.Add(
            new TurnAction
            {
                ParentTurnAction = previousTurnAction,
                Gift = gift,
                Player = player,
                TurnActionTypeId = (int)TurnActionTypeEnum.Trade,
            });

     
        
        await _giftExchangeRepository.SaveChangesAsync();
    }

    public async Task OpenGift(OpenGiftRequest request)
    {
        var exchange = await _giftExchangeRepository.Query<Exchange>(
                a => a.ExchangeIdentifier == request.ExchangeIdentifier)
            .Include(t => t.Turns)
            .ThenInclude(t => t.TurnActions)
            .SingleOrDefaultAsync();
        
        var gift = new Gift
        {
            Description = request.GiftDescription,
            HasLotteryTickets = request.HasLotteryTickets,
            Exchange = exchange
        };

        var currentTurn = exchange.Turns.FirstOrDefault(a => a.TurnStatusId == (int)TurnStatusEnum.InProgress);

        var player = await _giftExchangeRepository.Query<Player>(a => a.PlayerIdentifier == request.PlayerIdentifier)
            .SingleOrDefaultAsync();

        var lastTurnAction = currentTurn.TurnActions.MaxBy(a => a.TurnId);
        
        currentTurn.TurnActions.Add(new TurnAction
        {
            TurnActionIdentifier = default,
            ParentTurnActionId = null,
            ParentTurnAction = lastTurnAction,
            Gift = gift,
            Player = player,
            TurnActionTypeId = (int)TurnActionTypeEnum.New,
        });

        await _giftExchangeRepository.SaveChangesAsync();
    }
}