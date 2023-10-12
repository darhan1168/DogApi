namespace DogApi.Middlewares;

public class RequestLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _requestLimit;
    private int _requestCount;
    private DateTime _resetTime;

    public RequestLimitingMiddleware(RequestDelegate next, int requestLimit = 10)
    {
        _next = next;
        _requestLimit = requestLimit;
        _requestCount = 0;
        _resetTime = DateTime.UtcNow.AddSeconds(1);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var currentTime = DateTime.UtcNow;
        
        if (currentTime > _resetTime)
        {
            _requestCount = 0;
            _resetTime = currentTime.AddSeconds(1);
        }

        if (_requestCount >= _requestLimit)
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            
            await context.Response.WriteAsync("Too Many Requests. Please try again later.");
            
            return;
        }

        _requestCount++;
        
        await _next(context);
    }
}