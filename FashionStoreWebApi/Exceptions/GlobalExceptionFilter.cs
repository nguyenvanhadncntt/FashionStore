using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FashionStoreWebApi.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is EntityNotFoundException exp)
            {
                context.Result = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = exp.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
                context.ExceptionHandled = true;
            } 
            else if (IsBadRequestException(context.Exception))
            {
                Exception ex = context.Exception;
                context.Result = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                context.ExceptionHandled = true;
            }
            else
            {
                context.Result = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "An unexpected error occurred."
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                context.ExceptionHandled = true;
            }
        }

        private bool IsBadRequestException(Exception e)
        {
            return e is EntityAlreadyExistingException
                || e is QuantityOutOfStockException;
        }

    }
}
