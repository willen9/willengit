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
    public class ServiceArrangeModi_HDAL : IServiceArrangeModi_HDAL
    {
        public bool AddServiceArrangeModi_H(ServiceArrangeModi_H serviceArrangeModi_H, string strItemId,
            string strArrangeMonth, string strAddressSName, string strAddress,
            string strIsClosed, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtServiceArrange_H = dbMySQL.ExecuteDataTable(@"select * from servicearrange_h where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo)
                    });
                if (dtServiceArrange_H != null && dtServiceArrange_H.Rows.Count > 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into servicearrangemodi_h (Company,UserGroup,Creator,CreateDate,
                        SerArrangeOrderType,SerArrangeOrderNo,Version,OrderDate,ConfirmedDate,ForceToClose,
                        ModiReason,CustomerId,SourceOrderType,SourceOrderNo,SourceOrderItemId,ProductNo,ProductName,SerialNo,
                        SaleDate,RoutineServiceFreq,WarrantyPeriod,WarrantySDate,WarrantyEDate,Confirmed,
                        Confirmor,NewRemark,NewInternalRemark,OldRemark,OldInternalRemark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @SerArrangeOrderType,@SerArrangeOrderNo,@Version,@OrderDate,@ConfirmedDate,@ForceToClose,
                        @ModiReason,@CustomerId,@SourceOrderType,@SourceOrderNo,@SourceOrderItemId,@ProductNo,@ProductName,@SerialNo,
                        @SaleDate,@RoutineServiceFreq,@WarrantyPeriod,@WarrantySDate,@WarrantyEDate,@Confirmed,
                        @Confirmor,@NewRemark,@NewInternalRemark,@OldRemark,@OldInternalRemark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,serviceArrangeModi_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,serviceArrangeModi_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,serviceArrangeModi_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtServiceArrange_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,serviceArrangeModi_H.OrderDate),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,serviceArrangeModi_H.ConfirmedDate),
                            DataAccessMySQL.CreateParameter("ForceToClose",DbType.String,serviceArrangeModi_H.ForceToClose),
                            DataAccessMySQL.CreateParameter("ModiReason",DbType.String,serviceArrangeModi_H.ModiReason),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,serviceArrangeModi_H.CustomerId),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,serviceArrangeModi_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,serviceArrangeModi_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,serviceArrangeModi_H.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,serviceArrangeModi_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,serviceArrangeModi_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,serviceArrangeModi_H.SerialNo),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,serviceArrangeModi_H.SaleDate),
                            DataAccessMySQL.CreateParameter("RoutineServiceFreq",DbType.Int32,serviceArrangeModi_H.RoutineServiceFreq),
                            DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.Int32,serviceArrangeModi_H.WarrantyPeriod),
                            DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,serviceArrangeModi_H.WarrantySDate),
                            DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,serviceArrangeModi_H.WarrantyEDate),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,serviceArrangeModi_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,serviceArrangeModi_H.Confirmor),
                            DataAccessMySQL.CreateParameter("NewRemark",DbType.String,serviceArrangeModi_H.NewRemark),
                            DataAccessMySQL.CreateParameter("NewInternalRemark",DbType.String,serviceArrangeModi_H.NewInternalRemark),
                            DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtServiceArrange_H.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("OldInternalRemark",DbType.String,dtServiceArrange_H.Rows[0]["InternalRemark"].ToString())
                        });

                    dbMySQL.ExecuteNonQuery(@"update servicearrange_h set Version=@Version,Remark=@Remark,InternalRemark=@InternalRemark where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtServiceArrange_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,serviceArrangeModi_H.NewRemark),
                        DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,serviceArrangeModi_H.NewInternalRemark)
                    });

                    string sql = "";

                    if (strItemId != "" && strItemId != null)
                    {
                        string[] strItemIdArray = strItemId.Split(',');
                        string[] strArrangeMonthArray = strArrangeMonth.Split(',');
                        string[] strAddressSNameArray = strAddressSName.Split(',');
                        string[] strAddressArray = strAddress.Split(',');
                        string[] strIsClosedArray = strIsClosed.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');

                        sql = @"insert into servicearrangemodi_d (Company,UserGroup,Creator,
                        CreateDate,SerArrangeOrderType,SerArrangeOrderNo,Version,ItemId,NewArrangeMonth,NewAddressSName,
                        NewAddress,NewIsClosed,NewRemark,OldArrangeMonth,OldAddressSName,OldAddress,OldIsClosed,OldRemark) 
                        values (@Company,@UserGroup,@Creator,
                        @CreateDate,@SerArrangeOrderType,@SerArrangeOrderNo,@Version,@ItemId,@NewArrangeMonth,@NewAddressSName,
                        @NewAddress,@NewIsClosed,@NewRemark,@OldArrangeMonth,@OldAddressSName,@OldAddress,@OldIsClosed,@OldRemark)";

                        DataTable dtServiceArrangeChange_D = null;

                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }

                            dtServiceArrangeChange_D = dbMySQL.ExecuteDataTable(@"select * from servicearrange_d where 
                            SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and ItemId=@ItemId", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString())
                            });
                            if (dtServiceArrangeChange_D != null && dtServiceArrangeChange_D.Rows.Count>0)
                            {
                                dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,serviceArrangeModi_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,serviceArrangeModi_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,serviceArrangeModi_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtServiceArrange_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewArrangeMonth",DbType.String,strArrangeMonthArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewAddressSName",DbType.String,strAddressSNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewAddress",DbType.String,strAddressArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewIsClosed",DbType.String,strIsClosedArray[i].ToString().Substring(0,1)),
                                    DataAccessMySQL.CreateParameter("NewRemark",DbType.String,strRemarkArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldArrangeMonth",DbType.String,dtServiceArrangeChange_D.Rows[0]["ArrangeMonth"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldAddressSName",DbType.String,dtServiceArrangeChange_D.Rows[0]["AddressSName"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldAddress",DbType.String,dtServiceArrangeChange_D.Rows[0]["Address"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldIsClosed",DbType.String,dtServiceArrangeChange_D.Rows[0]["IsClosed"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtServiceArrangeChange_D.Rows[0]["Remark"].ToString())
                                });

                                dbMySQL.ExecuteNonQuery(@"update servicearrange_d set ArrangeMonth=@ArrangeMonth,AddressSName=@AddressSName,Address=@Address,IsClosed=@IsClosed,Remark=@Remark where 
                                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and ItemId=@ItemId", tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ArrangeMonth",DbType.String,strArrangeMonthArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("AddressSName",DbType.String,strAddressSNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Address",DbType.String,strAddressArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("IsClosed",DbType.String,strIsClosedArray[i].ToString().Substring(0,1)),
                                    DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
                                });
                            }else
                            {
                                dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,serviceArrangeModi_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,serviceArrangeModi_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,serviceArrangeModi_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtServiceArrange_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewArrangeMonth",DbType.String,strArrangeMonthArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewAddressSName",DbType.String,strAddressSNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewAddress",DbType.String,strAddressArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewIsClosed",DbType.String,strIsClosedArray[i].ToString().Substring(0,1)),
                                    DataAccessMySQL.CreateParameter("NewRemark",DbType.String,strRemarkArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldArrangeMonth",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldAddressSName",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldAddress",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldIsClosed",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldRemark",DbType.String,"")
                                });

                                dbMySQL.ExecuteNonQuery(@"insert into servicearrange_d ( SerArrangeOrderType,SerArrangeOrderNo,ItemId,ArrangeMonth,AddressSName,Address,IsClosed,Remark) values  
                                    ( @SerArrangeOrderType,@SerArrangeOrderNo,@ItemId,@ArrangeMonth,@AddressSName,@Address,@IsClosed,@Remark) ", tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ArrangeMonth",DbType.String,strArrangeMonthArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("AddressSName",DbType.String,strAddressSNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Address",DbType.String,strAddressArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("IsClosed",DbType.String,strIsClosedArray[i].ToString().Substring(0,1)),
                                    DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
                                });
                            }
                            
                        }
                    }

                    if (serviceArrangeModi_H.ForceToClose == "Y")
                    {
                        dbMySQL.ExecuteNonQuery("update servicearrangemodi_d set NewIsClosed='Y' where SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and Version=@Version", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                                DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtServiceArrange_H.Rows[0]["Version"].ToString())+1).ToString("0000"))
                            });

                        dbMySQL.ExecuteNonQuery("update servicearrange_d set IsClosed='Y' where SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo)
                            });

                    }
                    
                        
                    tran.Commit();
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "來源定保計劃不存在！";
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

        public bool UpdateServiceArrangeModi_H(ServiceArrangeModi_H serviceArrangeModi_H, string strItemId,
            string strArrangeMonth, string strAddressSName, string strAddress,
            string strIsClosed, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtServiceArrange_H = dbMySQL.ExecuteDataTable(@"select * from servicearrange_h where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo)
                    });
                if (dtServiceArrange_H != null && dtServiceArrange_H.Rows.Count > 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into servicearrangemodi_h (Company,UserGroup,Creator,CreateDate,
                        SerArrangeOrderType,SerArrangeOrderNo,Version,OrderDate,ConfirmedDate,ForceToClose,
                        ModiReason,CustomerId,SourceOrderType,SourceOrderNo,SourceOrderItemId,ProductNo,ProductName,SerialNo,
                        SaleDate,RoutineServiceFreq,WarrantyPeriod,WarrantySDate,WarrantyEDate,Confirmed,
                        Confirmor,NewRemark,NewInternalRemark,OldRemark,OldInternalRemark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @SerArrangeOrderType,@SerArrangeOrderNo,@Version,@OrderDate,@ConfirmedDate,@ForceToClose,
                        @ModiReason,@CustomerId,@SourceOrderType,@SourceOrderNo,@SourceOrderItemId,@ProductNo,@ProductName,@SerialNo,
                        @SaleDate,@RoutineServiceFreq,@WarrantyPeriod,@WarrantySDate,@WarrantyEDate,@Confirmed,
                        @Confirmor,@NewRemark,@NewInternalRemark,@OldRemark,@OldInternalRemark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,serviceArrangeModi_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,serviceArrangeModi_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,serviceArrangeModi_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtServiceArrange_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,serviceArrangeModi_H.OrderDate),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,serviceArrangeModi_H.ConfirmedDate),
                            DataAccessMySQL.CreateParameter("ForceToClose",DbType.String,serviceArrangeModi_H.ForceToClose),
                            DataAccessMySQL.CreateParameter("ModiReason",DbType.String,serviceArrangeModi_H.ModiReason),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,serviceArrangeModi_H.CustomerId),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,serviceArrangeModi_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,serviceArrangeModi_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,serviceArrangeModi_H.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,serviceArrangeModi_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,serviceArrangeModi_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,serviceArrangeModi_H.SerialNo),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,serviceArrangeModi_H.SaleDate),
                            DataAccessMySQL.CreateParameter("RoutineServiceFreq",DbType.Int32,serviceArrangeModi_H.RoutineServiceFreq),
                            DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.Int32,serviceArrangeModi_H.WarrantyPeriod),
                            DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,serviceArrangeModi_H.WarrantySDate),
                            DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,serviceArrangeModi_H.WarrantyEDate),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,serviceArrangeModi_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,serviceArrangeModi_H.Confirmor),
                            DataAccessMySQL.CreateParameter("NewRemark",DbType.String,serviceArrangeModi_H.NewRemark),
                            DataAccessMySQL.CreateParameter("NewInternalRemark",DbType.String,serviceArrangeModi_H.NewInternalRemark),
                            DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtServiceArrange_H.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("OldInternalRemark",DbType.String,dtServiceArrange_H.Rows[0]["InternalRemark"].ToString())
                        });

                    dbMySQL.ExecuteNonQuery(@"update servicearrange_h set Version=@Version,Remark=@Remark,InternalRemark=@InternalRemark where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtServiceArrange_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,serviceArrangeModi_H.NewRemark),
                        DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,serviceArrangeModi_H.NewInternalRemark)
                    });

                    string sql = "";

                    if (strItemId != "" && strItemId != null)
                    {
                        string[] strItemIdArray = strItemId.Split(',');
                        string[] strArrangeMonthArray = strArrangeMonth.Split(',');
                        string[] strAddressSNameArray = strAddressSName.Split(',');
                        string[] strAddressArray = strAddress.Split(',');
                        string[] strIsClosedArray = strIsClosed.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');

                        sql = @"insert into servicearrangemodi_d (Company,UserGroup,Creator,
                        CreateDate,SerArrangeOrderType,SerArrangeOrderNo,Version,ItemId,NewArrangeMonth,NewAddressSName,
                        NewAddress,NewIsClosed,NewRemark,OldArrangeMonth,OldAddressSName,OldAddress,OldIsClosed,OldRemark) 
                        values (@Company,@UserGroup,@Creator,
                        @CreateDate,@SerArrangeOrderType,@SerArrangeOrderNo,@Version,@ItemId,@NewArrangeMonth,@NewAddressSName,
                        @NewAddress,@NewIsClosed,@NewRemark,@OldArrangeMonth,@OldAddressSName,@OldAddress,@OldIsClosed,@OldRemark)";

                        DataTable dtServiceArrangeChange_D = null;

                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }

                            dtServiceArrangeChange_D = dbMySQL.ExecuteDataTable(@"select * from servicearrange_d where 
                            SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and ItemId=@ItemId", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString())
                            });
                            if (dtServiceArrangeChange_D != null && dtServiceArrangeChange_D.Rows.Count > 0)
                            {
                                dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,serviceArrangeModi_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,serviceArrangeModi_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,serviceArrangeModi_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtServiceArrange_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewArrangeMonth",DbType.String,strArrangeMonthArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewAddressSName",DbType.String,strAddressSNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewAddress",DbType.String,strAddressArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewIsClosed",DbType.String,strIsClosedArray[i].ToString().Substring(0,1)),
                                    DataAccessMySQL.CreateParameter("NewRemark",DbType.String,strRemarkArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldArrangeMonth",DbType.String,dtServiceArrangeChange_D.Rows[0]["ArrangeMonth"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldAddressSName",DbType.String,dtServiceArrangeChange_D.Rows[0]["AddressSName"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldAddress",DbType.String,dtServiceArrangeChange_D.Rows[0]["Address"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldIsClosed",DbType.String,dtServiceArrangeChange_D.Rows[0]["IsClosed"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtServiceArrangeChange_D.Rows[0]["Remark"].ToString())
                                });

                                dbMySQL.ExecuteNonQuery(@"update servicearrange_d set ArrangeMonth=@ArrangeMonth,AddressSName=@AddressSName,Address=@Address,IsClosed=@IsClosed,Remark=@Remark where 
                                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and ItemId=@ItemId", tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ArrangeMonth",DbType.String,strArrangeMonthArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("AddressSName",DbType.String,strAddressSNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Address",DbType.String,strAddressArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("IsClosed",DbType.String,strIsClosedArray[i].ToString().Substring(0,1)),
                                    DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
                                });
                            }
                            else
                            {
                                dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,serviceArrangeModi_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,serviceArrangeModi_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,serviceArrangeModi_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtServiceArrange_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewArrangeMonth",DbType.String,strArrangeMonthArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewAddressSName",DbType.String,strAddressSNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewAddress",DbType.String,strAddressArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewIsClosed",DbType.String,strIsClosedArray[i].ToString().Substring(0,1)),
                                    DataAccessMySQL.CreateParameter("NewRemark",DbType.String,strRemarkArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldArrangeMonth",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldAddressSName",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldAddress",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldIsClosed",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldRemark",DbType.String,"")
                                });

                                dbMySQL.ExecuteNonQuery(@"insert into servicearrange_d ( SerArrangeOrderType,SerArrangeOrderNo,ItemId,ArrangeMonth,AddressSName,Address,IsClosed,Remark) values  
                                    ( @SerArrangeOrderType,@SerArrangeOrderNo,@ItemId,@ArrangeMonth,@AddressSName,@Address,@IsClosed,@Remark) ", tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ArrangeMonth",DbType.String,strArrangeMonthArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("AddressSName",DbType.String,strAddressSNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Address",DbType.String,strAddressArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("IsClosed",DbType.String,strIsClosedArray[i].ToString().Substring(0,1)),
                                    DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
                                });
                            }

                        }
                    }

                    if (serviceArrangeModi_H.ForceToClose == "Y")
                    {
                        dbMySQL.ExecuteNonQuery("update servicearrangemodi_d set NewIsClosed='Y' where SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and Version=@Version", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                                DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtServiceArrange_H.Rows[0]["Version"].ToString())+1).ToString("0000"))
                            });

                        dbMySQL.ExecuteNonQuery("update servicearrange_d set IsClosed='Y' where SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                                DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo)
                            });

                    }


                    tran.Commit();
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "來源定保計劃不存在！";
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

        public bool DelServiceArrangeModi_H(ServiceArrangeModi_H serviceArrangeModi_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                DataTable dtServiceArrangeModi_H = dbMySQL.ExecuteDataTable(@"select * from servicearrangemodi_h where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,serviceArrangeModi_H.Version)
                    });
                if (dtServiceArrangeModi_H != null && dtServiceArrangeModi_H.Rows.Count > 0)
                {
                    dbMySQL.ExecuteNonQuery(@"update servicearrange_h set Version=@Version,Remark=@Remark,InternalRemark=@InternalRemark where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(serviceArrangeModi_H.Version)-1).ToString("0000")),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,dtServiceArrangeModi_H.Rows[0]["NewRemark"].ToString()),
                        DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,dtServiceArrangeModi_H.Rows[0]["NewInternalRemark"].ToString())
                    });

                    dbMySQL.ExecuteNonQuery(@"delete from servicearrangemodi_h where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,serviceArrangeModi_H.Version)
                    });
                }

                DataTable dtServiceArrangeModi_D = dbMySQL.ExecuteDataTable(@"select * from ServiceArrangeModi_D where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,serviceArrangeModi_H.Version)
                    });
                if (dtServiceArrangeModi_D != null && dtServiceArrangeModi_D.Rows.Count > 0)
                {
                    foreach(DataRow dr in dtServiceArrangeModi_D.Rows)
                    {
                        dbMySQL.ExecuteNonQuery(@"update servicearrange_d set ArrangeMonth=@ArrangeMonth,AddressSName=@AddressSName,Address=@Address,IsClosed=@IsClosed,Remark=@Remark where 
                        SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and ItemId=@ItemId", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,dr["ItemId"].ToString()),
                            DataAccessMySQL.CreateParameter("ArrangeMonth",DbType.String,dr["OldArrangeMonth"].ToString()),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,dr["OldAddressSName"].ToString()),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,dr["OldAddress"].ToString()),
                            DataAccessMySQL.CreateParameter("IsClosed",DbType.String,dr["OldIsClosed"].ToString()),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,dr["OldRemark"].ToString())
                        });
                    }
                }

                dbMySQL.ExecuteNonQuery(@"delete from servicearrangemodi_d where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,serviceArrangeModi_H.Version)
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

        public List<ServiceArrangeModi_H> SearchServiceArrangeModi_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<ServiceArrangeModi_H> objServiceArrangeModi_H = new List<ServiceArrangeModi_H>();

            string sql = @"select s.SerArrangeOrderType,s.SerArrangeOrderNo,max(s.Version) as Version,s.CustomerId,
                c.CustomerName,c.CustomerType,s.ProductNo,s.ProductName,s.SerialNo,
                s.WarrantySDate,s.WarrantyEDate from ServiceArrangeModi_H as s
                left join customer as c on c.CustomerId=s.CustomerId where 1=1";

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


            DataTable dtServiceArrangeModi_H = null;
            if (strCondition != "")
            {
                dtServiceArrangeModi_H = dbMySQL.ExecuteDataTable(sql + strCondition + " group by s.SerArrangeOrderType, s.SerArrangeOrderNo,Version", dbParameters);
            }
            else
            {
                dtServiceArrangeModi_H = dbMySQL.ExecuteDataTable(sql + " group by s.SerArrangeOrderType, s.SerArrangeOrderNo");
            }
            if (dtServiceArrangeModi_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtServiceArrangeModi_H.Rows)
                {
                    ServiceArrangeModi_H obj = new ServiceArrangeModi_H();
                    obj.SerArrangeOrderType = dr["SerArrangeOrderType"].ToString();
                    obj.SerArrangeOrderNo = dr["SerArrangeOrderNo"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    if (dr["CustomerType"].ToString() == "A")
                    {
                        obj.CustomerType = "A:一般客戶";
                    }
                    if (dr["CustomerType"].ToString() == "B")
                    {
                        obj.CustomerType = "B:經銷商";
                    }
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["serialNo"].ToString();
                    obj.WarrantySDate = dr["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.WarrantyEDate = dr["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    objServiceArrangeModi_H.Add(obj);
                }
            }

            return objServiceArrangeModi_H;
        }

        public ServiceArrangeModi_H GetServiceArrangeModi_HInfo(ServiceArrangeModi_H serviceArrangeModi_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            ServiceArrangeModi_H obj = null;

            DataTable dtServiceArrangeModi_H = dbMySQL.ExecuteDataTable(@"select h.SerArrangeOrderType,
                    h.SerArrangeOrderNo,h.OrderDate,h.ConfirmedDate,h.CustomerId,h.Version,
                    h.SourceOrderType,h.SourceOrderNo,h.SourceOrderItemId,h.ProductNo,h.ProductName,
                    h.SerialNo,h.SaleDate,h.RoutineServiceFreq,h.WarrantyPeriod,h.WarrantySDate,
                    h.WarrantyEDate,h.Confirmed,h.Confirmor,h.NewRemark,h.NewInternalRemark,h.OldRemark,
                    h.OldInternalRemark,h.ForceToClose,h.ModiReason,c.CustomerName,c.CustomerType from servicearrangemodi_h as h 
                    left join Customer as c on h.CustomerId=c.CustomerId where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo order by h.SerArrangeOrderType,
                    h.SerArrangeOrderNo,Version desc limit 1 ",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrangeModi_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrangeModi_H.SerArrangeOrderNo)
                    });

            if (dtServiceArrangeModi_H.Rows.Count > 0)
            {
                obj = new ServiceArrangeModi_H();
                obj.SerArrangeOrderType = dtServiceArrangeModi_H.Rows[0]["SerArrangeOrderType"].ToString();
                obj.SerArrangeOrderNo = dtServiceArrangeModi_H.Rows[0]["SerArrangeOrderNo"].ToString();
                obj.OrderDate = dtServiceArrangeModi_H.Rows[0]["OrderDate"].ToString();
                obj.ConfirmedDate = dtServiceArrangeModi_H.Rows[0]["ConfirmedDate"].ToString();
                obj.CustomerId = dtServiceArrangeModi_H.Rows[0]["CustomerId"].ToString();
                obj.CustomerName = dtServiceArrangeModi_H.Rows[0]["CustomerName"].ToString();
                if(dtServiceArrangeModi_H.Rows[0]["CustomerType"].ToString()!="")
                {
                    obj.CustomerType = dtServiceArrangeModi_H.Rows[0]["CustomerType"].ToString() == "A" ? "A:一般客戶" : "B:經銷商";
                }else
                {
                    obj.CustomerType = "";
                }
                
                obj.Version = dtServiceArrangeModi_H.Rows[0]["Version"].ToString();
                obj.SourceOrderType = dtServiceArrangeModi_H.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtServiceArrangeModi_H.Rows[0]["SourceOrderNo"].ToString();
                obj.SourceOrderItemId = dtServiceArrangeModi_H.Rows[0]["SourceOrderItemId"].ToString();
                obj.ProductNo = dtServiceArrangeModi_H.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtServiceArrangeModi_H.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtServiceArrangeModi_H.Rows[0]["SerialNo"].ToString();
                obj.SaleDate = dtServiceArrangeModi_H.Rows[0]["SaleDate"].ToString();
                obj.RoutineServiceFreq = int.Parse(dtServiceArrangeModi_H.Rows[0]["RoutineServiceFreq"].ToString() == "" ? "0" : dtServiceArrangeModi_H.Rows[0]["RoutineServiceFreq"].ToString());
                obj.WarrantyPeriod = int.Parse(dtServiceArrangeModi_H.Rows[0]["WarrantyPeriod"].ToString() == "" ? "0" : dtServiceArrangeModi_H.Rows[0]["WarrantyPeriod"].ToString());
                obj.WarrantySDate = dtServiceArrangeModi_H.Rows[0]["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dtServiceArrangeModi_H.Rows[0]["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.WarrantyEDate = dtServiceArrangeModi_H.Rows[0]["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dtServiceArrangeModi_H.Rows[0]["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                if(dtServiceArrangeModi_H.Rows[0]["Confirmed"].ToString()== "Y")
                {
                    obj.Confirmed = "Y:已確認";
                }
                if (dtServiceArrangeModi_H.Rows[0]["Confirmed"].ToString() == "N")
                {
                    obj.Confirmed = "N:未確認";
                }
                if (dtServiceArrangeModi_H.Rows[0]["Confirmed"].ToString() == "V")
                {
                    obj.Confirmed = "V:作廢";
                }
                obj.Confirmor = dtServiceArrangeModi_H.Rows[0]["Confirmor"].ToString();
                obj.Remark = obj.NewRemark = dtServiceArrangeModi_H.Rows[0]["NewRemark"].ToString();
                obj.InternalRemark = obj.NewInternalRemark = dtServiceArrangeModi_H.Rows[0]["NewInternalRemark"].ToString();
                obj.OldRemark = dtServiceArrangeModi_H.Rows[0]["OldRemark"].ToString();
                obj.OldInternalRemark = dtServiceArrangeModi_H.Rows[0]["OldInternalRemark"].ToString();
                obj.ForceToClose = dtServiceArrangeModi_H.Rows[0]["ForceToClose"].ToString();
                obj.ModiReason = dtServiceArrangeModi_H.Rows[0]["ModiReason"].ToString();
            }

            return obj;
        }

    }
}