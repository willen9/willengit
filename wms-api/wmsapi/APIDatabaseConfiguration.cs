using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace wmsapi
{
    public class APIDatabaseConfiguration
    {
        public string ActiveDatabaseName
        {
            get
            {
                return HttpContext.Current.Request.Headers["system_id"];
            }
        }

        public string SystemConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
        }

        public string ActiveConnectionString
        {
            get
            {
                return ConnectionStringBuilder4SystemID(ActiveDatabaseName);
            }
        }

        private string ConnectionStringBuilder4SystemID(string SystemID)
        {
            string newConnStr = "";
            if (!String.IsNullOrEmpty(SystemID))
            {
                DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
                builder.ConnectionString = this.SystemConnectionString;
                builder["Initial Catalog"] = SystemID;
                newConnStr = builder.ConnectionString;
            }
            else
                newConnStr = this.SystemConnectionString;

            return newConnStr;
        }
    }
}