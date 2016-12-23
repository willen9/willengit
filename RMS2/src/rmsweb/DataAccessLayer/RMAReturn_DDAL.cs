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
    public class RMAReturn_DDAL : IRMAReturn_DDAL
    {
        public List<RMAReturn_D> SearchRMAReturn_D(RMAReturn_D rMAReturn_D)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RMAReturn_D> objRMAReturn_D = new List<RMAReturn_D>();

            string sql = @"select * from RMAReturn_D where RMAReturnType=@RMAReturnType and RMAReturnNo=@RMAReturnNo";

            DataTable dtRMAReturn_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RMAReturnType",DbType.String,rMAReturn_D.RMAReturnType),
                    DataAccessMySQL.CreateParameter("RMAReturnNo",DbType.String,rMAReturn_D.RMAReturnNo)
                }); ;

            if (dtRMAReturn_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRMAReturn_D.Rows)
                {
                    RMAReturn_D obj = new RMAReturn_D();
                    obj.RMAReturnType = dr["RMAReturnType"].ToString();
                    obj.RMAReturnNo = dr["RMAReturnNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();               
                    obj.RMAType = dr["RMAType"].ToString();
                    obj.RMANo = dr["RMANo"].ToString();
                    obj.RMAItemId = dr["RMAItemId"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.Returned = dr["Returned"].ToString();
                    obj.RepairDetail = dr["RepairDetail"].ToString();
                    obj.MalfunctionReason = dr["MalfunctionReason"].ToString();
                    obj.RGAType = dr["RGAType"].ToString();
                    obj.RGANo = dr["RGANo"].ToString();

                    objRMAReturn_D.Add(obj);
                }
            }

            return objRMAReturn_D;
        }

    }
}