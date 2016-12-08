using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class APIHeader
    {
        private System.Collections.Specialized.NameValueCollection HttpHeaders
        {
            get
            {
                return System.Web.HttpContext.Current.Request.Headers;
            }
        }

        public APIHeader()
        {
            this.SYSTEM_ID = HttpHeaders["system_id"].ToString();
        }

        public string SYSTEM_ID { get; set; }
    }
}