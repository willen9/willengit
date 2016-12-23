using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer
{
    public class EmployeeDAL : IEmployee
    {
        DataTable IEmployee.SelectEmployee(BusinessLayer.Models.Employee employee,string condition)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select * from employee left join department on employee.DepartmentId=department.DepartmentId  where 1=1 ";
            if (employee != null)
            {
                if (!string.IsNullOrEmpty(employee.Company))
                {
                    sqlStr += " and Company=@Company ";
                }
                if (!string.IsNullOrEmpty(employee.UserGroup))
                {
                    sqlStr += " and UserGroup=@UserGroup ";
                }
                if (!string.IsNullOrEmpty(employee.Creator))
                {
                    sqlStr += " and Creator=@Creator ";
                }
                string departmentIdParameterValue = "";
                if (!string.IsNullOrEmpty(employee.EmployeeId))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        if (condition == "like1")
                        {
                            sqlStr += " and EmployeeId like @EmployeeId ";
                            departmentIdParameterValue = "%" + employee.EmployeeId;
                        }
                        else if (condition == "like2")
                        {
                            sqlStr += " and EmployeeId like @EmployeeId ";
                            departmentIdParameterValue = employee.EmployeeId + "%";
                        }
                        else if (condition == "like3")
                        {
                            sqlStr += " and EmployeeId like @EmployeeId ";
                            departmentIdParameterValue = "%" + employee.EmployeeId + "%";
                        }
                        else
                        {
                            sqlStr += " and EmployeeId " + condition + " @EmployeeId ";
                            departmentIdParameterValue = employee.EmployeeId;
                        }
                    }
                    else
                    {
                        sqlStr += " and EmployeeId=@EmployeeId ";
                        departmentIdParameterValue = employee.EmployeeId;
                    }
                }
                if (!string.IsNullOrEmpty(employee.EmployeeName))
                {
                    sqlStr += " and EmployeeName like @Name ";
                }

                sqlStr += " ORDER BY EmployeeId";
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("Company", DbType.String, employee.Company),
                    DataAccessMySQL.CreateParameter("UserGroup", DbType.String, employee.UserGroup),
                    DataAccessMySQL.CreateParameter("Creator", DbType.String, employee.Creator),
                    DataAccessMySQL.CreateParameter("EmployeeId", DbType.String, departmentIdParameterValue),
                    DataAccessMySQL.CreateParameter("Name", DbType.String, "%" + employee.EmployeeName + "%")
                });
            }
            sqlStr += " ORDER BY EmployeeId";
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }

        public bool IsModuleId(string employeeId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("Select Count(*) from employee where EmployeeId=@EmployeeId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("EmployeeId",DbType.String,employeeId)
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
        string IEmployee.UpdateEmployee(BusinessLayer.Models.Employee employee)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
              dataAccessMySql.ExecuteScalar("select count(*) from employee where EmployeeId=@EmployeeId",
                  new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("EmployeeId", DbType.String, employee.EmployeeId)
                    }).ToString()) <= 0)
            {
                return "1";
            }
            if (int.Parse(
             dataAccessMySql.ExecuteScalar("select count(*) from employee where EmployeeName=@EmployeeName and EmployeeId!=@EmployeeId",
                 new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("EmployeeName", DbType.String, employee.EmployeeName),
                        DataAccessMySQL.CreateParameter("EmployeeId", DbType.String, employee.EmployeeId)
                    }).ToString()) > 0)
            {
                return "2";
            }
            dataAccessMySql.ExecuteNonQuery(
                "update employee set EmployeeName=@EmployeeName,TelNo=@TelNo,ModiDate=@ModiDate,MobilePhone=@MobilePhone,DepartmentId=@DepartmentId,Address=@Address where EmployeeId=@EmployeeId",
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("EmployeeName", DbType.String, employee.EmployeeName),
                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, employee.TelNo),
                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                    DataAccessMySQL.CreateParameter("MobilePhone", DbType.String, employee.MobilePhone),
                    DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, employee.DepartmentId),
                    DataAccessMySQL.CreateParameter("Address", DbType.String,employee.Address),
                    DataAccessMySQL.CreateParameter("EmployeeId", DbType.String, employee.EmployeeId)
                });
            return "0";
        }

        bool IEmployee.DeleteEmployee(BusinessLayer.Models.Employee employee)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from employee where EmployeeId=@EmployeeId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("EmployeeId", DbType.String, employee.EmployeeId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            dataAccessMySql.ExecuteNonQuery("delete from employee where  EmployeeId=@EmployeeId",
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("EmployeeId", DbType.String, employee.EmployeeId)
                });
            return true;
        }

        string IEmployee.InsertEmployee(BusinessLayer.Models.Employee employee)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
                dataAccessMySql.ExecuteScalar("select count(*) from employee where EmployeeId=@EmployeeId",
                    new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("EmployeeId", DbType.String, employee.EmployeeId)
                    }).ToString()) > 0)
            {
                return "1";
            }
            //if (int.Parse(
            //  dataAccessMySql.ExecuteScalar("select count(*) from employee where EmployeeName=@EmployeeName",
            //      new DbParameter[]
            //        {
            //            DataAccessMySQL.CreateParameter("EmployeeName", DbType.String, employee.EmployeeName)
            //        }).ToString()) > 0)
            //{
            //    return "2";
            //}
            dataAccessMySql.ExecuteNonQuery(
                "insert into employee(CreateDate,DepartmentId,EmployeeName,EmployeeId,TelNo,MobilePhone,Address) values(@CreateDate,@DepartmentId,@EmployeeName,@EmployeeId,@TelNo,@MobilePhone,@Address)",
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                    DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, employee.DepartmentId),
                    DataAccessMySQL.CreateParameter("EmployeeName", DbType.String, employee.EmployeeName),
                    DataAccessMySQL.CreateParameter("EmployeeId", DbType.String, employee.EmployeeId),
                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, employee.TelNo),
                    DataAccessMySQL.CreateParameter("MobilePhone", DbType.String, employee.MobilePhone),
                    DataAccessMySQL.CreateParameter("Address", DbType.String, employee.Address)
                });
            return "0";
        }

        DataTable IEmployee.SearchDetailEmployee(string col, string condition, string value)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            string sqlStr = @"select * from employee left join department on employee.DepartmentId=department.DepartmentId  where 1=1 ";
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
                return dataAccessMySql.ExecuteDataTable(sqlStr + strCondition, dbParameters);
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }

        bool IEmployee.ImportFile(string path)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dataAccessMySql.CreateDbTransaction();
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    //StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("Big5"));
                    StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                    //StreamReader sr = new StreamReader(fs, GetFileEncodeType(path));
                    bool isFirstRow = true;
                    while (sr.Peek() > -1)
                    {
                        string[] valueArray = sr.ReadLine().Split(',');
                        if (isFirstRow)
                        {
                            if (valueArray.Length < 6)
                            {
                                return false;
                            }
                            isFirstRow = false;
                            continue;
                        }
                        if (int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from employee where EmployeeId=@EmployeeId",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("EmployeeId", DbType.String, valueArray[0].Trim())
                                }).ToString()) > 0)
                        {
                            dataAccessMySql.ExecuteNonQuery("update employee set EmployeeName=@EmployeeName,TelNo=@TelNo,ModiDate=@ModiDate,MobilePhone=@MobilePhone,DepartmentId=@DepartmentId,Address=@Address  where EmployeeId=@EmployeeId",
                            tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("EmployeeName", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("MobilePhone", DbType.String, valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("Address", DbType.String, valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("EmployeeId", DbType.String, valueArray[0].Trim())
                                });
                        }
                        else
                        {
                            dataAccessMySql.ExecuteNonQuery(
                                "insert into employee(CreateDate,DepartmentId,EmployeeName,EmployeeId,TelNo,MobilePhone,Address) values(@CreateDate,@DepartmentId,@EmployeeName,@EmployeeId,@TelNo,@MobilePhone,@Address)",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("DepartmentId", DbType.String, valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("EmployeeName", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("EmployeeId", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("MobilePhone", DbType.String, valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("Address", DbType.String, valueArray[5].Trim())
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

        public System.Text.Encoding GetFileEncodeType(string filename)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);

            Byte[] buffer = br.ReadBytes(2);

            if (buffer[0] >= 0xEF)
            {
                if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                {
                    return System.Text.Encoding.UTF8;
                }
                else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                {
                    return System.Text.Encoding.BigEndianUnicode;
                }
                else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                {
                    return System.Text.Encoding.Unicode;
                }
                else
                {
                    return System.Text.Encoding.Default;
                }

            }
            else
            {
                return System.Text.Encoding.Default;
            }
        }

        string IEmployee.GetEmployeeName(string EmployeeId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            object GetEmployeeName = dbMySQL.ExecuteScalar("select EmployeeName from employee where EmployeeId=@EmployeeId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("EmployeeId",DbType.String,EmployeeId)
                });
            if (GetEmployeeName != null)
            {
                return "YES-" + GetEmployeeName.ToString();
            }
            else
            {
                return "NO-人員不存在!";
            }

        }

        List<BusinessLayer.Models.Employee> IEmployee.GetEmployee(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<BusinessLayer.Models.Employee> objEmployee = new List<BusinessLayer.Models.Employee>();

            string sql = @"select e.*,d.DepartmentName from employee as e left join Department as d 
                on e.DepartmentId=d.DepartmentId where 1=1 ";

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

            DataTable dtEmployee = null;
            if (strCondition != "")
            {

                dtEmployee = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtEmployee = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }


            if (dtEmployee.Rows.Count > 0)
            {

                foreach (DataRow dr in dtEmployee.Rows)
                {
                    BusinessLayer.Models.Employee obj = new BusinessLayer.Models.Employee();
                    obj.EmployeeId = dr["EmployeeId"].ToString();
                    obj.EmployeeName = dr["EmployeeName"].ToString();
                    obj.DepartmentId = dr["DepartmentId"].ToString();
                    obj.DepartmentName = dr["DepartmentName"].ToString();
                    objEmployee.Add(obj);
                }
            }

            return objEmployee;
        }
    }
}