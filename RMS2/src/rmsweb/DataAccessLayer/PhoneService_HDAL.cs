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
    public class PhoneService_HDAL : IPhoneService_HDAL
    {
        public bool AddPhoneService_H(PhoneService_H phoneService_H, string strItemId,
            string strProcessDate, string strDescription, string strProcessMan,
            string strHours, string strUnit, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from PhoneService_H where 
                    PhoneSerType=@PhoneSerType and PhoneSerNo=@PhoneSerNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,phoneService_H.PhoneSerType),
                        DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,phoneService_H.PhoneSerNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into PhoneService_H (Company,UserGroup,Creator,CreateDate,
                        PhoneSerType,PhoneSerNo,OrderDate,NoOfPrints,CustomerId,AddressSName,Address,Contact,
                        TelNo,FaxNo,ProductNo,ProductName,SerialNo,ContractType,ContractNo,UnderWarranty,
                        SaleDate,PurchaseDate,WarrantyStartDate,WarrantyExpiryDate,Confirmed,Remark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @PhoneSerType,@PhoneSerNo,@OrderDate,@NoOfPrints,@CustomerId,@AddressSName,@Address,@Contact,
                        @TelNo,@FaxNo,@ProductNo,@ProductName,@SerialNo,@ContractType,@ContractNo,@UnderWarranty,
                        @SaleDate,@PurchaseDate,@WarrantyStartDate,@WarrantyExpiryDate,@Confirmed,@Remark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,phoneService_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,phoneService_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,phoneService_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,phoneService_H.PhoneSerType),
                            DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,phoneService_H.PhoneSerNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,phoneService_H.OrderDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,phoneService_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,phoneService_H.CustomerId),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,phoneService_H.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,phoneService_H.Address),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,phoneService_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,phoneService_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,phoneService_H.FaxNo),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,phoneService_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,phoneService_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,phoneService_H.SerialNo),
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,phoneService_H.ContractType),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,phoneService_H.ContractNo),
                            DataAccessMySQL.CreateParameter("UnderWarranty",DbType.String,phoneService_H.UnderWarranty),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,phoneService_H.SaleDate==null?"":phoneService_H.SaleDate),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,phoneService_H.PurchaseDate==null?"":phoneService_H.PurchaseDate),
                            DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,phoneService_H.WarrantyStartDate==null?"":phoneService_H.WarrantyStartDate),
                            DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,phoneService_H.WarrantyExpiryDate==null?"":phoneService_H.WarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,phoneService_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,phoneService_H.Remark)
                        });

                    string sql = "";

                    if (strItemId != "" && strItemId != null)
                    {
                        string[] strItemIdArray = strItemId.Split(',');
                        string[] strProcessDateArray = strProcessDate.Split(',');
                        string[] strDescriptionArray = strDescription.Split(',');
                        string[] strProcessManArray = strProcessMan.Split(',');
                        string[] strHoursArray = strHours.Split(',');
                        string[] strUnitArray = strUnit.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');

                        sql = @"insert into PhoneService_D (Company,UserGroup,Creator,
                        CreateDate,PhoneSerType,PhoneSerNo,ItemId,ProcessDate,Description,
                        ProcessMan,Hours,Unit,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@PhoneSerType,@PhoneSerNo,@ItemId,@ProcessDate,@Description,
                        @ProcessMan,@Hours,@Unit,@Remark)";


                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,phoneService_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,phoneService_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,phoneService_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,phoneService_H.PhoneSerType),
                                DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,phoneService_H.PhoneSerNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProcessDate",DbType.String,strProcessDateArray[i].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("Description",DbType.String,strDescriptionArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,strProcessManArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Hours",DbType.Double,double.Parse(strHoursArray[i].ToString()==""?"0":strHoursArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strUnitArray[i].ToString()),
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

        public bool UpdatePhoneService_H(PhoneService_H phoneService_H, string strItemId,
            string strProcessDate, string strDescription, string strProcessMan,
            string strHours, string strUnit, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update PhoneService_H set Modifier=@Modifier,
                        ModiDate=@ModiDate,OrderDate=@OrderDate,NoOfPrints=@NoOfPrints,
                        CustomerId=@CustomerId,AddressSName=@AddressSName,Address=@Address,Contact=@Contact,
                        TelNo=@TelNo,FaxNo=@FaxNo,ProductNo=@ProductNo,ProductName=@ProductName,
                        SerialNo=@SerialNo,ContractType=@ContractType,ContractNo=@ContractNo,
                        UnderWarranty=@UnderWarranty,SaleDate=@SaleDate,PurchaseDate=@PurchaseDate,
                        WarrantyStartDate=@WarrantyStartDate,WarrantyExpiryDate=@WarrantyExpiryDate,
                        Remark=@Remark
                        where PhoneSerType=@PhoneSerType and PhoneSerNo=@PhoneSerNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,phoneService_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,phoneService_H.PhoneSerType),
                            DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,phoneService_H.PhoneSerNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,phoneService_H.OrderDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,phoneService_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,phoneService_H.CustomerId),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,phoneService_H.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,phoneService_H.Address),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,phoneService_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,phoneService_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,phoneService_H.FaxNo),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,phoneService_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,phoneService_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,phoneService_H.SerialNo),
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,phoneService_H.ContractType),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,phoneService_H.ContractNo),
                            DataAccessMySQL.CreateParameter("UnderWarranty",DbType.String,phoneService_H.UnderWarranty),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,phoneService_H.SaleDate),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,phoneService_H.PurchaseDate),
                            DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,phoneService_H.WarrantyStartDate),
                            DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,phoneService_H.WarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,phoneService_H.Remark)
                        });

                dbMySQL.ExecuteNonQuery("delete from PhoneService_D where PhoneSerType=@PhoneSerType and PhoneSerNo=@PhoneSerNo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,phoneService_H.PhoneSerType),
                        DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,phoneService_H.PhoneSerNo)
                    });
                string sql = "";

                if (strItemId != "" && strItemId != null)
                {
                    string[] strItemIdArray = strItemId.Split(',');
                    string[] strProcessDateArray = strProcessDate.Split(',');
                    string[] strDescriptionArray = strDescription.Split(',');
                    string[] strProcessManArray = strProcessMan.Split(',');
                    string[] strHoursArray = strHours.Split(',');
                    string[] strUnitArray = strUnit.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');

                    sql = @"insert into PhoneService_D (Company,UserGroup,Creator,
                        CreateDate,PhoneSerType,PhoneSerNo,ItemId,ProcessDate,Description,
                        ProcessMan,Hours,Unit,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@PhoneSerType,@PhoneSerNo,@ItemId,@ProcessDate,@Description,
                        @ProcessMan,@Hours,@Unit,@Remark)";


                    for (int i = 0; i < strItemIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strItemIdArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,phoneService_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,phoneService_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,phoneService_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,phoneService_H.PhoneSerType),
                                DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,phoneService_H.PhoneSerNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProcessDate",DbType.String,strProcessDateArray[i].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("Description",DbType.String,strDescriptionArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,strProcessManArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Hours",DbType.Double,double.Parse(strHoursArray[i].ToString()==""?"0":strHoursArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strUnitArray[i].ToString()),
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

        public bool DelPhoneService_H(PhoneService_H phoneService_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery("delete from PhoneService_H where PhoneSerType=@PhoneSerType and PhoneSerNo=@PhoneSerNo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,phoneService_H.PhoneSerType),
                        DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,phoneService_H.PhoneSerNo)
                    });
                dbMySQL.ExecuteNonQuery("delete from PhoneService_D where PhoneSerType=@PhoneSerType and PhoneSerNo=@PhoneSerNo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,phoneService_H.PhoneSerType),
                        DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,phoneService_H.PhoneSerNo)
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

        public List<PhoneService_H> SearchPhoneService_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<PhoneService_H> objPhoneService_H = new List<PhoneService_H>();

            string sql = @"select h.*,c.CustomerName from PhoneService_H as h left join Customer as c on h.CustomerId=c.CustomerId  where 1=1";

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


            DataTable dtPhoneService_H = null;
            if (strCondition != "")
            {
                dtPhoneService_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtPhoneService_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtPhoneService_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPhoneService_H.Rows)
                {
                    PhoneService_H obj = new PhoneService_H();
                    obj.PhoneSerType = dr["PhoneSerType"].ToString();
                    obj.PhoneSerNo = dr["PhoneSerNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    if(dr["Confirmed"].ToString()=="Y")
                    {
                        obj.Confirmed = "Y.完工";
                    }
                    else if(dr["Confirmed"].ToString() == "V")
                    {
                        obj.Confirmed = "V.作廢";
                    }
                    else
                    {
                        obj.Confirmed = "N.未處理";
                    }
                    
                    objPhoneService_H.Add(obj);
                }
            }

            return objPhoneService_H;
        }

        public PhoneService_H GetPhoneService_HInfo(PhoneService_H phoneService_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            PhoneService_H obj = new PhoneService_H();

            DataTable dtPhoneService_H = dbMySQL.ExecuteDataTable(@"select h.*,o.OrderSName,c.CustomerName,
                    c.CustomerType,e.EmployeeName as CreatorName from PhoneService_H as h 
                    left join OrderCategory as o on o.OrderType=h.PhoneSerType 
                    left join Customer as c on c.CustomerId=h.CustomerId
                    left join Employee as e on e.EmployeeId=h.Creator
                    where PhoneSerType=@PhoneSerType and PhoneSerNo=@PhoneSerNo",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,phoneService_H.PhoneSerType),
                        DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,phoneService_H.PhoneSerNo)
                    });

            if (dtPhoneService_H.Rows.Count > 0)
            {
                obj.PhoneSerType = dtPhoneService_H.Rows[0]["PhoneSerType"].ToString();
                obj.PhoneSerNo = dtPhoneService_H.Rows[0]["PhoneSerNo"].ToString();
                obj.OrderDate = dtPhoneService_H.Rows[0]["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dtPhoneService_H.Rows[0]["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.NoOfPrints = int.Parse(dtPhoneService_H.Rows[0]["NoOfPrints"].ToString());
                obj.CustomerId = dtPhoneService_H.Rows[0]["CustomerId"].ToString();
                obj.AddressSName = dtPhoneService_H.Rows[0]["AddressSName"].ToString();
                obj.Address = dtPhoneService_H.Rows[0]["Address"].ToString();
                obj.Remark = dtPhoneService_H.Rows[0]["Remark"].ToString();
                obj.Contact = dtPhoneService_H.Rows[0]["Contact"].ToString();
                obj.TelNo = dtPhoneService_H.Rows[0]["TelNo"].ToString();
                obj.FaxNo = dtPhoneService_H.Rows[0]["FaxNo"].ToString();
                obj.Remark = dtPhoneService_H.Rows[0]["Remark"].ToString();
                obj.ProductNo = dtPhoneService_H.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtPhoneService_H.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtPhoneService_H.Rows[0]["SerialNo"].ToString();
                obj.ContractType = dtPhoneService_H.Rows[0]["ContractType"].ToString();
                obj.ContractNo = dtPhoneService_H.Rows[0]["ContractNo"].ToString();
                obj.UnderWarranty = dtPhoneService_H.Rows[0]["UnderWarranty"].ToString();
                obj.SaleDate = dtPhoneService_H.Rows[0]["SaleDate"].ToString() == "" ? "" : DateTime.ParseExact(dtPhoneService_H.Rows[0]["SaleDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.PurchaseDate = dtPhoneService_H.Rows[0]["PurchaseDate"].ToString() == "" ? "" : DateTime.ParseExact(dtPhoneService_H.Rows[0]["PurchaseDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.WarrantyStartDate = dtPhoneService_H.Rows[0]["WarrantyStartDate"].ToString() == "" ? "" : DateTime.ParseExact(dtPhoneService_H.Rows[0]["WarrantyStartDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.WarrantyExpiryDate = dtPhoneService_H.Rows[0]["WarrantyExpiryDate"].ToString() == "" ? "" : DateTime.ParseExact(dtPhoneService_H.Rows[0]["WarrantyExpiryDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                if(dtPhoneService_H.Rows[0]["Confirmed"].ToString()=="Y")
                {
                    obj.Confirmed = "Y.完工";
                }
                if (dtPhoneService_H.Rows[0]["Confirmed"].ToString() == "N")
                {
                    obj.Confirmed = "N.未處理";
                }
                if (dtPhoneService_H.Rows[0]["Confirmed"].ToString() == "V")
                {
                    obj.Confirmed = "V.作廢";
                }
                obj.Remark = dtPhoneService_H.Rows[0]["Remark"].ToString();
                obj.OrderSName = dtPhoneService_H.Rows[0]["OrderSName"].ToString();
                obj.CustomerName = dtPhoneService_H.Rows[0]["CustomerName"].ToString();
                obj.CustomerType = dtPhoneService_H.Rows[0]["CustomerType"].ToString();
                obj.CreatorName = dtPhoneService_H.Rows[0]["CreatorName"].ToString();
            }

            return obj;
        }

        public bool IsPhoneService_H(PhoneService_H phoneService_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from PhoneService_H where 
                    PhoneSerType=@PhoneSerType and PhoneSerNo=@PhoneSerNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,phoneService_H.PhoneSerType),
                        DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,phoneService_H.PhoneSerNo)
                    }).ToString()) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ConfirmedPhoneService_H(PhoneService_H phoneService_H, out string msg)
        {
            try
            {
                DataAccessMySQL dbMySQL = new DataAccessMySQL();
                dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

                dbMySQL.ExecuteNonQuery("update PhoneService_H set Confirmed=@Confirmed,Confirmor=@Confirmor,ConfirmedDate=@ConfirmedDate WHERE PhoneSerType=@PhoneSerType and PhoneSerNo=@PhoneSerNo",
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,phoneService_H.PhoneSerType),
                        DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,phoneService_H.PhoneSerNo),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,phoneService_H.Confirmed),
                        DataAccessMySQL.CreateParameter("Confirmor",DbType.String,phoneService_H.Confirmor),
                        DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,phoneService_H.ConfirmedDate)
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

        public List<PhoneService_H> SearchPhoneService(PhoneService_H phoneService_H, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<PhoneService_H> objPhoneService_H = new List<PhoneService_H>();

            string sql = @"select h.*,c.CustomerName from PhoneService_H as h left join Customer as c on h.CustomerId=c.CustomerId  where 1=1";

            if (!String.IsNullOrEmpty(phoneService_H.PhoneSerType))
            {
                sql += " and h.PhoneSerType like @PhoneSerType";
            }

            if (!String.IsNullOrEmpty(phoneService_H.PhoneSerNo))
            {
                sql += " and h.PhoneSerNo like @PhoneSerNo";
            }

            if (!String.IsNullOrEmpty(phoneService_H.CustomerId))
            {
                sql += " and h.CustomerId like @CustomerId";
            }

            if (!String.IsNullOrEmpty(phoneService_H.Confirmed))
            {
                sql += " and h.Confirmed = @Confirmed";
            }

            if (Page != 0)
            {
                sql += " limit @Page1,@Page2";
            }

            DataTable dtPhoneService_H = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,"%"+phoneService_H.PhoneSerType+"%"),
                    DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,"%"+phoneService_H.PhoneSerNo+"%"),
                    DataAccessMySQL.CreateParameter("CustomerId",DbType.String,"%"+phoneService_H.CustomerId+"%"),
                    DataAccessMySQL.CreateParameter("Confirmed",DbType.String,phoneService_H.Confirmed),
                    DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
                    DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
                });

            if (dtPhoneService_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPhoneService_H.Rows)
                {
                    PhoneService_H obj = new PhoneService_H();
                    obj.PhoneSerType = dr["PhoneSerType"].ToString();
                    obj.PhoneSerNo = dr["PhoneSerNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    if (dr["Confirmed"].ToString() == "Y")
                    {
                        obj.Confirmed = "Y.完工";
                    }
                    else if (dr["Confirmed"].ToString() == "V")
                    {
                        obj.Confirmed = "V.作廢";
                    }
                    else
                    {
                        obj.Confirmed = "N.未處理";
                    }

                    objPhoneService_H.Add(obj);
                }
            }

            return objPhoneService_H;
        }
    }
}