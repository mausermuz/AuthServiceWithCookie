using AuthServiceBulgakov.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AuthServiceBulgakov.Api.Filters
{
    public class AuthServiceExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch(context.Exception)
            {
                case FluentValidationException ex:
                    context.Result = new BadRequestObjectResult(string.Join(',', ex.Errors.SelectMany(x => x.Value)));
                    break;
                case ValidationApplicationException ex:
                    context.Result = new BadRequestObjectResult(ex.Message);
                    break;
                default:
                    var errorResult = new ObjectResult($"Ошибка сервера. {context.Exception.Message}")
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                    context.Result = errorResult;
                    break;
            }

            base.OnException(context);
        }
    }
}
