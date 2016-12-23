using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class VendorContactDAL : IVendorContactDAL
    {
        public List<VendorContact> GetVendorContact(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<VendorContact> objVendorContact = new List<VendorContact>();

            string sql = @"select * from VendorContact where 1=1";

            string[] colArray = Col.Split(',');
            string[] conditionArray = Condition.Split(',');
            string[] valueArray = conditionValue.Split(',');

            string strCondition = "";
            int len = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]))
                {
                    len++;
                }
            }

            DbParameter[] dbParameters = new DbParameter[len + 1];
            int dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                strCondition += " AND " + colArray[i] + " " + conditionArray[i].Replace("%", "") + " @Parameter" + i + " ";

                if (conditionArray[i] == "like%")
                {
                    dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i] + "%");
                }
                else if (conditionArray[i] == "%like")
                {
                    dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i]);
                }
                else if (conditionArray[i] == "%like%")
                {
                    dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i] + "%");
                }
                else
                {
                    dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i]);
                }

                dblen++;
            }
            if (Page != 0)
            {
                strCondition += " limit @Page1,@Page2";
            }
            dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
            dbParameters[dblen] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);

            DataTable dtVendorContact = null;
            if (strCondition != "")
            {

                dtVendorContact = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtVendorContact = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }
            if (dtVendorContact.Rows.Count > 0)
            {

                foreach (DataRow dr in dtVendorContact.Rows)
                {
                    VendorContact obj = new VendorContact();
                    obj.VendorId = dr["VendorId"].ToString();
                    obj.Contact = dr["Contact"].ToString();
                    obj.Department = dr["Department"].ToString();
                    obj.Occupation = dr["Occupation"].ToString();
                    obj.TelNo = dr["TelNo"].ToString();
                    obj.FaxNo = dr["FaxNo"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objVendorContact.Add(obj);
                }
            }

            return objVendorContact;
        }

    }
}