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
    public class RMAReturn_HDAL : IRMAReturn_HDAL
    {
        public bool AddRMAReturn_H(RMAReturn_H rMAReturn_H, string strItemId,
            string strRMAType, string strRMANo,
            string strRMAItemId, string strProductNo, string strProductName,
            string strSerialNo, string strRemark, string strReturned,
            string strRepairDetail, string strMalfunctionReason, string strRGAType, string strRGANo, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from RMAReturn_H where 
                    RMAReturnType=@RMAReturnType and RMAReturnNo=@RMAReturnNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RMAReturnType",DbType.String,rMAReturn_H.RMAReturnType),
                        DataAccessMySQL.CreateParameter("RMAReturnNo",DbType.String,rMAReturn_H.RMAReturnNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into RMAReturn_H (Company,UserGroup,Creator,CreateDate,
                        RMAReturnType,RMAReturnNo,OrderDate,NoOfPrints,Remark,VendorNo,Contact,TelNo,
                        FaxNo,Address,Confirmed,Amount) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @RMAReturnType,@RMAReturnNo,@OrderDate,@NoOfPrints,@Remark,@VendorNo,@Contact,@TelNo,
                        @FaxNo,@Address,@Confirmed,@Amount)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,rMAReturn_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rMAReturn_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,rMAReturn_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RMAReturnType",DbType.String,rMAReturn_H.RMAReturnType),
                            DataAccessMySQL.CreateParameter("RMAReturnNo",DbType.String,rMAReturn_H.RMAReturnNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rMAReturn_H.OrderDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rMAReturn_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rMAReturn_H.Remark),
                            DataAccessMySQL.CreateParameter("VendorNo",DbType.String,rMAReturn_H.VendorNo),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,rMAReturn_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,rMAReturn_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,rMAReturn_H.FaxNo),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,rMAReturn_H.Address),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rMAReturn_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Amount",DbType.String,rMAReturn_H.Amount)
                        });

                    string sql = "";

                    if (strItemId != "" && strItemId != null)
                    {
                        string[] strItemIdArray = strItemId.Split(',');
                        string[] strRMATypeArray = strRMAType.Split(',');
                        string[] strRMANoArray = strRMANo.Split(',');
                        string[] strRMAItemIdArray = strRMAItemId.Split(',');
                        string[] strProductNoArray = strProductNo.Split(',');
                        string[] strProductNameArray = strProductName.Split(',');
                        string[] strSerialNoArray = strSerialNo.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');
                        string[] strReturnedArray = strReturned.Split(',');
                        string[] strRepairDetailArray = strRepairDetail.Split(',');
                        string[] strMalfunctionReasonArray = strMalfunctionReason.Split(',');
                        string[] strRGATypeArray = strRGAType.Split(',');
                        string[] strRGANoArray = strRGANo.Split(',');

                        sql = @"insert into RMAReturn_D (Company,UserGroup,Creator,
                        CreateDate,RMAReturnType,RMAReturnNo,ItemId,RMAType,RMANo,
                        RMAItemId,ProductNo,ProductName,SerialNo,Remark,Returned,RepairDetail,
                        MalfunctionReason,RGAType,RGANo) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RMAReturnType,@RMAReturnNo,@ItemId,@RMAType,@RMANo,
                        @RMAItemId,@ProductNo,@ProductName,@SerialNo,@Remark,@Returned,@RepairDetail,
                        @MalfunctionReason,@RGAType,@RGANo)";


                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,rMAReturn_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rMAReturn_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,rMAReturn_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RMAReturnType",DbType.String,rMAReturn_H.RMAReturnType),
                                DataAccessMySQL.CreateParameter("RMAReturnNo",DbType.String,rMAReturn_H.RMAReturnNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RMAType",DbType.String,strRMATypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RMAItemId",DbType.String,strRMAItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strProductNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,strProductNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Returned",DbType.String,strReturnedArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RepairDetail",DbType.String,strRepairDetailArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("MalfunctionReason",DbType.String,strMalfunctionReasonArray[i].ToString()),
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

        public bool UpdateRMAReturn_H(RMAReturn_H rMAReturn_H, string strItemId,
            string strRMAType, string strRMANo,
            string strRMAItemId, string strProductNo, string strProductName,
            string strSerialNo, string strRemark, string strReturned,
            string strRepairDetail, string strMalfunctionReason, string strRGAType, string strRGANo, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update RMAReturn_H set Modifier=@Modifier,ModiDate=@ModiDate,
                        Remark=@Remark,VendorNo=@VendorNo,
                        Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,Address=@Address,
                        Confirmed=@Confirmed,Amount=@Amount
                        where RMAReturnType=@RMAReturnType and RMAReturnNo=@RMAReturnNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,rMAReturn_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RMAReturnType",DbType.String,rMAReturn_H.RMAReturnType),
                            DataAccessMySQL.CreateParameter("RMAReturnNo",DbType.String,rMAReturn_H.RMAReturnNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rMAReturn_H.OrderDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rMAReturn_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rMAReturn_H.Remark),
                            DataAccessMySQL.CreateParameter("VendorNo",DbType.String,rMAReturn_H.VendorNo),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,rMAReturn_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,rMAReturn_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,rMAReturn_H.FaxNo),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,rMAReturn_H.Address),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rMAReturn_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Amount",DbType.String,rMAReturn_H.Amount)
                        });

                dbMySQL.ExecuteNonQuery("delete from RMAReturn_D where RMAReturnType=@RMAReturnType and RMAReturnNo=@RMAReturnNo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RMAReturnType",DbType.String,rMAReturn_H.RMAReturnType),
                        DataAccessMySQL.CreateParameter("RMAReturnNo",DbType.String,rMAReturn_H.RMAReturnNo)
                    });
                string sql = "";

                if (strItemId != "" && strItemId != null)
                {
                    string[] strItemIdArray = strItemId.Split(',');
                    string[] strRMATypeArray = strRMAType.Split(',');
                    string[] strRMANoArray = strRMANo.Split(',');
                    string[] strRMAItemIdArray = strRMAItemId.Split(',');
                    string[] strProductNoArray = strProductNo.Split(',');
                    string[] strProductNameArray = strProductName.Split(',');
                    string[] strSerialNoArray = strSerialNo.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');
                    string[] strReturnedArray = strReturned.Split(',');
                    string[] strRepairDetailArray = strRepairDetail.Split(',');
                    string[] strMalfunctionReasonArray = strMalfunctionReason.Split(',');
                    string[] strRGATypeArray = strRGAType.Split(',');
                    string[] strRGANoArray = strRGANo.Split(',');

                    sql = @"insert into RMAReturn_D (Company,UserGroup,Creator,
                        CreateDate,RMAReturnType,RMAReturnNo,ItemId,RMAType,RMANo,
                        RMAItemId,ProductNo,ProductName,SerialNo,Remark,Returned,RepairDetail,
                        MalfunctionReason,RGAType,RGANo) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RMAReturnType,@RMAReturnNo,@ItemId,@RMAType,@RMANo,
                        @RMAItemId,@ProductNo,@ProductName,@SerialNo,@Remark,@Returned,@RepairDetail,
                        @MalfunctionReason,@RGAType,@RGANo)";


                    for (int i = 0; i < strItemIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strItemIdArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,rMAReturn_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rMAReturn_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,rMAReturn_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RMAReturnType",DbType.String,rMAReturn_H.RMAReturnType),
                                DataAccessMySQL.CreateParameter("RMAReturnNo",DbType.String,rMAReturn_H.RMAReturnNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RMAType",DbType.String,strRMATypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RMAItemId",DbType.String,strRMAItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strProductNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,strProductNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Returned",DbType.String,strReturnedArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("RepairDetail",DbType.String,strRepairDetailArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("MalfunctionReason",DbType.String,strMalfunctionReasonArray[i].ToString()),
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

        public bool DelRMAReturn_H(RMAReturn_H rMAReturn_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery("delete from RMAReturn_H where RMAReturnType=@RMAReturnType and RMAReturnNo=@RMAReturnNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RMAReturnType",DbType.String,rMAReturn_H.RMAReturnType),
                            DataAccessMySQL.CreateParameter("RMAReturnNo",DbType.String,rMAReturn_H.RMAReturnNo)
                        });
                dbMySQL.ExecuteNonQuery("delete from RMAReturn_D where RMAReturnType=@RMAReturnType and RMAReturnNo=@RMAReturnNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RMAReturnType",DbType.String,rMAReturn_H.RMAReturnType),
                            DataAccessMySQL.CreateParameter("RMAReturnNo",DbType.String,rMAReturn_H.RMAReturnNo)
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

        public List<RMAReturn_H> SearchRMAReturn_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RMAReturn_H> objRMAReturn_H = new List<RMAReturn_H>();

            string sql = @"select * from RMAReturn_H where 1=1";

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


            DataTable dtRMAReturn_H = null;
            if (strCondition != "")
            {
                dtRMAReturn_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRMAReturn_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtRMAReturn_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRMAReturn_H.Rows)
                {
                    RMAReturn_H obj = new RMAReturn_H();
                    obj.RMAReturnType = dr["RMAReturnType"].ToString();
                    obj.RMAReturnNo = dr["RMAReturnNo"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.VendorNo = dr["VendorNo"].ToString();
                    obj.Contact = dr["Contact"].ToString();
                    objRMAReturn_H.Add(obj);
                }
            }

            return objRMAReturn_H;
        }

        public RMAReturn_H GetRMAReturn_HInfo(RMAReturn_H rMAReturn_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            RMAReturn_H obj = null;

            DataTable dtRMAReturn_H = dbMySQL.ExecuteDataTable(@"select * from RMAReturn_H where 
                   RMAReturnType=@RMAReturnType and RMAReturnNo=@RMAReturnNo",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RMAReturnType",DbType.String,rMAReturn_H.RMAReturnType),
                        DataAccessMySQL.CreateParameter("RMAReturnNo",DbType.String,rMAReturn_H.RMAReturnNo)
                   });

            if (dtRMAReturn_H.Rows.Count > 0)
            {
                obj = new RMAReturn_H();
                obj.RMAReturnType = dtRMAReturn_H.Rows[0]["RMAReturnType"].ToString();
                obj.RMAReturnNo = dtRMAReturn_H.Rows[0]["RMAReturnNo"].ToString();
                obj.OrderDate = dtRMAReturn_H.Rows[0]["OrderDate"].ToString();
                obj.NoOfPrints = int.Parse(dtRMAReturn_H.Rows[0]["NoOfPrints"].ToString());
                obj.Remark = dtRMAReturn_H.Rows[0]["Remark"].ToString();
                obj.VendorNo = dtRMAReturn_H.Rows[0]["VendorNo"].ToString();
                obj.Contact = dtRMAReturn_H.Rows[0]["Contact"].ToString();
                obj.TelNo = dtRMAReturn_H.Rows[0]["TelNo"].ToString();
                obj.FaxNo = dtRMAReturn_H.Rows[0]["FaxNo"].ToString();
                obj.Address = dtRMAReturn_H.Rows[0]["Address"].ToString();
                obj.Confirmed = dtRMAReturn_H.Rows[0]["Confirmed"].ToString();
                obj.Amount = dtRMAReturn_H.Rows[0]["Amount"].ToString();
            }

            return obj;
        }
        
    }
}