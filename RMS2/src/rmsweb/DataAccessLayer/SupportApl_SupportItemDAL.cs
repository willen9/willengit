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
    public class SupportApl_SupportItemDAL:ISupportApl_SupportItemDAL
    {
        public List<SupportApl_SupportItem> GetSupportApl_SupportItem(string SupportAplOrderType, string SupportAplOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<SupportApl_SupportItem> objSupportApl_SupportItem = new List<SupportApl_SupportItem>();

            string sql = @"select * from SupportApl_SupportItem where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo";

            DataTable dtSupportApl_SupportItem = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,SupportAplOrderType),
                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,SupportAplOrderNo)
                });
            if (dtSupportApl_SupportItem.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSupportApl_SupportItem.Rows)
                {
                    SupportApl_SupportItem obj = new SupportApl_SupportItem();
                    obj.SupportAplOrderType = dr["SupportAplOrderType"].ToString();
                    obj.SupportAplOrderNo = dr["SupportAplOrderNo"].ToString();
                    obj.ItemNo = dr["ItemNo"].ToString();
                    obj.SupportItemId = dr["SupportItemId"].ToString();
                    obj.SupportItemName = dr["SupportItemName"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objSupportApl_SupportItem.Add(obj);
                }
            }

            return objSupportApl_SupportItem;
        }
    }
}