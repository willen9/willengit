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
    public class Quotation_DDAL : IQuotation_DDAL
    {
        public List<Quotation_D> SearchQuotation_D(Quotation_D quotation_D)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Quotation_D> objQuotation_D = new List<Quotation_D>();

            string sql = @"select * from Quotation_D where QuotationType=@QuotationType and QuotationNo=@QuotationNo";


            DataTable dtQuotation_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("QuotationType",DbType.String,quotation_D.QuotationType),
                    DataAccessMySQL.CreateParameter("QuotationNo",DbType.String,quotation_D.QuotationNo)
                }); ;

            if (dtQuotation_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtQuotation_D.Rows)
                {
                    Quotation_D obj = new Quotation_D();
                    obj.QuotationType = dr["QuotationType"].ToString();
                    obj.QuotationNo = dr["QuotationNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.PartNo = dr["PartNo"].ToString();               
                    obj.PartName = dr["PartName"].ToString();
                    obj.QTY = double.Parse(dr["QTY"].ToString());
                    obj.Unit = dr["Unit"].ToString();
                    obj.UnitPrice = double.Parse(dr["UnitPrice"].ToString());
                    obj.PreferentialPrice = double.Parse(dr["PreferentialPrice"].ToString());
                    obj.Subtotal = double.Parse(dr["Subtotal"].ToString());
                    obj.Remark = dr["Remark"].ToString();

                    objQuotation_D.Add(obj);
                }
            }

            return objQuotation_D;
        }

        public List<Quotation_D> SearchQuotation_DInfo(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<Quotation_D> objQuotation_D = new List<Quotation_D>();

            string sql = @"select d.*,h.CustomerId,c.CustomerName,h.ConfirmedDate,h.SourceOrderType,h.SourceOrderNo,h.ProductNo from Quotation_D as d 
                left join Quotation_H as h on d.QuotationType=h.QuotationType and d.QuotationNo=h.QuotationNo
                left join Customer as c on c.CustomerId=h.CustomerId where 1=1";

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


            DataTable dtQuotation_D = null;
            if (strCondition != "")
            {
                dtQuotation_D = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtQuotation_D = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtQuotation_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtQuotation_D.Rows)
                {
                    Quotation_D obj = new Quotation_D();
                    obj.QuotationType = dr["QuotationType"].ToString();
                    obj.QuotationNo = dr["QuotationNo"].ToString();
                    obj.PartNo = dr["PartNo"].ToString();
                    obj.PartName = dr["PartName"].ToString();
                    obj.QTY = double.Parse(dr["QTY"].ToString());
                    obj.Unit = dr["Unit"].ToString();
                    obj.UnitPrice = double.Parse(dr["UnitPrice"].ToString());
                    obj.PreferentialPrice = double.Parse(dr["PreferentialPrice"].ToString());
                    obj.Subtotal = double.Parse(dr["Subtotal"].ToString());
                    obj.Remark = dr["Remark"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.ConfirmedDate = dr["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ConfirmedDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    objQuotation_D.Add(obj);
                }
            }

            return objQuotation_D;
        }
    }
}