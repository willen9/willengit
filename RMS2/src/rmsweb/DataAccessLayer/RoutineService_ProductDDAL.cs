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
    public class RoutineService_ProductDDAL : IRoutineService_ProductDDAL
    {
        public List<RoutineService_ProductD> SearchRoutineService_ProductD(string RoutineSerOrderType, string RoutineSerOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RoutineService_ProductD> objRoutineService_ProductD = new List<RoutineService_ProductD>();

            string sql = @"select * from RoutineService_ProductD  where RoutineSerOrderType=@RoutineSerOrderType 
            and RoutineSerOrderNo=@RoutineSerOrderNo";


            DataTable dtRoutineService_ProductD = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,RoutineSerOrderType),
                    DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,RoutineSerOrderNo)
                }); ;

            if (dtRoutineService_ProductD.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRoutineService_ProductD.Rows)
                {
                    RoutineService_ProductD obj = new RoutineService_ProductD();
                    obj.RoutineSerOrderType = dr["RoutineSerOrderType"].ToString();
                    obj.RoutineSerOrderNo = dr["RoutineSerOrderNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.QTY = dr["QTY"].ToString();
                    obj.Unit = dr["Unit"].ToString();
                    obj.Remark = dr["Remark"].ToString();

                    objRoutineService_ProductD.Add(obj);
                }
            }

            return objRoutineService_ProductD;
        }

        
    }
}