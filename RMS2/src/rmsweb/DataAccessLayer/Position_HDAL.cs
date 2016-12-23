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
    public class Position_HDAL : IPosition_HDAL
    {
        public bool AddPosition_H(Position_H position_H, string strEmployeeId,
             string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from Position_H where 
                    PositionNo=@PositionNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PositionNo",DbType.String,position_H.PositionNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into Position_H (Company,UserGroup,Creator,CreateDate,
                        PositionNo,Position,PositionCategory,Remark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @PositionNo,@Position,@PositionCategory,@Remark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,position_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,position_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,position_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("PositionNo",DbType.String,position_H.PositionNo),
                            DataAccessMySQL.CreateParameter("Position",DbType.String,position_H.Position),
                            DataAccessMySQL.CreateParameter("PositionCategory",DbType.String,position_H.PositionCategory),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,position_H.Remark)
                        });

                    string sql = "";

                    if (strEmployeeId != "" && strEmployeeId != null)
                    {
                        string[] strEmployeeIdArray = strEmployeeId.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');

                        sql = @"insert into Position_D (Company,UserGroup,Creator,
                        CreateDate,PositionNo,EmployeeId,Remark,PositionCategory) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@PositionNo,@EmployeeId,@Remark,@PositionCategory)";


                        for (int i = 0; i < strEmployeeIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strEmployeeIdArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,position_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,position_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,position_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("PositionNo",DbType.String,position_H.PositionNo),
                                DataAccessMySQL.CreateParameter("EmployeeId",DbType.String,strEmployeeIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PositionCategory",DbType.String,position_H.PositionCategory)
                            });
                        }
                    }                        
                    tran.Commit();
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "資料已存在！";
                    return false;
                }

            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                msg = ex.Message;
                return false;
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
        }

        public bool UpdatePosition_H(Position_H position_H, string strEmployeeId,
            string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                string PositionCategory = "";

                switch (position_H.PositionCategory)
                {
                    case "1.工程":
                        PositionCategory = "1";
                        break;
                    case "3.業務":
                        PositionCategory = "3";
                        break;
                }

                dbMySQL.ExecuteNonQuery(@"update Position_H set Modifier=@Modifier,ModiDate=@ModiDate,Position=@Position,PositionCategory=@PositionCategory,
                         Remark=@Remark
                         where PositionNo=@PositionNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,position_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("PositionNo",DbType.String,position_H.PositionNo),
                            DataAccessMySQL.CreateParameter("Position",DbType.String,position_H.Position),
                            DataAccessMySQL.CreateParameter("PositionCategory",DbType.String,PositionCategory),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,position_H.Remark)
                        });

                dbMySQL.ExecuteNonQuery("delete from Position_D where PositionNo=@PositionNo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PositionNo",DbType.String,position_H.PositionNo)
                    });
                string sql = "";

                if (strEmployeeId != "" && strEmployeeId != null)
                {
                    string[] strEmployeeIdArray = strEmployeeId.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');

                    sql = @"insert into Position_D (Company,UserGroup,Creator,
                        CreateDate,PositionNo,EmployeeId,Remark,PositionCategory) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@PositionNo,@EmployeeId,@Remark,@PositionCategory)";


                    for (int i = 0; i < strEmployeeIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strEmployeeIdArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,position_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,position_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,position_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("PositionNo",DbType.String,position_H.PositionNo),
                                DataAccessMySQL.CreateParameter("EmployeeId",DbType.String,strEmployeeIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PositionCategory",DbType.String,PositionCategory)
                            });
                    }
                }

                tran.Commit();
                msg = "";
                return true;

            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                msg = ex.Message;
                return false;
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
        }

        public bool DelPosition_H(Position_H position_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery("delete from Position_H where PositionNo=@PositionNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("PositionNo",DbType.String,position_H.PositionNo)
                        });
                dbMySQL.ExecuteNonQuery("delete from Position_D where PositionNo=@PositionNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("PositionNo",DbType.String,position_H.PositionNo)
                        });
                tran.Commit();
                msg = "";
                return true;

            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                msg = ex.Message;
                return false;
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
        }

        public List<Position_H> SearchPosition_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<Position_H> objPosition_H = new List<Position_H>();

            string sql = @"select * from Position_H  where 1=1";

            string strCondition = "";
            DbParameter[] dbParameters = new DbParameter[colArray.Length - 1];
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                strCondition += " AND " + colArray[i] + " " + conditionArray[i].Replace("%", "") + " @Parameter" + i + " ";
                if (conditionArray[i] == "like%")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i] + "%");
                }
                else if (conditionArray[i] == "%like")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i]);
                }
                else if (conditionArray[i] == "%like%")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i] + "%");
                }
                else
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i]);
                }
            }


            DataTable dtPosition_H = null;
            if (strCondition != "")
            {
                dtPosition_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtPosition_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtPosition_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPosition_H.Rows)
                {
                    Position_H obj = new Position_H();
                    obj.PositionNo = dr["PositionNo"].ToString();
                    obj.Position = dr["Position"].ToString();
                    obj.PositionCategory = dr["PositionCategory"].ToString()=="1"? "1.工程": "3.業務";
                    obj.Remark = dr["Remark"].ToString();
                    objPosition_H.Add(obj);
                }
            }

            return objPosition_H;
        }

        public Position_H GetPosition_HInfo(Position_H position_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            Position_H obj = null;

            DataTable dtPosition_H = dbMySQL.ExecuteDataTable(@"select * from Position_H where 
                    PositionNo=@PositionNo",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PositionNo",DbType.String,position_H.PositionNo)
                    });

            if (dtPosition_H.Rows.Count > 0)
            {
                obj = new Position_H();
                obj.PositionNo = dtPosition_H.Rows[0]["PositionNo"].ToString();
                obj.Position = dtPosition_H.Rows[0]["Position"].ToString();
                obj.PositionCategory = dtPosition_H.Rows[0]["PositionCategory"].ToString();
                obj.Remark = dtPosition_H.Rows[0]["Remark"].ToString();
            }

            return obj;
        }

        public bool IsPositionNo(Position_H position_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from Position_H where 
                    PositionNo=@PositionNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PositionNo",DbType.String,position_H.PositionNo)
                    }).ToString()) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        

        public bool ImportFile(string path)
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
                            if (valueArray.Length < 8)
                            {
                                return false;
                            }
                            isFirstRow = false;
                            continue;
                        }
                        if (int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from Position_H where PositionNo=@PositionNo",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("PositionNo", DbType.String, valueArray[0].Trim()),
                                }).ToString()) == 0)
                        {
                            dataAccessMySql.ExecuteNonQuery(
                                   @"insert into Position_H(PositionNo,Position,PositionCategory,Remark) values
                                    (@PositionNo,@Position,@PositionCategory,@Remark)",
                                  tran,
                                   new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("PositionNo", DbType.String,valueArray[0].Trim()),
                                        DataAccessMySQL.CreateParameter("Position", DbType.String, valueArray[1].Trim()),
                                        DataAccessMySQL.CreateParameter("PositionCategory", DbType.String, valueArray[2].Trim()),
                                        DataAccessMySQL.CreateParameter("Remark", DbType.String, valueArray[3].Trim())
                                    });
                        }
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

    }
}