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
    public class GroupDAL : IGroup
    {
        DataTable IGroup.SelectGroup(BusinessLayer.Models.Group group, string condition, string condition1)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select * from GroupM where 1=1 ";

            if (group != null)
            {
                string departmentIdParameterValue = "";
                if (!string.IsNullOrEmpty(group.GroupId))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        if (condition == "like1")
                        {
                            sqlStr += " and GroupId like @GroupId ";
                            departmentIdParameterValue = "%" + group.GroupId;
                        }
                        else if (condition == "like2")
                        {
                            sqlStr += " and GroupId like @GroupId ";
                            departmentIdParameterValue = group.GroupId + "%";
                        }
                        else if (condition == "like3")
                        {
                            sqlStr += " and GroupId like @GroupId ";
                            departmentIdParameterValue = "%" + group.GroupId + "%";
                        }
                        else
                        {
                            sqlStr += " and GroupId " + condition + " @GroupId ";
                            departmentIdParameterValue = group.GroupId;
                        }
                    }
                    else
                    {
                        sqlStr += " and GroupId=@GroupId ";
                        departmentIdParameterValue = group.GroupId;
                    }
                }

                string departmentIdParameterValueName = "";
                if (!string.IsNullOrEmpty(group.GroupName))
                {
                    if (!string.IsNullOrEmpty(condition1))
                    {
                        if (condition1 == "like1")
                        {
                            sqlStr += " and GroupName like @GroupName ";
                            departmentIdParameterValueName = "%" + group.GroupName;
                        }
                        else if (condition1 == "like2")
                        {
                            sqlStr += " and GroupName like @GroupName ";
                            departmentIdParameterValueName = group.GroupName + "%";
                        }
                        else if (condition1 == "like3")
                        {
                            sqlStr += " and GroupName like @GroupName ";
                            departmentIdParameterValueName = "%" + group.GroupName + "%";
                        }
                        else
                        {
                            sqlStr += " and GroupName " + condition1 + " @GroupName ";
                            departmentIdParameterValueName = group.GroupName;
                        }
                    }
                    else
                    {
                        sqlStr += " and GroupName=@GroupName ";
                        departmentIdParameterValueName = group.GroupName;
                    }
                }
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("GroupId", DbType.String, departmentIdParameterValue),
                    DataAccessMySQL.CreateParameter("GroupName", DbType.String, departmentIdParameterValueName),
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }
        public bool IsModuleId(string groupId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("Select Count(*) from GroupM where GroupId=@GroupId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("GroupId",DbType.String,groupId)
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
        DataTable IGroup.GetAddressInfo(BusinessLayer.Models.Group tableList)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"Select p.ProgId,p.ProgName,g.ExcuteProg,g.NewData,g.ModiData,g.Confirm,g.Disconfirm,g.DelData,g.Invalid,g.Print,g.Export,g.Cost,g.Price from Program as p left join GroupD as g on p.ProgId = g.ProgId where 1=1 ";
            if (tableList != null)
            {
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("ProgId", DbType.String,tableList.ProgId)
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }
        string IGroup.UpdateCurrency(BusinessLayer.Models.Currency currency)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
              dataAccessMySql.ExecuteScalar("select count(*) from Currency where CurrencyId=@CurrencyId",
                  new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId)
                    }).ToString()) <= 0)
            {
                return "1";
            }
            if (int.Parse(
             dataAccessMySql.ExecuteScalar("select count(*) from Currency where Currency=@Currency and CurrencyId!=@CurrencyId",
                 new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("Currency", DbType.String, currency.CurrencyName),
                        DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId)
                    }).ToString()) > 0)
            {
                return "2";
            }
            dataAccessMySql.ExecuteNonQuery(
                "update Currency set Currency=@Currency,EffectiveDate=@EffectiveDate,ModiDate=@ModiDate,ExchangeRate=@ExchangeRate where CurrencyId=@CurrencyId",
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("Currency", DbType.String,currency.CurrencyName),
                    DataAccessMySQL.CreateParameter("EffectiveDate", DbType.String, currency.EffectiveDate),
                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                    DataAccessMySQL.CreateParameter("ExchangeRate", DbType.String, currency.ExchangeRate),
                    DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId)
                });
            return "0";
        }

        public bool AddGroup(BusinessLayer.Models.Group group, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = null;

            try
            {

                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(
                    @"insert into GroupM (CreateDate,GroupId,GroupName,Remark) VALUES (@CreateDate,@GroupId,@GroupName,@Remark)",
                    tran,
                    new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CreateDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("GroupId", DbType.String, group.GroupId),
                        DataAccessMySQL.CreateParameter("GroupName", DbType.String, group.GroupName),
                        DataAccessMySQL.CreateParameter("Remark", DbType.String, group.Remark)
                    });

                dbMySQL.ExecuteNonQuery(
                    @"insert into GroupD (GroupId,ProgId,ExcuteProg,NewData,ModiData,Confirm,Disconfirm,DelData,Invalid,Print,Export,Cost,Price) 
                      VALUES (@GroupId,@ProgId,@ExcuteProg,@NewData,@ModiData,@Confirm,@Disconfirm,@DelData,@Invalid,@Print,@Export,@Cost,@Price)", tran,
                    new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("GroupId",DbType.String,group.GroupId),
                        DataAccessMySQL.CreateParameter("ProgId",DbType.String,group.ProgId),
                        DataAccessMySQL.CreateParameter("ExcuteProg",DbType.String,group.ExcuteProg),
                        DataAccessMySQL.CreateParameter("NewData",DbType.String,group.NewData),
                        DataAccessMySQL.CreateParameter("ModiData",DbType.String,group.ModiData),
                        DataAccessMySQL.CreateParameter("Confirm",DbType.String,group.Confirm),
                        DataAccessMySQL.CreateParameter("Disconfirm",DbType.String,group.Disconfirm),
                        DataAccessMySQL.CreateParameter("DelData",DbType.String,group.DelData),
                        DataAccessMySQL.CreateParameter("Invalid",DbType.String,group.Invalid),
                        DataAccessMySQL.CreateParameter("Print",DbType.String,group.Print),
                        DataAccessMySQL.CreateParameter("Export",DbType.String,group.Exprt),
                        DataAccessMySQL.CreateParameter("Cost",DbType.String,group.Cost),
                        DataAccessMySQL.CreateParameter("Price",DbType.String,group.Price)
                    });

                tran.Commit();
                msg = "";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        bool IGroup.DeleteGroup(string groupId)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from GroupM where GroupId=@GroupId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("GroupId", DbType.String, groupId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            dataAccessMySql.ExecuteNonQuery("delete from GroupM where  GroupId=@GroupId",
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("GroupId", DbType.String, groupId)
                });
            return true;
        }

        string IGroup.InsertCurrency(BusinessLayer.Models.Currency currency)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
                dataAccessMySql.ExecuteScalar("select count(*) from Currency where CurrencyId=@CurrencyId",
                    new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId)
                    }).ToString()) > 0)
            {
                return "1";
            }
            if (int.Parse(
              dataAccessMySql.ExecuteScalar("select count(*) from Currency where Currency=@Currency",
                  new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("Currency", DbType.String, currency.CurrencyName)
                    }).ToString()) > 0)
            {
                return "2";
            }
            dataAccessMySql.ExecuteNonQuery(
                "insert into Currency(CreateDate,CurrencyId,Currency,EffectiveDate,ExchangeRate) values(@CreateDate,@CurrencyId,@Currency,@EffectiveDate,@ExchangeRate)",
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                    DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId),
                    DataAccessMySQL.CreateParameter("Currency", DbType.String, currency.CurrencyName),
                    DataAccessMySQL.CreateParameter("EffectiveDate", DbType.String, currency.EffectiveDate),
                    DataAccessMySQL.CreateParameter("ExchangeRate", DbType.String, currency.ExchangeRate)
                });
            return "0";
        }

        DataTable IGroup.SearchDetailCurrency(string col, string condition, string value)
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