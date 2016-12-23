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
    public class ScheduleDAL : IScheduleDAL
    {

        public bool ImportFile(string path, Schedule schedule)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dataAccessMySql.CreateDbTransaction();
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(fs);
                    bool isFirstRow = true;
                    while (sr.Peek() > -1)
                    {
                        string[] valueArray = sr.ReadLine().Split(',');
                        if (isFirstRow)
                        {
                            if (valueArray.Length <4)
                            {
                                return false;
                            }
                            isFirstRow = false;
                            continue;
                        }
                        string strGUID = System.Guid.NewGuid().ToString(); //直接返回字符串类型
                        dataAccessMySql.ExecuteNonQuery(
                            @"insert into Schedule(Company,UserGroup,Creator,CreateDate,guID,Title,StartDate,EndDate,Holiday,InCounting) values
                            (@Company,@UserGroup,@Creator,@CreateDate,@guID,@Title,@StartDate,@EndDate,@Holiday,@InCounting)",
                            tran,
                            new DbParameter[]
                            {
                                DataAccessMySQL.CreateParameter("Company", DbType.String,schedule.Company),
                                DataAccessMySQL.CreateParameter("UserGroup", DbType.String,schedule.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator", DbType.String,schedule.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate", DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("guID", DbType.String,strGUID),
                                DataAccessMySQL.CreateParameter("Title", DbType.String, valueArray[0].Trim()),
                                DataAccessMySQL.CreateParameter("StartDate", DbType.String, valueArray[1].Trim()),
                                DataAccessMySQL.CreateParameter("EndDate", DbType.String, ""),
                                DataAccessMySQL.CreateParameter("Holiday", DbType.String, valueArray[2].Trim()),
                                DataAccessMySQL.CreateParameter("InCounting", DbType.String, valueArray[3].Trim()),
                            });
                    }
                    tran.Commit();
                    sr.Close();
                    sr.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
        }

        public List<Schedule> SearchSchedule(string month)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Schedule> objSchedule = new List<Schedule>();

            string sql = @"select * from Schedule where 1=1";

            if (!String.IsNullOrEmpty(month))
            {
                sql += " and StartDate like @month";
            }

            DataTable dtSchedule = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("month",DbType.String,month+"%"),
                }); ;

            if (dtSchedule.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSchedule.Rows)
                {
                    Schedule obj = new Schedule();
                    obj.guID = dr["guID"].ToString();
                    obj.Title = dr["Title"].ToString();
                    obj.StartDate = dr["StartDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["StartDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    obj.EndDate = dr["EndDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["EndDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    obj.Holiday = dr["Holiday"].ToString();
                    obj.InCounting = dr["InCounting"].ToString();
                    objSchedule.Add(obj);
                }
            }

            return objSchedule;
        }

        public void DelSchedule(string id)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            dbMySQL.ExecuteNonQuery("delete from Schedule where guID=@guID",
                new DbParameter[] {
                    DataAccessMySQL.CreateParameter("guID",DbType.String,id)
                });
        }
    }
}