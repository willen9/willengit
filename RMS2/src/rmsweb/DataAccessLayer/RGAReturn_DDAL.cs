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
    public class RGAReturn_DDAL : IRGAReturn_DDAL
    {
        public List<RGAReturn_D> SearchRGAReturn_D(RGAReturn_D rGAReturn_D)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGAReturn_D> objRGAReturn_D = new List<RGAReturn_D>();

            string sql = @"select * from RGAReturn_D where RGAReturnType=@RGAReturnType and RGAReturnNo=@RGAReturnNo";

            DataTable dtRGAReturn_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RGAReturnType",DbType.String,rGAReturn_D.RGAReturnType),
                    DataAccessMySQL.CreateParameter("RGAReturnNo",DbType.String,rGAReturn_D.RGAReturnNo)
                }); ;

            if (dtRGAReturn_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGAReturn_D.Rows)
                {
                    RGAReturn_D obj = new RGAReturn_D();
                    obj.RGAReturnType = dr["RGAReturnType"].ToString();
                    obj.RGAReturnNo = dr["RGAReturnNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.RGAType = dr["RGAType"].ToString();
                    obj.RGANo = dr["RGANo"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.TestResult = dr["TestResult"].ToString();
                    obj.InternalRemark = dr["InternalRemark"].ToString();
                    obj.Remark = dr["Remark"].ToString();

                    objRGAReturn_D.Add(obj);
                }
            }

            return objRGAReturn_D;
        }

    }
}