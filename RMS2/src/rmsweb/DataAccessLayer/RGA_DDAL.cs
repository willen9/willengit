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
    public class RGA_DDAL : IRGA_DDAL
    {
        public List<RGA_D> SearchRGA_D(RGA_D rGA_D)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGA_D> objRGA_D = new List<RGA_D>();

            string sql = @"select * from RGA_D where RGAType=@RGAType and RGANo=@RGANo";

            DataTable dtRGA_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_D.RGAType),
                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_D.RGANo)
                }); ;

            if (dtRGA_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGA_D.Rows)
                {
                    RGA_D obj = new RGA_D();
                    obj.RGAType = dr["RGAType"].ToString();
                    obj.RGANo = dr["RGANo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();              
                    obj.PartNo = dr["PartNo"].ToString();
                    obj.PartName = dr["PartName"].ToString();
                    obj.QTY = double.Parse(dr["QTY"].ToString() == "" ? "0" : dr["QTY"].ToString());
                    obj.Unit = dr["Unit"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.Repaired = dr["Repaired"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.SourceItemId = dr["SourceItemId"].ToString();
                    obj.ResponseOrderType = dr["ResponseOrderType"].ToString();
                    obj.ResponseOrderNo = dr["ResponseOrderNo"].ToString();
                    obj.ResponseDate = dr["ResponseDate"].ToString();

                    objRGA_D.Add(obj);
                }
            }

            return objRGA_D;
        }

    }
}