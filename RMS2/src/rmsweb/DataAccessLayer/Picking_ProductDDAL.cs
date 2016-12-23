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
    public class Picking_ProductDDAL : IPicking_ProductDDAL
    {
        public List<Picking_ProductD> GetPicking_ProductD(string PickingOrderType, string PickingOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Picking_ProductD> objPicking_ProductD = new List<Picking_ProductD>();

            DataTable dtPicking_ProductD = dbMySQL.ExecuteDataTable(@"SELECT d.*,p.SerialControl FROM picking_productd as d left join 
                    product as p on d.ProductNo=p.ProductNo WHERE 
                d.PickingOrderType=@PickingOrderType AND d.PickingOrderNo=@PickingOrderNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,PickingOrderType),
                    DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,PickingOrderNo)
                });
            if (dtPicking_ProductD.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPicking_ProductD.Rows)
                {
                    Picking_ProductD obj = new Picking_ProductD();
                    obj.PickingOrderType = dr["PickingOrderType"].ToString();
                    obj.PickingOrderNo = dr["PickingOrderNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.QTY = dr["QTY"].ToString();
                    obj.PickingQTY = dr["PickingQTY"].ToString();
                    obj.Unit = dr["Unit"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.SerialControl = dr["SerialControl"].ToString();
                    objPicking_ProductD.Add(obj);
                }
            }

            return objPicking_ProductD;
        }
    }
}