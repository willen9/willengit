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
    public class SupportApl_ProductDDAL : ISupportApl_ProductDDAL
    {
        public List<SupportApl_ProductD> GetSupportApl_ProductD(string SupportAplOrderType, string SupportAplOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<SupportApl_ProductD> objSupportApl_ProductD = new List<SupportApl_ProductD>();

            string sql = @"select d.*,p.SerialControl from SupportApl_ProductD as d left join 
                    product as p on d.ProductNo=p.ProductNo where SupportAplOrderType=@SupportAplOrderType 
                    and SupportAplOrderNo=@SupportAplOrderNo";


            DataTable dtSupportApl_ProductD = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,SupportAplOrderType),
                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,SupportAplOrderNo)
                });
            if (dtSupportApl_ProductD.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSupportApl_ProductD.Rows)
                {
                    SupportApl_ProductD obj = new SupportApl_ProductD();
                    obj.SupportAplOrderType = dr["SupportAplOrderType"].ToString();
                    obj.SupportAplOrderNo = dr["SupportAplOrderNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.QTY = dr["QTY"].ToString();
                    obj.PickingQTY = dr["PickingQTY"].ToString();
                    obj.Unit = dr["Unit"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.SerialControl = dr["SerialControl"].ToString();
                    
                    objSupportApl_ProductD.Add(obj);
                }
            }

            return objSupportApl_ProductD;
        }
    }
}