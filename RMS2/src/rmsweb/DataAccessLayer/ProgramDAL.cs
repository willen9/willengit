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
    public class ProgramDAL : IProgram
    {
        DataTable IProgram.SelectCurrency(BusinessLayer.Models.Program program,string condition,string condition1)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"Select Program.*,Module.ModuleName from Program left join Module on Program.ModuleId=Module.ModuleId  where 1=1 ";
            if (program != null)
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
                if (!string.IsNullOrEmpty(program.ProgId))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        if (condition == "like1")
                        {
                            sqlStr += " and ProgId like @ProgId ";
                            departmentIdParameterValue = "%" + program.ProgId;
                        }
                        else if (condition == "like2")
                        {
                            sqlStr += " and ProgId like @ProgId ";
                            departmentIdParameterValue = program.ProgId + "%";
                        }
                        else if (condition == "like3")
                        {
                            sqlStr += " and ProgId like @ProgId ";
                            departmentIdParameterValue = "%" + program.ProgId + "%";
                        }
                        else
                        {
                            sqlStr += " and ProgId " + condition + " @ProgId ";
                            departmentIdParameterValue = program.ProgId;
                        }
                    }
                    else
                    {
                        sqlStr += " and ProgId=@ProgId ";
                        departmentIdParameterValue = program.ProgId;
                    }
                }

                string departmentIdParameterValueName = "";
                if (!string.IsNullOrEmpty(program.ProgName))
                {
                    if (!string.IsNullOrEmpty(condition1))
                    {
                        if (condition1 == "like1")
                        {
                            sqlStr += " and ProgName like @ProgName ";
                            departmentIdParameterValueName = "%" + program.ProgName;
                        }
                        else if (condition1 == "like2")
                        {
                            sqlStr += " and ProgName like @ProgName ";
                            departmentIdParameterValueName = program.ProgName + "%";
                        }
                        else if (condition1 == "like3")
                        {
                            sqlStr += " and ProgName like @ProgName ";
                            departmentIdParameterValueName = "%" + program.ProgName + "%";
                        }
                        else
                        {
                            sqlStr += " and ProgName " + condition1 + " @ProgName ";
                            departmentIdParameterValueName = program.ProgName;
                        }
                    }
                    else
                    {
                        sqlStr += " and ProgName=@ProgName ";
                        departmentIdParameterValueName = program.ProgName;
                    }
                }

                //if (!string.IsNullOrEmpty(program.ProgName))
                //{
                //    sqlStr += " and ProgName like @Name ";
                //}
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("ProgId", DbType.String, departmentIdParameterValue),
                    DataAccessMySQL.CreateParameter("ProgName", DbType.String,departmentIdParameterValueName),
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }

        public bool AddProgram(BusinessLayer.Models.Program program, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                    dbMySQL.ExecuteNonQuery(@"insert into Program (ProgId,ProgName,ModuleId,Remark) VALUES (@ProgId,@ProgName,@ModuleId,@Remark)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProgId",DbType.String,program.ProgId),
                            DataAccessMySQL.CreateParameter("ProgName",DbType.String,program.ProgName),
                            DataAccessMySQL.CreateParameter("ModuleId",DbType.String,program.ModuleId),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,program.Remark)
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
        public bool DeleteProgram(string SupportAplProgramId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"delete from Program where ProgId=@ProgId", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ProgId",DbType.String,SupportAplProgramId)
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
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsProgramId(string ProgramId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("Select Count(*) from Program where ProgId=@ProgramId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProgramId",DbType.String,ProgramId)
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

        //public List<BusinessLayer.Models.Program> GetBrand(BusinessLayer.Models.Program program, int Page)
        //{
        //    DataAccessMySQL dbMySQL = new DataAccessMySQL();
        //    dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

        //    List<BusinessLayer.Models.Program> objProgram = new List<BusinessLayer.Models.Program>();

        //    string sql = @"select BrandId,Brand,Remark from Program where 1=1";

        //    if (brand.BrandId != "" && brand.BrandId != null)
        //    {
        //        sql += " and BrandId like @BrandId";
        //    }
        //    if (brand.BrandName != "" && brand.BrandName != null)
        //    {
        //        sql += " and Brand like @Brand";
        //    }

        //    if (Page != 0)
        //    {
        //        sql += " limit @Page1,@Page2";
        //    }

        //    DataTable dtBrand = dbMySQL.ExecuteDataTable(sql,
        //        new DbParameter[]{
        //            DataAccessMySQL.CreateParameter("BrandId",DbType.String,"%"+brand.BrandId+"%"),
        //            DataAccessMySQL.CreateParameter("Brand",DbType.String,"%"+brand.BrandName+"%"),
        //            DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
        //            DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
        //        });
        //    if (dtBrand.Rows.Count > 0)
        //    {

        //        foreach (DataRow dr in dtBrand.Rows)
        //        {
        //            Brand obj = new Brand();
        //            obj.BrandId = dr["BrandId"].ToString();
        //            obj.BrandName = dr["Brand"].ToString();
        //            obj.Remark = dr["Remark"].ToString();
        //            objBrand.Add(obj);
        //        }
        //    }

        //    return objBrand;
        //}
        public BusinessLayer.Models.Program GetProgram(string ProgId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            BusinessLayer.Models.Program program = new BusinessLayer.Models.Program();

            DataTable dtBrand = dbMySQL.ExecuteDataTable(@"select ProgId,ProgName,ModuleId,Remark from Program where ProgId=@ProgId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProgId",DbType.String,ProgId)
                });

            if (dtBrand != null && dtBrand.Rows.Count > 0)
            {
                program.ProgId = dtBrand.Rows[0]["ProgId"].ToString();
                program.ProgName = dtBrand.Rows[0]["ProgName"].ToString();
                program.ModuleId = dtBrand.Rows[0]["ModuleId"].ToString();
                program.Remark = dtBrand.Rows[0]["Remark"].ToString();
                return program;
            }
            else
            {
                return null;
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

        DataTable IProgram.SearchDetailProgram(string col, string condition, string value)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            string sqlStr = @"Select Program.*,Module.ModuleName from Program left join Module on Program.ModuleId=Module.ModuleId  where 1=1";
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

        public bool UpdateProgram(BusinessLayer.Models.Program program)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"update Program set ProgName=@ProgName,ModuleId=@ModuleId,Remark=@Remark where 
                    ProgId=@ProgId ",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ProgId",DbType.String,program.ProgId.Trim()),
                        DataAccessMySQL.CreateParameter("ProgName",DbType.String,program.ProgName.Trim()),
                         DataAccessMySQL.CreateParameter("ModuleId",DbType.String,program.ModuleId.Trim()),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,program.Remark.Trim())
                    });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}