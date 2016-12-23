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
    public class RMAAplDAL : IRMAAplDAL
    {
        public bool AddRMAApl(RMAApl rMAApl, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                string count = dbMySQL.ExecuteScalar(@"select count(*) from RMAApl where 
                    RMAAplType=@RMAAplType and RMAAplNo=@RMAAplNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAApl.RMAAplType),
                        DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAApl.RMAAplNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    tran = dbMySQL.CreateDbTransaction();
                    dbMySQL.ExecuteNonQuery(@"insert into RMAApl (Company,UserGroup,Creator,CreateDate,Confirmor,ConfirmedDate,
                        RMAAplType,RMAAplNo,OrderDate,Version,NoOfPrints,SourceOrderType,SourceOrderNo,
                        CustomerId,ProductNo,ProductName,SerialNo,VendorWarrantyStartDate,
                        VendorWarrantyExpiryDate,UnderWarranty,WarrantyStartDate,WarrantyExpiryDate,
                        Confirmed,Closed,StatusCode,OptionDetail,Remark,TestResult,Returned) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,@Confirmor,@ConfirmedDate,
                        @RMAAplType,@RMAAplNo,@OrderDate,@Version,@NoOfPrints,@SourceOrderType,@SourceOrderNo,
                        @CustomerId,@ProductNo,@ProductName,@SerialNo,@VendorWarrantyStartDate,
                        @VendorWarrantyExpiryDate,@UnderWarranty,@WarrantyStartDate,@WarrantyExpiryDate,
                        @Confirmed,@Closed,@StatusCode,@OptionDetail,@Remark,@TestResult,@Returned)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,rMAApl.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rMAApl.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,rMAApl.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,rMAApl.Confirmor),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,rMAApl.ConfirmedDate),
                            DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAApl.RMAAplType),
                            DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAApl.RMAAplNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rMAApl.OrderDate),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,rMAApl.Version),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.String,rMAApl.NoOfPrints),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,rMAApl.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,rMAApl.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,rMAApl.CustomerId),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,rMAApl.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,rMAApl.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rMAApl.SerialNo),
                            DataAccessMySQL.CreateParameter("VendorWarrantyStartDate",DbType.String,rMAApl.VendorWarrantyStartDate),
                            DataAccessMySQL.CreateParameter("VendorWarrantyExpiryDate",DbType.String,rMAApl.VendorWarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("UnderWarranty",DbType.String,rMAApl.UnderWarranty),
                            DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,rMAApl.WarrantyStartDate),
                            DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,rMAApl.WarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rMAApl.Confirmed),
                            DataAccessMySQL.CreateParameter("Closed",DbType.String,"N"),
                            DataAccessMySQL.CreateParameter("StatusCode",DbType.String,rMAApl.StatusCode),
                            DataAccessMySQL.CreateParameter("OptionDetail",DbType.String,rMAApl.OptionDetail),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rMAApl.Remark),
                            DataAccessMySQL.CreateParameter("TestResult",DbType.String,rMAApl.TestResult),
                            DataAccessMySQL.CreateParameter("Returned",DbType.String,rMAApl.Returned)
                        });

                    if(!String.IsNullOrEmpty(rMAApl.SourceOrderType))
                    {
                        dbMySQL.ExecuteNonQuery(@"insert into RepairRecord (ProductNo,SerialNo,RGAType,RGANo,ChangeDate,
                        ChangeOrderType,ChangeOrderNo,ChangeMode) VALUES (@ProductNo,@SerialNo,@RGAType,@RGANo,@ChangeDate,
                        @ChangeOrderType,@ChangeOrderNo,@ChangeMode)", tran, new DbParameter[] {
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,rMAApl.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rMAApl.SerialNo),
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,rMAApl.SourceOrderType),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,rMAApl.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("ChangeDate",DbType.String,rMAApl.OrderDate),
                            DataAccessMySQL.CreateParameter("ChangeOrderType",DbType.String,rMAApl.RMAAplType),
                            DataAccessMySQL.CreateParameter("ChangeOrderNo",DbType.String,rMAApl.RMAAplNo),
                            DataAccessMySQL.CreateParameter("ChangeMode",DbType.String,"15")
                        });
                    }
                    if(!String.IsNullOrEmpty(rMAApl.ConfirmedDate))
                    {
                        dbMySQL.ExecuteNonQuery(@"insert into RepairRecord (ProductNo,SerialNo,RGAType,RGANo,ChangeDate,
                        ChangeOrderType,ChangeOrderNo,ChangeMode) VALUES (@ProductNo,@SerialNo,@RGAType,@RGANo,@ChangeDate,
                        @ChangeOrderType,@ChangeOrderNo,@ChangeMode)", tran, new DbParameter[] {
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,rMAApl.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rMAApl.SerialNo),
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,rMAApl.SourceOrderType),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,rMAApl.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("ChangeDate",DbType.String,rMAApl.ConfirmedDate),
                            DataAccessMySQL.CreateParameter("ChangeOrderType",DbType.String,rMAApl.RMAAplType),
                            DataAccessMySQL.CreateParameter("ChangeOrderNo",DbType.String,rMAApl.RMAAplNo),
                            DataAccessMySQL.CreateParameter("ChangeMode",DbType.String,"20")
                        });
                    }
                    tran.Commit();
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "品牌代號已存在！";
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

        public bool UpdateRMAApl(RMAApl rMAApl, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"update SaleSerial set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,ModiDate=@ModiDate,
                        SourceOrderType=@SourceOrderType,SourceOrderNo=@SourceOrderNo,CustomerId=@CustomerId,ProductNo=@ProductNo,
                        ProductName=@ProductName,SerialNo=@SerialNo,VendorWarrantyStartDate=@VendorWarrantyStartDate,
                        VendorWarrantyExpiryDate=@VendorWarrantyExpiryDate,UnderWarranty=@UnderWarranty,WarrantyStartDate=@WarrantyStartDate,
                        WarrantyExpiryDate=@WarrantyExpiryDate,Confirmed=@Confirmed,Closed=@Closed,StatusCode=@StatusCode,
                        OptionDetail=@OptionDetail,Remark=@Remark,TestResult=@TestResult,Returned=@Returned where 
                        ProductNo=@ProductNo and SerialNo=@SerialNo and CustomerId=@CustomerId and SaleDate=@SaleDate",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,rMAApl.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rMAApl.UserGroup),
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,rMAApl.Creator),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAApl.RMAAplType),
                            DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAApl.RMAAplNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rMAApl.OrderDate),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,rMAApl.Version),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.String,rMAApl.NoOfPrints),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,rMAApl.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,rMAApl.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,rMAApl.CustomerId),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,rMAApl.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,rMAApl.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rMAApl.SerialNo),
                            DataAccessMySQL.CreateParameter("VendorWarrantyStartDate",DbType.String,rMAApl.VendorWarrantyStartDate),
                            DataAccessMySQL.CreateParameter("VendorWarrantyExpiryDate",DbType.String,rMAApl.VendorWarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("UnderWarranty",DbType.String,rMAApl.UnderWarranty),
                            DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,rMAApl.WarrantyStartDate),
                            DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,rMAApl.WarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rMAApl.Confirmed),
                            DataAccessMySQL.CreateParameter("Closed",DbType.String,rMAApl.Closed),
                            DataAccessMySQL.CreateParameter("StatusCode",DbType.String,rMAApl.StatusCode),
                            DataAccessMySQL.CreateParameter("OptionDetail",DbType.String,rMAApl.OptionDetail),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rMAApl.Remark),
                            DataAccessMySQL.CreateParameter("TestResult",DbType.String,rMAApl.TestResult),
                            DataAccessMySQL.CreateParameter("Returned",DbType.String,rMAApl.Returned)
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

        public bool DelRMAApl(RMAApl rMAApl, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"delete from RMAApl  where 
                        RMAAplType=@RMAAplType and RMAAplNo=@RMAAplNo",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAApl.RMAAplType),
                            DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAApl.RMAAplNo)
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

        public List<RMAApl> SearchRMAApl(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RMAApl> objRMAApl = new List<RMAApl>();

            string sql = @"select r.*,c.CustomerName,o.OrderSName,
                e.EmployeeName as CreatorName from RMAApl as r
                left join customer as c on c.CustomerId=r.CustomerId
                left join OrderCategory as o on o.OrderType=r.RMAAplType
                left join Employee as e on e.EmployeeId=r.Creator where 1=1";

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


            DataTable dtRMAApl = null;
            if (strCondition != "")
            {
                dtRMAApl = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRMAApl = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtRMAApl.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRMAApl.Rows)
                {
                    RMAApl obj = new RMAApl();
                    obj.RMAAplType = dr["RMAAplType"].ToString();
                    obj.OrderSName = dr["OrderSName"].ToString();
                    obj.RMAAplNo = dr["RMAAplNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    if(dr["StatusCode"].ToString()=="0")
                    {
                        obj.StatusCode = "0.未處理";
                    }
                    if (dr["StatusCode"].ToString() == "1")
                    {
                        obj.StatusCode = "1.未審核";
                    }
                    if (dr["StatusCode"].ToString() == "2")
                    {
                        obj.StatusCode = "2.拒絕申請";
                    }
                    if (dr["StatusCode"].ToString() == "3")
                    {
                        obj.StatusCode = "3.允許申請";
                    }
                    if (dr["StatusCode"].ToString() == "4")
                    {
                        obj.StatusCode = "4.作廢";
                    }
                    obj.Creator = dr["Creator"].ToString();
                    obj.CreatorName = dr["CreatorName"].ToString();
                    objRMAApl.Add(obj);
                }
            }

            return objRMAApl;
        }

        public List<RMAApl> SearchRMAAplInfo(RMAApl rMAApl, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RMAApl> objRMAApl = new List<RMAApl>();

            string sql = @"select r.*,c.CustomerName,o.OrderSName,
                e.EmployeeName as CreatorName from RMAApl as r
                left join customer as c on c.CustomerId=r.CustomerId
                left join OrderCategory as o on o.OrderType=r.RMAAplType
                left join Employee as e on e.EmployeeId=r.Creator where 1=1";

            if (!String.IsNullOrEmpty(rMAApl.RMAAplType))
            {
                sql += " and r.RMAAplType like @RMAAplType";
            }

            if (!String.IsNullOrEmpty(rMAApl.RMAAplNo))
            {
                sql += " and r.RMAAplNo like @RMAAplNo";
            }

            DataTable dtRMAApl = dbMySQL.ExecuteDataTable(sql);
            if (dtRMAApl.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRMAApl.Rows)
                {
                    RMAApl obj = new RMAApl();
                    obj.RMAAplType = dr["RMAAplType"].ToString();
                    obj.OrderSName = dr["OrderSName"].ToString();
                    obj.RMAAplNo = dr["RMAAplNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    if (dr["StatusCode"].ToString() == "0")
                    {
                        obj.StatusCode = "0.未處理";
                    }
                    if (dr["StatusCode"].ToString() == "1")
                    {
                        obj.StatusCode = "1.未審核";
                    }
                    if (dr["StatusCode"].ToString() == "2")
                    {
                        obj.StatusCode = "2.拒絕申請";
                    }
                    if (dr["StatusCode"].ToString() == "3")
                    {
                        obj.StatusCode = "3.允許申請";
                    }
                    if (dr["StatusCode"].ToString() == "4")
                    {
                        obj.StatusCode = "4.作廢";
                    }
                    obj.Creator = dr["Creator"].ToString();
                    obj.CreatorName = dr["CreatorName"].ToString();
                    objRMAApl.Add(obj);
                }
            }

            return objRMAApl;
        }

        public RMAApl GetRMAAplInfo(RMAApl rMAApl)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            RMAApl obj = null;

            DataTable dtRMAApl = dbMySQL.ExecuteDataTable(@"select r.*,o.OrderSName,
                e.EmployeeName as CreatorName,ee.EmployeeName as ConfirmorName,CustomerName,CustomerType from RMAApl as r
                left join OrderCategory as o on o.OrderType=r.RMAAplType
                left join Employee as e on e.EmployeeId=r.Creator 
                left join Employee as ee on ee.EmployeeId=r.Confirmor 
                left join Customer as c on c.CustomerId=r.CustomerId
                where RMAAplType=@RMAAplType AND RMAAplNo=@RMAAplNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAApl.RMAAplType),
                    DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAApl.RMAAplNo)
                });

            if (dtRMAApl.Rows.Count > 0)
            {
                obj = new RMAApl();
                obj.RMAAplType = dtRMAApl.Rows[0]["RMAAplType"].ToString();
                obj.OrderSName = dtRMAApl.Rows[0]["OrderSName"].ToString();
                obj.RMAAplNo = dtRMAApl.Rows[0]["RMAAplNo"].ToString();
                obj.Creator= dtRMAApl.Rows[0]["Creator"].ToString();
                obj.CreatorName = dtRMAApl.Rows[0]["CreatorName"].ToString();
                obj.Confirmor = dtRMAApl.Rows[0]["Confirmor"].ToString();
                obj.ConfirmorName = dtRMAApl.Rows[0]["ConfirmorName"].ToString();
                obj.CustomerName = dtRMAApl.Rows[0]["CustomerName"].ToString();
                if(dtRMAApl.Rows[0]["CustomerType"].ToString()!="")
                {
                    obj.CustomerType = dtRMAApl.Rows[0]["CustomerType"].ToString() == "A" ? "A:一般客戶" : "B:經銷商";
                }
                else
                {
                    obj.CustomerType = "";
                }

                obj.OrderDate = dtRMAApl.Rows[0]["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRMAApl.Rows[0]["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.Version = dtRMAApl.Rows[0]["Version"].ToString();
                obj.NoOfPrints = int.Parse(dtRMAApl.Rows[0]["NoOfPrints"].ToString());
                obj.SourceOrderType = dtRMAApl.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtRMAApl.Rows[0]["SourceOrderNo"].ToString();
                obj.CustomerId = dtRMAApl.Rows[0]["CustomerId"].ToString();
                obj.ProductNo = dtRMAApl.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtRMAApl.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtRMAApl.Rows[0]["SerialNo"].ToString();
                obj.VendorWarrantyStartDate = dtRMAApl.Rows[0]["VendorWarrantyStartDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRMAApl.Rows[0]["VendorWarrantyStartDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.VendorWarrantyExpiryDate = dtRMAApl.Rows[0]["VendorWarrantyExpiryDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRMAApl.Rows[0]["VendorWarrantyExpiryDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.UnderWarranty = dtRMAApl.Rows[0]["UnderWarranty"].ToString();
                obj.WarrantyStartDate = dtRMAApl.Rows[0]["WarrantyStartDate"].ToString();
                obj.WarrantyExpiryDate = dtRMAApl.Rows[0]["WarrantyExpiryDate"].ToString();
                if(dtRMAApl.Rows[0]["Confirmed"].ToString()=="Y")
                {
                    obj.Confirmed = "Y:已接收";
                }
                if (dtRMAApl.Rows[0]["Confirmed"].ToString() == "N")
                {
                    obj.Confirmed = "N:未接收";
                }
                if (dtRMAApl.Rows[0]["Confirmed"].ToString() == "V")
                {
                    obj.Confirmed = "V:作廢";
                }
                obj.Closed = dtRMAApl.Rows[0]["Closed"].ToString();
                obj.StatusCode = dtRMAApl.Rows[0]["StatusCode"].ToString();
                obj.OptionDetail = dtRMAApl.Rows[0]["OptionDetail"].ToString();
                obj.Remark = dtRMAApl.Rows[0]["Remark"].ToString();
                obj.TestResult = dtRMAApl.Rows[0]["TestResult"].ToString();
                obj.Returned = dtRMAApl.Rows[0]["Returned"].ToString();
            }

            return obj;
        }
        
    }
}