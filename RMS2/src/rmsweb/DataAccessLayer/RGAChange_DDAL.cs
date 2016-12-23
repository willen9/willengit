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
    public class RGAChange_DDAL : IRGAChange_DDAL
    {
        public List<RGAChange_D> SearchRGAChange_D(RGAChange_D rGAChange_D)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGAChange_D> objRGAChange_D = new List<RGAChange_D>();

            string sql = @"select * from RGAChange_D where RGAType=@RGAType and RGANo=@RGANo";

            DataTable dtRGAChange_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_D.RGAType),
                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_D.RGANo)
                }); ;

            if (dtRGAChange_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGAChange_D.Rows)
                {
                    RGAChange_D obj = new RGAChange_D();
                    obj.RGAType = dr["RGAType"].ToString();
                    obj.RGANo = dr["RGANo"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();              
                    obj.NewPartNo = dr["NewPartNo"].ToString();
                    obj.NewPartName = dr["NewPartName"].ToString();
                    obj.NewQTY = double.Parse(dr["NewQTY"].ToString() == "" ? "0" : dr["NewQTY"].ToString());
                    obj.NewUnit = dr["NewUnit"].ToString();
                    obj.NewRemark = dr["NewRemark"].ToString();
                    obj.NewRepaired = dr["NewRepaired"].ToString();
                    obj.NewSourceOrderType = dr["NewSourceOrderType"].ToString();
                    obj.NewSourceOrderNo = dr["NewSourceOrderNo"].ToString();
                    obj.NewSourceItemId = dr["NewSourceItemId"].ToString();
                    obj.NewResponseOrderType = dr["NewResponseOrderType"].ToString();
                    obj.NewResponseOrderNo = dr["NewResponseOrderNo"].ToString();
                    obj.NewResponseDate = dr["NewResponseDate"].ToString();
                    obj.OldPartNo = dr["OldPartNo"].ToString();
                    obj.OldPartName = dr["OldPartName"].ToString();
                    obj.OldQTY = double.Parse(dr["OldQTY"].ToString() == "" ? "0" : dr["OldQTY"].ToString());
                    obj.OldUnit = dr["OldUnit"].ToString();
                    obj.OldRemark = dr["OldRemark"].ToString();
                    obj.OldRepaired = dr["OldRepaired"].ToString();
                    obj.OldSourceOrderType = dr["OldSourceOrderType"].ToString();
                    obj.OldSourceOrderNo = dr["OldSourceOrderNo"].ToString();
                    obj.OldSourceItemId = dr["OldSourceItemId"].ToString();
                    obj.OldResponseOrderType = dr["OldResponseOrderType"].ToString();
                    obj.OldResponseOrderNo = dr["OldResponseOrderNo"].ToString();
                    obj.OldResponseDate = dr["OldResponseDate"].ToString();

                    objRGAChange_D.Add(obj);
                }
            }

            return objRGAChange_D;
        }

    }
}