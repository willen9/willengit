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
    public class SupportApl_HDAL : ISupportApl_HDAL
    {
        public List<SupportApl_H> GetSupportItem(SupportApl_H supportApl_H, string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<SupportApl_H> objSupportApl_H = new List<SupportApl_H>();

            string sql = @"select h.SupportAplOrderType,o.OrderSName,h.SupportAplOrderNo,h.CustomerId,
                        c.customerName,h.SupportDept,d.DepartmentName as SupportDeptName,
                        h.RequestDate,h.AsignCheck,h.AsignDate,h.AsignMan,
                        e.EmployeeName as AsignManName,h.Creator,ee.EmployeeName as CreatorName,
                        h.ProcessMan,eee.EmployeeName as ProcessManName,h.IsPicking,
                        h.SourceOrderNo,h.CustomerType,h.OrderStatus,h.Confirmed,h.IsPicking from SupportApl_H as h
                        left join  Customer as c on h.CustomerId=c.CustomerId 
                        left join Department as d on d.DepartmentId=h.SupportDept
                        left join Employee as e on e.EmployeeId=h.AsignMan 
                        left join Employee as ee on ee.EmployeeId=h.Creator
                        left join Employee as eee on eee.EmployeeId=h.ProcessMan
                        left join OrderCategory as o on o.OrderType=h.SupportAplOrderType where 1=1";

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            string strCondition = "";
            int len = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]))
                {
                    len++;
                }
            }

            DbParameter[] dbParameters = new DbParameter[len - 1];
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

            DataTable dtSupportApl_H = null;
            if (strCondition != "")
            {
                dtSupportApl_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtSupportApl_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtSupportApl_H.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSupportApl_H.Rows)
                {
                    SupportApl_H obj = new SupportApl_H();
                    obj.SupportAplOrderType = dr["SupportAplOrderType"].ToString();
                    obj.OrderSName = dr["OrderSName"].ToString();
                    obj.SupportAplOrderNo = dr["SupportAplOrderNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    if(dr["CustomerType"].ToString()!="")
                    {
                        obj.CustomerType = dr["CustomerType"].ToString()=="A"? "A:一般客戶" : "B:經銷商";
                    }else
                    {
                        obj.CustomerType = "";
                    }
                    obj.IsPicking = dr["IsPicking"].ToString();
                    obj.RequestDate = dr["RequestDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["RequestDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.SupportDept = dr["SupportDept"].ToString();
                    obj.SupportDeptName = dr["SupportDeptName"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.AsignDate = dr["AsignDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["AsignDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.AsignMan = dr["AsignMan"].ToString();
                    obj.AsignManName = dr["AsignManName"].ToString();
                    obj.Creator = dr["Creator"].ToString();
                    obj.CreatorName = dr["CreatorName"].ToString();
                    obj.ProcessMan = dr["ProcessMan"].ToString();
                    obj.ProcessManName = dr["ProcessManName"].ToString();
                    if(dr["OrderStatus"].ToString()=="0")
                    {
                        obj.OrderStatus = "0:未處理";
                    }
                    if (dr["OrderStatus"].ToString() == "1")
                    {
                        obj.OrderStatus = "1:已派工";
                    }
                    if (dr["OrderStatus"].ToString() == "2")
                    {
                        obj.OrderStatus = "2:完工";
                    }
                    if (dr["OrderStatus"].ToString() == "3")
                    {
                        obj.OrderStatus = "3.作廢";
                    }
                    obj.AsignCheck = dr["AsignCheck"].ToString();
                    obj.Confirmed = dr["Confirmed"].ToString();
                    obj.IsPicking = dr["IsPicking"].ToString();
                    objSupportApl_H.Add(obj);
                }
            }

            return objSupportApl_H;
        }

        public string GetSupportAplOrderNo(string OrderType)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select CodeMode,SerialNrCodeLength,AutoConfirm from OrderCategory where OrderType=@OrderType",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("OrderType",DbType.String,OrderType)
                });

            if(dtOrderCategory!=null&&dtOrderCategory.Rows.Count>0)
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
                        if(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()!="0")
                        {
                            OrderNo = ManuscriptNum + (int.Parse(ManuscripNo.ToString().Substring(8)) + 1).ToString("000000000000".Substring(0, int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString())));
                        }else
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
                    string codeNum="000000000001";
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
            }else
            {
                return "None-單別不存在！";
            }

        }

        public bool AddSupportApl_H(SupportApl_H supportApl_H, string strsupportitemid, string strsupportitemname, 
            string strremark, string strproductno, string strproductname, string strqty,
            string strunit, string strproductdremark, string strProcessDate, string strProcessExplanation, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from supportapl_h where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into supportapl_h (Company,UserGroup,Creator,CreateDate,SupportAplOrderType,SupportAplOrderNo,OrderDate,ApplyDate,CustomerId,
                        CustomerType,SalesId,AddressSName,Address,TelNo,FaxNo,Remark,NoOfPrints,Picking,IsPicking,RequestDate,RequestTime,
                        SupportDept,SourceOrderNo,AsignDate,AsignMan,ProcessMan,CompletionDate,CompletionMan,OrderStatus,Confirmed,Contact,AsignCheck) values (
                        @Company,@UserGroup,@Creator,@CreateDate,@SupportAplOrderType,@SupportAplOrderNo,@OrderDate,@ApplyDate,@CustomerId,
                        @CustomerType,@SalesId,@AddressSName,@Address,@TelNo,@FaxNo,@Remark,@NoOfPrints,@Picking,@IsPicking,@RequestDate,@RequestTime,
                        @SupportDept,@SourceOrderNo,@AsignDate,@AsignMan,@ProcessMan,@CompletionDate,@CompletionMan,@OrderStatus,@Confirmed,@Contact,@AsignCheck)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,supportApl_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportApl_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,supportApl_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,supportApl_H.OrderDate),
                            DataAccessMySQL.CreateParameter("ApplyDate",DbType.String,supportApl_H.ApplyDate),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,supportApl_H.CustomerId),
                            DataAccessMySQL.CreateParameter("CustomerType",DbType.String,supportApl_H.CustomerType),
                            DataAccessMySQL.CreateParameter("SalesId",DbType.String,supportApl_H.SalesId),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,supportApl_H.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,supportApl_H.Address),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,supportApl_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,supportApl_H.FaxNo),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,supportApl_H.Remark),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,supportApl_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("Picking",DbType.String,supportApl_H.Picking),
                            DataAccessMySQL.CreateParameter("IsPicking",DbType.String,supportApl_H.IsPicking),
                            DataAccessMySQL.CreateParameter("RequestDate",DbType.String,supportApl_H.RequestDate),
                            DataAccessMySQL.CreateParameter("RequestTime",DbType.String,supportApl_H.RequestTime),
                            DataAccessMySQL.CreateParameter("SupportDept",DbType.String,supportApl_H.SupportDept),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,supportApl_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("AsignDate",DbType.String,supportApl_H.AsignDate),
                            DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportApl_H.AsignMan),
                            DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportApl_H.ProcessMan),
                            DataAccessMySQL.CreateParameter("CompletionDate",DbType.String,supportApl_H.CompletionDate),
                            DataAccessMySQL.CreateParameter("CompletionMan",DbType.String,supportApl_H.CompletionMan),
                            DataAccessMySQL.CreateParameter("OrderStatus",DbType.String,supportApl_H.OrderStatus),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,supportApl_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,supportApl_H.Contact),
                            DataAccessMySQL.CreateParameter("AsignCheck",DbType.String,"N")
                        });

                    string sql = "";

                    if(strsupportitemid!="")
                    {
                        string[] strsupportitemidArray = strsupportitemid.Split(',');
                        string[] strsupportitemnameArray = strsupportitemname.Split(',');
                        string[] strremarkArray = strremark.Split(',');

                        sql = @"insert into supportapl_supportitem (Company,UserGroup,Creator,CreateDate,SupportAplOrderType,SupportAplOrderNo,ItemNo,
                        SupportItemId,SupportItemName,Remark) values (@Company,@UserGroup,@Creator,@CreateDate,@SupportAplOrderType,@SupportAplOrderNo,@ItemNo,
                        @SupportItemId,@SupportItemName,@Remark)";


                        for (int i = 0; i < strsupportitemidArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strsupportitemidArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,supportApl_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportApl_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,supportApl_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
                            DataAccessMySQL.CreateParameter("ItemNo",DbType.String,(i+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("SupportItemId",DbType.String,strsupportitemidArray[i].ToString()),
                            DataAccessMySQL.CreateParameter("SupportItemName",DbType.String,strsupportitemnameArray[i].ToString()),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,strremarkArray[i].ToString())
                        });
                        }
                    }
                    
                    if(strproductno!="")
                    {
                        //string[] stritemidArray = stritemid.Split(',');
                        string[] strproductnoArray = strproductno.Split(',');
                        string[] strproductnameArray = strproductname.Split(',');
                        string[] strqtyArray = strqty.Split(',');
                        string[] strunitArray = strunit.Split(',');
                        string[] strproductdremarkArray = strproductdremark.Split(',');

                        sql = @"insert into SupportApl_ProductD (Company,UserGroup,Creator,CreateDate,SupportAplOrderType,SupportAplOrderNo,ItemId,
                        ProductNo,ProductName,QTY,Unit,Remark) values (@Company,@UserGroup,@Creator,@CreateDate,@SupportAplOrderType,@SupportAplOrderNo,@ItemId,
                        @ProductNo,@ProductName,@QTY,@Unit,@Remark)";


                        for (int i = 0; i < strproductnoArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strproductnoArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,supportApl_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportApl_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,supportApl_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strproductnoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,strproductnameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.Decimal,Decimal.Parse(strqtyArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strunitArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strproductdremarkArray[i].ToString())
                            });
                        }
                    }
                    if(strProcessDate!="")
                    {
                        string[] strProcessDateArray = strProcessDate.Split(',');
                        string[] strProcessExplanationArray = strProcessExplanation.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');

                        sql = @"insert into SupportApl_ProcessD (Company,UserGroup,Creator,CreateDate,SupportAplOrderType,
                        SupportAplOrderNo,ItemId,ProcessDate,ProcessExplanation,ProcessMan,Remark) values (@Company,@UserGroup,
                        @Creator,@CreateDate,@SupportAplOrderType,@SupportAplOrderNo,@ItemId,@ProcessDate,@ProcessExplanation,
                        @ProcessMan,@Remark)";


                        for (int i = 0; i < strProcessDateArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strProcessDateArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,supportApl_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportApl_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,supportApl_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("ProcessDate",DbType.String,strProcessDateArray[i].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("ProcessExplanation",DbType.String,strProcessExplanationArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportApl_H.Creator),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
                            });
                        }
                    }
                    
                }
                tran.Commit();
                msg="";
                return true;
            }
            catch(Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                msg=ex.Message;
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

        public bool DeleteSupportApl_H(string SupportAplOrderType, string SupportAplOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"delete from supportapl_h where SupportAplOrderType=@SupportAplOrderType and 
                    SupportAplOrderNo=@SupportAplOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,SupportAplOrderNo)
                    });

                dbMySQL.ExecuteNonQuery(@"delete from SupportApl_SupportItem where SupportAplOrderType=@SupportAplOrderType and 
                    SupportAplOrderNo=@SupportAplOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,SupportAplOrderNo)
                    });

                dbMySQL.ExecuteNonQuery(@"delete from SupportApl_ProcessD where SupportAplOrderType=@SupportAplOrderType and 
                    SupportAplOrderNo=@SupportAplOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,SupportAplOrderNo)
                    });

                dbMySQL.ExecuteNonQuery(@"delete from SupportApl_ProductD where SupportAplOrderType=@SupportAplOrderType and 
                    SupportAplOrderNo=@SupportAplOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,SupportAplOrderNo)
                    });

                dbMySQL.ExecuteNonQuery(@"delete from Assignment where SourceOrderType=@SourceOrderType and 
                    SourceOrderNo=@SourceOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,SupportAplOrderNo)
                    });

                dbMySQL.ExecuteNonQuery(@"delete from AssignmentChange where SourceOrderType=@SourceOrderType and 
                    SourceOrderNo=@SourceOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,SupportAplOrderNo)
                    });

                tran.Commit();

                return true;
            }
            catch
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

        public SupportApl_H SupportItemInfo(string SupportAplOrderType, string SupportAplOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            SupportApl_H supportApl_H = new SupportApl_H();

            DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable(@"select h.SupportAplOrderType,h.SupportAplOrderNo,h.CustomerId,
                        c.customerName as customerName,h.SupportDept,
                        d.DepartmentName as SupportDeptName,
                        h.RequestDate,h.AsignCheck,h.AsignMan,
                        e.EmployeeName as AsignManName,h.Creator,
                        ee.EmployeeName as CreatorName,
                        h.ProcessMan,eee.EmployeeName as ProcessManName,
                        h.IsPicking,h.SourceOrderNo,h.CustomerType,h.OrderStatus,
                        h.OrderDate,h.ApplyDate,h.SalesId,eeee.EmployeeName  as SalesName,
                        h.AddressSName,h.Address,h.TelNo,h.FaxNo,h.Remark,h.NoOfPrints,
                        h.Picking,h.IsPicking,h.RequestDate,h.RequestTime,h.SupportDept,
                        h.SourceOrderNo,h.AsignDate,h.AsignMan,ea.EmployeeName as AsignManName,
                        h.ProcessMan,ep.EmployeeName as ProcessManName,
                        h.CompletionDate,h.CompletionMan,ec.EmployeeName as CompletionManName,
                        h.Confirmed,h.Contact from supportapl_h as h
                        left join Customer as c on h.CustomerId=c.CustomerId
                        left join Department as d on d.DepartmentId=h.SupportDept
                        left join Employee as e on e.EmployeeId=h.AsignMan
                        left join Employee as ee on ee.EmployeeId=h.Creator
                        left join Employee as eee on eee.EmployeeId=h.ProcessMan
                        left join Employee as eeee on eeee.EmployeeId=h.SalesId
                        left join Employee as ea on ea.EmployeeId=h.AsignMan
                        left join Employee as ep on ep.EmployeeId=h.ProcessMan
                        left join Employee as ec on ec.EmployeeId=h.ProcessMan
                        where h.SupportAplOrderType=@SupportAplOrderType and 
                        h.SupportAplOrderNo=@SupportAplOrderNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,SupportAplOrderType),
                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,SupportAplOrderNo)
                });

            if (dtSupportApl_H != null && dtSupportApl_H.Rows.Count > 0)
            {
                supportApl_H.SupportAplOrderType = dtSupportApl_H.Rows[0]["SupportAplOrderType"].ToString();
                supportApl_H.SupportAplOrderNo = dtSupportApl_H.Rows[0]["SupportAplOrderNo"].ToString();
                supportApl_H.OrderDate = dtSupportApl_H.Rows[0]["OrderDate"].ToString();
                supportApl_H.ApplyDate = dtSupportApl_H.Rows[0]["ApplyDate"].ToString();
                supportApl_H.CustomerId = dtSupportApl_H.Rows[0]["CustomerId"].ToString();
                supportApl_H.CustomerName = dtSupportApl_H.Rows[0]["CustomerName"].ToString();
                supportApl_H.CustomerType = "";
                if (dtSupportApl_H.Rows[0]["CustomerType"].ToString().Trim()!="")
                {
                    supportApl_H.CustomerType = dtSupportApl_H.Rows[0]["CustomerType"].ToString()=="A"? "A:一般客戶" : "B:經銷商";
                }
                supportApl_H.SalesId = dtSupportApl_H.Rows[0]["SalesId"].ToString();
                supportApl_H.SalesName = dtSupportApl_H.Rows[0]["SalesName"].ToString();
                supportApl_H.AddressSName = dtSupportApl_H.Rows[0]["AddressSName"].ToString();
                supportApl_H.Address = dtSupportApl_H.Rows[0]["Address"].ToString();
                supportApl_H.TelNo = dtSupportApl_H.Rows[0]["TelNo"].ToString();
                supportApl_H.FaxNo = dtSupportApl_H.Rows[0]["FaxNo"].ToString();
                supportApl_H.Remark = dtSupportApl_H.Rows[0]["Remark"].ToString();
                supportApl_H.NoOfPrints = int.Parse(dtSupportApl_H.Rows[0]["NoOfPrints"].ToString());
                supportApl_H.Picking = dtSupportApl_H.Rows[0]["Picking"].ToString();
                supportApl_H.IsPicking = dtSupportApl_H.Rows[0]["IsPicking"].ToString();
                supportApl_H.RequestDate = dtSupportApl_H.Rows[0]["RequestDate"].ToString();
                supportApl_H.RequestTime = dtSupportApl_H.Rows[0]["RequestTime"].ToString();
                supportApl_H.SupportDept = dtSupportApl_H.Rows[0]["SupportDept"].ToString();
                supportApl_H.SourceOrderNo = dtSupportApl_H.Rows[0]["SourceOrderNo"].ToString();
                supportApl_H.AsignDate = dtSupportApl_H.Rows[0]["AsignDate"].ToString();
                supportApl_H.AsignMan = dtSupportApl_H.Rows[0]["AsignMan"].ToString();
                supportApl_H.AsignManName = dtSupportApl_H.Rows[0]["AsignManName"].ToString();
                supportApl_H.ProcessMan = dtSupportApl_H.Rows[0]["ProcessMan"].ToString();
                supportApl_H.ProcessManName = dtSupportApl_H.Rows[0]["ProcessManName"].ToString();
                supportApl_H.CompletionDate = dtSupportApl_H.Rows[0]["CompletionDate"].ToString();
                supportApl_H.CompletionMan = dtSupportApl_H.Rows[0]["CompletionMan"].ToString();
                supportApl_H.CompletionManName = dtSupportApl_H.Rows[0]["CompletionManName"].ToString();
                if(dtSupportApl_H.Rows[0]["OrderStatus"].ToString()=="0")
                {
                    supportApl_H.OrderStatus = "0:未處理";
                }
                if (dtSupportApl_H.Rows[0]["OrderStatus"].ToString() == "1")
                {
                    supportApl_H.OrderStatus = "1:已派工";
                }
                if (dtSupportApl_H.Rows[0]["OrderStatus"].ToString() == "2")
                {
                    supportApl_H.OrderStatus = "2:完工";
                }
                if (dtSupportApl_H.Rows[0]["OrderStatus"].ToString() == "3")
                {
                    supportApl_H.OrderStatus = "3.作廢";
                }
                supportApl_H.Confirmed = dtSupportApl_H.Rows[0]["Confirmed"].ToString();
                supportApl_H.Contact = dtSupportApl_H.Rows[0]["Contact"].ToString();
                return supportApl_H;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateAsign(SupportAplAsign supportAplAsign, string SupportId,out string msg)
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

                    orderInfo = GetSupportAplOrderNo(supportAplAsign.AsignOrderType).Split('-');

                    if (orderInfo[0].ToString() == "None")
                    {
                        msg = "單別不存在!";
                        return false;
                    }

                    if (orderInfo[0].ToString() == "NO")
                    {
                        msg = "單別編碼方式屬於手動填寫，請換單別!";
                        return false;
                    }

                    DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select AutoConfirm from OrderCategory where OrderType=@OrderType", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("OrderType",DbType.String,supportAplAsign.AsignOrderType)
                        });
                    string AutoConfirm = "N";
                    if(dtOrderCategory!=null&&dtOrderCategory.Rows.Count>0)
                    {
                        if(dtOrderCategory.Rows[0]["AutoConfirm"].ToString()=="Y")
                        {
                            dbMySQL.ExecuteNonQuery(@"update supportapl_h set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,AsignDate=@AsignDate,AsignMan=@AsignMan,
                            ProcessMan=@ProcessMan,OrderStatus=@OrderStatus,AsignCheck=@AsignCheck where SupportAplOrderType=@SupportAplOrderType 
                            and SupportAplOrderNo=@SupportAplOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,supportAplAsign.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportAplAsign.UserGroup),
                                DataAccessMySQL.CreateParameter("Modifier",DbType.String,supportAplAsign.Modifier),
                                DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,valueInfo[0].ToString()),
                                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,valueInfo[1].ToString()),
                                DataAccessMySQL.CreateParameter("AsignDate",DbType.String, supportAplAsign.AsignDate),
                                DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportAplAsign.AsignMan),
                                DataAccessMySQL.CreateParameter("ProcessMan",DbType.String, supportAplAsign.ProcessMan),
                                DataAccessMySQL.CreateParameter("OrderStatus",DbType.String,"1"),
                                DataAccessMySQL.CreateParameter("AsignCheck",DbType.String,"Y")
                            });
                            AutoConfirm = "Y";
                        }
                    }

                    

                    DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable(@"select * from supportapl_h where SupportAplOrderType=@SupportAplOrderType 
                        and SupportAplOrderNo=@SupportAplOrderNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,valueInfo[1].ToString()),
                        });
                    if(dtSupportApl_H!=null&&dtSupportApl_H.Rows.Count>0)
                    {
                        if(AutoConfirm == "N")
                        {
                            supportAplAsign.AsignMan = "";
                            supportAplAsign.AsignDate = "";
                        }
                        dbMySQL.ExecuteNonQuery(@"insert into Assignment (Company,UserGroup,Creator,CreateDate,AssignOrderType,AssignOrderNo,OrderDate,CustomerId,
                        Version,SourceOrderType,SourceOrderNo,Designee,Assignor,AssignDate,NoOfPrints,Remark,Confirmed) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,@AssignOrderType,@AssignOrderNo,@OrderDate,@CustomerId,@Version,@SourceOrderType,@SourceOrderNo,
                        @Designee,@Assignor,@AssignDate,@NoOfPrints,@Remark,@Confirmed)", tran,
                            new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,supportAplAsign.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportAplAsign.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,supportAplAsign.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,supportAplAsign.AsignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,orderInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,dtSupportApl_H.Rows[0]["OrderDate"].ToString()),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtSupportApl_H.Rows[0]["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,"0000"),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("Designee",DbType.String,supportAplAsign.ProcessMan),
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,supportAplAsign.AsignMan),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,supportAplAsign.AsignDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Decimal,0),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,dtSupportApl_H.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,AutoConfirm)
                            });
                    }

                }
                msg = "";
                tran.Commit();
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

        public bool UpdateAsignAgain(AssignmentChange assignmentChange, string SupportId,out string msg)
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
                    if(dt!=null&&dt.Rows.Count>0)
                    {
                        dbMySQL.ExecuteNonQuery(@"insert into AssignmentChange (Company,UserGroup,Creator,CreateDate,AssignOrderType,AssignOrderNo,Version,OrderDate,ModiReason,
                        CustomerId,SourceOrderType,SourceOrderNo,NoOfPrints,Designee,Assignor,AssignDate,Remark,
                        OldDesignee,OldAssignor,OldAssignDate,OldRemark,Confirmor,ConfirmedDate) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,@AssignOrderType,@AssignOrderNo,@Version,@OrderDate,@ModiReason,
                        @CustomerId,@SourceOrderType,@SourceOrderNo,@NoOfPrints,@Designee,@Assignor,@AssignDate,@Remark,
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
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,""),                            
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,dt.Rows[0]["SourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,dt.Rows[0]["SourceOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Decimal,dt.Rows[0]["NoOfPrints"].ToString()),
                            DataAccessMySQL.CreateParameter("Designee",DbType.String,dt.Rows[0]["Designee"].ToString()),
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,dt.Rows[0]["Assignor"].ToString()),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,dt.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("OldDesignee",DbType.String,dt.Rows[0]["Designee"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAssignor",DbType.String,dt.Rows[0]["Assignor"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAssignDate",DbType.String,dt.Rows[0]["AssignDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dt.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,""),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,"")
                        });

                        //dbMySQL.ExecuteNonQuery(@"update Assignment set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,Version=@Version,
                        //    Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Remark=@Remark,Modifier=@Modifier,ModiDate=@ModiDate where AssignOrderType=@AssignOrderType 
                        //    and AssignOrderNo=@AssignOrderNo", tran,
                        //        new DbParameter[]{
                        //        DataAccessMySQL.CreateParameter("Company",DbType.String,assignmentChange.Company),
                        //        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignmentChange.UserGroup),
                        //        DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignmentChange.Modifier),
                        //        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        //        DataAccessMySQL.CreateParameter("Version",DbType.String,(Int32.Parse(dt.Rows[0]["Version"].ToString())+1).ToString("0000")),
                        //        DataAccessMySQL.CreateParameter("Designee",DbType.String,assignmentChange.Designee),
                        //        DataAccessMySQL.CreateParameter("Assignor",DbType.String,assignmentChange.Assignor),
                        //        DataAccessMySQL.CreateParameter("AsignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        //        DataAccessMySQL.CreateParameter("Remark",DbType.String,dt.Rows[0]["Remark"].ToString()),
                        //        DataAccessMySQL.CreateParameter("Modifier",DbType.String,dt.Rows[0]["Modifier"].ToString()),
                        //        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        //        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,dt.Rows[0]["AssignOrderType"].ToString()),
                        //        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,dt.Rows[0]["AssignOrderNo"].ToString())
                        //    });

                        //dbMySQL.ExecuteNonQuery(@"update SupportApl_H set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,AsignCheck=@AsignCheck,OrderStatus=@OrderStatus where SupportAplOrderType=@SupportAplOrderType 
                        //    and SupportAplOrderNo=@SupportAplOrderNo", tran,
                        //        new DbParameter[]{
                        //        DataAccessMySQL.CreateParameter("Company",DbType.String,assignmentChange.Company),
                        //        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignmentChange.UserGroup),
                        //        DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignmentChange.Modifier),
                        //        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        //        DataAccessMySQL.CreateParameter("AsignCheck",DbType.String,"Y"),
                        //        DataAccessMySQL.CreateParameter("OrderStatus",DbType.String,"1"),
                        //        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,valueInfo[0].ToString()),
                        //        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,valueInfo[1].ToString())
                        //    });
                    }
                }
                msg = "";
                tran.Commit();
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

        public List<SupportApl_H> GetSupportInfo(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<SupportApl_H> objSupportApl_H = new List<SupportApl_H>();

            string sql = @"select h.SupportAplOrderType,h.SupportAplOrderNo,
                    h.CustomerId,c.CustomerName as CustomerName,
                    h.SupportDept,d.DepartmentName as SupportDeptName,h.AddressSName,h.ApplyDate,
                    h.RequestDate,h.SalesId,e.EmployeeName SalesName,h.CustomerType,
                    h.creator,ee.EmployeeName as creatorName,h.RequestTime,h.NoOfPrints
                     from supportapl_h as h 
                    left join Customer as c on c.CustomerId=h.CustomerId
                    left join Department as d on d.DepartmentId=h.SupportDept
                    left join Employee as e on e.EmployeeId=h.SalesId
                    left join Employee as ee on ee.EmployeeId=h.creator where 1=1";

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
            

            DataTable dtSupportApl_H = null;
            if (strCondition != "")
            {

                dtSupportApl_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtSupportApl_H = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }


            if (dtSupportApl_H.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSupportApl_H.Rows)
                {
                    SupportApl_H obj = new SupportApl_H();
                    obj.SupportAplOrderType = dr["SupportAplOrderType"].ToString();
                    obj.SupportAplOrderNo = dr["SupportAplOrderNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.SupportDept = dr["SupportDept"].ToString();
                    obj.SupportDeptName = dr["SupportDeptName"].ToString();
                    obj.AddressSName = dr["AddressSName"].ToString();
                    obj.ApplyDate = dr["ApplyDate"].ToString();
                    obj.RequestDate = dr["RequestDate"].ToString();
                    obj.SalesId = dr["SalesId"].ToString();
                    obj.SalesName = dr["SalesName"].ToString();
                    obj.CustomerType = "";
                    if(dr["CustomerType"].ToString().Trim()!="")
                    {
                        obj.CustomerType = dr["CustomerType"].ToString()=="A"? "A:一般客戶" : "B:經銷商";
                    }
                    obj.Creator = dr["creator"].ToString();
                    obj.CreatorName = dr["CreatorName"].ToString();
                    obj.RequestTime = dr["RequestTime"].ToString();
                    obj.NoOfPrints = int.Parse(dr["NoOfPrints"].ToString());
                    objSupportApl_H.Add(obj);
                }
            }

            return objSupportApl_H;
        }

        //public bool UpdateSupportApl_H(SupportApl_H supportApl_H, string strsupportitemid, string strsupportitemname,
        //    string strremark, string strproductno, string strproductname, string strqty,
        //    string strunit, string strproductdremark, string strProcessDate, string strProcessExplanation, string strRemark, out string msg)
        //{
        //    DataAccessMySQL dbMySQL = new DataAccessMySQL();
        //    dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
        //    DbTransaction tran = null;
        //    try
        //    {
        //        tran = dbMySQL.CreateDbTransaction();
        //        string count = dbMySQL.ExecuteScalar(@"select count(*) from SupportApl_H where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
        //            new DbParameter[]{
        //                DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
        //                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo)
        //            }).ToString();
        //        if (int.Parse(count) == 0)
        //        {
        //            dbMySQL.ExecuteNonQuery(@"update SupportApl_H set OrderDate=@OrderDate,ApplyDate=@ApplyDate,CustomerId=@CustomerId,
        //                CustomerType=@CustomerType,SalesId=@SalesId,AddressSName=@AddressSName,Address=@Address,TelNo=@TelNo,FaxNo=@FaxNo,
        //                Remark=@Remark,NoOfPrints=@NoOfPrints,Picking=@Picking,IsPicking=@IsPicking,RequestDate=@RequestDate,
        //                RequestTime=@RequestTime,SupportDept=@SupportDept,SourceOrderNo=@SourceOrderNo,AsignDate=@AsignDate,
        //                AsignMan=@AsignMan,ProcessMan=@ProcessMan,CompletionDate=@CompletionDate,CompletionMan=@CompletionMan,
        //                OrderStatus=@OrderStatus,Confirmed=@Confirmed,Contact=@Contact where SupportAplOrderType=@SupportAplOrderType 
        //                and @SupportAplOrderNo=@SupportAplOrderNo", tran,
        //                new DbParameter[]{
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
        //                    DataAccessMySQL.CreateParameter("OrderDate",DbType.String,supportApl_H.OrderDate),
        //                    DataAccessMySQL.CreateParameter("ApplyDate",DbType.String,supportApl_H.ApplyDate),
        //                    DataAccessMySQL.CreateParameter("CustomerId",DbType.String,supportApl_H.CustomerId),
        //                    DataAccessMySQL.CreateParameter("CustomerType",DbType.String,supportApl_H.CustomerType),
        //                    DataAccessMySQL.CreateParameter("SalesId",DbType.String,supportApl_H.SalesId),
        //                    DataAccessMySQL.CreateParameter("AddressSName",DbType.String,supportApl_H.AddressSName),
        //                    DataAccessMySQL.CreateParameter("Address",DbType.String,supportApl_H.Address),
        //                    DataAccessMySQL.CreateParameter("TelNo",DbType.String,supportApl_H.TelNo),
        //                    DataAccessMySQL.CreateParameter("FaxNo",DbType.String,supportApl_H.FaxNo),
        //                    DataAccessMySQL.CreateParameter("Remark",DbType.String,supportApl_H.Remark),
        //                    DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,supportApl_H.NoOfPrints),
        //                    DataAccessMySQL.CreateParameter("Picking",DbType.String,supportApl_H.Picking),
        //                    DataAccessMySQL.CreateParameter("IsPicking",DbType.String,supportApl_H.IsPicking),
        //                    DataAccessMySQL.CreateParameter("RequestDate",DbType.String,supportApl_H.RequestDate),
        //                    DataAccessMySQL.CreateParameter("RequestTime",DbType.String,supportApl_H.RequestTime),
        //                    DataAccessMySQL.CreateParameter("SupportDept",DbType.String,supportApl_H.SupportDept),
        //                    DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,supportApl_H.SourceOrderNo),
        //                    DataAccessMySQL.CreateParameter("AsignDate",DbType.String,supportApl_H.AsignDate),
        //                    DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportApl_H.AsignMan),
        //                    DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportApl_H.ProcessMan),
        //                    DataAccessMySQL.CreateParameter("CompletionDate",DbType.String,supportApl_H.CompletionDate),
        //                    DataAccessMySQL.CreateParameter("CompletionMan",DbType.String,supportApl_H.CompletionMan),
        //                    DataAccessMySQL.CreateParameter("OrderStatus",DbType.String,supportApl_H.OrderStatus),
        //                    DataAccessMySQL.CreateParameter("Confirmed",DbType.String,supportApl_H.Confirmed),
        //                    DataAccessMySQL.CreateParameter("Contact",DbType.String,supportApl_H.Contact)
        //                });

        //            dbMySQL.ExecuteNonQuery(@"delete from SupportApl_SupportItem where SupportAplOrderType=@SupportAplOrderType 
        //                and @SupportAplOrderNo=@SupportAplOrderNo", tran,
        //                new DbParameter[]{
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo)
        //                });

        //            dbMySQL.ExecuteNonQuery(@"delete from SupportApl_ProductD where SupportAplOrderType=@SupportAplOrderType 
        //                and @SupportAplOrderNo=@SupportAplOrderNo", tran,
        //                new DbParameter[]{
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo)
        //                });

        //            dbMySQL.ExecuteNonQuery(@"delete from SupportApl_ProcessD where SupportAplOrderType=@SupportAplOrderType 
        //                and @SupportAplOrderNo=@SupportAplOrderNo", tran,
        //                new DbParameter[]{
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo)
        //                });

        //            string sql = "";

        //            if (strsupportitemid != "")
        //            {
        //                string[] strsupportitemidArray = strsupportitemid.Split(',');
        //                string[] strsupportitemnameArray = strsupportitemname.Split(',');
        //                string[] strremarkArray = strremark.Split(',');

        //                sql = @"insert into SupportApl_SupportItem (SupportAplOrderType,SupportAplOrderNo,ItemNo,
        //                SupportItemId,SupportItemName,Remark) values (@SupportAplOrderType,@SupportAplOrderNo,@ItemNo,
        //                @SupportItemId,@SupportItemName,@Remark)";


        //                for (int i = 0; i < strsupportitemidArray.Length; i++)
        //                {
        //                    dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
        //                    DataAccessMySQL.CreateParameter("ItemNo",DbType.String,(i+1).ToString("0000")),
        //                    DataAccessMySQL.CreateParameter("SupportItemId",DbType.String,strsupportitemidArray[i].ToString()),
        //                    DataAccessMySQL.CreateParameter("SupportItemName",DbType.String,strsupportitemnameArray[i].ToString()),
        //                    DataAccessMySQL.CreateParameter("Remark",DbType.String,strremarkArray[i].ToString())
        //                });
        //                }
        //            }

        //            if (strproductno != "")
        //            {
        //                //string[] stritemidArray = stritemid.Split(',');
        //                string[] strproductnoArray = strproductno.Split(',');
        //                string[] strproductnameArray = strproductname.Split(',');
        //                string[] strqtyArray = strqty.Split(',');
        //                string[] strunitArray = strunit.Split(',');
        //                string[] strproductdremarkArray = strproductdremark.Split(',');

        //                dbMySQL.ExecuteNonQuery(@"delete from SupportApl_ProductD where SupportAplOrderType=@SupportAplOrderType 
        //                and @SupportAplOrderNo=@SupportAplOrderNo", tran,
        //                new DbParameter[]{
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo)
        //                });

        //                sql = @"insert into SupportApl_ProductD (SupportAplOrderType,SupportAplOrderNo,ItemId,
        //                ProductNo,ProductName,QTY,Unit,Remark) values (@SupportAplOrderType,@SupportAplOrderNo,@ItemId,
        //                @ProductNo,@ProductName,@QTY,@Unit,@Remark)";


        //                for (int i = 0; i < strproductnoArray.Length; i++)
        //                {
        //                    dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
        //                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
        //                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
        //                        DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
        //                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strproductnoArray[i].ToString()),
        //                        DataAccessMySQL.CreateParameter("ProductName",DbType.String,strproductnameArray[i].ToString()),
        //                        DataAccessMySQL.CreateParameter("QTY",DbType.Decimal,Decimal.Parse(strqtyArray[i].ToString())),
        //                        DataAccessMySQL.CreateParameter("Unit",DbType.String,strunitArray[i].ToString()),
        //                        DataAccessMySQL.CreateParameter("Remark",DbType.String,strproductdremarkArray[i].ToString())
        //                    });
        //                }
        //            }

        //            if (strProcessDate != "")
        //            {
        //                string[] strProcessDateArray = strProcessDate.Split(',');
        //                string[] strProcessExplanationArray = strProcessExplanation.Split(',');
        //                string[] strRemarkArray = strRemark.Split(',');

        //                sql = @"insert into SupportApl_ProcessD (Company,UserGroup,Creator,CreateDate,SupportAplOrderType,
        //                SupportAplOrderNo,ItemId,ProcessDate,ProcessExplanation,ProcessMan,Remark) values (@Company,@UserGroup,
        //                @Creator,@CreateDate,@SupportAplOrderType,@SupportAplOrderNo,@ItemId,@ProcessDate,@ProcessExplanation,
        //                @ProcessMan,@Remark)";


        //                for (int i = 0; i < strProcessDateArray.Length; i++)
        //                {
        //                    if (string.IsNullOrEmpty(strProcessDateArray[i]))
        //                    {
        //                        continue;
        //                    }
        //                    dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
        //                        DataAccessMySQL.CreateParameter("Company",DbType.String,supportApl_H.Company),
        //                        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportApl_H.UserGroup),
        //                        DataAccessMySQL.CreateParameter("Creator",DbType.String,supportApl_H.Creator),
        //                        DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
        //                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
        //                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
        //                        DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
        //                        DataAccessMySQL.CreateParameter("ProcessDate",DbType.String,strProcessDateArray[i].ToString().Replace("/","")),
        //                        DataAccessMySQL.CreateParameter("ProcessExplanation",DbType.String,strProcessExplanationArray[i].ToString()),
        //                        DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportApl_H.Creator),
        //                        DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
        //                    });
        //                }
        //            }

        //        }
        //        tran.Commit();
        //        msg = "";
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (tran != null)
        //        {
        //            tran.Rollback();
        //        }
        //        msg = ex.Message;
        //        return false;
        //    }
        //    finally
        //    {
        //        if (tran != null)
        //        {
        //            tran.Dispose();
        //        }
        //    }

        //}


        //支援申請發料列表顯示
        public List<SupportApl_H> GetSupportItemPick(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<SupportApl_H> objSupportApl_H = new List<SupportApl_H>();

            string sql = @"select h.SupportAplOrderType,h.SupportAplOrderNo,h.CustomerId,h.OrderDate,
                        c.CustomerName as CustomerName,h.CustomerType,
                        h.SupportDept,d.DepartmentName as SupportDeptName,
                        c.AddressSName,h.ApplyDate,
                        h.AsignDate,h.RequestDate,h.OrderStatus,h.ProcessMan,
                        e.EmployeeName as ProcessManName,h.SalesId,
                        ee.EmployeeName as SalesName,h.Creator,
                        eee.EmployeeName as CreatorName from supportapl_h as h
                        left join Customer as c on c.CustomerId=h.CustomerId
                        left join Department as d on d.DepartmentId=h.SupportDept
                        left join Employee as e on e.EmployeeId=h.ProcessMan
                        left join Employee as ee on ee.EmployeeId=h.SalesId
                        left join Employee as eee on eee.EmployeeId=h.Creator 
                        where h.Picking='Y'";

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            string strCondition = "";
            int len = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]))
                {
                    len++;
                }
            }

            DbParameter[] dbParameters = new DbParameter[len - 1];
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

            DataTable dtSupportApl_H = null;
            if (strCondition != "")
            {
                dtSupportApl_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtSupportApl_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtSupportApl_H.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSupportApl_H.Rows)
                {
                    SupportApl_H obj = new SupportApl_H();
                    obj.SupportAplOrderType = dr["SupportAplOrderType"].ToString();
                    obj.SupportAplOrderNo = dr["SupportAplOrderNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    if(dr["CustomerType"].ToString()!="")
                    {
                        obj.CustomerType = dr["CustomerType"].ToString()=="A"? "A.一般客戶" : "B.經銷商";
                    }
                    else
                    {
                        obj.CustomerType = "";
                    }
                    obj.SupportDept = dr["SupportDept"].ToString();
                    obj.SupportDeptName = dr["SupportDeptName"].ToString();
                    obj.AddressSName = dr["AddressSName"].ToString();
                    obj.ApplyDate = dr["ApplyDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ApplyDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.OrderDate = dr["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.AsignDate = dr["AsignDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["AsignDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.RequestDate = dr["RequestDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["RequestDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.OrderStatus = dr["OrderStatus"].ToString();
                    obj.ProcessMan = dr["ProcessMan"].ToString();
                    obj.ProcessManName = dr["ProcessManName"].ToString();
                    obj.SalesId = dr["SalesId"].ToString();
                    obj.SalesName = dr["SalesName"].ToString();
                    obj.Creator = dr["Creator"].ToString();
                    obj.CreatorName = dr["CreatorName"].ToString();
                    objSupportApl_H.Add(obj);
                }
            }

            return objSupportApl_H;
        
        }

        //完工
        public void UpdateConfirmedY(SupportApl_H supportApl_H,string type)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if(type=="Y")
            {
                dbMySQL.ExecuteNonQuery(@"update SupportApl_H set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,ModiDate=@ModiDate,AsignCheck=@AsignCheck,
                    OrderStatus=@OrderStatus,CompletionDate=@CompletionDate,CompletionMan=@CompletionMan where SupportAplOrderType=@SupportAplOrderType 
                    and SupportAplOrderNo=@SupportAplOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("Company",DbType.String,supportApl_H.Company),
                        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportApl_H.UserGroup),
                        DataAccessMySQL.CreateParameter("Modifier",DbType.String,supportApl_H.Modifier),
                        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
                        DataAccessMySQL.CreateParameter("CompletionDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("CompletionMan",DbType.String,supportApl_H.Modifier),
                        DataAccessMySQL.CreateParameter("AsignCheck",DbType.String, "Y"),
                        DataAccessMySQL.CreateParameter("OrderStatus",DbType.String, "2")
                    });
            }else if(type=="V")
            {
                dbMySQL.ExecuteNonQuery(@"update SupportApl_H set Company=@Company,UserGroup=@UserGroup,AsignCheck=@AsignCheck,Modifier=@Modifier,
                    ModiDate=@ModiDate,OrderStatus=@OrderStatus,CompletionDate='',CompletionMan='' where SupportAplOrderType=@SupportAplOrderType 
                    and SupportAplOrderNo=@SupportAplOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("Company",DbType.String,supportApl_H.Company),
                        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportApl_H.UserGroup),
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
                        DataAccessMySQL.CreateParameter("AsignCheck",DbType.String, "V"),
                        DataAccessMySQL.CreateParameter("Modifier",DbType.String,supportApl_H.Modifier),
                        DataAccessMySQL.CreateParameter("ModiDate",DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("OrderStatus",DbType.String, "3")
                    });
            }else
            {
                dbMySQL.ExecuteNonQuery(@"update SupportApl_H set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,ModiDate=@ModiDate,AsignCheck=@AsignCheck,
                    OrderStatus=@OrderStatus,CompletionDate='',CompletionMan='' where SupportAplOrderType=@SupportAplOrderType 
                    and SupportAplOrderNo=@SupportAplOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("Company",DbType.String,supportApl_H.Company),
                        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportApl_H.UserGroup),
                        DataAccessMySQL.CreateParameter("Modifier",DbType.String,supportApl_H.Modifier),
                        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
                        DataAccessMySQL.CreateParameter("AsignCheck",DbType.String, "N"),
                        DataAccessMySQL.CreateParameter("OrderStatus",DbType.String, "1")
                    });
            }
            

        }

        ////作廢
        //public void UpdateConfirmedV(SupportApl_H supportApl_H)
        //{
        //    DataAccessMySQL dbMySQL = new DataAccessMySQL();
        //    dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            

        //}

        public string countOrderStatus(SupportApl_H supportApl_H, string col, string condition, string value,string OrderStatus)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<SupportApl_H> objSupportApl_H = new List<SupportApl_H>();

            string sql = @"select count(*)  from SupportApl_H as h
                        left join  Customer as c on h.CustomerId=c.CustomerId 
                        left join Department as d on d.DepartmentId=h.SupportDept
                        left join Employee as e on e.EmployeeId=h.AsignMan 
                        left join Employee as ee on ee.EmployeeId=h.Creator
                        left join Employee as eee on eee.EmployeeId=h.ProcessMan where 1=1";

            col += ",OrderStatus";
            condition += ",=";
            value += "," + OrderStatus;

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            string strCondition = "";
            int len = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]))
                {
                    len++;
                }
            }

            DbParameter[] dbParameters = new DbParameter[len - 1];
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

            if (strCondition != "")
            {
                return dbMySQL.ExecuteScalar(sql + strCondition, dbParameters).ToString();
            }
            else
            {
                return dbMySQL.ExecuteScalar(sql).ToString();
            }
           
        }

        public List<SupportApl_H> GetSupportItemProcessD(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<SupportApl_H> objSupportApl_H = new List<SupportApl_H>();

            string sql = @"select h.SupportAplOrderType,h.SupportAplOrderNo,h.CustomerId,
                        c.customerName,h.SupportDept,d.DepartmentName as SupportDeptName,
                        h.AddressSName,h.RequestDate,h.AsignMan,
                        e.EmployeeName as AsignManName,h.Creator,ee.EmployeeName as CreatorName,
                        h.ProcessMan,eee.EmployeeName as ProcessManName,
						h.CustomerType,h.OrderStatus  from supportapl_h as h
                        left join  Customer as c on h.CustomerId=c.CustomerId 
                        left join Department as d on d.DepartmentId=h.SupportDept
                        left join Employee as e on e.EmployeeId=h.AsignMan 
                        left join Employee as ee on ee.EmployeeId=h.Creator
                        left join Employee as eee on eee.EmployeeId=h.ProcessMan where OrderStatus='1'";

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            string strCondition = "";
            int len = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]))
                {
                    len++;
                }
            }

            DbParameter[] dbParameters = new DbParameter[len - 1];
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

            DataTable dtSupportApl_H = null;
            if (strCondition != "")
            {
                dtSupportApl_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtSupportApl_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtSupportApl_H.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSupportApl_H.Rows)
                {
                    SupportApl_H obj = new SupportApl_H();
                    obj.SupportAplOrderType = dr["SupportAplOrderType"].ToString();     //單別
                    obj.SupportAplOrderNo = dr["SupportAplOrderNo"].ToString();         //單號
                    obj.CustomerId = dr["CustomerId"].ToString();                       //客戶ID
                    obj.CustomerName = dr["CustomerName"].ToString();                   //客戶名稱
                    obj.CustomerType = dr["CustomerType"].ToString();                   //客戶型態
                    obj.RequestDate = dr["RequestDate"].ToString();                     //需求日期
                    obj.SupportDept = dr["SupportDept"].ToString();                     //支援單位ID
                    obj.SupportDeptName = dr["SupportDeptName"].ToString();             //支援單位名稱
                    obj.AsignMan = dr["AsignMan"].ToString();                           //派工人員ID
                    obj.AsignManName = dr["AsignManName"].ToString();                   //派工人員名稱
                    obj.Creator = dr["Creator"].ToString();                             //建立人員ID
                    obj.CreatorName = dr["CreatorName"].ToString();                     //建立人員名稱
                    obj.ProcessMan = dr["ProcessMan"].ToString();                       //處理人員ID
                    obj.ProcessManName = dr["ProcessManName"].ToString();               //處理人員名稱
                    obj.OrderStatus = dr["OrderStatus"].ToString();                     //單據狀態
                    obj.AddressSName = dr["AddressSName"].ToString();                           //地址
                    objSupportApl_H.Add(obj);
                }
            }

            return objSupportApl_H;
        }

        //列印
        public void PrintSupprotApl_H(string SupportAplOrderType, string SupportAplOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            dbMySQL.ExecuteNonQuery(@"update SupportApl_H set NoOfPrints=NoOfPrints+1 where SupportAplOrderType=@SupportAplOrderType 
                    and SupportAplOrderNo=@SupportAplOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,SupportAplOrderNo)
                    });
        }

        public bool UpdateSupportApl_H(SupportApl_H supportApl_H, string strsupportitemid, string strsupportitemname,
            string strremark, string strproductno, string strproductname, string strqty,
            string strunit, string strproductdremark, string strProcessDate, string strProcessExplanation, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update SupportApl_H  set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,ModiDate=@ModiDate,OrderDate=@OrderDate,ApplyDate=@ApplyDate,CustomerId=@CustomerId,
                        CustomerType=@CustomerType,SalesId=@SalesId,AddressSName=@AddressSName,Address=@Address,TelNo=@TelNo,FaxNo=@FaxNo,
                        Remark=@Remark,RequestDate=@RequestDate,
                        RequestTime=@RequestTime,SupportDept=@SupportDept,SourceOrderNo=@SourceOrderNo
                        where
                        SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,supportApl_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportApl_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,supportApl_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,supportApl_H.OrderDate),
                            DataAccessMySQL.CreateParameter("ApplyDate",DbType.String,supportApl_H.ApplyDate),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,supportApl_H.CustomerId),
                            DataAccessMySQL.CreateParameter("CustomerType",DbType.String,supportApl_H.CustomerType),
                            DataAccessMySQL.CreateParameter("SalesId",DbType.String,supportApl_H.SalesId),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,supportApl_H.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,supportApl_H.Address),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,supportApl_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,supportApl_H.FaxNo),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,supportApl_H.Remark),
                            //DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,supportApl_H.NoOfPrints),
                            //DataAccessMySQL.CreateParameter("Picking",DbType.String,supportApl_H.Picking),
                            //DataAccessMySQL.CreateParameter("IsPicking",DbType.String,supportApl_H.IsPicking),
                            DataAccessMySQL.CreateParameter("RequestDate",DbType.String,supportApl_H.RequestDate),
                            DataAccessMySQL.CreateParameter("RequestTime",DbType.String,supportApl_H.RequestTime),
                            DataAccessMySQL.CreateParameter("SupportDept",DbType.String,supportApl_H.SupportDept),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,supportApl_H.SourceOrderNo),
                            //DataAccessMySQL.CreateParameter("AsignDate",DbType.String,supportApl_H.AsignDate),
                            //DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportApl_H.AsignMan),
                            //DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportApl_H.ProcessMan),
                            //DataAccessMySQL.CreateParameter("CompletionDate",DbType.String,supportApl_H.CompletionDate),
                            //DataAccessMySQL.CreateParameter("CompletionMan",DbType.String,supportApl_H.CompletionMan),
                            //DataAccessMySQL.CreateParameter("OrderStatus",DbType.String,supportApl_H.OrderStatus),
                            //DataAccessMySQL.CreateParameter("Confirmed",DbType.String,supportApl_H.Confirmed),
                            //DataAccessMySQL.CreateParameter("Contact",DbType.String,supportApl_H.Contact),
                            //DataAccessMySQL.CreateParameter("AsignCheck",DbType.String,"N")
                        });

                dbMySQL.ExecuteScalar(@"delete from SupportApl_SupportItem where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo)
                    });

                string sql = "";

                if (strsupportitemid != "")
                {
                    string[] strsupportitemidArray = strsupportitemid.Split(',');
                    string[] strsupportitemnameArray = strsupportitemname.Split(',');
                    string[] strremarkArray = strremark.Split(',');

                    sql = @"insert into SupportApl_SupportItem (Company,UserGroup,Creator,CreateDate,SupportAplOrderType,SupportAplOrderNo,ItemNo,
                        SupportItemId,SupportItemName,Remark) values (@Company,@UserGroup,@Creator,@CreateDate,@SupportAplOrderType,@SupportAplOrderNo,@ItemNo,
                        @SupportItemId,@SupportItemName,@Remark)";


                    for (int i = 0; i < strsupportitemidArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strsupportitemidArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,supportApl_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportApl_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,supportApl_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
                            DataAccessMySQL.CreateParameter("ItemNo",DbType.String,(i+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("SupportItemId",DbType.String,strsupportitemidArray[i].ToString()),
                            DataAccessMySQL.CreateParameter("SupportItemName",DbType.String,strsupportitemnameArray[i].ToString()),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,strremarkArray[i].ToString())
                        });
                    }
                }

                dbMySQL.ExecuteScalar(@"delete from SupportApl_ProductD where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo)
                    });

                if (strproductno != "")
                {
                    //string[] stritemidArray = stritemid.Split(',');
                    string[] strproductnoArray = strproductno.Split(',');
                    string[] strproductnameArray = strproductname.Split(',');
                    string[] strqtyArray = strqty.Split(',');
                    string[] strunitArray = strunit.Split(',');
                    string[] strproductdremarkArray = strproductdremark.Split(',');

                    sql = @"insert into SupportApl_ProductD (Company,UserGroup,Creator,CreateDate,SupportAplOrderType,SupportAplOrderNo,ItemId,
                        ProductNo,ProductName,QTY,Unit,Remark) values (@Company,@UserGroup,@Creator,@CreateDate,@SupportAplOrderType,@SupportAplOrderNo,@ItemId,
                        @ProductNo,@ProductName,@QTY,@Unit,@Remark)";


                    for (int i = 0; i < strproductnoArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strproductnoArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,supportApl_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportApl_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,supportApl_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strproductnoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,strproductnameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.Decimal,Decimal.Parse(strqtyArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strunitArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strproductdremarkArray[i].ToString())
                            });
                    }
                }

                dbMySQL.ExecuteScalar(@"delete from SupportApl_ProcessD where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo)
                    });

                if (strProcessDate != "")
                {
                    string[] strProcessDateArray = strProcessDate.Split(',');
                    string[] strProcessExplanationArray = strProcessExplanation.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');

                    sql = @"insert into SupportApl_ProcessD (Company,UserGroup,Creator,CreateDate,SupportAplOrderType,
                        SupportAplOrderNo,ItemId,ProcessDate,ProcessExplanation,ProcessMan,Remark) values (@Company,@UserGroup,
                        @Creator,@CreateDate,@SupportAplOrderType,@SupportAplOrderNo,@ItemId,@ProcessDate,@ProcessExplanation,
                        @ProcessMan,@Remark)";


                    for (int i = 0; i < strProcessDateArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strProcessDateArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,supportApl_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportApl_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,supportApl_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportApl_H.SupportAplOrderType),
                                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportApl_H.SupportAplOrderNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("ProcessDate",DbType.String,strProcessDateArray[i].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("ProcessExplanation",DbType.String,strProcessExplanationArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportApl_H.Creator),
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

    }
}