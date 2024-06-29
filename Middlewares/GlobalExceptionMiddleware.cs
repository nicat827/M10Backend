using M10Backend.DTOs;
using M10Backend.Exceptions.Base;

namespace M10Backend.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stream originalBodyStream = context.Response.Body;
            try
            {
                await _next.Invoke(context);
            }
            catch (BaseException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.StatusCode;
                ErrorResponseDto res = new ErrorResponseDto
                {
                    Status = ex.StatusCode,
                    Message = ex.Message,
                };
                await context.Response.WriteAsJsonAsync(res);
            }
            // handling unexpected exceptions
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                ErrorResponseDto res = new ErrorResponseDto
                {
                    Status = 500,
                    Message = ex.Message
                };
                await context.Response.WriteAsJsonAsync(res);
            }
            finally
            {
                // Восстанавливаем оригинальный поток
                context.Response.Body = originalBodyStream;
            }

        }
    }
}
