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
    public class RMAAplChangeDAL : IRMAAplChangeDAL
    {
        public bool AddRMAAplChange(RMAAplChange rMAAplChange, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtRMAApl = dbMySQL.ExecuteDataTable("select * from RMAApl where RMAAplType = @RMAAplType and RMAAplNo = @RMAAplNo",tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAAplChange.RMAAplType),
                        DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAAplChange.RMAAplNo)
                    });

                if(dtRMAApl!=null&& dtRMAApl.Rows.Count>0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into RMAAplChange (Company,UserGroup,Creator,CreateDate,
                    RMAAplType,RMAAplNo,Version,OrderDate,SourceOrderType,SourceOrderNo,
                    IsClosed,ChangeReason,CustomerId,ProductNo,ProductName,SerialNo,VendorWStartDate,
                    VendorWExpiryDate,WarrantyStartDate,WarrantyExpiryDate,
                    Confirmed,NoOfPrints,OptionDetail,Remark,Returned,TestResult,OldVersion,OldOptionDetail,OldRemark,OldTestResult) 
                    VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                    @RMAAplType,@RMAAplNo,@Version,@OrderDate,@SourceOrderType,@SourceOrderNo,
                    @IsClosed,@ChangeReason,@CustomerId,@ProductNo,@ProductName,@SerialNo,@VendorWStartDate,
                    @VendorWExpiryDate,@WarrantyStartDate,@WarrantyExpiryDate,
                    @Confirmed,@NoOfPrints,@OptionDetail,@Remark,@Returned,@TestResult,@OldVersion,@OldOptionDetail,@OldRemark,@OldTestResult)",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("Company",DbType.String,rMAAplChange.Company),
                        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rMAAplChange.UserGroup),
                        DataAccessMySQL.CreateParameter("Creator",DbType.String,rMAAplChange.Creator),
                        DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAAplChange.RMAAplType),
                        DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAAplChange.RMAAplNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRMAApl.Rows[0]["Version"].ToString())+1).ToString("0000")),
                        DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rMAAplChange.OrderDate),
                        DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,rMAAplChange.SourceOrderType),
                        DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,rMAAplChange.SourceOrderNo),
                        DataAccessMySQL.CreateParameter("IsClosed",DbType.String,rMAAplChange.IsClosed),
                        DataAccessMySQL.CreateParameter("ChangeReason",DbType.String,rMAAplChange.ChangeReason),
                        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,rMAAplChange.CustomerId),
                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,rMAAplChange.ProductNo),
                        DataAccessMySQL.CreateParameter("ProductName",DbType.String,rMAAplChange.ProductName),
                        DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rMAAplChange.SerialNo),
                        DataAccessMySQL.CreateParameter("VendorWStartDate",DbType.String,rMAAplChange.VendorWStartDate),
                        DataAccessMySQL.CreateParameter("VendorWExpiryDate",DbType.String,rMAAplChange.VendorWExpiryDate),
                        DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,rMAAplChange.WarrantyStartDate),
                        DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,rMAAplChange.WarrantyExpiryDate),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rMAAplChange.Confirmed),
                        DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rMAAplChange.NoOfPrints),
                        DataAccessMySQL.CreateParameter("OptionDetail",DbType.String,rMAAplChange.OptionDetail),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,rMAAplChange.Remark),
                        DataAccessMySQL.CreateParameter("TestResult",DbType.String,rMAAplChange.TestResult),
                        DataAccessMySQL.CreateParameter("Returned",DbType.String,rMAAplChange.Returned),
                        DataAccessMySQL.CreateParameter("OldVersion",DbType.String,dtRMAApl.Rows[0]["Version"].ToString()),
                        DataAccessMySQL.CreateParameter("OldOptionDetail",DbType.String,dtRMAApl.Rows[0]["OptionDetail"].ToString()),
                        DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtRMAApl.Rows[0]["Remark"].ToString()),
                        DataAccessMySQL.CreateParameter("OldTestResult",DbType.String,dtRMAApl.Rows[0]["TestResult"].ToString()), 
                    });


                    dbMySQL.ExecuteNonQuery(@"update RMAApl set Version=@Version,OptionDetail=@OptionDetail,Remark=@Remark,
                        TestResult=@TestResult where RMAAplType = @RMAAplType and RMAAplNo = @RMAAplNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAAplChange.RMAAplType),
                            DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAAplChange.RMAAplNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRMAApl.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OptionDetail",DbType.String,rMAAplChange.OptionDetail),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rMAAplChange.Remark),
                            DataAccessMySQL.CreateParameter("TestResult",DbType.String,rMAAplChange.TestResult)
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

        public bool UpdateRMAAplChange(RMAAplChange rMAAplChange, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtRMAApl = dbMySQL.ExecuteDataTable("select * from RMAApl where RMAAplType = @RMAAplType and RMAAplNo = @RMAAplNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAAplChange.RMAAplType),
                        DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAAplChange.RMAAplNo)
                    });

                if (dtRMAApl != null && dtRMAApl.Rows.Count > 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into RMAAplChange (Company,UserGroup,Creator,CreateDate,
                    RMAAplType,RMAAplNo,Version,OrderDate,SourceOrderType,SourceOrderNo,
                    IsClosed,ChangeReason,CustomerId,ProductNo,ProductName,SerialNo,VendorWStartDate,
                    VendorWExpiryDate,WarrantyStartDate,WarrantyExpiryDate,
                    Confirmed,NoOfPrints,OptionDetail,Remark,Returned,TestResult,OldVersion,OldOptionDetail,OldRemark,OldTestResult) 
                    VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                    @RMAAplType,@RMAAplNo,@Version,@OrderDate,@SourceOrderType,@SourceOrderNo,
                    @IsClosed,@ChangeReason,@CustomerId,@ProductNo,@ProductName,@SerialNo,@VendorWStartDate,
                    @VendorWExpiryDate,@WarrantyStartDate,@WarrantyExpiryDate,
                    @Confirmed,@NoOfPrints,@OptionDetail,@Remark,@Returned,@TestResult,@OldVersion,@OldOptionDetail,@OldRemark,@OldTestResult)",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("Company",DbType.String,rMAAplChange.Company),
                        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rMAAplChange.UserGroup),
                        DataAccessMySQL.CreateParameter("Creator",DbType.String,rMAAplChange.Creator),
                        DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAAplChange.RMAAplType),
                        DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAAplChange.RMAAplNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRMAApl.Rows[0]["Version"].ToString())+1).ToString("0000")),
                        DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rMAAplChange.OrderDate),
                        DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,rMAAplChange.SourceOrderType),
                        DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,rMAAplChange.SourceOrderNo),
                        DataAccessMySQL.CreateParameter("IsClosed",DbType.String,rMAAplChange.IsClosed),
                        DataAccessMySQL.CreateParameter("ChangeReason",DbType.String,rMAAplChange.ChangeReason),
                        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,rMAAplChange.CustomerId),
                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,rMAAplChange.ProductNo),
                        DataAccessMySQL.CreateParameter("ProductName",DbType.String,rMAAplChange.ProductName),
                        DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rMAAplChange.SerialNo),
                        DataAccessMySQL.CreateParameter("VendorWStartDate",DbType.String,rMAAplChange.VendorWStartDate),
                        DataAccessMySQL.CreateParameter("VendorWExpiryDate",DbType.String,rMAAplChange.VendorWExpiryDate),
                        DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,rMAAplChange.WarrantyStartDate),
                        DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,rMAAplChange.WarrantyExpiryDate),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rMAAplChange.Confirmed),
                        DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rMAAplChange.NoOfPrints),
                        DataAccessMySQL.CreateParameter("OptionDetail",DbType.String,rMAAplChange.OptionDetail),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,rMAAplChange.Remark),
                        DataAccessMySQL.CreateParameter("TestResult",DbType.String,rMAAplChange.TestResult),
                        DataAccessMySQL.CreateParameter("Returned",DbType.String,rMAAplChange.Returned),
                        DataAccessMySQL.CreateParameter("OldVersion",DbType.String,dtRMAApl.Rows[0]["Version"].ToString()),
                        DataAccessMySQL.CreateParameter("OldOptionDetail",DbType.String,dtRMAApl.Rows[0]["OptionDetail"].ToString()),
                        DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtRMAApl.Rows[0]["Remark"].ToString()),
                        DataAccessMySQL.CreateParameter("OldTestResult",DbType.String,dtRMAApl.Rows[0]["TestResult"].ToString()),
                    });


                    dbMySQL.ExecuteNonQuery(@"update RMAApl set Version=@Version,OptionDetail=@OptionDetail,Remark=@Remark,
                        TestResult=@TestResult where RMAAplType = @RMAAplType and RMAAplNo = @RMAAplNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAAplChange.RMAAplType),
                            DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAAplChange.RMAAplNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRMAApl.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OptionDetail",DbType.String,rMAAplChange.OptionDetail),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rMAAplChange.Remark),
                            DataAccessMySQL.CreateParameter("TestResult",DbType.String,rMAAplChange.TestResult)
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

        public bool DelRMAAplChange(RMAAplChange rMAAplChange, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtRMAAplChange = dbMySQL.ExecuteDataTable("select * from RMAAplChange where RMAAplType=@RMAAplType and RMAAplNo=@RMAAplNo and Version=@Version", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAAplChange.RMAAplType),
                        DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAAplChange.RMAAplNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,rMAAplChange.Version)
                    });
                if(dtRMAAplChange!=null&& dtRMAAplChange.Rows.Count>0)
                {
                    dbMySQL.ExecuteNonQuery(@"update RMAApl set Version=@Version,OptionDetail=@OptionDetail,Remark=@Remark,
                        TestResult=@TestResult,Returned=@Returned where RMAAplType = @RMAAplType and RMAAplNo = @RMAAplNo", tran,
                       new DbParameter[] {
                            DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAAplChange.RMAAplType),
                            DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAAplChange.RMAAplNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,dtRMAAplChange.Rows[0]["OldVersion"].ToString()),
                            DataAccessMySQL.CreateParameter("OptionDetail",DbType.String,dtRMAAplChange.Rows[0]["OldOptionDetail"].ToString()),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,dtRMAAplChange.Rows[0]["OldRemark"].ToString()),
                            DataAccessMySQL.CreateParameter("TestResult",DbType.String,dtRMAAplChange.Rows[0]["OldTestResult"].ToString())
                       });
                }
                dbMySQL.ExecuteNonQuery(@"delete from RMAAplChange  where 
                        RMAAplType=@RMAAplType and RMAAplNo=@RMAAplNo and Version=@Version",tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAAplChange.RMAAplType),
                            DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAAplChange.RMAAplNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,rMAAplChange.Version)
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

        public List<RMAAplChange> SearchRMAAplChange(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RMAAplChange> objRMAAplChange = new List<RMAAplChange>();

            string sql = @"select r.*,c.CustomerName,o.OrderSName,
                e.EmployeeName as CreatorName from RMAAplChange as r
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
                    RMAAplChange obj = new RMAAplChange();
                    obj.RMAAplType = dr["RMAAplType"].ToString();
                    obj.OrderSName = dr["OrderSName"].ToString();
                    obj.RMAAplNo = dr["RMAAplNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    //if(dr["StatusCode"].ToString()=="0")
                    //{
                    //    obj.SerialNo = "0.未處理";
                    //}
                    //if (dr["StatusCode"].ToString() == "1")
                    //{
                    //    obj.SerialNo = "1.未審核";
                    //}
                    //if (dr["StatusCode"].ToString() == "2")
                    //{
                    //    obj.SerialNo = "2.拒絕申請";
                    //}
                    //if (dr["StatusCode"].ToString() == "3")
                    //{
                    //    obj.SerialNo = "3.允許申請";
                    //}
                    //if (dr["StatusCode"].ToString() == "4")
                    //{
                    //    obj.SerialNo = "4.作廢";
                    //}
                    obj.Creator = dr["Creator"].ToString();
                    obj.CreatorName = dr["CreatorName"].ToString();
                    objRMAAplChange.Add(obj);
                }
            }

            return objRMAAplChange;
        }

        public RMAAplChange GetRMAAplChangeInfo(RMAAplChange rMAAplChange)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            RMAAplChange obj = null;

            DataTable dtRMAAplChange = dbMySQL.ExecuteDataTable(@"select * from RMAAplChange 
                where RMAAplType=@RMAAplType AND RMAAplNo=@RMAAplNo and Version=@Version",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,rMAAplChange.RMAAplType),
                    DataAccessMySQL.CreateParameter("RMAAplNo",DbType.String,rMAAplChange.RMAAplNo),
                    DataAccessMySQL.CreateParameter("Version",DbType.String,rMAAplChange.Version)
                });

            if (dtRMAAplChange.Rows.Count > 0)
            {
                obj = new RMAAplChange();
                obj.RMAAplType = dtRMAAplChange.Rows[0]["RMAAplType"].ToString();
                obj.RMAAplNo = dtRMAAplChange.Rows[0]["RMAAplNo"].ToString();
                obj.OrderDate = dtRMAAplChange.Rows[0]["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRMAAplChange.Rows[0]["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.Version = dtRMAAplChange.Rows[0]["Version"].ToString();
                obj.SourceOrderType = dtRMAAplChange.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtRMAAplChange.Rows[0]["SourceOrderNo"].ToString();
                obj.IsClosed = dtRMAAplChange.Rows[0]["IsClosed"].ToString();
                obj.ChangeReason = dtRMAAplChange.Rows[0]["ChangeReason"].ToString();
                obj.CustomerId = dtRMAAplChange.Rows[0]["CustomerId"].ToString();
                obj.ProductNo = dtRMAAplChange.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtRMAAplChange.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtRMAAplChange.Rows[0]["SerialNo"].ToString();
                obj.VendorWStartDate = dtRMAAplChange.Rows[0]["VendorWStartDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRMAAplChange.Rows[0]["VendorWStartDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.VendorWExpiryDate = dtRMAAplChange.Rows[0]["VendorWExpiryDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRMAAplChange.Rows[0]["VendorWExpiryDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.WarrantyStartDate = dtRMAAplChange.Rows[0]["WarrantyStartDate"].ToString();
                obj.WarrantyExpiryDate = dtRMAAplChange.Rows[0]["WarrantyExpiryDate"].ToString();
                obj.Confirmed = dtRMAAplChange.Rows[0]["Confirmed"].ToString() == "Y" ? "Y:已確認" : "N:未確認";
                obj.NoOfPrints = int.Parse(dtRMAAplChange.Rows[0]["NoOfPrints"].ToString());
                obj.OptionDetail = dtRMAAplChange.Rows[0]["OptionDetail"].ToString();
                obj.Remark = dtRMAAplChange.Rows[0]["Remark"].ToString();
                obj.TestResult = dtRMAAplChange.Rows[0]["TestResult"].ToString();
                obj.Returned = dtRMAAplChange.Rows[0]["Returned"].ToString();
                obj.OldVersion = dtRMAAplChange.Rows[0]["OldVersion"].ToString();
                obj.OldOptionDetail = dtRMAAplChange.Rows[0]["OldOptionDetail"].ToString();
                obj.OldRemark = dtRMAAplChange.Rows[0]["OldRemark"].ToString();
                obj.OldTestResult = dtRMAAplChange.Rows[0]["OldTestResult"].ToString();
            }

            return obj;
        }
        
    }
}