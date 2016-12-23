using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer
{
    public class DepartmentDAL : IDepartmentDAL
    {
        DataTable IDepartmentDAL.SelectDepartment(Department department, string condition)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select * from Department where 1=1 ";
            if (department != null)
            {
                if (!string.IsNullOrEmpty(department.Company))
                {
                    sqlStr += " and Company=@Company ";
                }
                if (!string.IsNullOrEmpty(department.UserGroup))
                {
                    sqlStr += " and UserGroup=@UserGroup ";
                }
                if (!string.IsNullOrEmpty(department.Creator))
                {
                    sqlStr += " and Creator=@Creator ";
                }
                string departmentIdParameterValue = "";
                if (!string.IsNullOrEmpty(department.DepartmentId))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        if (condition == "like1")
                        {
                            sqlStr += " and DepartmentId like @DepartmentId ";
                            departmentIdParameterValue = "%" + department.DepartmentId;
                        }
                        else if (condition == "like2")
                        {
                            sqlStr += " and DepartmentId like @DepartmentId ";
                            departmentIdParameterValue = department.DepartmentId + "%";
                        }
                        else if (condition == "like3")
                        {
                            sqlStr += " and DepartmentId like @DepartmentId ";
                            departmentIdParameterValue ="%"+ department.DepartmentId+"%";
                        }
                        else
                        {
                            sqlStr += " and DepartmentId "+condition+" @DepartmentId ";
                            departmentIdParameterValue = department.DepartmentId;
                        }
                    }
                    else
                    {
                         sqlStr += " and DepartmentId=@DepartmentId ";
                         departmentIdParameterValue=department.DepartmentId;
                    }
                }
                if (!string.IsNullOrEmpty(department.DepartmentName))
                {
                    sqlStr += " and DepartmentName like @DepartmentName ";
                }
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("Company", DbType.String, department.Company),
                    DataAccessMySQL.CreateParameter("UserGroup", DbType.String, department.UserGroup),
                    DataAccessMySQL.CreateParameter("Creator", DbType.String, department.Creator),
                    DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, departmentIdParameterValue),
                    DataAccessMySQL.CreateParameter("DepartmentName", DbType.String, "%" + department.DepartmentName + "%")
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }

        public bool IsModuleId(string deptid)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("Select Count(*) from Department where DepartmentId=@DepartmentId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("DepartmentId",DbType.String,deptid)
                }).ToString();

            if (int.Parse(count) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Department> GetDepartment(Department department, string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Department> objDepartment = new List<Department>();

            string sql = @"select * from Department where 1=1 ";

            //if (!string.IsNullOrEmpty(department.DepartmentId))
            //{
            //    sql += " and DepartmentId like @DepartmentId ";
            //}
            //if (!string.IsNullOrEmpty(department.DepartmentName))
            //{
            //    sql += " and DepartmentName like @DepartmentName ";
            //}

            //if (Page != 0)
            //{
            //    sql += " limit @Page1,@Page2";
            //}

            //DataTable dtDepartment = dbMySQL.ExecuteDataTable(sql,
            //    new DbParameter[]{
            //        DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, "%" +department.DepartmentId+ "%"),
            //        DataAccessMySQL.CreateParameter("DepartmentName", DbType.String, "%" + department.DepartmentName + "%"),
            //        DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
            //        DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
            //    });

            string strCondition = "";
            DbParameter[] dbParameters = null;
            if (!String.IsNullOrEmpty(conditionValue))
            {
                dbParameters = new DbParameter[3];
                strCondition += " AND " + Col + " " + Condition.Replace("%", "") + " @Parameter" + 0 + " ";
                if (Page != 0)
                {
                    strCondition += " limit @Page1,@Page2";
                }
                if (Condition == "like%")
                {
                    dbParameters[0] = DataAccessMySQL.CreateParameter("Parameter" + 0, DbType.String, conditionValue + "%");
                }
                else if (Condition == "%like")
                {
                    dbParameters[0] = DataAccessMySQL.CreateParameter("Parameter" + 0, DbType.String, "%" + conditionValue);
                }
                else if (Condition == "%like%")
                {
                    dbParameters[0] = DataAccessMySQL.CreateParameter("Parameter" + 0, DbType.String, "%" + conditionValue + "%");
                }
                else
                {
                    dbParameters[0] = DataAccessMySQL.CreateParameter("Parameter" + 0, DbType.String, conditionValue);
                }
                dbParameters[1] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
                dbParameters[2] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);
            }
            else
            {
                dbParameters = new DbParameter[2];
                if (Page != 0)
                {
                    sql += " limit @Page1,@Page2";
                }
                dbParameters[0] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
                dbParameters[1] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);
            }

            DataTable dtDepartment = null;
            if (strCondition != "")
            {

                dtDepartment = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtDepartment = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }
            if (dtDepartment.Rows.Count > 0)
            {

                foreach (DataRow dr in dtDepartment.Rows)
                {
                    Department obj = new Department();
                    obj.DepartmentId = dr["DepartmentId"].ToString();
                    obj.DepartmentName = dr["DepartmentName"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objDepartment.Add(obj);
                }
            }

            return objDepartment;
        }
        
        string IDepartmentDAL.UpdateDepartment(BusinessLayer.Models.Department department)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
              dataAccessMySql.ExecuteScalar("select count(*) from Department where DepartmentId=@DepartmentId",
                  new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, department.DepartmentId)
                    }).ToString()) <=0)
            {
                return "1";
            }
            if (int.Parse(
             dataAccessMySql.ExecuteScalar("select count(*) from Department where DepartmentName=@DepartmentName and DepartmentId!=@DepartmentId",
                 new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("DepartmentName", DbType.String, department.DepartmentName),
                        DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, department.DepartmentId)
                    }).ToString()) > 0)
            {
                return "2";
            }
            dataAccessMySql.ExecuteNonQuery(
                "update Department set DepartmentName=@DepartmentName,Remark=@Remark,ModiDate=@ModiDate where DepartmentId=@DepartmentId",
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("DepartmentName", DbType.String, department.DepartmentName),
                    DataAccessMySQL.CreateParameter("Remark", DbType.String, department.Remark),
                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                    DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, department.DepartmentId)
                });
            return "0";
        }

        string IDepartmentDAL.InsertDepartment(BusinessLayer.Models.Department department)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
                dataAccessMySql.ExecuteScalar("select count(*) from Department where DepartmentId=@DepartmentId",
                    new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, department.DepartmentId)
                    }).ToString()) > 0)
            {
                return "1";
            }
            if (int.Parse(
              dataAccessMySql.ExecuteScalar("select count(*) from Department where DepartmentName=@DepartmentName",
                  new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("DepartmentName", DbType.String, department.DepartmentName)
                    }).ToString()) > 0)
            {
                return "2";
            }
            dataAccessMySql.ExecuteNonQuery(
                "insert into Department(CreateDate,DepartmentId,DepartmentName,Remark) values(@CreateDate,@DepartmentId,@DepartmentName,@Remark)",
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                    DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, department.DepartmentId),
                    DataAccessMySQL.CreateParameter("DepartmentName", DbType.String, department.DepartmentName),
                    DataAccessMySQL.CreateParameter("Remark", DbType.String, department.Remark)
                });
            return "0";
        }

        bool IDepartmentDAL.DeleteDepartment(Department department)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from Department where DepartmentId=@DepartmentId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, department.DepartmentId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            dataAccessMySql.ExecuteNonQuery("delete from Department where  DepartmentId=@DepartmentId",
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, department.DepartmentId)
                });
            return true;
        }

        DataTable IDepartmentDAL.SearchDetailDepartment(string col, string condition, string value)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            string sqlStr = @"select * from Department where 1=1 ";
            string strCondition = "";
            int len = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]))
                {
                    len++;
                }
            }

            DbParameter[] dbParameters = new DbParameter[len - 1];
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
            if (strCondition != "")
            {
                return dataAccessMySql.ExecuteDataTable(sqlStr+strCondition,dbParameters);
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }
    }
}