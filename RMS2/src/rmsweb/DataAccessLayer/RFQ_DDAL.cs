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
    public class RFQ_DDAL : IRFQ_DDAL
    {
        public List<RFQ_D> SearchRFQ_D(RFQ_D rFQ_D)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RFQ_D> objRFQ_D = new List<RFQ_D>();

            string sql = @"select * from RFQ_D where RFQType=@RFQType and RFQNo=@RFQNo";

            DataTable dtRFQ_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RFQType",DbType.String,rFQ_D.RFQType),
                    DataAccessMySQL.CreateParameter("RFQNo",DbType.String,rFQ_D.RFQNo)
                }); ;

            if (dtRFQ_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRFQ_D.Rows)
                {
                    RFQ_D obj = new RFQ_D();
                    obj.RFQType = dr["RFQType"].ToString();
                    obj.RFQNo = dr["RFQNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();               
                    obj.PartNo = dr["PartNo"].ToString();
                    obj.PartName = dr["PartName"].ToString();
                    obj.QTY =double.Parse(dr["QTY"].ToString());
                    obj.Unit = dr["Unit"].ToString();
                    obj.UnitPrice = double.Parse(dr["UnitPrice"].ToString());
                    obj.Price = double.Parse(dr["Price"].ToString());
                    obj.ListPrice = double.Parse(dr["ListPrice"].ToString());
                    obj.Remark = dr["Remark"].ToString();
                    obj.Repaired = dr["Repaired"].ToString();

                    objRFQ_D.Add(obj);
                }
            }

            return objRFQ_D;
        }

        public List<RFQ_D> SearchRFQ_DInfo(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RFQ_D> objRFQ_D = new List<RFQ_D>();

            string sql = @"select d.*,h.VendorId,v.VendorName,h.ConfirmedDate,h.SourceOrderType,h.SourceOrderNo,h.ProductNo from RFQ_D as d 
                left join RFQ_H as h on d.RFQType=h.RFQType and d.RFQNo=h.RFQNo
                left join Vendor as v on v.VendorId=h.VendorId where 1=1";

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


            DataTable dtRFQ_D = null;
            if (strCondition != "")
            {
                dtRFQ_D = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRFQ_D = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtRFQ_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRFQ_D.Rows)
                {
                    RFQ_D obj = new RFQ_D();
                    obj.RFQType = dr["RFQType"].ToString();
                    obj.RFQNo = dr["RFQNo"].ToString();
                    obj.PartNo = dr["PartNo"].ToString();
                    obj.PartName = dr["PartName"].ToString();
                    obj.QTY = double.Parse(dr["QTY"].ToString());
                    obj.Unit = dr["Unit"].ToString();
                    obj.UnitPrice = double.Parse(dr["UnitPrice"].ToString());
                    obj.Price = double.Parse(dr["Price"].ToString());
                    obj.ListPrice = double.Parse(dr["ListPrice"].ToString());
                    obj.Remark = dr["Remark"].ToString();
                    obj.Repaired = dr["Repaired"].ToString();
                    obj.VendorId = dr["VendorId"].ToString();
                    obj.VendorName = dr["VendorName"].ToString();
                    obj.ConfirmedDate = dr["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ConfirmedDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    objRFQ_D.Add(obj);
                }
            }

            return objRFQ_D;
        }
    }
}