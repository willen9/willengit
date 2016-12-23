using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class RMA_DDAL : IRMA_DDAL
    {
        public List<RMA_D> SearchRMA_D(RMA_D rMA_D)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RMA_D> objRMA_D = new List<RMA_D>();

            string sql = @"select * from RMA_D where RMAType=@RMAType and RMANo=@RMANo";

            DataTable dtRMA_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RMAType",DbType.String,rMA_D.RMAType),
                    DataAccessMySQL.CreateParameter("RMANo",DbType.String,rMA_D.RMANo)
                }); ;

            if (dtRMA_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRMA_D.Rows)
                {
                    RMA_D obj = new RMA_D();
                    obj.RMAType = dr["RMAType"].ToString();
                    obj.RMANo = dr["RMANo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();               
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.TestResult = dr["TestResult"].ToString();
                    obj.Returned = dr["Returned"].ToString();
                    obj.Closed = dr["Closed"].ToString();
                    obj.RGAType = dr["RGAType"].ToString();
                    obj.RGANo = dr["RGANo"].ToString();

                    objRMA_D.Add(obj);
                }
            }

            return objRMA_D;
        }

        public List<RMA_D> SearchRMA(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RMA_D> objRMA_D = new List<RMA_D>();

            string sql = @"select d.*,o.OrderSName,h.VendorId,v.VendorName,h.Confirmed,h.ConfirmDate,h.Confirmor,e.EmployeeName as ConfirmorName from RMA_D as d 
                left join RMA_H as h on d.RMAType=h.RMAType and d.RMANo=h.RMANo 
                left join Vendor as v on v.VendorId=h.VendorId
                left join OrderCategory as o on o.OrderType=d.RMAType
                left join Employee as e on e.EmployeeId=h.Confirmor
                where 1=1";

            string strCondition = "";
            DbParameter[] dbParameters = new DbParameter[colArray.Length - 1];
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                strCondition += " AND " + colArray[i] + " " + conditionArray[i].Replace("%", "") + " @Parameter" + i + " ";
                if (conditionArray[i] == "like%")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i] + "%");
                }
                else if (conditionArray[i] == "%like")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i]);
                }
                else if (conditionArray[i] == "%like%")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i] + "%");
                }
                else
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i]);
                }
            }

            DataTable dtRMA_D = null;
            if (strCondition != "")
            {
                dtRMA_D = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRMA_D = dbMySQL.ExecuteDataTable(sql);
            }

            if (dtRMA_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRMA_D.Rows)
                {
                    RMA_D obj = new RMA_D();
                    obj.RMAType = dr["RMAType"].ToString();
                    obj.OrderSName = dr["OrderSName"].ToString();
                    obj.RMANo = dr["RMANo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.VendorId = dr["VendorId"].ToString();
                    obj.VendorName = dr["VendorName"].ToString();
                    obj.ConfirmDate = dr["ConfirmDate"].ToString();
                    obj.ConfirmorName = dr["ConfirmorName"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.TestResult = dr["TestResult"].ToString();
                    obj.Returned = dr["Returned"].ToString();
                    obj.Closed = dr["Closed"].ToString();
                    obj.RGAType = dr["RGAType"].ToString();
                    obj.RGANo = dr["RGANo"].ToString();

                    objRMA_D.Add(obj);
                }
            }

            return objRMA_D;
        }
    }
}