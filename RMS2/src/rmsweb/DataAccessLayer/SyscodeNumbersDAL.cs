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
    public class SyscodeNumbersDAL : ISyscodeNumbersDAL
    {
        public string CountSysc(string CodeType, string CodeNumber)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select * from OrderCategory where OrderType=@OrderType",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("OrderType",DbType.String,CodeType)
                });
            if (dtOrderCategory.Rows.Count==0)
            {
                return "NO-單別不存在！";
            }

            int orderlen = 0;

            if (dtOrderCategory.Rows[0]["CodeMode"].ToString() == "1")
            {
                orderlen = 8;
            }
            if (dtOrderCategory.Rows[0]["CodeMode"].ToString() == "2")
            {
                orderlen = 6;
            }
            orderlen = orderlen + int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString());

            if (CodeNumber.Length!=orderlen)
            {
                return "NO-單號的長度是"+orderlen.ToString();
            }

            if (dtOrderCategory.Rows[0]["CodeMode"].ToString() == "1")
            {
                try
                {
                    DateTime.ParseExact(CodeNumber.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.CurrentCulture);
                }
                catch
                {
                    return "NO-請輸入正確的格式：yyyyMMdd" + "999999999999".Substring(0, int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()));
                }
            }

            if (dtOrderCategory.Rows[0]["CodeMode"].ToString() == "2")
            {
                try
                {
                    DateTime.ParseExact(CodeNumber.Substring(0, 8), "yyyyMM",
                        System.Globalization.CultureInfo.CurrentCulture);
                }
                catch
                {
                    return "NO-請輸入正確的格式：yyyyMM" + "999999999999".Substring(0, int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()));
                }
            }

            if (Int32.Parse(dbMySQL.ExecuteScalar("select count(*) from SyscodeNumbers where CodeType=@CodeType and CodeNumber=@CodeNumber",
                new DbParameter[]{
                DataAccessMySQL.CreateParameter("CodeType",DbType.String,CodeType),
                DataAccessMySQL.CreateParameter("CodeNumber",DbType.String,CodeNumber)
            }).ToString()) > 0)
            {
                return "NO-單號已存在！";
            }
            else
            {
                dbMySQL.ExecuteScalar("insert into syscodenumbers (CodeType,CodeNumber) values (@CodeType,@CodeNumber)",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("CodeType",DbType.String,CodeType),
                        DataAccessMySQL.CreateParameter("CodeNumber",DbType.String,CodeNumber)
                    });
                return "OK";
            }

        }
    }
}