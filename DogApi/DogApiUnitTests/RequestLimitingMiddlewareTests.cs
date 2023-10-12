using DogApi.Middlewares;
using Microsoft.AspNetCore.Http;
using Moq;

namespace DogApiUnitTests;

public class RequestLimitingMiddlewareTests
{
    [Fact]
    public async Task RequestLimitingMiddleware_TooManyRequests_Status429TooManyRequests()
    {
        var context = new DefaultHttpContext();
        context.Response.Headers["Content-Type"] = "text/plain";

        var next = new RequestDelegate(ctx => Task.CompletedTask);
        var middleware = new RequestLimitingMiddleware(next, requestLimit: 2); 
        
        for (var i = 0; i < 3; i++)
        {
            await middleware.InvokeAsync(context);
        }

        Assert.Equal(StatusCodes.Status429TooManyRequests, context.Response.StatusCode);
    }
}