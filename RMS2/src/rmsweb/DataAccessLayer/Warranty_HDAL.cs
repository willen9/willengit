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
    public class Warranty_HDAL : IWarranty_HDAL
    {
        public bool AddWarranty_H(Warranty_H warranty_H, string strChangeDate, 
            string strChangeOrderType, string strChangeOrderNo,string strChangeOrderItemId ,
            string strTargetId, string strTargetName,
            string strWarrantyPeriod, string strWarrantySDate, string strWarrantyEDate, 
            string strWarrantyType, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from Warranty_H where 
                    ProductNo=@ProductNo and SerialNo=@SerialNo",tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                        DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into Warranty_H (Company,UserGroup,Creator,CreateDate,
                        ProductNo,SerialNo,Remark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @ProductNo,@SerialNo,@Remark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,warranty_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,warranty_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,warranty_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,warranty_H.Remark)
                        });

                    string sql = "";

                    string LastWarrantyType = "";
                    string LastWarrantySDate = "";
                    string LastWarrantyEDate = "";

                    if (strChangeDate != "" && strChangeDate!=null)
                    {
                        string[] strChangeDateArray = strChangeDate.Split(',');
                        string[] strChangeOrderTypeArray = strChangeOrderType.Split(',');
                        string[] strChangeOrderNoArray = strChangeOrderNo.Split(',');
                        string[] strChangeOrderItemIdArray = strChangeOrderItemId.Split(',');
                        string[] strTargetIdArray = strTargetId.Split(',');
                        string[] strTargetNameArray = strTargetName.Split(',');
                        string[] strWarrantyPeriodArray = strWarrantyPeriod.Split(',');
                        string[] strWarrantySDateArray = strWarrantySDate.Split(',');
                        string[] strWarrantyEDateArray = strWarrantyEDate.Split(',');
                        string[] strWarrantyTypeArray = strWarrantyType.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');

                        sql = @"insert into Warranty_D (Company,UserGroup,Creator,
                        CreateDate,ProductNo,SerialNo,ChangeDate,ChangeOrderType,ChangeOrderNo,
                        ChangeOrderItemId,TargetId,TargetName,DataDate,DataTime,WarrantyPeriod,
                        WarrantySDate,WarrantyEDate,WarrantyType,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@ProductNo,@SerialNo,@ChangeDate,@ChangeOrderType,@ChangeOrderNo,
                        @ChangeOrderItemId,@TargetId,@TargetName,@DataDate,@DataTime,@WarrantyPeriod,
                        @WarrantySDate,@WarrantyEDate,@WarrantyType,@Remark)";


                        for (int i = 0; i < strChangeDateArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strChangeDateArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,warranty_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,warranty_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,warranty_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo),
                                DataAccessMySQL.CreateParameter("ChangeDate",DbType.String,strChangeDateArray[i].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("ChangeOrderType",DbType.String,strChangeOrderTypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ChangeOrderNo",DbType.String,strChangeOrderNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ChangeOrderItemId",DbType.String,strChangeOrderItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("TargetId",DbType.String,strTargetIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("TargetName",DbType.String,strTargetNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("DataDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("DataTime",DbType.String,DateTime.Now.ToString("HHmmss")),
                                DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.String,strWarrantyPeriodArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,strWarrantySDateArray[i].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,strWarrantyEDateArray[i].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("WarrantyType",DbType.String,strWarrantyTypeArray[i].ToString().Substring(0,1)),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
                            });
                            LastWarrantyType = strWarrantyTypeArray[i].ToString().Substring(0, 1);
                            LastWarrantySDate = strWarrantySDateArray[i].ToString().Replace("/", "");
                            LastWarrantyEDate = strWarrantyEDateArray[i].ToString().Replace("/", "");
                        }
                    }

                    DataTable dtWarranty_D = dbMySQL.ExecuteDataTable("select * from Warranty_D where ProductNo=@ProductNo and SerialNo=@SerialNo order by WarrantySDate desc,WarrantyEDate desc",
                            tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo)
                            });

                    if (dtWarranty_D != null && dtWarranty_D.Rows.Count>0)
                    {
                        dbMySQL.ExecuteNonQuery(@"update Warranty_H set LastWarrantyType=@LastWarrantyType,
                        LastWarrantySDate=@LastWarrantySDate,
                        LastWarrantyEDate=@LastWarrantyEDate where ProductNo=@ProductNo and SerialNo=@SerialNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo),
                            DataAccessMySQL.CreateParameter("LastWarrantyType",DbType.String,dtWarranty_D.Rows[0]["WarrantyType"].ToString()),
                            //DataAccessMySQL.CreateParameter("LastWarrantyRemark",DbType.String,dtWarranty_D.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("LastWarrantySDate",DbType.String,dtWarranty_D.Rows[0]["WarrantySDate"].ToString()),
                            DataAccessMySQL.CreateParameter("LastWarrantyEDate",DbType.String,dtWarranty_D.Rows[0]["WarrantyEDate"].ToString())
                        });
                    }
                        
                    tran.Commit();
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "品號、序號已存在！";
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

        public bool UpdateWarranty_H(Warranty_H warranty_H, string strChangeDate,
            string strChangeOrderType, string strChangeOrderNo, string strChangeOrderItemId,
            string strTargetId, string strTargetName,
            string strWarrantyPeriod, string strWarrantySDate, string strWarrantyEDate,
            string strWarrantyType, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                dbMySQL.ExecuteNonQuery(@"update Warranty_H set Modifier=@Modifier,
                        ModiDate=@ModiDate,Remark=@Remark where ProductNo=@ProductNo and SerialNo=@SerialNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo),
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,warranty_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,warranty_H.ModiDate),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,warranty_H.Remark),
                        });
                dbMySQL.ExecuteNonQuery("delete from Warranty_D where ProductNo=@ProductNo and SerialNo=@SerialNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo)
                        });
                string sql = "";
                string LastWarrantyType = "";
                string LastWarrantySDate = "";
                string LastWarrantyEDate = "";

                if (strChangeDate != "" && strChangeDate != null)
                {
                    string[] strChangeDateArray = strChangeDate.Split(',');
                    string[] strChangeOrderTypeArray = strChangeOrderType.Split(',');
                    string[] strChangeOrderNoArray = strChangeOrderNo.Split(',');
                    string[] strChangeOrderItemIdArray = strChangeOrderItemId.Split(',');
                    string[] strTargetIdArray = strTargetId.Split(',');
                    string[] strTargetNameArray = strTargetName.Split(',');
                    string[] strWarrantyPeriodArray = strWarrantyPeriod.Split(',');
                    string[] strWarrantySDateArray = strWarrantySDate.Split(',');
                    string[] strWarrantyEDateArray = strWarrantyEDate.Split(',');
                    string[] strWarrantyTypeArray = strWarrantyType.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');

                    sql = @"insert into Warranty_D (Company,UserGroup,Creator,
                        CreateDate,ProductNo,SerialNo,ChangeDate,ChangeOrderType,ChangeOrderNo,
                        ChangeOrderItemId,TargetId,TargetName,DataDate,DataTime,WarrantyPeriod,
                        WarrantySDate,WarrantyEDate,WarrantyType,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@ProductNo,@SerialNo,@ChangeDate,@ChangeOrderType,@ChangeOrderNo,
                        @ChangeOrderItemId,@TargetId,@TargetName,@DataDate,@DataTime,@WarrantyPeriod,
                        @WarrantySDate,@WarrantyEDate,@WarrantyType,@Remark)";


                    for (int i = 0; i < strChangeDateArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strChangeDateArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,warranty_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,warranty_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,warranty_H.Modifier),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo),
                                DataAccessMySQL.CreateParameter("ChangeDate",DbType.String,strChangeDateArray[i].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("ChangeOrderType",DbType.String,strChangeOrderTypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ChangeOrderNo",DbType.String,strChangeOrderNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ChangeOrderItemId",DbType.String,strChangeOrderItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("TargetId",DbType.String,strTargetIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("TargetName",DbType.String,strTargetNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("DataDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("DataTime",DbType.String,DateTime.Now.ToString("HHmmss")),
                                DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.String,strWarrantyPeriodArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,strWarrantySDateArray[i].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,strWarrantyEDateArray[i].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("WarrantyType",DbType.String,strWarrantyTypeArray[i].ToString().Substring(0,1)),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
                            });

                        LastWarrantyType = strWarrantyTypeArray[i].ToString().Substring(0, 1);
                        LastWarrantySDate = strWarrantySDateArray[i].ToString().Replace("/", "");
                        LastWarrantyEDate = strWarrantyEDateArray[i].ToString().Replace("/", "");
                    }
                }

                DataTable dtWarranty_D = dbMySQL.ExecuteDataTable("select * from Warranty_D where ProductNo=@ProductNo and SerialNo=@SerialNo order by WarrantySDate desc,WarrantyEDate desc",
                        tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo)
                            });

                if (dtWarranty_D != null && dtWarranty_D.Rows.Count > 0)
                {
                    dbMySQL.ExecuteNonQuery(@"update Warranty_H set LastWarrantyType=@LastWarrantyType,
                        LastWarrantySDate=@LastWarrantySDate,
                        LastWarrantyEDate=@LastWarrantyEDate where ProductNo=@ProductNo and SerialNo=@SerialNo", tran,
                    new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo),
                            DataAccessMySQL.CreateParameter("LastWarrantyType",DbType.String,dtWarranty_D.Rows[0]["WarrantyType"].ToString()),
                            //DataAccessMySQL.CreateParameter("LastWarrantyRemark",DbType.String,dtWarranty_D.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("LastWarrantySDate",DbType.String,dtWarranty_D.Rows[0]["WarrantySDate"].ToString()),
                            DataAccessMySQL.CreateParameter("LastWarrantyEDate",DbType.String,dtWarranty_D.Rows[0]["WarrantyEDate"].ToString())
                        });
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

        public bool DelWarranty_H(Warranty_H warranty_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery("delete from Warranty_H where ProductNo=@ProductNo and SerialNo=@SerialNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo)
                        });
                dbMySQL.ExecuteNonQuery("delete from Warranty_D where ProductNo=@ProductNo and SerialNo=@SerialNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo)
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

        public List<Warranty_H> SearchWarranty_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<Warranty_H> objWarranty_H = new List<Warranty_H>();

            string sql = @"select w.*,p.ProductName from Warranty_H as w
                left join product as p on w.ProductNo=p.ProductNo where 1=1";

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


            DataTable dtWarranty_H = null;
            if (strCondition != "")
            {
                dtWarranty_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtWarranty_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtWarranty_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtWarranty_H.Rows)
                {
                    Warranty_H obj = new Warranty_H();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    if (dr["LastWarrantyType"].ToString()=="0")
                    {
                        obj.LastWarrantyType = "0.空白";
                    }
                    if (dr["LastWarrantyType"].ToString() == "1")
                    {
                        obj.LastWarrantyType = "1.廠商";
                    }
                    if (dr["LastWarrantyType"].ToString() == "2")
                    {
                        obj.LastWarrantyType = "2.客戶";
                    }
                    if (dr["LastWarrantyType"].ToString() == "3")
                    {
                        obj.LastWarrantyType = "3.合約";
                    }
                    obj.LastWarrantyRemark = dr["LastWarrantyRemark"].ToString();
                    obj.LastWarrantySDate = dr["LastWarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["LastWarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.LastWarrantyEDate = dr["LastWarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["LastWarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    objWarranty_H.Add(obj);
                }
            }

            return objWarranty_H;
        }

        public Warranty_H GetWarranty_HInfo(Warranty_H warranty_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            Warranty_H obj = null;

            DataTable dtWarranty_H = dbMySQL.ExecuteDataTable(@"select w.*,p.ProductName from Warranty_H as w
                left join product as p on w.ProductNo=p.ProductNo where 
                w.ProductNo=@ProductNo and w.SerialNo=@SerialNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,warranty_H.ProductNo),
                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,warranty_H.SerialNo)
                });

            if (dtWarranty_H.Rows.Count > 0)
            {
                obj = new Warranty_H();
                obj.ProductNo = dtWarranty_H.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtWarranty_H.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtWarranty_H.Rows[0]["SerialNo"].ToString();
                obj.Remark = dtWarranty_H.Rows[0]["Remark"].ToString();
                if (dtWarranty_H.Rows[0]["LastWarrantyType"].ToString() == "0")
                {
                    obj.LastWarrantyType = "0.空白";
                }
                if (dtWarranty_H.Rows[0]["LastWarrantyType"].ToString() == "1")
                {
                    obj.LastWarrantyType = "1.廠商";
                }
                if (dtWarranty_H.Rows[0]["LastWarrantyType"].ToString() == "2")
                {
                    obj.LastWarrantyType = "2.客戶";
                }
                if (dtWarranty_H.Rows[0]["LastWarrantyType"].ToString() == "3")
                {
                    obj.LastWarrantyType = "3.合約";
                }
                obj.LastWarrantyRemark = dtWarranty_H.Rows[0]["LastWarrantyRemark"].ToString();
                obj.LastWarrantySDate = dtWarranty_H.Rows[0]["LastWarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dtWarranty_H.Rows[0]["LastWarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.LastWarrantyEDate = dtWarranty_H.Rows[0]["LastWarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dtWarranty_H.Rows[0]["LastWarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            }

            return obj;
        }

        public bool IsSerialNo(string SerialNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from Warranty_H where SerialNo=@SerialNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,SerialNo)
                }).ToString())>0)
            {
                return false;
            }
            return true;

        }

    }
}