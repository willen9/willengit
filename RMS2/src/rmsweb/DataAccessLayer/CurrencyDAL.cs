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
    public class CurrencyDAL : ICurrency
    {
        DataTable ICurrency.SelectCurrency(BusinessLayer.Models.Currency currency,string condition)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select * from Currency where 1=1 ";
            if (currency != null)
            {
                if (!string.IsNullOrEmpty(currency.Company))
                {
                    sqlStr += " and Company=@Company ";
                }
                if (!string.IsNullOrEmpty(currency.UserGroup))
                {
                    sqlStr += " and UserGroup=@UserGroup ";
                }
                if (!string.IsNullOrEmpty(currency.Creator))
                {
                    sqlStr += " and Creator=@Creator ";
                }
                string departmentIdParameterValue = "";
                if (!string.IsNullOrEmpty(currency.CurrencyId))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        if (condition == "like1")
                        {
                            sqlStr += " and CurrencyId like @CurrencyId ";
                            departmentIdParameterValue = "%" + currency.CurrencyId;
                        }
                        else if (condition == "like2")
                        {
                            sqlStr += " and CurrencyId like @CurrencyId ";
                            departmentIdParameterValue = currency.CurrencyId +"%";
                        }
                        else if (condition == "like3")
                        {
                            sqlStr += " and CurrencyId like @CurrencyId ";
                            departmentIdParameterValue = "%" + currency.CurrencyId+"%";
                        }
                        else
                        {
                            sqlStr += " and CurrencyId " + condition + " @CurrencyId ";
                            departmentIdParameterValue = currency.CurrencyId;
                        }
                    }
                    else
                    {
                        sqlStr += " and CurrencyId=@CurrencyId ";
                        departmentIdParameterValue = currency.CurrencyId;
                    }
                }
                if (!string.IsNullOrEmpty(currency.CurrencyName))
                {
                    sqlStr += " and Currency like @Name ";
                }
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("Company", DbType.String, currency.Company),
                    DataAccessMySQL.CreateParameter("UserGroup", DbType.String, currency.UserGroup),
                    DataAccessMySQL.CreateParameter("Creator", DbType.String, currency.Creator),
                    DataAccessMySQL.CreateParameter("CurrencyId", DbType.String,departmentIdParameterValue),
                    DataAccessMySQL.CreateParameter("Name", DbType.String, "%" + currency.CurrencyName + "%")
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }
        public bool IsModuleId(string currencyId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("Select Count(*) from Currency where CurrencyId=@CurrencyId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("CurrencyId",DbType.String,currencyId)
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
        string ICurrency.UpdateCurrency(BusinessLayer.Models.Currency currency)
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

        bool ICurrency.DeleteCurrency(BusinessLayer.Models.Currency currency)
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
                return false;
            }
            dataAccessMySql.ExecuteNonQuery("delete from Currency where  CurrencyId=@CurrencyId",
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId)
                });
            return true;
        }

        string ICurrency.InsertCurrency(BusinessLayer.Models.Currency currency)
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

        DataTable ICurrency.SearchDetailCurrency(string col, string condition, string value)
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