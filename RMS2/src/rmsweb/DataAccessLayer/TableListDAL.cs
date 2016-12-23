using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer
{
    public class TableListDAL : ITableList
    {
        DataTable ITableList.SelectTableList(BusinessLayer.Models.TableList tableList, string condition, string condition1)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select H.TableId,H.TableName,H.Mode,D.ModuleId,D.ModuleName,H.Remark from TableList_H as H left join Module as D on H.ModuleId=D.ModuleId where 1=1 ";

            if (tableList != null)
            {
                string departmentIdParameterValue = "";
                if (!string.IsNullOrEmpty(tableList.TableId))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        if (condition == "like1")
                        {
                            sqlStr += " and H.TableId like @TableId ";
                            departmentIdParameterValue = "%" + tableList.TableId;
                        }
                        else if (condition == "like2")
                        {
                            sqlStr += " and H.TableId like @TableId ";
                            departmentIdParameterValue = tableList.TableId + "%";
                        }
                        else if (condition == "like3")
                        {
                            sqlStr += " and H.TableId like @TableId ";
                            departmentIdParameterValue = "%" + tableList.TableId + "%";
                        }
                        else
                        {
                            sqlStr += " and H.TableId " + condition + " @TableId ";
                            departmentIdParameterValue = tableList.TableId;
                        }
                    }
                    else
                    {
                        sqlStr += " and H.TableId=@TableId ";
                        departmentIdParameterValue = tableList.TableId;
                    }
                }

                string departmentIdParameterValueName = "";
                if (!string.IsNullOrEmpty(tableList.TableName))
                {
                    if (!string.IsNullOrEmpty(condition1))
                    {
                        if (condition1 == "like1")
                        {
                            sqlStr += " and H.TableName like @TableName ";
                            departmentIdParameterValueName = "%" + tableList.TableName;
                        }
                        else if (condition1 == "like2")
                        {
                            sqlStr += " and H.TableName like @TableName ";
                            departmentIdParameterValueName = tableList.TableName + "%";
                        }
                        else if (condition1 == "like3")
                        {
                            sqlStr += " and H.TableName like @TableName ";
                            departmentIdParameterValueName = "%" + tableList.TableName + "%";
                        }
                        else
                        {
                            sqlStr += " and H.TableName " + condition1 + " @TableName ";
                            departmentIdParameterValueName = tableList.TableName;
                        }
                    }
                    else
                    {
                        sqlStr += " and H.TableName=@TableName ";
                        departmentIdParameterValueName = tableList.TableName;
                    }
                }
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("TableId", DbType.String, departmentIdParameterValue),
                    DataAccessMySQL.CreateParameter("TableName", DbType.String, departmentIdParameterValueName),
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }
        public bool IsModuleId(string tableId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("Select Count(*) from TableList_H where TableId=@TableId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("TableId",DbType.String,tableId)
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
        public bool IsProgramId(string moduleId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("Select Count(*) from Program where ModuleId=@ModuleId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ModuleId",DbType.String,moduleId)
                }).ToString();

            if (int.Parse(count) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        string ITableList.UpdateCurrency(BusinessLayer.Models.TableList tableList,string AddressId,string ColumnNo,string ColumnName,string DataType,string ColLength,string Remark)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = dataAccessMySql.CreateDbTransaction();

            dataAccessMySql.ExecuteNonQuery(
                "update TableList_H set Modifier=@Modifier,TableName=@TableName,ModuleId=@ModuleId,ModiDate=@ModiDate,Mode=@Mode,Remark=@Remark where TableId=@TableId", tran,
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("Modifier", DbType.String,""),
                    DataAccessMySQL.CreateParameter("TableName", DbType.String, tableList.TableName),
                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                    DataAccessMySQL.CreateParameter("ModuleId", DbType.String, tableList.ModuleId),
                    DataAccessMySQL.CreateParameter("Mode", DbType.String, tableList.Mode),
                    DataAccessMySQL.CreateParameter("Remark", DbType.String, tableList.Remark),
                    DataAccessMySQL.CreateParameter("TableId", DbType.String, tableList.TableId)
                });

            dataAccessMySql.ExecuteNonQuery("DELETE FROM TableList_D where TableId=@TableId",tran,new DbParameter[]
            {
                DataAccessMySQL.CreateParameter("TableId", DbType.String,tableList.TableId)
            });

            if (ColumnNo != "" && AddressId != null)
            {
                string[] txtAddressId = AddressId.Split(',');
                string[] txtColumnNo = ColumnNo.Split(',');
                string[] txtColumnName = ColumnName.Split(',');
                string[] txtDataType = DataType.Split(',');
                string[] txtColLength = ColLength.Split(',');
                string[] txtRemark = Remark.Split(',');

                for (int i = 0; i < txtAddressId.Length; i++)
                {
                    if (string.IsNullOrEmpty(txtAddressId[i]))
                    {
                        continue;
                    }

                    dataAccessMySql.ExecuteNonQuery(
                        "insert into TableList_D(Company,UserGroup,Creator,CreateDate,TableId,ItemId,ColumnNo,ColumnName,DataType,ColLength,Remark) values(@Company,@UserGroup,@Creator,@CreateDate,@TableId,@ItemId,@ColumnNo,@ColumnName,@DataType,@ColLength,@Remark)",
                        tran,
                        new DbParameter[]
                        {
                            DataAccessMySQL.CreateParameter("Company", DbType.String, ""),
                            DataAccessMySQL.CreateParameter("UserGroup", DbType.String, ""),
                            DataAccessMySQL.CreateParameter("Creator", DbType.String, ""),
                            DataAccessMySQL.CreateParameter("CreateDate", DbType.String,
                                DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("TableId", DbType.String, tableList.TableId),
                            DataAccessMySQL.CreateParameter("ItemId", DbType.String, txtAddressId[i].ToString()),
                            DataAccessMySQL.CreateParameter("ColumnNo", DbType.String, txtColumnNo[i].ToString()),
                            DataAccessMySQL.CreateParameter("ColumnName", DbType.String, txtColumnName[i].ToString()),
                            DataAccessMySQL.CreateParameter("DataType", DbType.String, txtDataType[i].ToString()),
                            DataAccessMySQL.CreateParameter("ColLength", DbType.String, txtColLength[i].ToString()),
                            DataAccessMySQL.CreateParameter("Remark", DbType.String, txtRemark[i].ToString())
                        });
                }
            }

            tran.Commit();

            return "0";
        }

        bool ITableList.DeleteGroup(string tableId)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = dataAccessMySql.CreateDbTransaction();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from TableList_H where TableId=@TableId", tran,
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("TableId", DbType.String, tableId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            dataAccessMySql.ExecuteNonQuery("delete from TableList_H where  TableId=@TableId", tran,
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("TableId", DbType.String, tableId)
                });
            dataAccessMySql.ExecuteNonQuery("delete from TableList_D where  TableId=@TableId", tran,
              new DbParameter[]
              {
                    DataAccessMySQL.CreateParameter("TableId", DbType.String, tableId)
              });

            tran.Commit();

            return true;
        }
        DataTable ITableList.GetAddressInfo(BusinessLayer.Models.TableList tableList)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select * from TableList_D where TableId=@TableId";
            if (tableList != null)
            {
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("TableId", DbType.String,tableList.TableId)
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }
        string ITableList.InsertCurrency(BusinessLayer.Models.TableList tableList, string AddressId, string ColumnNo,
            string ColumnName, string DataType, string ColLength, string Remark)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = dataAccessMySql.CreateDbTransaction();

            if (int.Parse(
                dataAccessMySql.ExecuteScalar("select count(*) from TableList_H where TableId=@TableId", tran,
                    new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("TableId", DbType.String, tableList.TableId)
                    }).ToString()) > 0)
            {
                return "1";
            }

            dataAccessMySql.ExecuteNonQuery(
                "insert into TableList_H(Company,UserGroup,Creator,CreateDate,TableId,TableName,ModuleId,Mode,Remark) values(@Company,@UserGroup,@Creator,@CreateDate,@TableId,@TableName,@ModuleId,@Mode,@Remark)",
                tran,
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("Company", DbType.String, ""),
                    DataAccessMySQL.CreateParameter("UserGroup", DbType.String, ""),
                    DataAccessMySQL.CreateParameter("Creator", DbType.String, ""),
                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                    DataAccessMySQL.CreateParameter("TableId", DbType.String, tableList.TableId),
                    DataAccessMySQL.CreateParameter("TableName", DbType.String, tableList.TableName),
                    DataAccessMySQL.CreateParameter("ModuleId", DbType.String, tableList.ModuleId),
                    DataAccessMySQL.CreateParameter("Mode", DbType.String, tableList.Mode),
                    DataAccessMySQL.CreateParameter("Remark", DbType.String, tableList.Remark)
                });

            if (ColumnNo != "" && AddressId != null)
            {
                string[] txtAddressId = AddressId.Split(',');
                string[] txtColumnNo = ColumnNo.Split(',');
                string[] txtColumnName = ColumnName.Split(',');
                string[] txtDataType = DataType.Split(',');
                string[] txtColLength = ColLength.Split(',');
                string[] txtRemark = Remark.Split(',');

                for (int i = 0; i < txtAddressId.Length; i++)
                {
                    if (string.IsNullOrEmpty(txtAddressId[i]))
                    {
                        continue;
                    }

                    dataAccessMySql.ExecuteNonQuery(
                        "insert into TableList_D(Company,UserGroup,Creator,CreateDate,TableId,ItemId,ColumnNo,ColumnName,DataType,ColLength,Remark) values(@Company,@UserGroup,@Creator,@CreateDate,@TableId,@ItemId,@ColumnNo,@ColumnName,@DataType,@ColLength,@Remark)",
                        tran,
                        new DbParameter[]
                        {
                            DataAccessMySQL.CreateParameter("Company", DbType.String, ""),
                            DataAccessMySQL.CreateParameter("UserGroup", DbType.String, ""),
                            DataAccessMySQL.CreateParameter("Creator", DbType.String, ""),
                            DataAccessMySQL.CreateParameter("CreateDate", DbType.String,
                                DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("TableId", DbType.String, tableList.TableId),
                            DataAccessMySQL.CreateParameter("ItemId", DbType.String, txtAddressId[i].ToString()),
                            DataAccessMySQL.CreateParameter("ColumnNo", DbType.String, txtColumnNo[i].ToString()),
                            DataAccessMySQL.CreateParameter("ColumnName", DbType.String, txtColumnName[i].ToString()),
                            DataAccessMySQL.CreateParameter("DataType", DbType.String, txtDataType[i].ToString()),
                            DataAccessMySQL.CreateParameter("ColLength", DbType.String, txtColLength[i].ToString()),
                            DataAccessMySQL.CreateParameter("Remark", DbType.String, txtRemark[i].ToString())
                        });
                }
            }

            tran.Commit();

            return "0";
        }

        DataTable ITableList.SearchDetailCurrency(string col, string condition, string value)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            string sqlStr = @"select * from Currency  where 1=1 ";
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
    }
}