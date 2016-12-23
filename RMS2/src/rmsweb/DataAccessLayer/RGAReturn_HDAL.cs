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
    public class RGAReturn_HDAL : IRGAReturn_HDAL
    {
        public bool AddRGAReturn_H(RGAReturn_H rGAReturn_H, string strItemId,
            string strSourceOrderType, string strSourceOrderNo, string strRGAType,
            string strRGANo, string strProductNo, string strProductName, string strSerialNo,
            string strTestResult, string strInternalRemark, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from RGAReturn_H where 
                    RGAReturnType=@RGAReturnType and RGAReturnNo=@RGAReturnNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAReturnType",DbType.String,rGAReturn_H.RGAReturnType),
                        DataAccessMySQL.CreateParameter("RGAReturnNo",DbType.String,rGAReturn_H.RGAReturnNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into RGAReturn_H (Company,UserGroup,Creator,CreateDate,
                        RGAReturnType,RGAReturnNo,OrderDate,NoOfPrints,PostalCategory,ShippingNo,Remark,
                        CustomerId,AddressSName,Address,Contact,TelNo,FaxNo,Confirmed,Amount) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @RGAReturnType,@RGAReturnNo,@OrderDate,@NoOfPrints,@PostalCategory,@ShippingNo,@Remark,
                        @CustomerId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo,@Confirmed,@Amount)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,rGAReturn_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAReturn_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAReturn_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RGAReturnType",DbType.String,rGAReturn_H.RGAReturnType),
                            DataAccessMySQL.CreateParameter("RGAReturnNo",DbType.String,rGAReturn_H.RGAReturnNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rGAReturn_H.OrderDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rGAReturn_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("PostalCategory",DbType.String,rGAReturn_H.PostalCategory),
                            DataAccessMySQL.CreateParameter("ShippingNo",DbType.String,rGAReturn_H.ShippingNo),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rGAReturn_H.Remark),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,rGAReturn_H.CustomerId),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,rGAReturn_H.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,rGAReturn_H.Address),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,rGAReturn_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,rGAReturn_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,rGAReturn_H.FaxNo),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"N"),
                            DataAccessMySQL.CreateParameter("Amount",DbType.String,rGAReturn_H.Amount)
                        });

                    string sql = "";

                    if (strItemId != "" && strItemId != null)
                    {
                        string[] strItemIdArray = strItemId.Split(',');
                        string[] strSourceOrderTypeArray = strSourceOrderType.Split(',');
                        string[] strSourceOrderNoArray = strSourceOrderNo.Split(',');
                        string[] strRGATypeArray = strRGAType.Split(',');
                        string[] strRGANoArray = strRGANo.Split(',');
                        string[] strProductNoArray = strProductNo.Split(',');
                        string[] strProductNameArray = strProductName.Split(',');
                        string[] strSerialNoArray = strSerialNo.Split(',');
                        string[] strTestResultArray = strTestResult.Split(',');
                        string[] strInternalRemarkArray = strInternalRemark.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');

                        sql = @"insert into RGAReturn_D (Company,UserGroup,Creator,
                        CreateDate,RGAReturnType,RGAReturnNo,ItemId,SourceOrderType,SourceOrderNo,
                        RGAType,RGANo,ProductNo,ProductName,SerialNo,TestResult,InternalRemark,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@MajorComponentNo,@ItemId,@ComponentNo,@QTY,@Remark)";


                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,rGAReturn_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAReturn_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAReturn_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RGAReturnType",DbType.String,rGAReturn_H.RGAReturnType),
                                DataAccessMySQL.CreateParameter("RGAReturnNo",DbType.String,rGAReturn_H.RGAReturnNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,strSourceOrderTypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,strSourceOrderNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,strRGATypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,strRGANoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strProductNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,strProductNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("TestResult",DbType.String,strTestResultArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,strInternalRemarkArray[i].ToString()),
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

        public bool UpdateRGAReturn_H(RGAReturn_H rGAReturn_H, string strItemId,
            string strSourceOrderType, string strSourceOrderNo, string strRGAType,
            string strRGANo, string strProductNo, string strProductName, string strSerialNo,
            string strTestResult, string strInternalRemark, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update RGAReturn_D set Modifier=@Modifier,ModiDate=@ModiDate,
                        PostalCategory=@PostalCategory,ShippingNo=@ShippingNo,
                        Remark=@Remark,CustomerId=@CustomerId,AddressSName=@AddressSName,
                        Address=@Address,Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,
                        Confirmed=@Confirmed,Amount=@Amount
                        where MajorComponentNo=@MajorComponentNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,rGAReturn_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RGAReturnType",DbType.String,rGAReturn_H.RGAReturnType),
                            DataAccessMySQL.CreateParameter("RGAReturnNo",DbType.String,rGAReturn_H.RGAReturnNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rGAReturn_H.OrderDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rGAReturn_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("PostalCategory",DbType.String,rGAReturn_H.PostalCategory),
                            DataAccessMySQL.CreateParameter("ShippingNo",DbType.String,rGAReturn_H.ShippingNo),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rGAReturn_H.Remark),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,rGAReturn_H.CustomerId),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,rGAReturn_H.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,rGAReturn_H.Address),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,rGAReturn_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,rGAReturn_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,rGAReturn_H.FaxNo),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rGAReturn_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Amount",DbType.String,rGAReturn_H.Amount)
                        });

                dbMySQL.ExecuteNonQuery("delete from RGAReturn_D where RGAReturnType=@RGAReturnType and RGAReturnNo=@RGAReturnNo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAReturnType",DbType.String,rGAReturn_H.RGAReturnType),
                        DataAccessMySQL.CreateParameter("RGAReturnNo",DbType.String,rGAReturn_H.RGAReturnNo)
                    });
                string sql = "";

                if (strItemId != "" && strItemId != null)
                {
                    string[] strItemIdArray = strItemId.Split(',');
                    string[] strSourceOrderTypeArray = strSourceOrderType.Split(',');
                    string[] strSourceOrderNoArray = strSourceOrderNo.Split(',');
                    string[] strRGATypeArray = strRGAType.Split(',');
                    string[] strRGANoArray = strRGANo.Split(',');
                    string[] strProductNoArray = strProductNo.Split(',');
                    string[] strProductNameArray = strProductName.Split(',');
                    string[] strSerialNoArray = strSerialNo.Split(',');
                    string[] strTestResultArray = strTestResult.Split(',');
                    string[] strInternalRemarkArray = strInternalRemark.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');

                    sql = @"insert into RGAReturn_D (Company,UserGroup,Creator,
                        CreateDate,RGAReturnType,RGAReturnNo,ItemId,SourceOrderType,SourceOrderNo,
                        RGAType,RGANo,ProductNo,ProductName,SerialNo,TestResult,InternalRemark,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@MajorComponentNo,@ItemId,@ComponentNo,@QTY,@Remark)";


                    for (int i = 0; i < strItemIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strItemIdArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,rGAReturn_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAReturn_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAReturn_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RGAReturnType",DbType.String,rGAReturn_H.RGAReturnType),
                                DataAccessMySQL.CreateParameter("RGAReturnNo",DbType.String,rGAReturn_H.RGAReturnNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,strSourceOrderTypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,strSourceOrderNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,strRGATypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,strRGANoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strProductNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,strProductNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("TestResult",DbType.String,strTestResultArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,strInternalRemarkArray[i].ToString()),
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

        public bool DelRGAReturn_H(RGAReturn_H rGAReturn_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery("delete from RGAReturn_H where RGAReturnType=@RGAReturnType and RGAReturnNo=@RGAReturnNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RGAReturnType",DbType.String,rGAReturn_H.RGAReturnType),
                            DataAccessMySQL.CreateParameter("RGAReturnNo",DbType.String,rGAReturn_H.RGAReturnNo)
                        });
                dbMySQL.ExecuteNonQuery("delete from RGAReturn_D where RGAReturnType=@RGAReturnType and RGAReturnNo=@RGAReturnNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RGAReturnType",DbType.String,rGAReturn_H.RGAReturnType),
                            DataAccessMySQL.CreateParameter("RGAReturnNo",DbType.String,rGAReturn_H.RGAReturnNo)
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

        public List<RGAReturn_H> SearchRGAReturn_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RGAReturn_H> objRGAReturn_H = new List<RGAReturn_H>();

            string sql = @"select  h.*,o.OrderSName,c.CustomerName,d.ProductNo,d.ProductName,d.SerialNo from RGAReturn_H as h 
                            left join RGAReturn_D as d on d.RGAReturnType=h.RGAReturnType and d.RGAReturnNo=h.RGAReturnNo
                           left join OrderCategory as o on o.OrderType=h.RGAReturnType
                           left join Customer as c on c.CustomerId=h.CustomerId where 1=1";

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


            DataTable dtRGAReturn_H = null;
            if (strCondition != "")
            {
                dtRGAReturn_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRGAReturn_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtRGAReturn_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGAReturn_H.Rows)
                {
                    RGAReturn_H obj = new RGAReturn_H();
                    obj.RGAReturnType = dr["RGAReturnType"].ToString();
                    obj.OrderSName = dr["OrderSName"].ToString();
                    obj.RGAReturnNo = dr["RGAReturnNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    if(dr["Confirmed"].ToString() == "Y")
                    {
                        obj.Confirmed = "Y.已出件";
                    }else if(dr["Confirmed"].ToString() == "N")
                    {
                        obj.Confirmed = "N:未出件";
                    }else
                    {
                        obj.Confirmed = "V:作廢";
                    }
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    objRGAReturn_H.Add(obj);
                }
            }

            return objRGAReturn_H;
        }

        public RGAReturn_H GetRGAReturn_HInfo(RGAReturn_H rGAReturn_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            RGAReturn_H obj = null;

            DataTable dtRGAReturn_H = dbMySQL.ExecuteDataTable(@"select * from RGAReturn_H where 
                    RGAReturnType=@RGAReturnType and RGAReturnNo=@RGAReturnNo",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAReturnType",DbType.String,rGAReturn_H.RGAReturnType),
                        DataAccessMySQL.CreateParameter("RGAReturnNo",DbType.String,rGAReturn_H.RGAReturnNo)
                    });

            if (dtRGAReturn_H.Rows.Count > 0)
            {
                obj = new RGAReturn_H();
                obj.CreateDate = dtRGAReturn_H.Rows[0]["CreateDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRGAReturn_H.Rows[0]["CreateDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.ModiDate = dtRGAReturn_H.Rows[0]["ModiDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRGAReturn_H.Rows[0]["ModiDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.Confirmor = dtRGAReturn_H.Rows[0]["Confirmor"].ToString();
                obj.ConfirmDate = dtRGAReturn_H.Rows[0]["ConfirmDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRGAReturn_H.Rows[0]["ConfirmDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.RGAReturnType = dtRGAReturn_H.Rows[0]["RGAReturnType"].ToString();
                obj.RGAReturnNo = dtRGAReturn_H.Rows[0]["RGAReturnNo"].ToString();
                obj.OrderDate = dtRGAReturn_H.Rows[0]["OrderDate"].ToString();
                obj.NoOfPrints = int.Parse(dtRGAReturn_H.Rows[0]["NoOfPrints"].ToString());
                obj.PostalCategory = dtRGAReturn_H.Rows[0]["PostalCategory"].ToString();
                obj.ShippingNo = dtRGAReturn_H.Rows[0]["ShippingNo"].ToString();
                obj.Remark = dtRGAReturn_H.Rows[0]["Remark"].ToString();
                obj.AddressSName = dtRGAReturn_H.Rows[0]["AddressSName"].ToString();
                obj.Address = dtRGAReturn_H.Rows[0]["Address"].ToString();
                obj.Contact = dtRGAReturn_H.Rows[0]["Contact"].ToString();
                obj.TelNo = dtRGAReturn_H.Rows[0]["TelNo"].ToString();
                obj.FaxNo = dtRGAReturn_H.Rows[0]["FaxNo"].ToString();
                obj.Confirmed = dtRGAReturn_H.Rows[0]["Confirmed"].ToString();
                obj.Amount = dtRGAReturn_H.Rows[0]["Amount"].ToString();
            }

            return obj;
        }
        
    }
}