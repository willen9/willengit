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
    public class Position_DDAL : IPosition_DDAL
    {
        public List<Position_D> SearchPosition_D(Position_D position_D)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Position_D> objPosition_D = new List<Position_D>();

            string sql = @"select * from Position_D as d left join Employee as e on e.EmployeeId=d.EmployeeId where 1=1";

            if(!String.IsNullOrEmpty(position_D.PositionNo))
            {
                sql += " and PositionNo=@PositionNo";
            }
            
            DataTable dtPosition_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("PositionNo",DbType.String,position_D.PositionNo)
                }); ;

            if (dtPosition_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPosition_D.Rows)
                {
                    Position_D obj = new Position_D();
                    obj.PositionNo = dr["PositionNo"].ToString();
                    obj.EmployeeId = dr["EmployeeId"].ToString();
                    obj.EmployeeName = dr["EmployeeName"].ToString();
                    obj.Remark = dr["Remark"].ToString();

                    objPosition_D.Add(obj);
                }
            }

            return objPosition_D;
        }
        
        public List<Position_D> Position_DList(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Position_D> objPosition_D = new List<Position_D>();

            string sql = @"select distinct d.EmployeeId,e.EmployeeName,e.DepartmentId,de.DepartmentName from Position_D as d 
                left join Employee as e on e.EmployeeId=d.EmployeeId
                left join Department as de on de.DepartmentId=e.DepartmentId  where 1=1 ";

            string[] colArray = Col.Split(',');
            string[] conditionArray = Condition.Split(',');
            string[] valueArray = conditionValue.Split(',');

            string strCondition = "";
            int len = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]))
                {
                    len++;
                }
            }

            DbParameter[] dbParameters = new DbParameter[len + 1];
            int dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                strCondition += " AND " + colArray[i] + " " + conditionArray[i].Replace("%", "") + " @Parameter" + i + " ";

                if (conditionArray[i] == "like%")
                {
                    dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i] + "%");
                }
                else if (conditionArray[i] == "%like")
                {
                    dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i]);
                }
                else if (conditionArray[i] == "%like%")
                {
                    dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i] + "%");
                }
                else
                {
                    dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i]);
                }

                dblen++;
            }
            if (Page != 0)
            {
                strCondition += " limit @Page1,@Page2";
            }
            dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
            dbParameters[dblen] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);

            DataTable dtPosition_D = null;
            if (strCondition != "")
            {

                dtPosition_D = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtPosition_D = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }


            if (dtPosition_D.Rows.Count > 0)
            {

                foreach (DataRow dr in dtPosition_D.Rows)
                {
                    Position_D obj = new Position_D();
                    obj.EmployeeId = dr["EmployeeId"].ToString();
                    obj.EmployeeName = dr["EmployeeName"].ToString();
                    obj.DepartmentId = dr["DepartmentId"].ToString();
                    obj.DepartmentName = dr["DepartmentName"].ToString();
                    objPosition_D.Add(obj);
                }
            }

            return objPosition_D;
        }
    }
}