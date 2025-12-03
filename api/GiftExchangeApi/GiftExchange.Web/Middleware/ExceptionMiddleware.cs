using System.Text;
using GiftExchange.Core.Exceptions;

namespace GiftExchange.Web.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (BaseApiException ex)
        {
            context.Response.StatusCode = (int)ex.StatusCode;
            byte[] byteArray = Encoding.UTF8.GetBytes(ex.Message);
            
            using (var messageStream = new MemoryStream(byteArray))
            {
                await messageStream.CopyToAsync(context.Response.Body);
            }
        }
    }
}