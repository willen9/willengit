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
    public class RoutineService_ProcessDDAL : IRoutineService_ProcessDDAL
    {
        public List<RoutineService_ProcessD> SearchRoutineService_ProcessD(string RoutineSerOrderType, string RoutineSerOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RoutineService_ProcessD> objRoutineService_ProcessD = new List<RoutineService_ProcessD>();

            string sql = @"select * from RoutineService_ProcessD  where RoutineSerOrderType=@RoutineSerOrderType 
            and RoutineSerOrderNo=@RoutineSerOrderNo";


            DataTable dtRoutineService_ProcessD = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,RoutineSerOrderType),
                    DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,RoutineSerOrderNo)
                }); ;

            if (dtRoutineService_ProcessD.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRoutineService_ProcessD.Rows)
                {
                    RoutineService_ProcessD obj = new RoutineService_ProcessD();
                    obj.RoutineSerOrderType = dr["RoutineSerOrderType"].ToString();
                    obj.RoutineSerOrderNo = dr["RoutineSerOrderNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ProcessDate = dr["ProcessDate"].ToString();
                    obj.ProcessExplanation = dr["ProcessExplanation"].ToString();
                    obj.ProcessMan = dr["ProcessMan"].ToString();
                    obj.Remark = dr["Remark"].ToString();

                    objRoutineService_ProcessD.Add(obj);
                }
            }

            return objRoutineService_ProcessD;
        }

        
    }
}