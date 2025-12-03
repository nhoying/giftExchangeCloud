using System.Net;

namespace GiftExchange.Core.Exceptions;

public abstract class BaseApiException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }

    public BaseApiException(string message) : base(message)
    {
        
    }
    
}