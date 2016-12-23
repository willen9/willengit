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
    public class RMA_HDAL : IRMA_HDAL
    {
        public bool AddRMA_H(RMA_H rMA_H, string strItemId,
            string strSourceOrderType, string strSourceOrderNo,
            string strProductNo, string strProductName, string strSerialNo,
            string strRemark, string strTestResult, string strReturned,
            string strClosed, string strRGAType, string strRGANo, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from RMA_H where 
                    RMAType=@RMAType and RMANo=@RMANo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RMAType",DbType.String,rMA_H.RMAType),
                        DataAccessMySQL.CreateParameter("RMANo",DbType.String,rMA_H.RMANo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into RMA_H (Company,UserGroup,Creator,CreateDate,
                        RMAType,RMANo,OrderDate,NoOfPrints,VendorOrderNo,ShippingNo,Remark,VendorId,
                        Contact,TelNo,FaxNo,Address,Confirmed,Amount) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @RMAType,@RMANo,@OrderDate,@NoOfPrints,@VendorOrderNo,@ShippingNo,@Remark,@VendorId,
                        @Contact,@TelNo,@FaxNo,@Address,@Confirmed,@Amount)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,rMA_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rMA_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,rMA_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RMAType",DbType.String,rMA_H.RMAType),
                            DataAccessMySQL.CreateParameter("RMANo",DbType.String,rMA_H.RMANo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rMA_H.OrderDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rMA_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("VendorOrderNo",DbType.String,rMA_H.VendorOrderNo),
                            DataAccessMySQL.CreateParameter("ShippingNo",DbType.String,rMA_H.ShippingNo),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rMA_H.Remark),
                            DataAccessMySQL.CreateParameter("VendorId",DbType.String,rMA_H.VendorId),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,rMA_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,rMA_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,rMA_H.FaxNo),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,rMA_H.Address),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"N"),
                            DataAccessMySQL.CreateParameter("Amount",DbType.String,rMA_H.Amount)
                        });

                    string sql = "";

                    if (strItemId != "" && strItemId != null)
                    {
                        string[] strItemIdArray = strItemId.Split(',');
                        string[] strSourceOrderTypeArray = strSourceOrderType.Split(',');
                        string[] strSourceOrderNoArray = strSourceOrderNo.Split(',');
                        string[] strProductNoArray = strProductNo.Split(',');
                        string[] strProductNameArray = strProductName.Split(',');
                        string[] strSerialNoArray = strSerialNo.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');
                        string[] strTestResultArray = strTestResult.Split(',');
                        string[] strReturnedArray = strReturned.Split(',');
                        string[] strClosedArray = strClosed.Split(',');
                        string[] strRGATypeArray = strRGAType.Split(',');
                        string[] strRGANoArray = strRGANo.Split(',');

                        sql = @"insert into RMA_D (Company,UserGroup,Creator,
                        CreateDate,RMAType,RMANo,ItemId,SourceOrderType,SourceOrderNo,
                        ProductNo,ProductName,SerialNo,Remark,TestResult,Returned,Closed,
                        RGAType,RGANo) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RMAType,@RMANo,@ItemId,@SourceOrderType,@SourceOrderNo,
                        @ProductNo,@ProductName,@SerialNo,@Remark,@TestResult,@Returned,@Closed,@RGAType,@RGANo)";


                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,rMA_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rMA_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,rMA_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RMAType",DbType.String,rMA_H.RMAType),
                                DataAccessMySQL.CreateParameter("RMANo",DbType.String,rMA_H.RMANo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,strSourceOrderTypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,strSourceOrderNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strProductNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,strProductNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("TestResult",DbType.String,strTestResultArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Returned",DbType.String,strReturnedArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Closed",DbType.String,strClosedArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,strRGATypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,strRGANoArray[i].ToString())
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

        public bool UpdateRMA_H(RMA_H rMA_H, string strItemId,
            string strSourceOrderType, string strSourceOrderNo,
            string strProductNo, string strProductName, string strSerialNo,
            string strRemark, string strTestResult, string strReturned,
            string strClosed, string strRGAType, string strRGANo, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update RMA_H set Modifier=@Modifier,ModiDate=@ModiDate,
                        VendorOrderNo=@VendorOrderNo,ShippingNo=@ShippingNo,
                        Remark=@Remark,VendorId=@VendorId,Contact=@Contact,TelNo=@TelNo,
                        FaxNo=@FaxNo,Address=@Address,Confirmed=@Confirmed,Amount=@Amount
                        where RMAType=@RMAType and RMANo=@RMANo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,rMA_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("VendorOrderNo",DbType.String,rMA_H.VendorOrderNo),
                            DataAccessMySQL.CreateParameter("ShippingNo",DbType.String,rMA_H.ShippingNo),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rMA_H.Remark),
                            DataAccessMySQL.CreateParameter("VendorId",DbType.String,rMA_H.VendorId),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,rMA_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,rMA_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,rMA_H.FaxNo),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,rMA_H.Address),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rMA_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Amount",DbType.String,rMA_H.Amount),
                            DataAccessMySQL.CreateParameter("RMAType",DbType.String,rMA_H.RMAType),
                            DataAccessMySQL.CreateParameter("RMANo",DbType.String,rMA_H.RMANo),
                        });

                dbMySQL.ExecuteNonQuery("delete from RMA_D where RMAType=@RMAType and RMANo=@RMANo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RMAType",DbType.String,rMA_H.RMAType),
                        DataAccessMySQL.CreateParameter("RMANo",DbType.String,rMA_H.RMANo),
                    });
                string sql = "";

                if (strItemId != "" && strItemId != null)
                {
                    string[] strItemIdArray = strItemId.Split(',');
                    string[] strSourceOrderTypeArray = strSourceOrderType.Split(',');
                    string[] strSourceOrderNoArray = strSourceOrderNo.Split(',');
                    string[] strProductNoArray = strProductNo.Split(',');
                    string[] strProductNameArray = strProductName.Split(',');
                    string[] strSerialNoArray = strSerialNo.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');
                    string[] strTestResultArray = strTestResult.Split(',');
                    string[] strReturnedArray = strReturned.Split(',');
                    string[] strClosedArray = strClosed.Split(',');
                    string[] strRGATypeArray = strRGAType.Split(',');
                    string[] strRGANoArray = strRGANo.Split(',');

                    sql = @"insert into RMA_D (Company,UserGroup,Creator,
                        CreateDate,RMAType,RMANo,ItemId,SourceOrderType,SourceOrderNo,
                        ProductNo,ProductName,SerialNo,Remark,TestResult,Returned,Closed,
                        RGAType,RGANo) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RMAType,@RMANo,@ItemId,@SourceOrderType,@SourceOrderNo,
                        @ProductNo,@ProductName,@SerialNo,@Remark,@TestResult,@Returned,@Closed,@RGAType,@RGANo)";


                    for (int i = 0; i < strItemIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strItemIdArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,rMA_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rMA_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,rMA_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RMAType",DbType.String,rMA_H.RMAType),
                                DataAccessMySQL.CreateParameter("RMANo",DbType.String,rMA_H.RMANo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,strSourceOrderTypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,strSourceOrderNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strProductNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,strProductNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("TestResult",DbType.String,strTestResultArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Returned",DbType.String,strReturnedArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Closed",DbType.String,strClosedArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,strRGATypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,strRGANoArray[i].ToString())
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

        public bool DelRMA_H(RMA_H rMA_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery("delete from RMA_H where RMAType=@RMAType and RMANo=@RMANo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RMAType",DbType.String,rMA_H.RMAType),
                            DataAccessMySQL.CreateParameter("RMANo",DbType.String,rMA_H.RMANo)
                        });
                dbMySQL.ExecuteNonQuery("delete from RMA_D where RMAType=@RMAType and RMANo=@RMANo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RMAType",DbType.String,rMA_H.RMAType),
                            DataAccessMySQL.CreateParameter("RMANo",DbType.String,rMA_H.RMANo)
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

        public List<RMA_H> SearchRMA_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RMA_H> objRMA_H = new List<RMA_H>();

            string sql = @"select h.*,v.VendorName,e.EmployeeName as CreatorName from RMA_H as h 
                left join Vendor as v on v.VendorId=h.VendorId
                left join Employee as e on e.EmployeeId=h.Creator where 1=1";

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


            DataTable dtRMA_H = null;
            if (strCondition != "")
            {
                dtRMA_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRMA_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtRMA_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRMA_H.Rows)
                {
                    RMA_H obj = new RMA_H();
                    obj.RMAType = dr["RMAType"].ToString();
                    obj.RMANo = dr["RMANo"].ToString();
                    obj.VendorId = dr["VendorId"].ToString();
                    obj.VendorName = dr["VendorName"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString();
                    obj.Creator = dr["Creator"].ToString();
                    obj.CreatorName = dr["CreatorName"].ToString();
                    if(dr["Confirmed"].ToString()== "Y")
                    {
                        obj.Confirmed = "Y:已送廠";
                    }
                    if (dr["Confirmed"].ToString() == "N")
                    {
                        obj.Confirmed = "N:未送廠";
                    }
                    if (dr["Confirmed"].ToString() == "V")
                    {
                        obj.Confirmed = "V:作廢";
                    }
                    objRMA_H.Add(obj);
                }
            }

            return objRMA_H;
        }

        public RMA_H GetRMA_HInfo(RMA_H rMA_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            RMA_H obj = null;

            DataTable dtRMA_H = dbMySQL.ExecuteDataTable(@"select * from RMA_H where 
                   RMAType=@RMAType and RMANo=@RMANo",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RMAType",DbType.String,rMA_H.RMAType),
                        DataAccessMySQL.CreateParameter("RMANo",DbType.String,rMA_H.RMANo)
                   });

            if (dtRMA_H.Rows.Count > 0)
            {
                obj = new RMA_H();
                obj.RMAType = dtRMA_H.Rows[0]["RMAType"].ToString();
                obj.RMANo = dtRMA_H.Rows[0]["RMANo"].ToString();
                obj.OrderDate = dtRMA_H.Rows[0]["OrderDate"].ToString();
                obj.NoOfPrints = int.Parse(dtRMA_H.Rows[0]["NoOfPrints"].ToString());
                obj.VendorOrderNo = dtRMA_H.Rows[0]["VendorOrderNo"].ToString();
                obj.ShippingNo = dtRMA_H.Rows[0]["ShippingNo"].ToString();
                obj.Remark = dtRMA_H.Rows[0]["Remark"].ToString();
                obj.VendorId = dtRMA_H.Rows[0]["VendorId"].ToString();
                obj.Contact = dtRMA_H.Rows[0]["Contact"].ToString();
                obj.TelNo = dtRMA_H.Rows[0]["TelNo"].ToString();
                obj.FaxNo = dtRMA_H.Rows[0]["FaxNo"].ToString();
                obj.Address = dtRMA_H.Rows[0]["Address"].ToString();
                obj.Confirmed = dtRMA_H.Rows[0]["Confirmed"].ToString();
                obj.Amount = dtRMA_H.Rows[0]["Amount"].ToString();
            }

            return obj;
        }
        
    }
}