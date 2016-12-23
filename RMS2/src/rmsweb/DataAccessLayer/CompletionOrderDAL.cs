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
    public class CompletionOrderDAL : ICompletionOrderDAL
    {
        public bool AddCompletionOrder(CompletionOrder completionOrder, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                string count = dbMySQL.ExecuteScalar(@"select count(*) from CompletionOrder where 
                    CompletionType=@CompletionType and CompletionNo=@CompletionNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("CompletionType",DbType.String,completionOrder.CompletionType),
                        DataAccessMySQL.CreateParameter("CompletionNo",DbType.String,completionOrder.CompletionNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into CompletionOrder (Company,UserGroup,Creator,CreateDate,
                        CompletionType,CompletionNo,OrderDate,NoOfPrints,SourceOrderType,SourceOrderNo,Remark,
                        CustomerId,AddressSName,Address,Contact,TelNo,FaxNo,ProductNo,ProductName,SerialNo,
                        TestResult,InternalRemark,Confirmed,Closed) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @CompletionType,@CompletionNo,@OrderDate,@NoOfPrints,@SourceOrderType,@SourceOrderNo,@Remark,
                        @CustomerId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo,@ProductNo,@ProductName,@SerialNo,
                        @TestResult,@InternalRemark,@Confirmed,@Closed)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,completionOrder.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,completionOrder.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,completionOrder.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("CompletionType",DbType.String,completionOrder.CompletionType),
                            DataAccessMySQL.CreateParameter("CompletionNo",DbType.String,completionOrder.CompletionNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,completionOrder.OrderDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.String,completionOrder.NoOfPrints),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,completionOrder.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,completionOrder.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,completionOrder.Remark),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,completionOrder.CustomerId),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,completionOrder.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,completionOrder.Address),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,completionOrder.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,completionOrder.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,completionOrder.FaxNo),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,completionOrder.CustomerId),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,completionOrder.CustomerId),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,completionOrder.SerialNo),
                            DataAccessMySQL.CreateParameter("TestResult",DbType.String,completionOrder.TestResult),
                            DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,completionOrder.InternalRemark),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,completionOrder.Confirmed),
                            DataAccessMySQL.CreateParameter("Closed",DbType.String,"N")
                        });
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
                msg = ex.Message;
                return false;
            }
        }

        public bool UpdateCompletionOrder(CompletionOrder completionOrder, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"update completionOrder set Modifier=@Modifier,ModiDate=@ModiDate,
                        Remark=@Remark,CustomerId=@CustomerId,AddressSName=@AddressSName,
                        Address=@Address,Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,
                        InternalRemark=@InternalRemark where 
                        CompletionType=@CompletionType and CompletionNo=@CompletionNo",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,completionOrder.Creator),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("CompletionType",DbType.String,completionOrder.CompletionType),
                            DataAccessMySQL.CreateParameter("CompletionNo",DbType.String,completionOrder.CompletionNo),
                            //DataAccessMySQL.CreateParameter("OrderDate",DbType.String,completionOrder.OrderDate),
                            //DataAccessMySQL.CreateParameter("NoOfPrints",DbType.String,completionOrder.NoOfPrints),
                            //DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,completionOrder.SourceOrderType),
                            //DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,completionOrder.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,completionOrder.Remark),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,completionOrder.CustomerId),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,completionOrder.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,completionOrder.Address),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,completionOrder.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,completionOrder.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,completionOrder.FaxNo),
                            //DataAccessMySQL.CreateParameter("ProductNo",DbType.String,completionOrder.CustomerId),
                            //DataAccessMySQL.CreateParameter("ProductName",DbType.String,completionOrder.CustomerId),
                            //DataAccessMySQL.CreateParameter("SerialNo",DbType.String,completionOrder.SerialNo),
                            //DataAccessMySQL.CreateParameter("TestResult",DbType.String,completionOrder.TestResult),
                            DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,completionOrder.InternalRemark),
                            //DataAccessMySQL.CreateParameter("Confirmed",DbType.String,completionOrder.Confirmed)
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

        public bool DelCompletionOrder(CompletionOrder completionOrder, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"delete from CompletionOrder  where 
                        CompletionType=@CompletionType and CompletionNo=@CompletionNo",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("CompletionType",DbType.String,completionOrder.CompletionType),
                            DataAccessMySQL.CreateParameter("CompletionNo",DbType.String,completionOrder.CompletionNo)
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

        public List<CompletionOrder> SearchCompletionOrder(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<CompletionOrder> objCompletionOrder = new List<CompletionOrder>();

            string sql = @"select c.*,o.OrderSName,cu.CustomerName from CompletionOrder as c 
                        left join OrderCategory as o on o.OrderType=c.CompletionType
                        left join Customer as cu on cu.CustomerId=c.CustomerId where 1=1";

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


            DataTable dtCompletionOrder = null;
            if (strCondition != "")
            {
                dtCompletionOrder = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtCompletionOrder = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtCompletionOrder.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCompletionOrder.Rows)
                {
                    CompletionOrder obj = new CompletionOrder();
                    obj.CompletionType = dr["CompletionType"].ToString();
                    obj.OrderSName = dr["OrderSName"].ToString();
                    obj.CompletionNo = dr["CompletionNo"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.NoOfPrints = int.Parse(dr["NoOfPrints"].ToString());
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.AddressSName = dr["AddressSName"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.Contact = dr["Contact"].ToString();
                    obj.TelNo = dr["TelNo"].ToString();
                    obj.FaxNo = dr["FaxNo"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.TestResult = dr["TestResult"].ToString();
                    obj.InternalRemark = dr["InternalRemark"].ToString();
                    if(dr["Confirmed"].ToString()=="Y")
                    {
                        obj.Confirmed = "Y.已完工";
                    }
                    if (dr["Confirmed"].ToString() == "N")
                    {
                        obj.Confirmed = "N.未完工";
                    }
                    if (dr["Confirmed"].ToString() == "V")
                    {
                        obj.Confirmed = "V.作廢";
                    }
                    obj.Closed = dr["Closed"].ToString();
                    objCompletionOrder.Add(obj);
                }
            }

            return objCompletionOrder;
        }

        public CompletionOrder GetCompletionOrderInfo(CompletionOrder completionOrder)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            CompletionOrder obj = null;

            DataTable dtCompletionOrder = dbMySQL.ExecuteDataTable(@"select h.AssetNo,h.ContractType,h.ContractNo,h.UnderWarranty,c.*,cu.CustomerType,cu.CustomerName,o.OrderSName from CompletionOrder as c 
                left join RGA_H as h on h.RGAType=c.SourceOrderType and h.RGANo=c.SourceOrderNo
                left join Customer as cu on cu.CustomerId=c.CustomerId
                left join OrderCategory as o on o.OrderType=c.CompletionType where 
                CompletionType=@CompletionType and CompletionNo=@CompletionNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("CompletionType",DbType.String,completionOrder.CompletionType),
                    DataAccessMySQL.CreateParameter("CompletionNo",DbType.String,completionOrder.CompletionNo)
                });

            if (dtCompletionOrder.Rows.Count > 0)
            {
                obj = new CompletionOrder();
                obj.CompletionType = dtCompletionOrder.Rows[0]["CompletionType"].ToString();
                obj.OrderSName = dtCompletionOrder.Rows[0]["OrderSName"].ToString();
                obj.CompletionNo = dtCompletionOrder.Rows[0]["CompletionNo"].ToString();
                obj.OrderDate = dtCompletionOrder.Rows[0]["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dtCompletionOrder.Rows[0]["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.NoOfPrints = int.Parse(dtCompletionOrder.Rows[0]["NoOfPrints"].ToString());
                obj.SourceOrderType = dtCompletionOrder.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtCompletionOrder.Rows[0]["SourceOrderNo"].ToString();
                obj.Remark = dtCompletionOrder.Rows[0]["Remark"].ToString();
                obj.CustomerId = dtCompletionOrder.Rows[0]["CustomerId"].ToString();
                obj.CustomerName = dtCompletionOrder.Rows[0]["CustomerName"].ToString();
                obj.CustomerType= dtCompletionOrder.Rows[0]["CustomerType"].ToString()=="A"? "A:一般客戶":"B:經銷商";
                obj.AddressSName = dtCompletionOrder.Rows[0]["AddressSName"].ToString();
                obj.Address = dtCompletionOrder.Rows[0]["Address"].ToString();
                obj.Contact = dtCompletionOrder.Rows[0]["Contact"].ToString();
                obj.TelNo = dtCompletionOrder.Rows[0]["TelNo"].ToString();
                obj.FaxNo = dtCompletionOrder.Rows[0]["FaxNo"].ToString();
                obj.ProductNo = dtCompletionOrder.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtCompletionOrder.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtCompletionOrder.Rows[0]["SerialNo"].ToString();
                obj.AssetNo = dtCompletionOrder.Rows[0]["AssetNo"].ToString();
                obj.Contract = dtCompletionOrder.Rows[0]["ContractType"].ToString()+"-"+ dtCompletionOrder.Rows[0]["ContractNo"].ToString();
                obj.UnderWarranty= dtCompletionOrder.Rows[0]["UnderWarranty"].ToString();
                obj.TestResult = dtCompletionOrder.Rows[0]["TestResult"].ToString();
                obj.InternalRemark = dtCompletionOrder.Rows[0]["InternalRemark"].ToString();
                obj.Confirmed = dtCompletionOrder.Rows[0]["Confirmed"].ToString();
            }

            return obj;
        }

        public bool ConfirmedCompletionOrder(CompletionOrder completionOrder, out string msg)
        {
            try
            {
                DataAccessMySQL dbMySQL = new DataAccessMySQL();
                dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

                dbMySQL.ExecuteNonQuery("update CompletionOrder set Confirmed=@Confirmed,Confirmor=@Confirmor,ConfirmedDate=@ConfirmedDate WHERE CompletionType=@CompletionType and CompletionNo=@CompletionNo",
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("CompletionType",DbType.String,completionOrder.CompletionType),
                        DataAccessMySQL.CreateParameter("CompletionNo",DbType.String,completionOrder.CompletionNo),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,completionOrder.Confirmed),
                        DataAccessMySQL.CreateParameter("Confirmor",DbType.String,completionOrder.Confirmor),
                        DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,completionOrder.ConfirmedDate)
                    });

                if(completionOrder.Confirmed=="Y")
                {

                }else
                {

                }

                msg = "";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

        }


    }
}