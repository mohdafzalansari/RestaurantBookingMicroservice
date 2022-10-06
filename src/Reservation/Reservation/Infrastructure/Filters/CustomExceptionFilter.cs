using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Reservation.Infrastructure.Exceptions;
using System;

namespace Reservation.Infrastructure.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ICustomException)
            {
                context.Result = new ObjectResult(new ExceptionMessage
                {
                    Message = context.Exception.Message
                })
                {
                    StatusCode = context.Exception is IStatusCode ?
                        (context.Exception as ICustomException).StatusCode : 500
                };
            }
            else if (context.Exception is Exception)
            {
                context.Result = new ObjectResult(new ExceptionMessage
                {
                    Message = context.Exception.Message
                })
                {
                    StatusCode = context.Exception is IStatusCode ?
                        (context.Exception as ICustomException).StatusCode : 500
                };
            }
        }
    }
}
