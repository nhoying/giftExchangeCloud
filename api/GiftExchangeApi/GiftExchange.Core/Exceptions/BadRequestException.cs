using System.Net;
using GiftExchange.Core.Exceptions;

namespace GiftExchange.Core.Exceptions;

public class BadRequestException(string message) : BaseApiException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}