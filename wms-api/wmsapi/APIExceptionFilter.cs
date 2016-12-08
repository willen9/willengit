using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace wmsapi
{
    public class APIExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            int Code = 9999;
            string Message = string.Empty;

            Message = actionExecutedContext.Exception.Message;

            if (actionExecutedContext.Exception is APIException)
            {
                var exception = actionExecutedContext.Exception as APIException;
                Code = exception.Code;
                Message = exception.Message;
            }

            actionExecutedContext.Response = new HttpResponseMessage()
            {
                Content = new StringContent(
                    APIUtility.GetErrorResponseJSONMessage(Code, Message),
                    System.Text.Encoding.UTF8,
                    "application/json"),
                StatusCode = HttpStatusCode.BadRequest
            };

            
            base.OnException(actionExecutedContext);
        }
    }
}