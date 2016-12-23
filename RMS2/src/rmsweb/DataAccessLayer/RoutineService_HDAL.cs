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
    public class RoutineService_HDAL : IRoutineService_HDAL
    {
        public bool AddRoutineService_H(RoutineService_H routineService_H, string strPItemId,
            string strPProcessDate, string strPProcessExplanation, string strPProcessMan,
            string strPRemark, string strDItemId, string strDProductNo,
            string strDProductName, string strDQTY, string strDUnit,
            string strDRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtRoutineService_H = dbMySQL.ExecuteDataTable(@"select * from routineservice_h where 
                    RoutineSerOrderType=@RoutineSerOrderType and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                        DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo)
                    });
                if ( dtRoutineService_H.Rows.Count == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into routineservice_h (Company,UserGroup,Creator,CreateDate,
                        RoutineSerOrderType,RoutineSerOrderNo,OrderDate,ConfirmedDate,
                        SourceOrderType,SourceOrderNo,SourceOrderItemId,ArrangeMonth,CustomerId,AddressSName,
                        Address,Contact,TelNo,FaxNo,ProductNo,ProductName,
                        SerialNo,SaleDate,WarrantySDate,WarrantyEDate,Confirmed,
                        Remark,InternalRemark,AssignCheck,
                        NoOfPrints) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @RoutineSerOrderType,@RoutineSerOrderNo,@OrderDate,@ConfirmedDate,
                        @SourceOrderType,@SourceOrderNo,@SourceOrderItemId,@ArrangeMonth,@CustomerId,@AddressSName,
                        @Address,@Contact,@TelNo,@FaxNo,@ProductNo,@ProductName,
                        @SerialNo,@SaleDate,@WarrantySDate,@WarrantyEDate,@Confirmed,
                        @Remark,@InternalRemark,@AssignCheck,
                        @NoOfPrints)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,routineService_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,routineService_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,routineService_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                            DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,routineService_H.OrderDate),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,routineService_H.ConfirmedDate),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,routineService_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,routineService_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,routineService_H.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("ArrangeMonth",DbType.String,routineService_H.ArrangeMonth),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,routineService_H.CustomerId),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,routineService_H.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,routineService_H.Address),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,routineService_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,routineService_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,routineService_H.FaxNo),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,routineService_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,routineService_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,routineService_H.SerialNo),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,routineService_H.SaleDate),
                            DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,routineService_H.WarrantySDate),
                            DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,routineService_H.WarrantyEDate),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,routineService_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,routineService_H.Remark),
                            DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,routineService_H.InternalRemark),
                            DataAccessMySQL.CreateParameter("AssignCheck",DbType.String,"N"),
                            //DataAccessMySQL.CreateParameter("AssignDate",DbType.String,routineService_H.AssignDate),
                            //DataAccessMySQL.CreateParameter("RoutineSerMan",DbType.String,routineService_H.RoutineSerMan),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.String,routineService_H.NoOfPrints),
                            //DataAccessMySQL.CreateParameter("Confirmor",DbType.String,routineService_H.Confirmor)
                        });

                    dbMySQL.ExecuteNonQuery(@"update servicearrange_d set IsClosed='Y' where 
                        SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and ItemId=@ItemId", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,routineService_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,routineService_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,routineService_H.SourceOrderItemId)
                        });

                    DataTable dtServiceArrange_H = dbMySQL.ExecuteDataTable(@"select Version from servicearrange_h 
                        where SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,routineService_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,routineService_H.SourceOrderNo)
                        });
                    if(dtServiceArrange_H!=null&&dtServiceArrange_H.Rows.Count>0)
                    {
                        dbMySQL.ExecuteNonQuery(@"update servicearrangemodi_d set NewIsClosed='Y' where 
                        SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and ItemId=@ItemId and Version=@Version", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,routineService_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,routineService_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,routineService_H.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,dtServiceArrange_H.Rows[0]["Version"].ToString())
                        });
                    }

                    string sql = "";

                    if (strPItemId != "" && strPItemId != null)
                    {
                        string[] strPItemIdArray = strPItemId.Split(',');
                        string[] strPProcessDateArray = strPProcessDate.Split(',');
                        string[] strPProcessExplanationArray = strPProcessExplanation.Split(',');
                        string[] strPProcessManArray = strPProcessMan.Split(',');
                        string[] strPRemarkArray = strPRemark.Split(',');

                        sql = @"insert into RoutineService_ProcessD (Company,UserGroup,Creator,
                        CreateDate,RoutineSerOrderType,RoutineSerOrderNo,ItemId,ProcessDate,ProcessExplanation,ProcessMan,
                        Remark) 
                        values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RoutineSerOrderType,@RoutineSerOrderNo,@ItemId,@ProcessDate,@ProcessExplanation,@ProcessMan,
                        @Remark)";

                        for (int i = 0; i < strPItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strPItemIdArray[i]))
                            {
                                continue;
                            }

                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,routineService_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,routineService_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,routineService_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                                    DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strPItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ProcessDate",DbType.String,strPProcessDateArray[i].ToString().Replace("/","")),
                                    DataAccessMySQL.CreateParameter("ProcessExplanation",DbType.String,strPProcessExplanationArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,strPProcessManArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Remark",DbType.String,strPRemarkArray[i].ToString())
                                });
                            
                        }
                    }

                    if (strDItemId != "" && strDItemId != null)
                    {
                        string[] strDItemIdArray = strDItemId.Split(',');
                        string[] strDProductNoArray = strDProductNo.Split(',');
                        string[] strDProductNameArray = strDProductName.Split(',');
                        string[] strDQTYArray = strDQTY.Split(',');
                        string[] strDUnitArray = strDUnit.Split(',');
                        string[] strDRemarkArray = strDRemark.Split(',');

                        sql = @"insert into routineservice_productd (Company,UserGroup,Creator,
                        CreateDate,RoutineSerOrderType,RoutineSerOrderNo,ItemId,ProductNo,ProductName,QTY,
                        Unit,Remark) 
                        values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RoutineSerOrderType,@RoutineSerOrderNo,@ItemId,@ProductNo,@ProductName,@QTY,
                        @Unit,@Remark)";

                        for (int i = 0; i < strDItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strDItemIdArray[i]))
                            {
                                continue;
                            }

                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,routineService_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,routineService_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,routineService_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                                    DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strDItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strDProductNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ProductName",DbType.String,strDProductNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("QTY",DbType.String,strDQTYArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Unit",DbType.String,strDUnitArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Remark",DbType.String,strDRemarkArray[i].ToString())
                                });

                        }
                    }
                    
                        
                    tran.Commit();
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "定保計劃已存在！";
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

        public List<RoutineService_H> SearchRGA_H(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            List<RoutineService_H> objRGA_H = new List<RoutineService_H>();
            try
            {
                string sql = @"select h.*,o.OrderSName,c.CustomerType,c.CustomerName,e.EmployeeName as RepairmanName from RoutineService_H as h 
                left join OrderCategory as o on o.OrderType=h.RoutineSerOrderType 
                left join Customer as c on c.CustomerId=h.CustomerId
                left join Employee as e on e.EmployeeId=h.RoutineSerMan where 1=1";

                string[] colArray = Col.Split(',');
                string[] conditionArray = Condition.Split(',');
                string[] valueArray = conditionValue.Split(',');

                string strCondition = "";
                int len = 1;
                for (int i = 1; i < colArray.Length; i++)
                {
                    if (!String.IsNullOrEmpty(valueArray[i]))
                    {
                        len++;
                    }
                }

                DbParameter[] dbParameters = new DbParameter[len + 1];
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
                if (Page != 0)
                {
                    strCondition += " limit @Page1,@Page2";
                }
                dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
                dbParameters[dblen] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);

                DataTable dtRGA_H = null;
                if (strCondition != "")
                {

                    dtRGA_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
                }
                else
                {
                    dtRGA_H = dbMySQL.ExecuteDataTable(sql, dbParameters);
                }

                if (dtRGA_H.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRGA_H.Rows)
                    {
                        RoutineService_H obj = new RoutineService_H();
                        obj.RoutineSerOrderType = dr["RoutineSerOrderType"].ToString();     //維修單別
                        //obj.OrderSName = dr["OrderSName"].ToString();     //單據名稱
                        obj.RoutineSerOrderNo = dr["RoutineSerOrderNo"].ToString();         //維修單號
                        obj.OrderDate = dr["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");         //單據日期
                        obj.CustomerId = dr["CustomerId"].ToString();       //客戶代號
                        obj.CustomerName = dr["CustomerName"].ToString();       //客戶名稱
                        if (dr["CustomerType"].ToString() != "")
                        {
                            obj.CustomerType = dr["CustomerType"].ToString() == "A" ? "A:一般客戶" : "B:經銷商";       //客戶類型
                        }
                        else
                        {
                            obj.CustomerType = "";
                        }
                        obj.AddressSName = dr["AddressSName"].ToString();       //地址簡稱
                        obj.ProductNo = dr["ProductNo"].ToString();         //品號
                        obj.ProductName = dr["ProductName"].ToString();     //品名
                        obj.SerialNo = dr["SerialNo"].ToString();           //序號
                        if (dr["Confirmed"].ToString() == "Y")           //狀態碼
                        {
                            obj.Confirmed = "已確認";
                        }
                        if (dr["StatusCode"].ToString() == "N")
                        {
                            obj.Confirmed = "未確認";
                        }
                        if (dr["StatusCode"].ToString() == "V")
                        {
                            obj.Confirmed = "作廢";
                        }
                        obj.Remark = dr["Remark"].ToString();       //入件說明

                        objRGA_H.Add(obj);
                    }
                }
                return objRGA_H;
            }
            catch (Exception ex)
            {
                return objRGA_H;
            }
        }
        public bool UpdateRoutineService_H(RoutineService_H routineService_H, string strPItemId,
            string strPProcessDate, string strPProcessExplanation, string strPProcessMan,
            string strPRemark, string strDItemId, string strDProductNo,
            string strDProductName, string strDQTY, string strDUnit,
            string strDRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update RoutineService_H set Modifier=@Modifier,ModiDate=@ModiDate,OrderDate=@OrderDate,ConfirmedDate=@ConfirmedDate,
                    SourceOrderType=@SourceOrderType,SourceOrderNo=@SourceOrderNo,SourceOrderItemId=@SourceOrderItemId,
                    ArrangeMonth=@ArrangeMonth,CustomerId=@CustomerId,AddressSName=@AddressSName,
                    Address=@Address,Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,ProductNo=@ProductNo,ProductName=@ProductName,
                    SerialNo=@SerialNo,SaleDate=@SaleDate,WarrantySDate=@WarrantySDate,WarrantyEDate=@WarrantyEDate,Confirmed=@Confirmed,
                    Remark=@Remark,InternalRemark=@InternalRemark,AssignCheck=@AssignCheck,AssignDate=@AssignDate,RoutineSerMan=@RoutineSerMan,
                    NoOfPrints=@NoOfPrints,Confirmor=@Confirmor where RoutineSerOrderType=@RoutineSerOrderType and RoutineSerOrderNo=@RoutineSerOrderNo",tran,
                    new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,routineService_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                            DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,routineService_H.OrderDate),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,routineService_H.ConfirmedDate),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,routineService_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,routineService_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,routineService_H.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("ArrangeMonth",DbType.String,routineService_H.ArrangeMonth),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,routineService_H.CustomerId),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,routineService_H.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,routineService_H.Address),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,routineService_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,routineService_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,routineService_H.FaxNo),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,routineService_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,routineService_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,routineService_H.SerialNo),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,routineService_H.SaleDate),
                            DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,routineService_H.WarrantySDate),
                            DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,routineService_H.WarrantyEDate),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,routineService_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,routineService_H.Remark),
                            DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,routineService_H.InternalRemark),
                            DataAccessMySQL.CreateParameter("AssignCheck",DbType.String,routineService_H.AssignCheck),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,routineService_H.AssignDate),
                            DataAccessMySQL.CreateParameter("RoutineSerMan",DbType.String,routineService_H.RoutineSerMan),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.String,routineService_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,routineService_H.Confirmor)
                        });

                dbMySQL.ExecuteNonQuery("delete from RoutineService_ProcessD where RoutineSerOrderType=@RoutineSerOrderType and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                        DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo)
                    });

                dbMySQL.ExecuteNonQuery("delete from RoutineService_ProductD where RoutineSerOrderType=@RoutineSerOrderType and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                        DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo)
                    });

                string sql = "";

                if (strPItemId != "" && strPItemId != null)
                {
                    string[] strPItemIdArray = strPItemId.Split(',');
                    string[] strPProcessDateArray = strPProcessDate.Split(',');
                    string[] strPProcessExplanationArray = strPProcessExplanation.Split(',');
                    string[] strPProcessManArray = strPProcessMan.Split(',');
                    string[] strPRemarkArray = strPRemark.Split(',');

                    sql = @"insert into RoutineService_ProcessD (Company,UserGroup,Creator,
                        CreateDate,RoutineSerOrderType,RoutineSerOrderNo,ItemId,ProcessDate,ProcessExplanation,ProcessMan,
                        Remark) 
                        values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RoutineSerOrderType,@RoutineSerOrderNo,@ItemId,@ProcessDate,@ProcessExplanation,@ProcessMan,
                        @Remark)";

                    for (int i = 0; i < strPItemIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strPItemIdArray[i]))
                        {
                            continue;
                        }

                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,routineService_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,routineService_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,routineService_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                                    DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strPItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ProcessDate",DbType.String,strPProcessDateArray[i].ToString().Replace("/","")),
                                    DataAccessMySQL.CreateParameter("ProcessExplanation",DbType.String,strPProcessExplanationArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,strPProcessManArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Remark",DbType.String,strPRemarkArray[i].ToString())
                                });

                    }
                }

                if (strDItemId != "" && strDItemId != null)
                {
                    string[] strDItemIdArray = strDItemId.Split(',');
                    string[] strDProductNoArray = strDProductNo.Split(',');
                    string[] strDProductNameArray = strDProductName.Split(',');
                    string[] strDQTYArray = strDQTY.Split(',');
                    string[] strDUnitArray = strDUnit.Split(',');
                    string[] strDRemarkArray = strDRemark.Split(',');

                    sql = @"insert into RoutineService_ProductD (Company,UserGroup,Creator,
                        CreateDate,RoutineSerOrderType,RoutineSerOrderNo,ItemId,ProductNo,ProductName,QTY,
                        Unit,Remark) 
                        values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RoutineSerOrderType,@RoutineSerOrderNo,@ItemId,@ProductNo,@ProductName,@QTY,
                        @Unit,@Remark)";

                    for (int i = 0; i < strDItemIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strDItemIdArray[i]))
                        {
                            continue;
                        }

                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,routineService_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,routineService_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,routineService_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                                    DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strDItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strDProductNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ProductName",DbType.String,strDProductNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("QTY",DbType.String,strDQTYArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Unit",DbType.String,strDUnitArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Remark",DbType.String,strDRemarkArray[i].ToString())
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

        public bool DelRoutineService_H(RoutineService_H routineService_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                DataTable dtRoutineService_H=dbMySQL.ExecuteDataTable("select * from RoutineService_H where RoutineSerOrderType=@RoutineSerOrderType and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                        DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo)
                    });
                if(dtRoutineService_H!=null&&dtRoutineService_H.Rows.Count>0)
                {
                    dbMySQL.ExecuteNonQuery(@"update ServiceArrange_D set IsClosed='Y' where 
                        SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and ItemId=@ItemId", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,dtRoutineService_H.Rows[0]["SourceOrderType"]),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,dtRoutineService_H.Rows[0]["SourceOrderNo"]),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,dtRoutineService_H.Rows[0]["SourceOrderItemId"])
                        });

                    DataTable dtServiceArrange_H = dbMySQL.ExecuteDataTable(@"select Version from ServiceArrange_H 
                        where SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,dtRoutineService_H.Rows[0]["SourceOrderType"]),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,dtRoutineService_H.Rows[0]["SourceOrderNo"])
                        });
                    if (dtServiceArrange_H != null && dtServiceArrange_H.Rows.Count > 0)
                    {
                        dbMySQL.ExecuteNonQuery(@"update ServiceArrangeModi_D set NewIsClosed='Y' where 
                        SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo and ItemId=@ItemId and Version=@Version", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,dtRoutineService_H.Rows[0]["SourceOrderType"]),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,dtRoutineService_H.Rows[0]["SourceOrderNo"]),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,dtRoutineService_H.Rows[0]["SourceOrderItemId"]),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,dtServiceArrange_H.Rows[0]["Version"].ToString())
                        });
                    }

                }
                dbMySQL.ExecuteNonQuery("delete from RoutineService_H where RoutineSerOrderType=@RoutineSerOrderType and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                        DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo)
                    });

                dbMySQL.ExecuteNonQuery("delete from RoutineService_ProcessD where RoutineSerOrderType=@RoutineSerOrderType and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                        DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo)
                    });

                dbMySQL.ExecuteNonQuery("delete from RoutineService_ProductD where RoutineSerOrderType=@RoutineSerOrderType and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                        DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo)
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

        public List<RoutineService_H> SearchRoutineService_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RoutineService_H> objRoutineService_H = new List<RoutineService_H>();

            string sql = @"select h.*,c.CustomerName,c.CustomerType
                 from routineservice_h as h
                left join customer as c on c.CustomerId=h.CustomerId where 1=1";

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


            DataTable dtRoutineService_H = null;
            if (strCondition != "")
            {
                dtRoutineService_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRoutineService_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtRoutineService_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRoutineService_H.Rows)
                {
                    RoutineService_H obj = new RoutineService_H();
                    obj.RoutineSerOrderType = dr["RoutineSerOrderType"].ToString();
                    obj.RoutineSerOrderNo = dr["RoutineSerOrderNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    if(dr["AssignCheck"].ToString()=="Y")
                    {
                        obj.AssignCheck = "Y.已派工";
                    }else
                    {
                        obj.AssignCheck = "N.未派工";
                    }
                    if (dr["CustomerType"].ToString() == "A")
                    {
                        obj.CustomerType = "A:一般客戶";
                    }
                    if (dr["CustomerType"].ToString() == "B")
                    {
                        obj.CustomerType = "B:經銷商";
                    }
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.ArrangeMonth = dr["ArrangeMonth"].ToString();
                    obj.RoutineSerMan = dr["RoutineSerMan"].ToString();
                    obj.WarrantySDate = dr["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.WarrantyEDate = dr["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.ConfirmedDate = dr["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ConfirmedDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    objRoutineService_H.Add(obj);
                }
            }

            return objRoutineService_H;
        }

        public RoutineService_H GetRoutineService_HInfo(RoutineService_H routineService_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            RoutineService_H obj = null;

            DataTable dtRoutineService_H = dbMySQL.ExecuteDataTable(@"select h.*,c.CustomerName,c.CustomerType
                 from routineservice_h as h
                 left join customer as c on c.CustomerId=h.CustomerId where 
                 RoutineSerOrderType=@RoutineSerOrderType and RoutineSerOrderNo=@RoutineSerOrderNo",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,routineService_H.RoutineSerOrderType),
                        DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,routineService_H.RoutineSerOrderNo)
                    });

            if (dtRoutineService_H.Rows.Count > 0)
            {
                obj = new RoutineService_H();
                obj.RoutineSerOrderType = dtRoutineService_H.Rows[0]["RoutineSerOrderType"].ToString();
                obj.RoutineSerOrderNo = dtRoutineService_H.Rows[0]["RoutineSerOrderNo"].ToString();
                obj.OrderDate = dtRoutineService_H.Rows[0]["OrderDate"].ToString();
                obj.ConfirmedDate = dtRoutineService_H.Rows[0]["ConfirmedDate"].ToString();
                obj.SourceOrderType = dtRoutineService_H.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtRoutineService_H.Rows[0]["SourceOrderNo"].ToString();
                obj.SourceOrderItemId = dtRoutineService_H.Rows[0]["SourceOrderItemId"].ToString();
                obj.ArrangeMonth = dtRoutineService_H.Rows[0]["ArrangeMonth"].ToString();
                obj.CustomerId = dtRoutineService_H.Rows[0]["CustomerId"].ToString();
                obj.CustomerName = dtRoutineService_H.Rows[0]["CustomerName"].ToString();
                if(dtRoutineService_H.Rows[0]["CustomerType"].ToString()!="")
                {
                    obj.CustomerType = dtRoutineService_H.Rows[0]["CustomerType"].ToString() == "A" ? "A:一般客戶" : "B:經銷商";
                }
                else
                {
                    obj.CustomerType = "";
                }

                obj.AddressSName = dtRoutineService_H.Rows[0]["AddressSName"].ToString();
                obj.Address = dtRoutineService_H.Rows[0]["Address"].ToString();
                obj.Contact = dtRoutineService_H.Rows[0]["Contact"].ToString();
                obj.TelNo = dtRoutineService_H.Rows[0]["TelNo"].ToString();
                obj.FaxNo = dtRoutineService_H.Rows[0]["FaxNo"].ToString();
                obj.ProductNo = dtRoutineService_H.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtRoutineService_H.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtRoutineService_H.Rows[0]["SerialNo"].ToString();
                obj.SaleDate = dtRoutineService_H.Rows[0]["SaleDate"].ToString();
                obj.WarrantySDate = dtRoutineService_H.Rows[0]["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRoutineService_H.Rows[0]["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.WarrantyEDate = dtRoutineService_H.Rows[0]["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRoutineService_H.Rows[0]["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.Confirmed = dtRoutineService_H.Rows[0]["Confirmed"].ToString();
                obj.Remark = dtRoutineService_H.Rows[0]["Remark"].ToString();
                obj.InternalRemark = dtRoutineService_H.Rows[0]["InternalRemark"].ToString();
                //obj.AssignCheck = dtRoutineService_H.Rows[0]["AssignCheck"].ToString();
                //obj.AssignDate = dtRoutineService_H.Rows[0]["AssignDate"].ToString();
                //obj.RoutineSerMan = dtRoutineService_H.Rows[0]["RoutineSerMan"].ToString();
                obj.NoOfPrints = int.Parse(dtRoutineService_H.Rows[0]["NoOfPrints"].ToString());
                //obj.Confirmor = dtRoutineService_H.Rows[0]["Confirmor"].ToString();
            }

            return obj;
        }

        public string GetSupportAplOrderNo(string OrderType)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select CodeMode,SerialNrCodeLength,AutoConfirm from OrderCategory where OrderType=@OrderType",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("OrderType",DbType.String,OrderType)
                });

            if (dtOrderCategory != null && dtOrderCategory.Rows.Count > 0)
            {
                string ManuscriptNum = "";
                if (dtOrderCategory.Rows[0]["CodeMode"].ToString() == "1")
                {
                    ManuscriptNum = DateTime.Now.ToString("yyyyMMdd");
                    object ManuscripNo = dbMySQL.ExecuteScalar("SELECT MAX(CodeNumber) FROM syscodenumbers WHERE CodeNumber like @CodeNumber and CodeType=@CodeType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("CodeNumber", DbType.String, ManuscriptNum+"%"),
                            DataAccessMySQL.CreateParameter("CodeType", DbType.String, OrderType)
                        });
                    string OrderNo = "";
                    string codeNum = "000000000001";
                    if (ManuscripNo == null || ManuscripNo.ToString() == "")
                    {
                        OrderNo = ManuscriptNum + codeNum.Substring(codeNum.Length - int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()), int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()));
                    }
                    else
                    {
                        if (dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString() != "0")
                        {
                            OrderNo = ManuscriptNum + (int.Parse(ManuscripNo.ToString().Substring(8)) + 1).ToString("000000000000".Substring(0, int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString())));
                        }
                        else
                        {
                            return "None-由於當前單別的單號格式是：yyyy/MM/dd，已使用，請換單別！";
                        }

                    }
                    dbMySQL.ExecuteScalar("insert into syscodenumbers (CodeType,CodeNumber) values (@CodeType,@CodeNumber)",
                       new DbParameter[]{
                            DataAccessMySQL.CreateParameter("CodeNumber", DbType.String, OrderNo),
                            DataAccessMySQL.CreateParameter("CodeType", DbType.String, OrderType)
                        });

                    return "YES-" + OrderNo + "-" + dtOrderCategory.Rows[0]["AutoConfirm"].ToString();
                }
                //else
                //{
                //    return "NO--" + dtOrderCategory.Rows[0]["AutoConfirm"].ToString();
                //}
                if (dtOrderCategory.Rows[0]["CodeMode"].ToString() == "2")
                {
                    ManuscriptNum = DateTime.Now.ToString("yyyyMM");
                    object ManuscripNo = dbMySQL.ExecuteScalar("SELECT MAX(CodeNumber) FROM syscodenumbers WHERE CodeNumber like @CodeNumber and CodeType=@CodeType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("CodeNumber", DbType.String, ManuscriptNum+"%"),
                            DataAccessMySQL.CreateParameter("CodeType", DbType.String, OrderType)
                        });

                    string OrderNo = "";
                    string codeNum = "000000000001";
                    if (ManuscripNo == null || ManuscripNo.ToString() == "")
                    {
                        OrderNo = ManuscriptNum + codeNum.Substring(codeNum.Length - int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()), int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()));
                    }
                    else
                    {
                        if (dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString() != "0")
                        {
                            OrderNo = ManuscriptNum + (int.Parse(ManuscripNo.ToString().Substring(6)) + 1).ToString("000000000000".Substring(0, int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString())));
                        }
                        else
                        {
                            return "None-由於當前單別的單號格式是：yyyy/MM，已使用，請換單別！";
                        }
                    }

                    dbMySQL.ExecuteScalar("insert into syscodenumbers (CodeType,CodeNumber) values (@CodeType,@CodeNumber)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("CodeNumber", DbType.String, OrderNo),
                            DataAccessMySQL.CreateParameter("CodeType", DbType.String, OrderType)
                        });

                    return "YES-" + OrderNo + "-" + dtOrderCategory.Rows[0]["AutoConfirm"].ToString();
                }
                //else
                //{
                //    return "NO--" + dtOrderCategory.Rows[0]["AutoConfirm"].ToString();
                //}
                if (dtOrderCategory.Rows[0]["CodeMode"].ToString() == "3")
                {
                    object ManuscripNo = dbMySQL.ExecuteScalar("SELECT MAX(CodeNumber) FROM syscodenumbers WHERE CodeNumber like @CodeNumber and CodeType=@CodeType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("CodeNumber", DbType.String, ManuscriptNum+"%"),
                            DataAccessMySQL.CreateParameter("CodeType", DbType.String, OrderType)
                        });

                    string OrderNo = "";
                    string codeNum = "000000000001";
                    if (ManuscripNo == null || ManuscripNo.ToString() == "")
                    {
                        OrderNo = codeNum.Substring(codeNum.Length - int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()), int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()));
                    }
                    else
                    {
                        OrderNo = (int.Parse(ManuscripNo.ToString()) + 1).ToString("000000000000".Substring(0, int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString())));
                    }

                    dbMySQL.ExecuteScalar("insert into syscodenumbers (CodeType,CodeNumber) values (@CodeType,@CodeNumber)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("CodeNumber", DbType.String, OrderNo),
                            DataAccessMySQL.CreateParameter("CodeType", DbType.String, OrderType)
                        });

                    return "YES-" + OrderNo + "-" + dtOrderCategory.Rows[0]["AutoConfirm"].ToString();

                }
                //else
                //{
                return "NO--" + dtOrderCategory.Rows[0]["AutoConfirm"].ToString();
                //}
            }
            else
            {
                return "None-單別不存在！";
            }

        }

        public bool UpdateAsign(Assignment rssignment, string SupportId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                string[] SupportIdArray = SupportId.Split(',');

                string[] valueInfo = null;

                string[] orderInfo = null;
                for (int i = 0; i < SupportIdArray.Length; i++)
                {
                    valueInfo = SupportIdArray[i].Split('-');

                    orderInfo = GetSupportAplOrderNo(rssignment.AssignOrderType).Split('-');

                    if (orderInfo[0].ToString() == "None")
                    {
                        return false;
                    }

                    dbMySQL.ExecuteNonQuery(@"update RoutineService_H set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,AssignCheck=@AssignCheck,AssignDate=@AssignDate,
                        RoutineSerMan=@RoutineSerMan where RoutineSerOrderType=@RoutineSerOrderType 
                        and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,rssignment.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rssignment.UserGroup),
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,rssignment.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String, rssignment.AssignDate),
                            DataAccessMySQL.CreateParameter("RoutineSerMan",DbType.String, rssignment.Designee),
                            DataAccessMySQL.CreateParameter("AssignCheck",DbType.String,"Y")
                        });

                    DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable(@"select * from RoutineService_H where RoutineSerOrderType=@RoutineSerOrderType 
                        and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,valueInfo[1].ToString()),
                        });
                    if (dtSupportApl_H != null && dtSupportApl_H.Rows.Count > 0)
                    {
                        dbMySQL.ExecuteNonQuery(@"insert into Assignment (Company,UserGroup,Creator,CreateDate,AssignOrderType,AssignOrderNo,OrderDate,CustomerId,
                        Version,SourceOrderType,SourceOrderNo,Designee,Assignor,AssignDate,NoOfPrints,Remark,Confirmed) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,@AssignOrderType,@AssignOrderNo,@OrderDate,@CustomerId,@Version,@SourceOrderType,@SourceOrderNo,
                        @Designee,@Assignor,@AssignDate,@NoOfPrints,@Remark,@Confirmed)", tran,
                            new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,rssignment.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rssignment.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,rssignment.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,rssignment.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,orderInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,dtSupportApl_H.Rows[0]["OrderDate"].ToString()),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtSupportApl_H.Rows[0]["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,"0000"),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("Designee",DbType.String,rssignment.Designee),
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,rssignment.Assignor),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,rssignment.AssignDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Decimal,0),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,dtSupportApl_H.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"N")
                            });
                    }

                }

                tran.Commit();
                return true;
            }
            catch (Exception ex)
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

        public bool UpdateAsignAgain(AssignmentChange assignmentChange, string SupportId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {

                tran = dbMySQL.CreateDbTransaction();

                string[] SupportIdArray = SupportId.Split(',');

                string[] valueInfo = null;

                for (int i = 0; i < SupportIdArray.Length; i++)
                {
                    valueInfo = SupportIdArray[i].Split('-');

                    DataTable dt = dbMySQL.ExecuteDataTable(@"select * from Assignment where SourceOrderType=@SourceOrderType 
                        and SourceOrderNo=@SourceOrderNo and Confirmed='Y'", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,valueInfo[1].ToString())
                        });

                    if (dt != null && dt.Rows.Count > 0)
                    {

                        if (int.Parse(
                            dbMySQL.ExecuteScalar(
                                @"select count(*) from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo and ConfirmedDate=''",
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("AssignOrderType", DbType.String,
                                        dt.Rows[0]["AssignOrderType"].ToString()),
                                    DataAccessMySQL.CreateParameter("AssignOrderNo", DbType.String,
                                        dt.Rows[0]["AssignOrderNo"].ToString())
                                }).ToString()) > 0)
                        {
                            //msg = "當前變更單有未確認，請先確認！";
                            return false;
                        }

                        dbMySQL.ExecuteNonQuery(@"insert into AssignmentChange (Company,UserGroup,Creator,CreateDate,AssignOrderType,AssignOrderNo,Version,OrderDate,ModiReason,
                        SourceOrderType,SourceOrderNo,Designee,Assignor,AssignDate,Remark,
                        OldDesignee,OldAssignor,OldAssignDate,OldRemark,Confirmor,ConfirmedDate) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,@AssignOrderType,@AssignOrderNo,@Version,@OrderDate,@ModiReason,
                        @SourceOrderType,@SourceOrderNo,@Designee,@Assignor,@AssignDate,@Remark,
                        @OldDesignee,@OldAssignor,@OldAssignDate,@OldRemark,@Confirmor,@ConfirmedDate)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,assignmentChange.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignmentChange.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,assignmentChange.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,dt.Rows[0]["AssignOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,dt.Rows[0]["AssignOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(Int32.Parse(dt.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,dt.Rows[0]["OrderDate"].ToString()),
                            DataAccessMySQL.CreateParameter("ModiReason",DbType.String,assignmentChange.ModiReason),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,dt.Rows[0]["SourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,dt.Rows[0]["SourceOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("Designee",DbType.String,dt.Rows[0]["Designee"].ToString()),
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,dt.Rows[0]["Assignor"].ToString()),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,dt.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("OldDesignee",DbType.String,dt.Rows[0]["Designee"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAssignor",DbType.String,dt.Rows[0]["Assignor"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAsignDate",DbType.String,dt.Rows[0]["AssignDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dt.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,assignmentChange.Confirmor),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,DateTime.Now.ToString("yyyyMMdd"))
                        });

                        dbMySQL.ExecuteNonQuery(@"update Assignment set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,Version=@Version,
                            Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Remark=@Remark,Modifier=@Modifier,ModiDate=@ModiDate where AssignOrderType=@AssignOrderType 
                            and AssignOrderNo=@AssignOrderNo", tran,
                                new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,assignmentChange.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignmentChange.UserGroup),
                                DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignmentChange.Modifier),
                                DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("Version",DbType.String,(Int32.Parse(dt.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("Designee",DbType.String,assignmentChange.Designee),
                                DataAccessMySQL.CreateParameter("Assignor",DbType.String,assignmentChange.Assignor),
                                DataAccessMySQL.CreateParameter("AsignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,dt.Rows[0]["Remark"].ToString()),
                                DataAccessMySQL.CreateParameter("Modifier",DbType.String,dt.Rows[0]["Modifier"].ToString()),
                                DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,dt.Rows[0]["AssignOrderType"].ToString()),
                                DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,dt.Rows[0]["AssignOrderNo"].ToString())
                            });
                        dbMySQL.ExecuteNonQuery(@"update RoutineService_H set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,AssignCheck=@AssignCheck,AssignDate=@AssignDate,
                        RoutineSerMan=@RoutineSerMan where RoutineSerOrderType=@RoutineSerOrderType 
                        and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,assignmentChange.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignmentChange.UserGroup),
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignmentChange.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String, assignmentChange.AssignDate),
                            DataAccessMySQL.CreateParameter("RoutineSerMan",DbType.String, assignmentChange.Designee),
                            DataAccessMySQL.CreateParameter("AssignCheck",DbType.String,"Y")
                        });
                    }
                }

                tran.Commit();
                return true;
            }
            catch (Exception ex)
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
    }
}