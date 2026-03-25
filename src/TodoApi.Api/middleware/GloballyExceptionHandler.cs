using TodoApi.Domain.Exception;

namespace TodoApi.middleware;

public class GloballyExceptionHandler:IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
 next(context);
 return Task.CompletedTask;
        }
        catch(NotFoundException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 404;
            return context.Response.WriteAsync(exception.Message);
        }
        catch (Exception e)
        {
            context.Response.ContentType="application/json";
            context.Response.StatusCode = 500;
            return context.Response.WriteAsync(e.Message);
        }
    }
}