using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using REGAL.Data.DataAccess;

namespace DAL
{
    public class DBHelper
    {
        private static readonly string conStr = ConfigurationManager.AppSettings["conStr"];
        private static readonly string conStrView = ConfigurationManager.AppSettings["conStrView"];

        private static DataAccess _dataAccess;
        public static DataAccess DataAccess
        {
            get
            {
                if (_dataAccess == null)
                {
                    _dataAccess = new DataAccess();
                    _dataAccess.ConnectionString = conStr;
                    _dataAccess.ProviderName = "System.Data.SqlClient";
                    _dataAccess.EnableDebugMode = true;
                }
                return _dataAccess;
            }
            set { _dataAccess = value; }
        }

        private static DataAccess _dataAccessView;

        public static DataAccess DataAccessView
        {
            get
            {
                if (_dataAccessView == null)
                {
                    _dataAccessView = new DataAccess();
                    _dataAccessView.ConnectionString = conStrView;
                    _dataAccessView.ProviderName = "System.Data.SqlClient";
                    _dataAccessView.EnableDebugMode = true;
                }
                return _dataAccess;
            }
            set { _dataAccessView = value; }
        }
    }
}