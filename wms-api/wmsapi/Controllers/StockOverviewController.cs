using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;
using REGAL.Data.DataAccess;

namespace wmsapi.Controllers
{
    public class StockOverviewController : BaseController
    {
        [Route("stock/overview/basic/{plant}/{location}")]
        [Route("stock/overview/lot")]
        [Route("stock/overview/lot/{number}")]
        [Route("stock/overview/datecode")]
        [Route("stock/overview/datecode/{number}")]

        [Route("materials/{id}/stock/overview/basic/{plant}/{location}")]

        [Route("materials/{id}/stock/standard/{plant}/{location}"), HttpGet]
        public HttpResponseMessage GetMaterials_Standard(string id, string plant, string location)
        {
            try
            {
                var result = new HttpResponseMessage();
                var httpRequest = System.Web.HttpContext.Current.Request;
                var columns = httpRequest.QueryString["fields"];

                //get header parameter
                string system_id = httpRequest.Headers["system_id"];

                DataAccess dataAccess = new DataAccess();
                dataAccess.ConnectionString = new APIUtility().ConnectionStringBuilder4SystemID(system_id);
                dataAccess.ProviderName = "System.Data.SqlClient";

                string sql = string.IsNullOrEmpty(columns)
                    ? "SELECT * FROM MMSOB AS m WHERE m.SOB001=@SOB001 AND m.SOB002=@SOB002 AND m.SOB003=@SOB003"
                    : "SELECT " + columns + " FROM MMSOB AS m WHERE m.SOB001=@SOB001 AND m.SOB002=@SOB002 AND m.SOB003=@SOB003";
                DataTable dtMaterials = dataAccess.ExecuteDataTable(sql, new DbParameter[]
                {
                    DataAccess.CreateParameter("SOB001",DbType.String,id),
                    DataAccess.CreateParameter("SOB002",DbType.String,plant),
                    DataAccess.CreateParameter("SOB003",DbType.String,location)
                });

                if (dtMaterials.Rows.Count > 0)
                {
                    result.StatusCode = HttpStatusCode.OK;
                    result.Content = new StringContent(JsonConvert.SerializeObject(dtMaterials));
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
                else
                {
                    result.StatusCode = HttpStatusCode.NoContent;
                    result.Content = new StringContent(JsonConvert.SerializeObject(""));
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }

                return result;
            }
            catch (Exception ex)
            {
                return APIUtility.GetErrorResponseMessage(ex.Message);
            }            
        }

        [Route("materials/{id}/stock/lot/{plant}/{location}"), HttpGet]
        public HttpResponseMessage GetMaterials_Lot(string id, string plant, string location)
        {
            try
            {
                var result = new HttpResponseMessage();
                var httpRequest = System.Web.HttpContext.Current.Request;
                var columns = httpRequest.QueryString["fields"];

                //get header parameter
                string system_id = httpRequest.Headers["system_id"];

                DataAccess dataAccess = new DataAccess();
                dataAccess.ConnectionString = new APIUtility().ConnectionStringBuilder4SystemID(system_id);
                dataAccess.ProviderName = "System.Data.SqlClient";

                string sql = string.IsNullOrEmpty(columns)
                    ? "SELECT * FROM MMSOC AS m WHERE m.SOC001=@SOC001 AND m.SOC002=@SOC002 AND m.SOC003=@SOC003"
                    : "SELECT " + columns + " FROM MMSOC AS m WHERE m.SOC001=@SOC001 AND m.SOC002=@SOC002 AND m.SOC003=@SOC003";

                DataTable dtMaterials = dataAccess.ExecuteDataTable(sql, new DbParameter[]
                {
                    DataAccess.CreateParameter("SOC001",DbType.String,id),
                    DataAccess.CreateParameter("SOC002",DbType.String,plant),
                    DataAccess.CreateParameter("SOC003",DbType.String,location)
                });

                if (dtMaterials.Rows.Count > 0)
                {
                    result.StatusCode = HttpStatusCode.OK;
                    result.Content = new StringContent(JsonConvert.SerializeObject(dtMaterials));
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
                else
                {
                    result.StatusCode = HttpStatusCode.NoContent;
                    result.Content = new StringContent(JsonConvert.SerializeObject(""));
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }

                return result;
            }
            catch (Exception ex)
            {
                return APIUtility.GetErrorResponseMessage(ex.Message);
            }             
        }

        [Route("materials/{id}/stock/datecode/{plant}/{location}"), HttpGet]
        public HttpResponseMessage GetMaterials_DateCode(string id, string plant, string location)
        {
            try
            {
                var result = new HttpResponseMessage();
                var httpRequest = System.Web.HttpContext.Current.Request;
                var columns = httpRequest.QueryString["fields"];

                //get header parameter
                string system_id = httpRequest.Headers["system_id"];

                DataAccess dataAccess = new DataAccess();
                dataAccess.ConnectionString = new APIUtility().ConnectionStringBuilder4SystemID(system_id);
                dataAccess.ProviderName = "System.Data.SqlClient";

                string sql = string.IsNullOrEmpty(columns)
                    ? "SELECT * FROM MMSOD AS m WHERE m.SOD001=@SOD001 AND m.SOD002=@SOD002  AND m.SOD003=@SOD003"
                    : "SELECT " + columns + " FROM MMSOD AS m WHERE m.SOD001=@SOD001 AND m.SOD002=@SOD002  AND m.SOD003=@SOD003";
                DataTable dtMaterials = dataAccess.ExecuteDataTable(sql, new DbParameter[]
                {
                    DataAccess.CreateParameter("SOD001",DbType.String,id),
                    DataAccess.CreateParameter("SOD002",DbType.String,plant),
                    DataAccess.CreateParameter("SOD003",DbType.String,location)
                });

                if (dtMaterials.Rows.Count > 0)
                {
                    result.StatusCode = HttpStatusCode.OK;
                    result.Content = new StringContent(JsonConvert.SerializeObject(dtMaterials));
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
                else
                {
                    result.StatusCode = HttpStatusCode.NoContent;
                    result.Content = new StringContent(JsonConvert.SerializeObject(""));
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }

                return result;
            }
            catch (Exception ex)
            {
                return APIUtility.GetErrorResponseMessage(ex.Message);
            }             
        }
    }
}
