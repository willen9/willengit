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
    public class ModuleDAL : IModule
    {
        DataTable IModule.SelectModule(BusinessLayer.Models.Module module, string condition,string condition1)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"Select ModuleId,ModuleName,Remark from Module where 1=1 ";
            if (module != null)
            {
                //if (!string.IsNullOrEmpty(program.ProgId))
                //{
                //    sqlStr += " and ProgId=@ProgId ";
                //}
                //if (!string.IsNullOrEmpty(program.ProgName))
                //{
                //    sqlStr += " and ProgName=@ProgName ";
                //}
                string departmentIdParameterValue = "";
                if (!string.IsNullOrEmpty(module.ModuleId))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        if (condition == "like1")
                        {
                            sqlStr += " and ModuleId like @ModuleId ";
                            departmentIdParameterValue = "%" + module.ModuleId;
                        }
                        else if (condition == "like2")
                        {
                            sqlStr += " and ModuleId like @ModuleId ";
                            departmentIdParameterValue = module.ModuleId + "%";
                        }
                        else if (condition == "like3")
                        {
                            sqlStr += " and ModuleId like @ModuleId ";
                            departmentIdParameterValue = "%" + module.ModuleId + "%";
                        }
                        else
                        {
                            sqlStr += " and ModuleId " + condition + " @ModuleId ";
                            departmentIdParameterValue = module.ModuleId;
                        }
                    }
                    else
                    {
                        sqlStr += " and ModuleId=@ModuleId ";
                        departmentIdParameterValue = module.ModuleId;
                    }
                }

                string departmentIdParameterValueName = "";
                if (!string.IsNullOrEmpty(module.ModuleName))
                {
                    if (!string.IsNullOrEmpty(condition1))
                    {
                        if (condition1 == "like1")
                        {
                            sqlStr += " and ModuleName like @ModuleName ";
                            departmentIdParameterValueName = "%" + module.ModuleName;
                        }
                        else if (condition1 == "like2")
                        {
                            sqlStr += " and ModuleName like @ModuleName ";
                            departmentIdParameterValueName = module.ModuleName + "%";
                        }
                        else if (condition1 == "like3")
                        {
                            sqlStr += " and ModuleName like @ModuleName ";
                            departmentIdParameterValueName = "%" + module.ModuleName + "%";
                        }
                        else
                        {
                            sqlStr += " and ModuleName " + condition1 + " @ModuleName ";
                            departmentIdParameterValueName = module.ModuleName;
                        }
                    }
                    else
                    {
                        sqlStr += " and ModuleName=@ModuleName ";
                        departmentIdParameterValueName = module.ModuleName;
                    }
                }

                //if (!string.IsNullOrEmpty(program.ProgName))
                //{
                //    sqlStr += " and ProgName like @Name ";
                //}
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("ModuleId", DbType.String, departmentIdParameterValue),
                    DataAccessMySQL.CreateParameter("ModuleName", DbType.String,departmentIdParameterValueName),
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }

        public bool AddModule(BusinessLayer.Models.Module module, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"insert into module (ModuleId,ModuleName,Remark) VALUES (@ModuleId,@ModuleName,@Remark)",
                    new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ModuleId",DbType.String,module.ModuleId),
                             DataAccessMySQL.CreateParameter("ModuleName",DbType.String,module.ModuleName),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,module.Remark)
                    });
                msg = "";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        public bool UpdateModule(BusinessLayer.Models.Module module)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"update Module set ModuleName=@ModuleName,Remark=@Remark where ModuleId=@ModuleId",
                    new DbParameter[]{
                         DataAccessMySQL.CreateParameter("ModuleId",DbType.String,module.ModuleId.Trim()),
                         DataAccessMySQL.CreateParameter("ModuleName",DbType.String,module.ModuleName.Trim()),
                         DataAccessMySQL.CreateParameter("Remark",DbType.String,module.Remark.Trim())
                    });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsModuleId(string ModuleId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("Select Count(*) from Module where ModuleId=@ModuleId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ModuleId",DbType.String,ModuleId)
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
        public bool DeleteProgram(string SupportAplProgramId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                if (Convert.ToInt32(dbMySQL.ExecuteScalar("Select Count(*) from Program left join Module on Program.ModuleId=Module.ModuleId where Module.ModuleId=@ModuleId", tran, new DbParameter[] {
                    DataAccessMySQL.CreateParameter("ModuleId",DbType.String,SupportAplProgramId)
                }).ToString()) > 0)
                {
                    return false;
                }

                dbMySQL.ExecuteNonQuery(@"delete from Module where ModuleId=@ModuleId", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ModuleId",DbType.String,SupportAplProgramId)
                    });

                tran.Commit();

                return true;
            }
            catch
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
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

        //string IProgram.UpdateCurrency(BusinessLayer.Models.Program program)
        //{
        //    DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
        //    dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

        //    if (int.Parse(
        //      dataAccessMySql.ExecuteScalar("select count(*) from Currency where CurrencyId=@CurrencyId",
        //          new DbParameter[]
        //            {
        //                DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId)
        //            }).ToString()) <= 0)
        //    {
        //        return "1";
        //    }
        //    if (int.Parse(
        //     dataAccessMySql.ExecuteScalar("select count(*) from Currency where Currency=@Currency and CurrencyId!=@CurrencyId",
        //         new DbParameter[]
        //            {
        //                DataAccessMySQL.CreateParameter("Currency", DbType.String, currency.CurrencyName),
        //                DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId)
        //            }).ToString()) > 0)
        //    {
        //        return "2";
        //    }
        //    dataAccessMySql.ExecuteNonQuery(
        //        "update Currency set Currency=@Currency,EffectiveDate=@EffectiveDate,ModiDate=@ModiDate,ExchangeRate=@ExchangeRate where CurrencyId=@CurrencyId",
        //        new DbParameter[]
        //        {
        //            DataAccessMySQL.CreateParameter("Currency", DbType.String,currency.CurrencyName),
        //            DataAccessMySQL.CreateParameter("EffectiveDate", DbType.String, currency.EffectiveDate),
        //            DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
        //            DataAccessMySQL.CreateParameter("ExchangeRate", DbType.String, currency.ExchangeRate),
        //            DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId)
        //        });
        //    return "0";
        //}

        //bool IProgram.DeleteCurrency(BusinessLayer.Models.Program program)
        //{
        //    DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
        //    dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

        //    if (int.Parse(
        //       dataAccessMySql.ExecuteScalar("select count(*) from Currency where CurrencyId=@CurrencyId",
        //           new DbParameter[]
        //            {
        //                DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId)
        //            }).ToString()) <= 0)
        //    {
        //        return false;
        //    }
        //    dataAccessMySql.ExecuteNonQuery("delete from Currency where  CurrencyId=@CurrencyId",
        //        new DbParameter[]
        //        {
        //            DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId)
        //        });
        //    return true;
        //}

        //string IProgram.InsertCurrency(BusinessLayer.Models.Program program)
        //{
        //    DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
        //    dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

        //    if (int.Parse(
        //        dataAccessMySql.ExecuteScalar("select count(*) from Currency where CurrencyId=@CurrencyId",
        //            new DbParameter[]
        //            {
        //                DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId)
        //            }).ToString()) > 0)
        //    {
        //        return "1";
        //    }
        //    if (int.Parse(
        //      dataAccessMySql.ExecuteScalar("select count(*) from Currency where Currency=@Currency",
        //          new DbParameter[]
        //            {
        //                DataAccessMySQL.CreateParameter("Currency", DbType.String, currency.CurrencyName)
        //            }).ToString()) > 0)
        //    {
        //        return "2";
        //    }
        //    dataAccessMySql.ExecuteNonQuery(
        //        "insert into Currency(CreateDate,CurrencyId,Currency,EffectiveDate,ExchangeRate) values(@CreateDate,@CurrencyId,@Currency,@EffectiveDate,@ExchangeRate)",
        //        new DbParameter[]
        //        {
        //            DataAccessMySQL.CreateParameter("CreateDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
        //            DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, currency.CurrencyId),
        //            DataAccessMySQL.CreateParameter("Currency", DbType.String, currency.CurrencyName),
        //            DataAccessMySQL.CreateParameter("EffectiveDate", DbType.String, currency.EffectiveDate),
        //            DataAccessMySQL.CreateParameter("ExchangeRate", DbType.String, currency.ExchangeRate)
        //        });
        //    return "0";
        //}

        DataTable IModule.SearchDetailModule(string col, string condition, string value)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            string sqlStr = @"select * from Module  where 1=1 ";
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