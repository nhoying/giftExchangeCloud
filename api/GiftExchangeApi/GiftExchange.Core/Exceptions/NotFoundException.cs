using System.Net;

namespace GiftExchange.Core.Exceptions;

public class NotFoundException(string message) : BaseApiException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}