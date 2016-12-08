using Newtonsoft.Json;
using REGAL.Data.DataAccess;
using REGAL.WMS.WebApi.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace wmsapi.Controllers
{
    public class AccountController : ApiController
    {
        [Route("account/credentials"), HttpPost]
        public HttpResponseMessage accountCredentials(AccountCredentialsPostParameter posts)
        {
            try
            {
                var result = new HttpResponseMessage();
                var httpRequest = System.Web.HttpContext.Current.Request;

                if (posts == null)
                {
                    return APIUtility.GetErrorResponseMessage("parameter incorrect");
                }

                if (String.IsNullOrEmpty(posts.username) || String.IsNullOrEmpty(posts.password))
                {
                    return APIUtility.GetErrorResponseMessage("parameter incorrect");
                }

                DataAccess dataAccess = new DataAccess();
                dataAccess.ConnectionString = new APIUtility().ConnectionStringBuilder4SystemID("WMSADM");
                dataAccess.ProviderName = "System.Data.SqlClient";

                Dictionary<string, string> dicReturnValues = new Dictionary<string, string>();

                dicReturnValues.Add("username", posts.username);

                using (DbDataReader objReader = dataAccess.ExecuteReader("SELECT MAC001,MAC002,MAC005 FROM ADMAC WITH(NOLOCK) WHERE MAC001=@MAC001 AND MAC003=@MAC003",
                        new DbParameter[] {
                        DataAccess.CreateParameter("MAC001", DbType.String, posts.username),
                        DataAccess.CreateParameter("MAC003", DbType.String, posts.password)
                }))
                {
                    if (objReader.HasRows)
                    {
                        objReader.Read();
                        dicReturnValues.Add("account_name", objReader["MAC002"].ToString());
                        dicReturnValues.Add("locked", objReader["MAC005"].ToString());
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                }

                //Generate access token
                dicReturnValues.Add("access_token", Guid.NewGuid().ToString());

                //Put access token into cache
                //access_token:<token>
                CacheLayer.Init().Set("access_token:" + dicReturnValues["access_token"], posts.username);

                //access_token:user:<username>
                CacheLayer.Init().Set("access_token:user:" + posts.username, dicReturnValues["access_token"]);

                result.StatusCode = HttpStatusCode.OK;
                result.Content = new StringContent(JsonConvert.SerializeObject(dicReturnValues));
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return result;
            }
            catch (Exception ex)
            {
                return APIUtility.GetErrorResponseMessage(ex.Message);
            }            
        }

        public class AccountCredentialsPostParameter
        {
            public string username { get; set; }

            public string password { get; set; }
        }
    }
}
