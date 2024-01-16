using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Microsoft.AspNetCore.Http;

using System.Net;

namespace Infrastructure.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ISerializerService _jsonSerializer;

        public ExceptionMiddleware(
            ISerializerService jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                string errorId = Guid.NewGuid().ToString();
                var errorResult = new ErrorResult
                {
                    //Source = exception.TargetSite?.DeclaringType?.FullName,
                    Exception = exception.Message.Trim(),
                    ErrorId = errorId
                };
                errorResult.Messages!.Add(exception.Message);
                var response = context.Response;
                response.ContentType = "application/jzon";
                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                switch (exception)
                {
                    case CustomException e:
                        response.StatusCode = errorResult.StatusCode = (int)e.StatusCode;
                        if (e.ErrorMessages is not null)
                        {
                            errorResult.Messages = e.ErrorMessages;
                        }

                        break;

                    case KeyNotFoundException:
                        response.StatusCode = errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        response.StatusCode = errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await response.WriteAsync(_jsonSerializer.Serialize(errorResult));
            }
        }
    }
}