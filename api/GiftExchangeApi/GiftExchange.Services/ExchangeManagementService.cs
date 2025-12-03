using GiftExchange.Core.Exceptions;
using GiftExchange.Core.Models.Exchange;
using GiftExchange.Data.GiftExchangeDb;
using GiftExchange.Data.GiftExchangeDb.Schema;
using Microsoft.EntityFrameworkCore;

namespace GiftExchange.Services;

public interface IExchangeManagementService
{
    Task<ExchangeResponse> GetExchange(Guid exchangeIdentifier, CancellationToken cancellationToken = default);
    Task<ExchangeResponse> CreateOrUpdateExchange(SaveExchangeRequest request,
        CancellationToken cancellationToken = default);
}

public class ExchangeManagementService : IExchangeManagementService
{
    protected readonly IGiftExchangeRepository _giftExchangeRepository;


    public async Task<ExchangeResponse> GetExchange(Guid exchangeIdentifier,
        CancellationToken cancellationToken = default)
    {
        var entity =  await _giftExchangeRepository
            .Query<Exchange>(t => t.ExchangeIdentifier == exchangeIdentifier)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException($"Unable to find Exchange with identifier {exchangeIdentifier.ToString()}");
        }

        return new ExchangeResponse
        {
            ExchangeIdentifier = entity.ExchangeIdentifier,
            Name = entity.Name,
            Description = entity.Description,
            MaxSwipes = entity.MaxSwipes,
            MaxTurnActions = entity.MaxTurnActions,
            CreatedBy = entity.CreatedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedBy = entity.ModifiedBy,
            ModifiedDate = entity.ModifiedDate
        };
    }

    public async Task<ExchangeResponse> CreateOrUpdateExchange(SaveExchangeRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity =  await _giftExchangeRepository
            .Query<Exchange>(t => t.ExchangeIdentifier == request.ExchangeIdentifier)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            entity = new Exchange
            {
                Name = request.Name,
                Description = request.Description,
                MaxSwipes = request.MaxSwipes,
                MaxTurnActions = request.MaxTurnActions,
            };
            _giftExchangeRepository.Add(entity);
        }
        else
        {
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.MaxSwipes = request.MaxSwipes;
            entity.MaxTurnActions = request.MaxTurnActions;
        }

        await _giftExchangeRepository.SaveChangesAsync(cancellationToken);

        return await GetExchange(entity.ExchangeIdentifier, cancellationToken);
    }
}