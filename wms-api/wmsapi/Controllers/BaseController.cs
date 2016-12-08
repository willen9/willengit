using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace wmsapi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BaseController : ApiController
    {
        public System.Collections.Specialized.NameValueCollection HttpHeaders
        {
            get
            {
                return System.Web.HttpContext.Current.Request.Headers;
            }
        }
        
        public override System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Web.Http.Controllers.HttpControllerContext controllerContext, System.Threading.CancellationToken cancellationToken)
        {
            string requestAbsolutePath = controllerContext.Request.RequestUri.AbsolutePath;

            if (requestAbsolutePath.Equals("account/credentials"))
            {
                return base.ExecuteAsync(controllerContext, cancellationToken);
            }

            //get header parameter
            string access_token = HttpHeaders["access_token"];
            string system_id = HttpHeaders["system_id"];
            
            if(string.IsNullOrEmpty(system_id))
            {
                var response = APIUtility.GetErrorResponseMessage("parameter incorrect");
                return System.Threading.Tasks.Task.FromResult(response);
            }

            if(string.IsNullOrEmpty(access_token))
            {

            }

            if (!APIUtility.VerifyAccessToken(access_token))
            {
            //    var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            //    response.Content = new StringContent(APIUtility.JSONErrorMessage("access token incorrect"));
            //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //    return System.Threading.Tasks.Task.FromResult(response);
            }

            return base.ExecuteAsync(controllerContext, cancellationToken);
        }
    }
}
