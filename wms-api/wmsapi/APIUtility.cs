using REGAL.WMS.WebApi.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace wmsapi
{
    public class APIUtility
    {
        public static bool VerifyAccessToken(string access_token)
        {
            return CacheLayer.Init().Get("access_token:" + access_token) != null;
        }

        public string ConnectionStringBuilder4SystemID(string SystemID)
        {
            string newConnStr = "";
            if (!String.IsNullOrEmpty(SystemID))
            {
                DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
                builder.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SYSTEM_CONNECTION_STRING"].ConnectionString;
                builder["Initial Catalog"] = SystemID;
                newConnStr = builder.ConnectionString;
            }
            else
                newConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["SYSTEM_CONNECTION_STRING"].ConnectionString;

            return newConnStr;
        }

        public static HttpResponseMessage GetErrorResponseMessage(string message)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new StringContent(GetErrorResponseJSONMessage(message));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;
        }

        public static string GetErrorResponseJSONMessage(Exception exception)
        {
            return GetErrorResponseJSONMessage(9999, exception);
        }

        public static string GetErrorResponseJSONMessage(string message)
        {
            return GetErrorResponseJSONMessage(9999, message);
        }

        public static string GetErrorResponseJSONMessage(int code, Exception exception)
        {
            return GetErrorResponseJSONMessage(code, exception.Message);
        }

        public static string GetErrorResponseJSONMessage(int code, string message)
        {
            Dictionary<string, object> dicResponse = new Dictionary<string, object>();
            dicResponse.Add("code", code);
            dicResponse.Add("message", message);

            var error = new object[] { "error", dicResponse };

            return JsonConvert.SerializeObject(error);
        }
    }
}