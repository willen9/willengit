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
    public class Substitutes_HDAL: ISubstitutes_HDAL
    {
        public bool AddSubstitutes_H(Substitutes_H substitutes_H, string strSubstitutesNo,
            string strQTY, string strPriority, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from Substitutes_H where 
                    ComponentNo=@ComponentNo and MajorComponentNo=@MajorComponentNo and SubstitutesType=@SubstitutesType", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,substitutes_H.ComponentNo),
                        DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,substitutes_H.MajorComponentNo),
                        DataAccessMySQL.CreateParameter("SubstitutesType",DbType.String,substitutes_H.SubstitutesType)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into Substitutes_H (Company,UserGroup,Creator,CreateDate,
                        ComponentNo,MajorComponentNo,SubstitutesType,Remark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @ComponentNo,@MajorComponentNo,@SubstitutesType,@Remark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,substitutes_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,substitutes_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,substitutes_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,substitutes_H.ComponentNo),
                            DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,substitutes_H.MajorComponentNo),
                            DataAccessMySQL.CreateParameter("SubstitutesType",DbType.String,substitutes_H.SubstitutesType),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,substitutes_H.Remark)
                        });

                    string sql = "";

                    if (strSubstitutesNo != "" && strSubstitutesNo != null)
                    {
                        string[] strSubstitutesNoArray = strSubstitutesNo.Split(',');
                        string[] strQTYArray = strQTY.Split(',');
                        string[] strPriorityArray = strPriority.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');

                        sql = @"insert into Substitutes_D (Company,UserGroup,Creator,
                        CreateDate,ComponentNo,MajorComponentNo,SubstitutesType,SubstitutesNo,QTY,Priority,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@ComponentNo,@MajorComponentNo,@SubstitutesType,@SubstitutesNo,@QTY,@Priority,@Remark)";


                        for (int i = 0; i < strSubstitutesNoArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strSubstitutesNoArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,substitutes_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,substitutes_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,substitutes_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,substitutes_H.ComponentNo),
                                DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,substitutes_H.MajorComponentNo),
                                DataAccessMySQL.CreateParameter("SubstitutesType",DbType.String,substitutes_H.SubstitutesType),
                                DataAccessMySQL.CreateParameter("SubstitutesNo",DbType.String,strSubstitutesNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.Double,double.Parse(strQTYArray[i].ToString()==""?"0":strQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Priority",DbType.String,strPriorityArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
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

        public bool UpdateSubstitutes_H(Substitutes_H substitutes_H, string strSubstitutesNo,
            string strQTY, string strPriority, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update Substitutes_H set Modifier=@Modifier,ModiDate=@ModiDate,Remark=@Remark
                         where ComponentNo=@ComponentNo and MajorComponentNo=@MajorComponentNo and 
                         SubstitutesType=@SubstitutesType", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,substitutes_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,substitutes_H.Remark),
                            DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,substitutes_H.ComponentNo),
                            DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,substitutes_H.MajorComponentNo),
                            DataAccessMySQL.CreateParameter("SubstitutesType",DbType.String,substitutes_H.SubstitutesType)
                        });

                dbMySQL.ExecuteNonQuery(@"delete from Substitutes_D where ComponentNo=@ComponentNo and MajorComponentNo=@MajorComponentNo and 
                         SubstitutesType = @SubstitutesType",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,substitutes_H.ComponentNo),
                            DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,substitutes_H.MajorComponentNo),
                            DataAccessMySQL.CreateParameter("SubstitutesType",DbType.String,substitutes_H.SubstitutesType)
                    });
                string sql = "";

                if (strSubstitutesNo != "" && strSubstitutesNo != null)
                {
                    string[] strSubstitutesNoArray = strSubstitutesNo.Split(',');
                    string[] strQTYArray = strQTY.Split(',');
                    string[] strPriorityArray = strPriority.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');

                    sql = @"insert into Substitutes_D (Company,UserGroup,Creator,
                        CreateDate,ComponentNo,MajorComponentNo,SubstitutesType,SubstitutesNo,QTY,Priority,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@ComponentNo,@MajorComponentNo,@SubstitutesType,@SubstitutesNo,@QTY,@Priority,@Remark)";


                    for (int i = 0; i < strSubstitutesNoArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strSubstitutesNoArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,substitutes_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,substitutes_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,substitutes_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,substitutes_H.ComponentNo),
                                DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,substitutes_H.MajorComponentNo),
                                DataAccessMySQL.CreateParameter("SubstitutesType",DbType.String,substitutes_H.SubstitutesType),
                                DataAccessMySQL.CreateParameter("SubstitutesNo",DbType.String,strSubstitutesNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.Double,double.Parse(strQTYArray[i].ToString()==""?"0":strQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Priority",DbType.String,strPriorityArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
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

        public bool DelSubstitutes_H(Substitutes_H substitutes_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery("delete from Substitutes_H where ComponentNo=@ComponentNo and MajorComponentNo=@MajorComponentNo and SubstitutesType=@SubstitutesType",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,substitutes_H.ComponentNo),
                            DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,substitutes_H.MajorComponentNo),
                            DataAccessMySQL.CreateParameter("SubstitutesType",DbType.String,substitutes_H.SubstitutesType)
                        });
                dbMySQL.ExecuteNonQuery("delete from Substitutes_D where ComponentNo=@ComponentNo and MajorComponentNo=@MajorComponentNo and SubstitutesType=@SubstitutesType",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,substitutes_H.ComponentNo),
                            DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,substitutes_H.MajorComponentNo),
                            DataAccessMySQL.CreateParameter("SubstitutesType",DbType.String,substitutes_H.SubstitutesType)
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

        public List<Substitutes_H> SearchSubstitutes_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<Substitutes_H> objSubstitutes_H = new List<Substitutes_H>();

            string sql = @"select h.*,p.ProductName as ProductNameComponentNo,
                p.Specification as SpecificationComponentNo,p.Unit as UnitComponentNo,
                p2.ProductName as ProductNameMajorComponentNo,
                p2.Specification as SpecificationMajorComponentNo,p2.Unit as UnitMajorComponentNo from Substitutes_H as h 
                left join Product as p on p.ProductNo=h.ComponentNo
                left join Product as p2 on p2.ProductNo=h.MajorComponentNo where 1=1";

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


            DataTable dtSubstitutes_H = null;
            if (strCondition != "")
            {
                dtSubstitutes_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtSubstitutes_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtSubstitutes_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSubstitutes_H.Rows)
                {
                    Substitutes_H obj = new Substitutes_H();
                    obj.ComponentNo = dr["ComponentNo"].ToString();
                    obj.MajorComponentNo = dr["MajorComponentNo"].ToString();
                    obj.SubstitutesType = dr["SubstitutesType"].ToString()=="1"? "1.取代料件": "2.替代料件";
                    obj.Remark = dr["Remark"].ToString();
                    obj.ProductNameComponentNo = dr["ProductNameComponentNo"].ToString();
                    obj.SpecificationComponentNo = dr["SpecificationComponentNo"].ToString();
                    obj.UnitComponentNo = dr["UnitComponentNo"].ToString();
                    obj.ProductNameMajorComponentNo = dr["ProductNameMajorComponentNo"].ToString();
                    obj.SpecificationMajorComponentNo = dr["SpecificationMajorComponentNo"].ToString();
                    obj.UnitMajorComponentNo = dr["UnitMajorComponentNo"].ToString();
                    obj.Confirmed = dr["Confirmed"].ToString();
                    objSubstitutes_H.Add(obj);
                }
            }

            return objSubstitutes_H;
        }

        public Substitutes_H GetSubstitutes_HInfo(Substitutes_H substitutes_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            Substitutes_H obj  = new Substitutes_H();

            DataTable dtSubstitutes_H = dbMySQL.ExecuteDataTable(@"select h.*,p.ProductName as ProductNameComponentNo,
                    p.Specification as SpecificationComponentNo,p.Unit as UnitComponentNo,
                    p2.ProductName as ProductNameMajorComponentNo,
                    p2.Specification as SpecificationMajorComponentNo,p2.Unit as UnitMajorComponentNo from Substitutes_H as h 
                    left join Product as p on p.ProductNo=h.ComponentNo
                    left join Product as p2 on p2.ProductNo=h.MajorComponentNo where 
                    ComponentNo=@ComponentNo and MajorComponentNo=@MajorComponentNo and SubstitutesType=@SubstitutesType",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,substitutes_H.ComponentNo),
                        DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,substitutes_H.MajorComponentNo),
                        DataAccessMySQL.CreateParameter("SubstitutesType",DbType.String,substitutes_H.SubstitutesType)
                    });

            if (dtSubstitutes_H.Rows.Count > 0)
            {
                
                obj.ComponentNo = dtSubstitutes_H.Rows[0]["ComponentNo"].ToString();
                obj.MajorComponentNo = dtSubstitutes_H.Rows[0]["MajorComponentNo"].ToString();
                obj.SubstitutesType = dtSubstitutes_H.Rows[0]["SubstitutesType"].ToString();
                obj.Remark = dtSubstitutes_H.Rows[0]["Remark"].ToString();
                obj.ProductNameComponentNo = dtSubstitutes_H.Rows[0]["ProductNameComponentNo"].ToString();
                obj.SpecificationComponentNo = dtSubstitutes_H.Rows[0]["SpecificationComponentNo"].ToString();
                obj.UnitComponentNo = dtSubstitutes_H.Rows[0]["UnitComponentNo"].ToString();
                obj.ProductNameMajorComponentNo = dtSubstitutes_H.Rows[0]["ProductNameMajorComponentNo"].ToString();
                obj.SpecificationMajorComponentNo = dtSubstitutes_H.Rows[0]["SpecificationMajorComponentNo"].ToString();
                obj.UnitMajorComponentNo = dtSubstitutes_H.Rows[0]["UnitMajorComponentNo"].ToString();
                obj.Confirmed= dtSubstitutes_H.Rows[0]["Confirmed"].ToString();
            }

            return obj;
        }

        public bool IsSubstitutes_H(Substitutes_H substitutes_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if(int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from Substitutes_H where 
                    ComponentNo=@ComponentNo and MajorComponentNo=@MajorComponentNo and SubstitutesType=@SubstitutesType",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,substitutes_H.ComponentNo),
                        DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,substitutes_H.MajorComponentNo),
                        DataAccessMySQL.CreateParameter("SubstitutesType",DbType.String,substitutes_H.SubstitutesType)
                    }).ToString())>0)
            {
                return false;
            }else
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
                            if (valueArray.Length < 5)
                            {
                                return false;
                            }
                            isFirstRow = false;
                            continue;
                        }
                        if (int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from Substitutes_H where ComponentNo=@ComponentNo and MajorComponentNo=@MajorComponentNo and SubstitutesType=@SubstitutesType",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ComponentNo", DbType.String, valueArray[0]),
                                    DataAccessMySQL.CreateParameter("MajorComponentNo", DbType.String, valueArray[1]),
                                    DataAccessMySQL.CreateParameter("SubstitutesType", DbType.String, valueArray[2]),
                                }).ToString()) == 0)
                        {
                            if (int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from Product where ProductNo=@ProductNo",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String, valueArray[0]),
                                }).ToString()) >0&& 
                                int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from Product where ProductNo=@ProductNo",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String, valueArray[1]),
                                }).ToString()) > 0)
                            {
                                dataAccessMySql.ExecuteNonQuery(
                                   @"insert into Substitutes_H(ComponentNo,MajorComponentNo,SubstitutesType,Remark) values
                                    (@ComponentNo,@MajorComponentNo,@SubstitutesType,@Remark)",
                                  tran,
                                   new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("ComponentNo", DbType.String, valueArray[0]),
                                        DataAccessMySQL.CreateParameter("MajorComponentNo", DbType.String, valueArray[1]),
                                        DataAccessMySQL.CreateParameter("SubstitutesType", DbType.String, valueArray[2]),
                                        DataAccessMySQL.CreateParameter("Remark", DbType.String, valueArray[3]),
                                    });
                            }else
                            {
                                continue;
                            }
                        }
                        if (int.Parse(
                            dataAccessMySql.ExecuteScalar(@"select count(*) from Substitutes_H where 
                                ComponentNo=@ComponentNo and MajorComponentNo=@MajorComponentNo and 
                                SubstitutesType=@SubstitutesType and SubstitutesNo=@SubstitutesNo and Priority=@Priority",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ComponentNo", DbType.String, valueArray[0]),
                                    DataAccessMySQL.CreateParameter("MajorComponentNo", DbType.String, valueArray[1]),
                                    DataAccessMySQL.CreateParameter("SubstitutesType", DbType.String, valueArray[2]),
                                    DataAccessMySQL.CreateParameter("SubstitutesNo", DbType.String, valueArray[4]),
                                    DataAccessMySQL.CreateParameter("Priority", DbType.String, valueArray[5]),
                                }).ToString()) == 0)
                        {
                           
                            if (int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from Product where ProductNo=@ProductNo",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String, valueArray[0]),
                                }).ToString()) > 0)
                            {
                                dataAccessMySql.ExecuteNonQuery(
                                   @"insert into Substitutes_D(ComponentNo,MajorComponentNo,SubstitutesType,SubstitutesNo,QTY,Priority,Remark) values
                                    (@ComponentNo,@MajorComponentNo,@SubstitutesType,@SubstitutesNo,@QTY,@Priority,@Remark)",
                                  tran,
                                   new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("ComponentNo", DbType.String, valueArray[0]),
                                        DataAccessMySQL.CreateParameter("MajorComponentNo", DbType.String, valueArray[1]),
                                        DataAccessMySQL.CreateParameter("SubstitutesType", DbType.String, valueArray[2]),
                                        DataAccessMySQL.CreateParameter("SubstitutesNo", DbType.String, valueArray[4]),
                                        DataAccessMySQL.CreateParameter("QTY", DbType.String, valueArray[6]),
                                        DataAccessMySQL.CreateParameter("Priority", DbType.String, valueArray[5]),
                                        DataAccessMySQL.CreateParameter("Remark", DbType.String, valueArray[7])
                                    });
                            }
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

        public bool ConfirmedSubstitutes_H(Substitutes_H substitutes_H, out string msg)
        {
            try
            {
                DataAccessMySQL dbMySQL = new DataAccessMySQL();
                dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

                dbMySQL.ExecuteNonQuery("update Substitutes_H set Confirmed=@Confirmed,Confirmor=@Confirmor,ConfirmDate=@ConfirmDate WHERE ComponentNo=@ComponentNo and MajorComponentNo=@MajorComponentNo and SubstitutesType=@SubstitutesType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,substitutes_H.ComponentNo),
                            DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,substitutes_H.MajorComponentNo),
                            DataAccessMySQL.CreateParameter("SubstitutesType",DbType.String,substitutes_H.SubstitutesType),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,substitutes_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,substitutes_H.Confirmor),
                            DataAccessMySQL.CreateParameter("ConfirmDate",DbType.String,substitutes_H.ConfirmDate)
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
    }
}