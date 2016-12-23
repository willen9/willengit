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
    public class RGA_HDAL : IRGA_HDAL
    {
        public bool AddRGA_H(RGA_H rGA_H, 
            string strQQuestionNo, string strQQuestion,
            string strQDescription, string strDItemId,
            string strDPartNo, string strDPartName,
            string strDQTY, string strDUnit, string strDRemark,
            string strDRepaired, string strDSourceOrderType,
            string strDSourceOrderNo, string strDSourceItemId, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from RGA_H where 
                    RGAType=@RGAType and RGANo=@RGANo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into RGA_H (Company,UserGroup,Creator,CreateDate,
                        RGAType,RGANo,OrderDate,StatusCode,NoOfPrints,SourceOrderType,SourceOrderNo,Version,Repairman,
                        CustomerId,AddressSName,Address,Contact,TelNo,FaxNo,ProductNo,ProductName,SerialNo,
                        AssetNo,ContractType,ContractNo,UnderWarranty,SaleDate,PurchaseDate,WarrantyStartDate,
                        WarrantyExpiryDate,PCompletionDate,CompletionDate,Confirmed,Closed,OptionDetail,Remark,
                        Returned,MalfunctionReason,TestResult,InternalRemark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @RGAType,@RGANo,@OrderDate,@StatusCode,@NoOfPrints,@SourceOrderType,@SourceOrderNo,@Version,@Repairman,
                        @CustomerId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo,@ProductNo,@ProductName,@SerialNo,
                        @AssetNo,@ContractType,@ContractNo,@UnderWarranty,@SaleDate,@PurchaseDate,@WarrantyStartDate,
                        @WarrantyExpiryDate,@PCompletionDate,@CompletionDate,@Confirmed,@Closed,@OptionDetail,@Remark,
                        @Returned,@MalfunctionReason,@TestResult,@InternalRemark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,rGA_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGA_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,rGA_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rGA_H.OrderDate),
                            DataAccessMySQL.CreateParameter("StatusCode",DbType.String,"00"),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rGA_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,rGA_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,rGA_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,rGA_H.Version),
                            DataAccessMySQL.CreateParameter("Repairman",DbType.String,rGA_H.Repairman),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,rGA_H.CustomerId),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,rGA_H.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,rGA_H.Address),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,rGA_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,rGA_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,rGA_H.FaxNo),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,rGA_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,rGA_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rGA_H.SerialNo),
                            DataAccessMySQL.CreateParameter("AssetNo",DbType.String,rGA_H.AssetNo),
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,rGA_H.ContractType),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,rGA_H.ContractNo),
                            DataAccessMySQL.CreateParameter("UnderWarranty",DbType.String,rGA_H.UnderWarranty),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,rGA_H.SaleDate),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,rGA_H.PurchaseDate),
                            DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,rGA_H.WarrantyStartDate),
                            DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,rGA_H.WarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("PCompletionDate",DbType.String,rGA_H.PCompletionDate),
                            DataAccessMySQL.CreateParameter("CompletionDate",DbType.String,rGA_H.CompletionDate),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"N"),
                            DataAccessMySQL.CreateParameter("Closed",DbType.String,"N"),
                            DataAccessMySQL.CreateParameter("OptionDetail",DbType.String,rGA_H.OptionDetail),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rGA_H.Remark),
                            DataAccessMySQL.CreateParameter("Returned",DbType.String,rGA_H.Returned),
                            DataAccessMySQL.CreateParameter("MalfunctionReason",DbType.String,rGA_H.MalfunctionReason),
                            DataAccessMySQL.CreateParameter("TestResult",DbType.String,rGA_H.TestResult),
                            DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,rGA_H.InternalRemark)
                        });

                    dbMySQL.ExecuteNonQuery(@"insert into RepairRecord (ProductNo,SerielNo,RGAType,RGANo,ChangeDate,
                        ChangeOrderType,ChangeOrderNo,ChangeMode,Remark) values (@ProductNo,@SerielNo,@RGAType,@RGANo,
                        @ChangeDate,@ChangeOrderType,@ChangeOrderNo,@ChangeMode,@Remark)", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,rGA_H.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rGA_H.SerialNo),
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo),
                            DataAccessMySQL.CreateParameter("ChangeDate",DbType.String,rGA_H.OrderDate),
                            DataAccessMySQL.CreateParameter("ChangeOrderType",DbType.String,rGA_H.RGAType),
                            DataAccessMySQL.CreateParameter("ChangeOrderNo",DbType.String,rGA_H.RGANo),
                            DataAccessMySQL.CreateParameter("ChangeMode",DbType.String,"00"),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,""),
                        });

                    string sql = "";

                    if (strQQuestionNo != "" && strQQuestionNo != null)
                    {
                        //string[] strQItemIdArray = strQItemId.Split(',');
                        string[] strQQuestionNoArray = strQQuestionNo.Split(',');
                        string[] strQQuestionArray = strQQuestion.Split(',');
                        string[] strQDescriptionArray = strQDescription.Split(',');

                        sql = @"insert into RGA_Q (Company,UserGroup,Creator,
                        CreateDate,RGAType,RGANo,ItemId,QuestionNo,Question,Description) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RGAType,@RGANo,@ItemId,@QuestionNo,@Question,@Description)";


                        for (int i = 0; i < strQQuestionNoArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strQQuestionNoArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,rGA_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGA_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,rGA_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,strQQuestionNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Question",DbType.String,strQQuestionArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Description",DbType.String,strQDescriptionArray[i].ToString())
                            });
                        }
                    }

                    if (strDItemId != "" && strDItemId != null)
                    {
                        string[] strDItemIdArray = strDItemId.Split(',');
                        string[] strDPartNoArray = strDPartNo.Split(',');
                        string[] strDPartNameArray = strDPartName.Split(',');
                        string[] strDQTYArray = strDQTY.Split(',');
                        string[] strDUnitArray = strDUnit.Split(',');
                        string[] strDRemarkArray = strDRemark.Split(',');
                        string[] strDRepairedArray = strDRepaired.Split(',');
                        string[] strDSourceOrderTypeArray = strDSourceOrderType.Split(',');
                        string[] strDSourceOrderNoArray = strDSourceOrderNo.Split(',');
                        string[] strDSourceItemIdArray = strDSourceItemId.Split(',');
                        //string[] strDResponseOrderTypeArray = strDResponseOrderType.Split(',');
                        //string[] sstrDResponseOrderNoArray = strDResponseOrderNo.Split(',');
                        //string[] strDResponseDateArray = strDResponseDate.Split(',');

                        sql = @"insert into RGA_D (Company,UserGroup,Creator,
                        CreateDate,RGAType,RGANo,ItemId,PartNo,PartName,QTY,Unit,Remark,
                        Repaired,SourceOrderType,SourceOrderNo,SourceItemId) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RGAType,@RGANo,@ItemId,@PartNo,@PartName,@QTY,@Unit,@Remark,
                        @Repaired,@SourceOrderType,@SourceOrderNo,@SourceItemId)";


                        for (int i = 0; i < strDItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strDItemIdArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,rGA_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGA_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,rGA_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strDItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PartNo",DbType.String,strDPartNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PartName",DbType.String,strDPartNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.Double,double.Parse(strDQTYArray[i].ToString()==""?"0":strDQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strDUnitArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strDRemarkArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Repaired",DbType.String,strDRepairedArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,strDSourceOrderTypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,strDSourceOrderNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceItemId",DbType.String,strDSourceItemIdArray[i].ToString()),
                                //DataAccessMySQL.CreateParameter("ResponseOrderType",DbType.String,strDResponseOrderTypeArray[i].ToString()),
                                //DataAccessMySQL.CreateParameter("ResponseOrderNo",DbType.String,sstrDResponseOrderNoArray[i].ToString()),
                                //DataAccessMySQL.CreateParameter("ResponseDate",DbType.String,strDResponseDateArray[i].ToString())
                            });
                        }
                    }
                    tran.Commit();
                    msg = "";
                    return true;
                }
                else
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    msg = "資料已存在！";
                    return false;
                }

            }
            catch (Exception ex)
            {
                if(tran!=null)
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

        public bool UpdateRGA_H(RGA_H rGA_H, 
            string strQQuestionNo, string strQQuestion,
            string strQDescription, string strDItemId,
            string strDPartNo, string strDPartName,
            string strDQTY, string strDUnit, string strDRemark,
            string strDRepaired, string strDSourceOrderType,
            string strDSourceOrderNo, string strDSourceItemId, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update RGA_H set Modifier=@Modifier,ModiDate=@ModiDate,OrderDate=@OrderDate,
                        CustomerId=@CustomerId,
                        AddressSName=@AddressSName,Address=@Address,
                        Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,
                        ProductNo=@ProductNo,ProductName=@ProductName,
                        SerialNo=@SerialNo,AssetNo=@AssetNo,
                        ContractType=@ContractType,ContractNo=@ContractNo,
                        UnderWarranty=@UnderWarranty,SaleDate=@SaleDate,
                        PurchaseDate=@PurchaseDate,WarrantyStartDate=@WarrantyStartDate,
                        WarrantyExpiryDate=@WarrantyExpiryDate,
                        PCompletionDate=@PCompletionDate,
                        OptionDetail=@OptionDetail,
                        Remark=@Remark,
                        Returned=@Returned,
                        MalfunctionReason=@MalfunctionReason,
                        TestResult=@TestResult,
                        InternalRemark=@InternalRemark
                        where RGAType=@RGAType and RGANo=@RGANo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,rGA_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rGA_H.OrderDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rGA_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,rGA_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,rGA_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,rGA_H.Version),
                            DataAccessMySQL.CreateParameter("Repairman",DbType.String,rGA_H.Repairman),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,rGA_H.CustomerId),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,rGA_H.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,rGA_H.Address),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,rGA_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,rGA_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,rGA_H.FaxNo),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,rGA_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,rGA_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rGA_H.SerialNo),
                            DataAccessMySQL.CreateParameter("AssetNo",DbType.String,rGA_H.AssetNo),
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,rGA_H.ContractType),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,rGA_H.ContractNo),
                            DataAccessMySQL.CreateParameter("UnderWarranty",DbType.String,rGA_H.UnderWarranty),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,rGA_H.SaleDate),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,rGA_H.PurchaseDate),
                            DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,rGA_H.WarrantyStartDate),
                            DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,rGA_H.WarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("PCompletionDate",DbType.String,rGA_H.PCompletionDate),
                            DataAccessMySQL.CreateParameter("CompletionDate",DbType.String,rGA_H.CompletionDate),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rGA_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Closed",DbType.String,rGA_H.Closed),
                            DataAccessMySQL.CreateParameter("OptionDetail",DbType.String,rGA_H.OptionDetail),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rGA_H.Remark),
                            DataAccessMySQL.CreateParameter("Returned",DbType.String,rGA_H.Returned),
                            DataAccessMySQL.CreateParameter("MalfunctionReason",DbType.String,rGA_H.MalfunctionReason),
                            DataAccessMySQL.CreateParameter("TestResult",DbType.String,rGA_H.TestResult),
                            DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,rGA_H.InternalRemark)
                        });

                dbMySQL.ExecuteNonQuery("delete from RGA_Q where RGAType=@RGAType and RGANo=@RGANo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo)
                    });

                dbMySQL.ExecuteNonQuery("delete from RGA_D where RGAType=@RGAType and RGANo=@RGANo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo)
                    });
                string sql = "";

                if (strQQuestionNo != "" && strQQuestionNo != null)
                {
                    //string[] strQItemIdArray = strQItemId.Split(',');
                    string[] strQQuestionNoArray = strQQuestionNo.Split(',');
                    string[] strQQuestionArray = strQQuestion.Split(',');
                    string[] strQDescriptionArray = strQDescription.Split(',');

                    sql = @"insert into RGA_Q (Company,UserGroup,Creator,
                        CreateDate,RGAType,RGANo,ItemId,QuestionNo,Question,Description) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RGAType,@RGANo,@ItemId,@QuestionNo,@Question,@Description)";


                    for (int i = 0; i < strQQuestionNoArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strQQuestionNoArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,rGA_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGA_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,rGA_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,strQQuestionNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Question",DbType.String,strQQuestionArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Description",DbType.String,strQDescriptionArray[i].ToString())
                            });
                    }
                }

                if (strDItemId != "" && strDItemId != null)
                {
                    string[] strDItemIdArray = strDItemId.Split(',');
                    string[] strDPartNoArray = strDPartNo.Split(',');
                    string[] strDPartNameArray = strDPartName.Split(',');
                    string[] strDQTYArray = strDQTY.Split(',');
                    string[] strDUnitArray = strDUnit.Split(',');
                    string[] strDRemarkArray = strDRemark.Split(',');
                    string[] strDRepairedArray = strDRepaired.Split(',');
                    string[] strDSourceOrderTypeArray = strDSourceOrderType.Split(',');
                    string[] strDSourceOrderNoArray = strDSourceOrderNo.Split(',');
                    string[] strDSourceItemIdArray = strDSourceItemId.Split(',');
                    //string[] strDResponseOrderTypeArray = strDResponseOrderType.Split(',');
                    //string[] sstrDResponseOrderNoArray = strDResponseOrderNo.Split(',');
                    //string[] strDResponseDateArray = strDResponseDate.Split(',');

                    sql = @"insert into RGA_D (Company,UserGroup,Creator,
                        CreateDate,RGAType,RGANo,ItemId,PartNo,PartName,QTY,Unit,Remark,
                        Repaired,SourceOrderType,SourceOrderNo,SourceItemId) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RGAType,@RGANo,@ItemId,@PartNo,@PartName,@QTY,@Unit,@Remark,
                        @Repaired,@SourceOrderType,@SourceOrderNo,@SourceItemId)";


                    for (int i = 0; i < strDItemIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strDItemIdArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,rGA_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGA_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,rGA_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strDItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PartNo",DbType.String,strDPartNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PartName",DbType.String,strDPartNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.Double,double.Parse(strDQTYArray[i].ToString()==""?"0":strDQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strDUnitArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strDRemarkArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Repaired",DbType.String,strDRepairedArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,strDSourceOrderTypeArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,strDSourceOrderNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("SourceItemId",DbType.String,strDSourceItemIdArray[i].ToString()),
                                //DataAccessMySQL.CreateParameter("ResponseOrderType",DbType.String,strDResponseOrderTypeArray[i].ToString()),
                                //DataAccessMySQL.CreateParameter("ResponseOrderNo",DbType.String,sstrDResponseOrderNoArray[i].ToString()),
                                //DataAccessMySQL.CreateParameter("ResponseDate",DbType.String,strDResponseDateArray[i].ToString())
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

        public bool DelRGA_H(RGA_H rGA_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery("delete from RGA_H where RGAType=@RGAType and RGANo=@RGANo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo)
                        });
                dbMySQL.ExecuteNonQuery("delete from RGA_Q where RGAType=@RGAType and RGANo=@RGANo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo)
                        });
                dbMySQL.ExecuteNonQuery("delete from RGA_D where RGAType=@RGAType and RGANo=@RGANo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo)
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

        public List<RGA_H> SearchRGA_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RGA_H> objRGA_H = new List<RGA_H>();

            string sql = @"select h.*,o.OrderSName,c.CustomerType,c.CustomerName,e.EmployeeName as RepairmanName from RGA_H as h 
                left join OrderCategory as o on o.OrderType=h.RGAType 
                left join Customer as c on c.CustomerId=h.CustomerId
                left join Employee as e on e.EmployeeId=h.Repairman where 1=1";

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


            DataTable dtRGA_H = null;
            if (strCondition != "")
            {
                dtRGA_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRGA_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtRGA_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGA_H.Rows)
                {
                    RGA_H obj = new RGA_H();
                    obj.RGAType = dr["RGAType"].ToString();     //維修單別
                    obj.RGANo = dr["RGANo"].ToString();         //維修單號
                    obj.CustomerId = dr["CustomerId"].ToString();       //客戶代號
                    obj.AddressSName = dr["AddressSName"].ToString();       //地址簡稱
                    obj.ProductNo = dr["ProductNo"].ToString();         //品號
                    obj.ProductName = dr["ProductName"].ToString();     //品名
                    obj.SerialNo = dr["SerialNo"].ToString();           //序號
                    if (dr["StatusCode"].ToString()=="00")           //狀態碼
                    {
                        obj.StatusCode = "未處理";
                    }
                    if (dr["StatusCode"].ToString() == "05")
                    {
                        obj.StatusCode = "已派工";
                    }
                    if (dr["StatusCode"].ToString() == "10")
                    {
                        obj.StatusCode = "送廠申請";
                    }
                    if (dr["StatusCode"].ToString() == "20")
                    {
                        obj.StatusCode = "已檢測";
                    }
                    if (dr["StatusCode"].ToString() == "25")
                    {
                        obj.StatusCode = "送廠維修";
                    }
                    if (dr["StatusCode"].ToString() == "30")
                    {
                        obj.StatusCode = "原廠報價";
                    }
                    if (dr["StatusCode"].ToString() == "35")
                    {
                        obj.StatusCode = "已報價";
                    }
                    if (dr["StatusCode"].ToString() == "40")
                    {
                        obj.StatusCode = "已回覆報價";
                    }
                    if (dr["StatusCode"].ToString() == "45")
                    {
                        obj.StatusCode = "送廠歸還";
                    }
                    if (dr["StatusCode"].ToString() == "50")
                    {
                        obj.StatusCode = "完工";
                    }
                    if (dr["StatusCode"].ToString() == "55")
                    {
                        obj.StatusCode = "結案";
                    }
                    if (dr["StatusCode"].ToString() == "D")
                    {
                        obj.StatusCode = "作廢";
                    }
                    obj.Repairman = dr["Repairman"].ToString();         //維修人員ID
                    obj.AssetNo = dr["AssetNo"].ToString();         //資產編號
                    obj.PCompletionDate = dr["PCompletionDate"].ToString();     //預計完工日
                    obj.OrderSName = dr["OrderSName"].ToString();           //維修單別名稱
                    obj.CustomerName = dr["CustomerName"].ToString();       //客戶簡稱
                    obj.RepairmanName = dr["RepairmanName"].ToString();     //維修人員
                    obj.CustomerType = dr["CustomerType"].ToString();       //客戶型態
                    obj.Returned = dr["Returned"].ToString();       //覆修否
                    //obj.Repaired = "";      //維修否
                    //原廠維修申請
                    objRGA_H.Add(obj);
                }
            }

            return objRGA_H;
        }

        public List<RGA_H> SearchRGA(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            List<RGA_H> objRGA_H = new List<RGA_H>();
            try
            {
                string sql = @"select h.*,o.OrderSName,c.CustomerType,c.CustomerName,e.EmployeeName as RepairmanName from RGA_H as h 
                left join OrderCategory as o on o.OrderType=h.RGAType 
                left join Customer as c on c.CustomerId=h.CustomerId
                left join Employee as e on e.EmployeeId=h.Repairman where 1=1";

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
                        RGA_H obj = new RGA_H();
                        obj.RGAType = dr["RGAType"].ToString();     //維修單別
                        obj.OrderSName = dr["OrderSName"].ToString();     //單據名稱
                        obj.RGANo = dr["RGANo"].ToString();         //維修單號
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
                        if (dr["StatusCode"].ToString() == "00")           //狀態碼
                        {
                            obj.StatusCode = "未處理";
                        }
                        if (dr["StatusCode"].ToString() == "05")
                        {
                            obj.StatusCode = "已派工";
                        }
                        if (dr["StatusCode"].ToString() == "10")
                        {
                            obj.StatusCode = "送廠申請";
                        }
                        if (dr["StatusCode"].ToString() == "20")
                        {
                            obj.StatusCode = "已檢測";
                        }
                        if (dr["StatusCode"].ToString() == "25")
                        {
                            obj.StatusCode = "送廠維修";
                        }
                        if (dr["StatusCode"].ToString() == "30")
                        {
                            obj.StatusCode = "原廠報價";
                        }
                        if (dr["StatusCode"].ToString() == "35")
                        {
                            obj.StatusCode = "已報價";
                        }
                        if (dr["StatusCode"].ToString() == "40")
                        {
                            obj.StatusCode = "已回覆報價";
                        }
                        if (dr["StatusCode"].ToString() == "45")
                        {
                            obj.StatusCode = "送廠歸還";
                        }
                        if (dr["StatusCode"].ToString() == "50")
                        {
                            obj.StatusCode = "完工";
                        }
                        if (dr["StatusCode"].ToString() == "55")
                        {
                            obj.StatusCode = "結案";
                        }
                        if (dr["StatusCode"].ToString() == "D")
                        {
                            obj.StatusCode = "作廢";
                        }
                        obj.Repairman = dr["Repairman"].ToString();         //維修人員ID
                        obj.AssetNo = dr["AssetNo"].ToString();         //資產編號
                        obj.PCompletionDate = dr["PCompletionDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["PCompletionDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //預計完工日
                        obj.RepairmanName = dr["RepairmanName"].ToString();     //維修人員
                        obj.Returned = dr["Returned"].ToString();       //覆修否
                        obj.Remark = dr["Remark"].ToString();       //入件說明
                                                                    //obj.Repaired = "";      //維修否
                                                                    //原廠維修申請
                        objRGA_H.Add(obj);
                    }
                }
                return objRGA_H;
            }catch(Exception ex)
            {
                return objRGA_H;
            }

            
        }

        public RGA_H GetRGA_HInfo(RGA_H rGA_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            RGA_H obj = new RGA_H();

            DataTable dtRGA_H = dbMySQL.ExecuteDataTable(@"select h.*,o.OrderSName,c.CustomerName,c.CustomerType from RGA_H as h 
                    left join Customer as c on c.CustomerId=h.CustomerId 
                    left join OrderCategory as o on o.OrderType=h.RGAType
                    where RGAType=@RGAType and RGANo=@RGANo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo)
                    });

            if (dtRGA_H.Rows.Count > 0)
            {
                obj.RGAType = dtRGA_H.Rows[0]["RGAType"].ToString();
                obj.OrderSName = dtRGA_H.Rows[0]["OrderSName"].ToString();
                obj.RGANo = dtRGA_H.Rows[0]["RGANo"].ToString();
                obj.OrderDate = dtRGA_H.Rows[0]["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRGA_H.Rows[0]["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                if (dtRGA_H.Rows[0]["StatusCode"].ToString() == "00")           //狀態碼
                {
                    obj.StatusCode = "00.未處理";
                }
                if (dtRGA_H.Rows[0]["StatusCode"].ToString() == "05")
                {
                    obj.StatusCode = "05.已派工";
                }
                if (dtRGA_H.Rows[0]["StatusCode"].ToString() == "10")
                {
                    obj.StatusCode = "10.送廠申請";
                }
                if (dtRGA_H.Rows[0]["StatusCode"].ToString() == "20")
                {
                    obj.StatusCode = "20.已檢測";
                }
                if (dtRGA_H.Rows[0]["StatusCode"].ToString() == "25")
                {
                    obj.StatusCode = "25.送廠維修";
                }
                if (dtRGA_H.Rows[0]["StatusCode"].ToString() == "30")
                {
                    obj.StatusCode = "30.原廠報價";
                }
                if (dtRGA_H.Rows[0]["StatusCode"].ToString() == "35")
                {
                    obj.StatusCode = "35.已報價";
                }
                if (dtRGA_H.Rows[0]["StatusCode"].ToString() == "40")
                {
                    obj.StatusCode = "40.已回覆報價";
                }
                if (dtRGA_H.Rows[0]["StatusCode"].ToString() == "45")
                {
                    obj.StatusCode = "45.送廠歸還";
                }
                if (dtRGA_H.Rows[0]["StatusCode"].ToString() == "50")
                {
                    obj.StatusCode = "50.完工";
                }
                if (dtRGA_H.Rows[0]["StatusCode"].ToString() == "55")
                {
                    obj.StatusCode = "55.結案";
                }
                if (dtRGA_H.Rows[0]["StatusCode"].ToString() == "D")
                {
                    obj.StatusCode = "D.作廢";
                }
                obj.NoOfPrints = int.Parse(dtRGA_H.Rows[0]["NoOfPrints"].ToString());
                obj.SourceOrderType = dtRGA_H.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtRGA_H.Rows[0]["SourceOrderNo"].ToString();
                obj.Version = dtRGA_H.Rows[0]["Version"].ToString();
                obj.Repairman = dtRGA_H.Rows[0]["Repairman"].ToString();
                obj.CustomerId = dtRGA_H.Rows[0]["CustomerId"].ToString();
                obj.CustomerName = dtRGA_H.Rows[0]["CustomerName"].ToString();
                if(dtRGA_H.Rows[0]["CustomerType"].ToString()!="")
                {
                    obj.CustomerType = dtRGA_H.Rows[0]["CustomerType"].ToString() == "A" ? "A:一般客戶" : "B:經銷商";
                }
                else
                {
                    obj.CustomerType = "";
                }

                obj.AddressSName = dtRGA_H.Rows[0]["AddressSName"].ToString();
                obj.Address = dtRGA_H.Rows[0]["Address"].ToString();
                obj.Contact = dtRGA_H.Rows[0]["Contact"].ToString();
                obj.TelNo = dtRGA_H.Rows[0]["TelNo"].ToString();
                obj.FaxNo = dtRGA_H.Rows[0]["FaxNo"].ToString();
                obj.ProductNo = dtRGA_H.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtRGA_H.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtRGA_H.Rows[0]["SerialNo"].ToString();
                obj.AssetNo = dtRGA_H.Rows[0]["AssetNo"].ToString();
                obj.ContractType = dtRGA_H.Rows[0]["ContractType"].ToString();
                obj.ContractNo = dtRGA_H.Rows[0]["ContractNo"].ToString();
                obj.UnderWarranty = dtRGA_H.Rows[0]["UnderWarranty"].ToString();
                obj.SaleDate = dtRGA_H.Rows[0]["SaleDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRGA_H.Rows[0]["SaleDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.PurchaseDate = dtRGA_H.Rows[0]["PurchaseDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRGA_H.Rows[0]["PurchaseDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.WarrantyStartDate = dtRGA_H.Rows[0]["WarrantyStartDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRGA_H.Rows[0]["WarrantyStartDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.WarrantyExpiryDate = dtRGA_H.Rows[0]["WarrantyExpiryDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRGA_H.Rows[0]["WarrantyExpiryDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.PCompletionDate = dtRGA_H.Rows[0]["PCompletionDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRGA_H.Rows[0]["PCompletionDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.CompletionDate = dtRGA_H.Rows[0]["CompletionDate"].ToString() == "" ? "" : DateTime.ParseExact(dtRGA_H.Rows[0]["CompletionDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.Confirmed = dtRGA_H.Rows[0]["Confirmed"].ToString();
                obj.Closed = dtRGA_H.Rows[0]["Closed"].ToString();
                obj.OptionDetail = dtRGA_H.Rows[0]["OptionDetail"].ToString();
                obj.Remark = dtRGA_H.Rows[0]["Remark"].ToString();
                obj.Returned = dtRGA_H.Rows[0]["Returned"].ToString();
                obj.MalfunctionReason = dtRGA_H.Rows[0]["MalfunctionReason"].ToString();
                obj.TestResult = dtRGA_H.Rows[0]["TestResult"].ToString();
                obj.InternalRemark = dtRGA_H.Rows[0]["InternalRemark"].ToString();
            }

            return obj;
        }

        public bool IsRGA_H(RGA_H rGA_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from RGA_H where 
                    RGAType=@RGAType and RGANo=@RGANo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo)
                    }).ToString()) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ConfirmedRGA_H(RGA_H rGA_H, out string msg)
        {
            try
            {
                DataAccessMySQL dbMySQL = new DataAccessMySQL();
                dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

                dbMySQL.ExecuteNonQuery("update RGA_H set Confirmed=@Confirmed,Confirmor=@Confirmor,ConfirmedDate=@ConfirmedDate,StatusCode=@StatusCode WHERE RGAType=@RGAType and RGANo=@RGANo",
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_H.RGANo),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rGA_H.Confirmed),
                        DataAccessMySQL.CreateParameter("Confirmor",DbType.String,rGA_H.Confirmor),
                        DataAccessMySQL.CreateParameter("StatusCode",DbType.String,rGA_H.StatusCode),
                        DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,rGA_H.ConfirmedDate)
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

        public bool UpdateAsign(Assignment assignment, string SupportId)
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

                    orderInfo = GetSupportAplOrderNo(assignment.AssignOrderType).Split('-');

                    if (orderInfo[0].ToString() == "None")
                    {
                        return false;
                    }
                    DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable(@"select * from RGA_H where RGAType=@RGAType 
                        and RGANo=@RGANo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,valueInfo[1].ToString()),
                        });
                    DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select AutoConfirm from OrderCategory where OrderType=@OrderType", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("OrderType",DbType.String,assignment.AssignOrderType)
                        });
                    string AutoConfirm = "N";
                    if (dtOrderCategory != null && dtOrderCategory.Rows.Count > 0)
                    {
                        if (dtOrderCategory.Rows[0]["AutoConfirm"].ToString() == "Y")
                        {
                            dbMySQL.ExecuteNonQuery(@"update RGA_H set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,ModiDate=@ModiDate,StatusCode=@StatusCode,Repairman=@Repairman
                             where RGAType=@RGAType 
                            and RGANo=@RGANo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,assignment.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignment.UserGroup),
                                DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignment.Modifier),
                                DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,valueInfo[0].ToString()),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,valueInfo[1].ToString()),
                                DataAccessMySQL.CreateParameter("StatusCode",DbType.String, "05"),
                                DataAccessMySQL.CreateParameter("Repairman",DbType.String,assignment.Designee)
                            });

                            dbMySQL.ExecuteNonQuery(@"insert into  RepairRecord ( ProductNo,SerielNo, 
                                    RGAType,RGANo,ChangeDate,ChangeOrderType,ChangeOrderNo,ChangeMode) values ( @ProductNo,@SerielNo, 
                                    @RGAType,@RGANo,@ChangeDate,@ChangeOrderType,@ChangeOrderNo,@ChangeMode)", tran,
                            new DbParameter[] {
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,dtSupportApl_H.Rows[0]["ProductNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("SerielNo",DbType.String,dtSupportApl_H.Rows[0]["SerielNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,valueInfo[0].ToString()),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,valueInfo[1].ToString()),
                                    DataAccessMySQL.CreateParameter("ChangeDate",DbType.String,assignment.AssignDate),
                                    DataAccessMySQL.CreateParameter("ChangeOrderType",DbType.String,assignment.AssignOrderType),
                                    DataAccessMySQL.CreateParameter("ChangeOrderNo",DbType.String,assignment.AssignOrderNo),
                                    DataAccessMySQL.CreateParameter("ChangeMode",DbType.String,"05"),
                            });

                            AutoConfirm = "Y";
                        }
                    }
                    
                    if (dtSupportApl_H != null && dtSupportApl_H.Rows.Count > 0)
                    {
                        dbMySQL.ExecuteNonQuery(@"insert into Assignment (Company,UserGroup,Creator,CreateDate,AssignOrderType,AssignOrderNo,OrderDate,CustomerId,
                        Version,SourceOrderType,SourceOrderNo,Designee,Assignor,AssignDate,NoOfPrints,Remark,Confirmed) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,@AssignOrderType,@AssignOrderNo,@OrderDate,@CustomerId,@Version,@SourceOrderType,@SourceOrderNo,
                        @Designee,@Assignor,@AssignDate,@NoOfPrints,@Remark,@Confirmed)", tran,
                            new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,assignment.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignment.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,assignment.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,orderInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,dtSupportApl_H.Rows[0]["OrderDate"].ToString()),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtSupportApl_H.Rows[0]["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,"0000"),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("Designee",DbType.String,assignment.Designee),
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,assignment.Assignor),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,assignment.AssignDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Decimal,0),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,dtSupportApl_H.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,AutoConfirm)
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
                        and SourceOrderNo=@SourceOrderNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,valueInfo[1].ToString())
                        });

                    if (dt != null && dt.Rows.Count > 0)
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

        public List<RGA_H> SearchRGAInBom(RGA_H rGA_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGA_H> objRGA_H = new List<RGA_H>();

            string sql = @"select h.*,d.*,re.ConfirmedDate,re.Remark from RGA_H as h 
                left join RGA_D as d on h.RGAType=d.RGAType and h.RGANo=d.RGANo
                left join (select rh.ConfirmedDate,rd.Remark,rd.RGAType,rd.RGANo from RGAReturn_D as rd 
                left join RGAReturn_H as rh on rd.RGAReturnType=rh.RGAReturnType and rd.RGAReturnNo=rh.RGAReturnNo) as re
                on re.RGAType=h.RGAType and re.RGANo=h.RGANo  where 1=1";

            if (!String.IsNullOrEmpty(rGA_H.RGAType))
            {
                sql += " and h.RGAType = @RGAType";
            }

            if (!String.IsNullOrEmpty(rGA_H.RGANo))
            {
                sql += " and h.RGANo = @RGANo";
            }

            if (!String.IsNullOrEmpty(rGA_H.ProductNo))
            {
                sql += " and h.ProductNo = @ProductNo";
            }

            if (!String.IsNullOrEmpty(rGA_H.StatusCode))
            {
                sql += " and h.StatusCode = @StatusCode";
            }

            DataTable dtRGA_H = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,"%"+rGA_H.RGAType+"%"),
                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,"%"+rGA_H.RGANo+"%"),
                    DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rGA_H.Confirmed),
                    DataAccessMySQL.CreateParameter("Closed",DbType.String,rGA_H.Closed),
                    DataAccessMySQL.CreateParameter("StatusCode",DbType.String,rGA_H.StatusCode)
                });

            if (dtRGA_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGA_H.Rows)
                {
                    RGA_H obj = new RGA_H();
                    obj.RGAType = dr["RGAType"].ToString();     //維修單別
                    obj.OrderSName = dr["OrderSName"].ToString();     //單據名稱
                    obj.RGANo = dr["RGANo"].ToString();         //維修單號
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
                    if (dr["StatusCode"].ToString() == "00")           //狀態碼
                    {
                        obj.StatusCode = "未處理";
                    }
                    if (dr["StatusCode"].ToString() == "05")
                    {
                        obj.StatusCode = "已派工";
                    }
                    if (dr["StatusCode"].ToString() == "10")
                    {
                        obj.StatusCode = "送廠申請";
                    }
                    if (dr["StatusCode"].ToString() == "20")
                    {
                        obj.StatusCode = "已檢測";
                    }
                    if (dr["StatusCode"].ToString() == "25")
                    {
                        obj.StatusCode = "送廠維修";
                    }
                    if (dr["StatusCode"].ToString() == "30")
                    {
                        obj.StatusCode = "原廠報價";
                    }
                    if (dr["StatusCode"].ToString() == "35")
                    {
                        obj.StatusCode = "已報價";
                    }
                    if (dr["StatusCode"].ToString() == "40")
                    {
                        obj.StatusCode = "已回覆報價";
                    }
                    if (dr["StatusCode"].ToString() == "45")
                    {
                        obj.StatusCode = "送廠歸還";
                    }
                    if (dr["StatusCode"].ToString() == "50")
                    {
                        obj.StatusCode = "完工";
                    }
                    if (dr["StatusCode"].ToString() == "55")
                    {
                        obj.StatusCode = "結案";
                    }
                    if (dr["StatusCode"].ToString() == "D")
                    {
                        obj.StatusCode = "作廢";
                    }
                    obj.Repairman = dr["Repairman"].ToString();         //維修人員ID
                    obj.AssetNo = dr["AssetNo"].ToString();         //資產編號
                    obj.PCompletionDate = dr["PCompletionDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["PCompletionDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //預計完工日
                    obj.RepairmanName = dr["RepairmanName"].ToString();     //維修人員
                    obj.Returned = dr["Returned"].ToString();       //覆修否
                    obj.Remark = dr["Remark"].ToString();       //入件說明
                    //obj.Repaired = "";      //維修否
                    //原廠維修申請
                    objRGA_H.Add(obj);
                }
            }

            return objRGA_H;
        }
        
        public List<RGA_H> SearchPartLifetime()
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGA_H> objRGA_H = new List<RGA_H>();

            string sql = @"select *,(select count(*) from Warranty_D as d where rga.ProductNo=d.ProductNo and WarrantyType in ('2','3')) as PartNume from (
                    select h.ProductNo,h.ProductName,count(*) as RGANum from RGA_H as h where 
                    StatusCode='55' group by h.ProductNo ) as rga order by  RGANum desc limit 0,10";

            DataTable dtRGA_H = dbMySQL.ExecuteDataTable(sql);

            if (dtRGA_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGA_H.Rows)
                {
                    RGA_H obj = new RGA_H();
                    obj.ProductNo = dr["ProductNo"].ToString();     //品號
                    obj.ProductName = dr["ProductName"].ToString();     //品名
                    obj.RGANum = dr["RGANum"].ToString();         //商品數量
                    obj.PartNume = dr["PartNume"].ToString();         //維修數量
                    objRGA_H.Add(obj);
                }
            }

            return objRGA_H;
        }

        public List<RGA_H> SearchPartLifetimeCover()
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGA_H> objRGA_H = new List<RGA_H>();

            string sql = @"select *,(select count(*) from Warranty_D as d where rga.ProductNo=d.ProductNo and WarrantyType in ('2','3')) as PartNume from (
                    select h.ProductNo,h.ProductName,count(*) as RGANum from RGA_H as h where 
                    StatusCode='55' and Returned='Y' group by h.ProductNo ) as rga order by  RGANum desc limit 0,10";

            DataTable dtRGA_H = dbMySQL.ExecuteDataTable(sql);

            if (dtRGA_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGA_H.Rows)
                {
                    RGA_H obj = new RGA_H();
                    obj.ProductNo = dr["ProductNo"].ToString();     //品號
                    obj.ProductName = dr["ProductName"].ToString();     //品名
                    obj.RGANum = dr["RGANum"].ToString();         //商品數量
                    obj.PartNume = dr["PartNume"].ToString();         //維修數量
                    objRGA_H.Add(obj);
                }
            }

            return objRGA_H;
        }

        public List<RGA_H> Search_Contract()
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGA_H> objRGA_H = new List<RGA_H>();

            string sql = @"select *,(select count(*) from Contract_ProductD as d where rga.ProductNo=d.ProductNo) as PartNume from (
                    select h.ProductNo,h.ProductName,count(*) as RGANum from RGA_H as h where 
                    StatusCode='55' group by h.ProductNo ) as rga order by  RGANum desc limit 0,10";

            DataTable dtRGA_H = dbMySQL.ExecuteDataTable(sql);

            if (dtRGA_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGA_H.Rows)
                {
                    RGA_H obj = new RGA_H();
                    obj.ProductNo = dr["ProductNo"].ToString();     //品號
                    obj.ProductName = dr["ProductName"].ToString();     //品名
                    obj.RGANum = dr["RGANum"].ToString();         //商品數量
                    obj.PartNume = dr["PartNume"].ToString();         //維修數量
                    objRGA_H.Add(obj);
                }
            }

            return objRGA_H;
        }

        public List<RGA_H> Search_ContractCover()
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGA_H> objRGA_H = new List<RGA_H>();

            string sql = @"select *,(select count(*) from Contract_ProductD as d where 
                           rga.ProductNo=d.ProductNo)
                           as PartNume from (select h.ProductNo,h.ProductName,count(*) 
                           as RGANum from RGA_H as h where StatusCode='55' and Returned='Y' 
                           group by h.ProductNo ) as rga order by  RGANum desc limit 0,10";

            DataTable dtRGA_H = dbMySQL.ExecuteDataTable(sql);

            if (dtRGA_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGA_H.Rows)
                {
                    RGA_H obj = new RGA_H();
                    obj.ProductNo = dr["ProductNo"].ToString();     //品號
                    obj.ProductName = dr["ProductName"].ToString();     //品名
                    obj.RGANum = dr["RGANum"].ToString();         //商品數量
                    obj.PartNume = dr["PartNume"].ToString();         //維修數量
                    objRGA_H.Add(obj);
                }
            }

            return objRGA_H;
        }

        public string SearchPartSum(string Col, string Condition, string conditionValue)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            //銷售
            string sql = @"select count(*) as qty from Warranty_D where WarrantyType in ('2','3')";

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

            DbParameter[] dbParameters = new DbParameter[len - 1];
            int dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                if (colArray[i] == "CustomerNo")
                    colArray[i] = "TargetId";
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

            DataTable dtRGA_H = null;
            if (strCondition != "")
            {

                dtRGA_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRGA_H = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }
            string sumqty = "";
            if (dtRGA_H.Rows.Count > 0)
            {
                sumqty = dtRGA_H.Rows[0]["qty"].ToString();
            }

            return sumqty;
        }

        public List<Warranty_D> SearchPartSumList(string Col, string Condition, string conditionValue)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Warranty_D> objWarranty_D = new List<Warranty_D>();
            //銷售
            string sql = @"select d.*,p.ProductName,c.CustomerName from Warranty_D as d 
                left join Product as p on p.ProductNo=d.ProductNo
                left join Customer as c on c.CustomerId=d.TargetId where WarrantyType in ('2','3')";

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

            DbParameter[] dbParameters = new DbParameter[len - 1];
            int dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                if (colArray[i] == "CustomerNo")
                    colArray[i] = "TargetId";

                if (colArray[i] == "ProductNo")
                    colArray[i] = "d.ProductNo";
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

            DataTable dtRGA_H = null;
            if (strCondition != "")
            {

                dtRGA_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRGA_H = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }
            foreach (DataRow dr in dtRGA_H.Rows)
            {
                Warranty_D obj = new Warranty_D();
                obj.ChangeDate = dr["ChangeDate"].ToString();
                obj.ProductNo = dr["ProductId"].ToString();
                obj.ProductName = dr["ProductName"].ToString();
                obj.TargetId = dr["TargetId"].ToString();
                obj.CustomerName = dr["CustomerName"].ToString();
                obj.ChangeOrderType = dr["ChangeOrderType"].ToString();
                obj.ChangeOrderNo = dr["ChangeOrderNo"].ToString();
                obj.Remark = dr["Remark"].ToString();
                objWarranty_D.Add(obj);
            }

            return objWarranty_D;
        }

        public string SearchPartW(string Col, string Condition, string conditionValue)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sql = @"select count(*) as qty from RGA_H where StatusCode='55'";

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

            DbParameter[] dbParameters = new DbParameter[len - 1];
            int dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                if (colArray[i] == "CustomerNo")
                    colArray[i] = "CustomerId";
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

            DataTable dtRGA_H = null;
            if (strCondition != "")
            {

                dtRGA_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRGA_H = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }
            string sumqty = "";
            if (dtRGA_H.Rows.Count > 0)
            {
                sumqty = dtRGA_H.Rows[0]["qty"].ToString();
            }

            return sumqty;
        }

        public List<RGA_H> SearchPartWList(string Col, string Condition, string conditionValue)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGA_H> objRGA_H = new List<RGA_H>();

            string sql = @"select h.*,o.OrderSName,c.CustomerName from RGA_H as h 
                left join OrderCategory as o on o.OrderType=h.RGAType
                left join Customer as c on c.CustomerId=h.CustomerId where 1=1";

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

            DbParameter[] dbParameters = new DbParameter[len - 1];
            int dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                if (colArray[i] == "CustomerNo")
                    colArray[i] = "CustomerId";
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

            DataTable dtRGA_H = null;
            if (strCondition != "")
            {

                dtRGA_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRGA_H = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }
            foreach (DataRow dr in dtRGA_H.Rows)
            {
                RGA_H obj = new RGA_H();
                obj.StatusCode = dr["StatusCode"].ToString();
                obj.RGAType = dr["RGAType"].ToString();
                obj.OrderSName = dr["OrderSName"].ToString();
                obj.RGANo = dr["RGANo"].ToString();
                obj.CustomerName = dr["CustomerName"].ToString();
                obj.CustomerId = dr["CustomerId"].ToString();
                obj.AddressSName = dr["AddressSName"].ToString();
                obj.ProductNo = dr["ProductNo"].ToString();
                obj.ProductName = dr["ProductName"].ToString();
                obj.SerialNo = dr["SerialNo"].ToString();
                obj.AssetNo = dr["AssetNo"].ToString();
                obj.SaleDate = dr["SaleDate"].ToString();
                obj.PurchaseDate = dr["PurchaseDate"].ToString();
                obj.Remark = dr["Remark"].ToString();
                obj.TestResult = dr["TestResult"].ToString();
                obj.Confirmor = dr["Confirmor"].ToString();
                obj.ConfirmedDate = dr["ConfirmedDate"].ToString();
                obj.InternalRemark = dr["InternalRemark"].ToString();
                objRGA_H.Add(obj);
            }

            return objRGA_H;
        }

        public string SearchPartC(string Col, string Condition, string conditionValue)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sql = @"select count(*) as qty from RGA_H where StatusCode='55' and Returned='Y'";

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

            DbParameter[] dbParameters = new DbParameter[len - 1];
            int dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                if (colArray[i] == "CustomerNo")
                    colArray[i] = "CustomerId";
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

            DataTable dtRGA_H = null;
            if (strCondition != "")
            {

                dtRGA_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRGA_H = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }
            string sumqty = "";
            if (dtRGA_H.Rows.Count > 0)
            {
                sumqty = dtRGA_H.Rows[0]["qty"].ToString();
            }

            return sumqty;
        }

        public List<RGA_H> SearchPartCList(string Col, string Condition, string conditionValue)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGA_H> objRGA_H = new List<RGA_H>();

            string sql = @"select h.*,o.OrderSName,c.CustomerName from RGA_H as h 
                left join OrderCategory as o on o.OrderType=h.RGAType
                left join Customer as c on c.CustomerId=h.CustomerId where Returned='Y'";

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

            DbParameter[] dbParameters = new DbParameter[len - 1];
            int dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                if (colArray[i] == "CustomerNo")
                    colArray[i] = "CustomerId";
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

            DataTable dtRGA_H = null;
            if (strCondition != "")
            {

                dtRGA_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRGA_H = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }
            foreach (DataRow dr in dtRGA_H.Rows)
            {
                RGA_H obj = new RGA_H();
                obj.StatusCode = dr["StatusCode"].ToString();
                obj.RGAType = dr["RGAType"].ToString();
                obj.OrderSName = dr["OrderSName"].ToString();
                obj.RGANo = dr["RGANo"].ToString();
                obj.CustomerName = dr["CustomerName"].ToString();
                obj.CustomerId = dr["CustomerId"].ToString();
                obj.AddressSName = dr["AddressSName"].ToString();
                obj.ProductNo = dr["ProductNo"].ToString();
                obj.ProductName = dr["ProductName"].ToString();
                obj.SerialNo = dr["SerialNo"].ToString();
                obj.AssetNo = dr["AssetNo"].ToString();
                obj.SaleDate = dr["SaleDate"].ToString();
                obj.PurchaseDate = dr["PurchaseDate"].ToString();
                obj.Remark = dr["Remark"].ToString();
                obj.TestResult = dr["TestResult"].ToString();
                obj.Confirmor = dr["Confirmor"].ToString();
                obj.ConfirmedDate = dr["ConfirmedDate"].ToString();
                obj.InternalRemark = dr["InternalRemark"].ToString();
                objRGA_H.Add(obj);
            }

            return objRGA_H;
        }

        public string Search_ContractSum(string Col, string Condition, string conditionValue)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            //銷售
            string sql = @"select count(*) as qty from Contract_ProductD where 1=1";

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

            DbParameter[] dbParameters = new DbParameter[len - 1];
            int dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                if (colArray[i] == "CustomerNo")
                    colArray[i] = "TargetId";
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

            DataTable dtRGA_H = null;
            if (strCondition != "")
            {

                dtRGA_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRGA_H = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }
            string sumqty = "";
            if (dtRGA_H.Rows.Count > 0)
            {
                sumqty = dtRGA_H.Rows[0]["qty"].ToString();
            }

            return sumqty;
        }

        public List<RGA_H> SearchPartLifetimeAerry()
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGA_H> objRGA_H = new List<RGA_H>();

            string sql = @"select * from(
                    select ProductNo,ProductName,
                    count(*) as RGANum,(select count(*) 
                    from PartLifetimeRecord where 
                    PartLifetimeRecord.ProductNo=RGA_H.ProductNo) as PartNume
                    from RGA_H where StatusCode='55' group by ProductNo,ProductName) as rga
                    order by RGANum desc limit 0,10";

            DataTable dtRGA_H = dbMySQL.ExecuteDataTable(sql);

            if (dtRGA_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGA_H.Rows)
                {
                    RGA_H obj = new RGA_H();
                    obj.ProductNo = dr["ProductNo"].ToString();     //品號
                    obj.ProductName = dr["ProductName"].ToString();     //品名
                    obj.RGANum = dr["RGANum"].ToString();         //商品數量
                    obj.PartNume = dr["PartNume"].ToString();         //維修數量
                    objRGA_H.Add(obj);
                }
            }

            return objRGA_H;
        }


        public List<RGA_H> SearchRGAList_H()
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGA_H> objRGA_H = new List<RGA_H>();

            string sql = @"select p.ProductNo,h.ProductName,count(*) as QTY,
floor(sum(p.SLifetime)/count(*)/30) as Sale,
floor(sum(p.Plifetime)/count(*)/30) as Purchase 
from ProductLifetimeRecord p,RGA_H h 
where p.RGAOrderType=h.RGAType and p.ProductNo=h.ProductNo 
and p.RGAOrderNo=h.RGANo and h.Closed='Y'
group by p.ProductNo,h.ProductName";

            DataTable dtRGA_H = dbMySQL.ExecuteDataTable(sql);

            if (dtRGA_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGA_H.Rows)
                {
                    RGA_H obj = new RGA_H();
                    obj.ProductNo = dr["ProductNo"].ToString();     //品號
                    obj.ProductName = dr["ProductName"].ToString();     //品名
                    obj.PartNume = dr["QTY"].ToString();         //商品數量
                    obj.SaleDate = dr["Sale"].ToString();
                    obj.PurchaseDate = dr["Purchase"].ToString();
                    objRGA_H.Add(obj);
                }
            }

            return objRGA_H;
        }

        public List<RGA_H> SearchRGAList_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RGA_H> objRGA_H = new List<RGA_H>();

            string sql = @"select p.ProductNo,h.ProductName,count(*) as QTY,
floor(sum(p.SLifetime)/count(*)/30) as Sale,
floor(sum(p.Plifetime)/count(*)/30) as Purchase 
from ProductLifetimeRecord p,RGA_H h 
where p.RGAOrderType=h.RGAType and p.ProductNo=h.ProductNo 
and p.RGAOrderNo=h.RGANo ";
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

            string str = " group by p.ProductNo,h.ProductName";
            DataTable dtRGA_H = null;
            if (strCondition != "")
            {
                dtRGA_H = dbMySQL.ExecuteDataTable(sql + strCondition + str, dbParameters);
            }
            else
            {
                dtRGA_H = dbMySQL.ExecuteDataTable(sql + str);
            }

            if (dtRGA_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGA_H.Rows)
                {
                    RGA_H obj = new RGA_H();
                    obj.ProductNo = dr["ProductNo"].ToString();     //品號
                    obj.ProductName = dr["ProductName"].ToString();     //品名
                    obj.PartNume = dr["QTY"].ToString();         //商品數量
                    obj.SaleDate = dr["Sale"].ToString();
                    obj.PurchaseDate = dr["Purchase"].ToString();
                    objRGA_H.Add(obj);
                }
            }

            return objRGA_H;
        }

        public List<ProductLifetimeRecord> SearchRGA(string ProductNo, string closed, string CustomerId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<ProductLifetimeRecord> objRGA_H = new List<ProductLifetimeRecord>();


            string sql = @"select r.Closed,p.RGAOrderType,o.OrderSName,p.RGAOrderNo,p.FirstRepairDate,r.CustomerId,c.CustomerName,p.ProductNo,r.ProductName,p.SerialNo,r.SaleDate,floor(sum(p.SLifetime)/count(*)/30) as Sale,r.PurchaseDate,floor(sum(p.Plifetime)/count(*)/30) as Purchase from RGA_H r,Customer c,ProductLifetimeRecord p,OrderCategory o where r.CustomerId=c.CustomerId and p.ProductNo=r.ProductNo and p.RGAOrderNo=r.RGANo and p.RGAOrderType=r.RGAType and o.OrderType=p.RGAOrderType and p.ProductNo=" + ProductNo;

            string str = "";
            if (closed != "")
            {
                str += " and r.Closed='" + closed + "'";
            }
            if (CustomerId!=null&& CustomerId != "")
            {
                str += " and r.CustomerId=" + CustomerId;
            }
            DataTable dtRGA_H = dbMySQL.ExecuteDataTable(sql + str);

            if (dtRGA_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGA_H.Rows)
                {
                    ProductLifetimeRecord obj = new ProductLifetimeRecord();
                    if (dr["Closed"].ToString() == "Y")           //狀態碼
                    {
                        obj.closed = "已結案";
                    }
                    if (dr["Closed"].ToString() == "N")
                    {
                        obj.closed = "未結案";
                    }
                    obj.RGAType = dr["RGAOrderType"].ToString();
                    obj.RGAName = dr["OrderSName"].ToString();
                    obj.RGANo = dr["RGAOrderNo"].ToString();
                    obj.OrderDate = dr["FirstRepairDate"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.Confirmor = dr["CustomerName"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.SaleDate = dr["SaleDate"].ToString();
                    obj.SaleMonth = dr["Sale"].ToString();
                    obj.PurchaseDate = dr["PurchaseDate"].ToString();
                    obj.PurchaseMonth = dr["Purchase"].ToString();
                    objRGA_H.Add(obj);
                }
            }

            return objRGA_H;
        }

        public List<RGA_H> AvgRGA_H(string Col, string Condition, string conditionValue)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            List<RGA_H> objRGA_H = new List<RGA_H>();
            try
            {
                string sql = @"select ProductNo,ProductName,(select count(*) 
                    from RepairRecord as d where d.ProductNo=h.ProductNo) as sumQty from RGA_H as h where 1=1";

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
                //if (Page != 0)
                //{
                //    strCondition += " limit @Page1,@Page2";
                //}
                //dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
                //dbParameters[dblen] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);

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
                        RGA_H obj = new RGA_H();
                        obj.ProductNo = dr["ProductNo"].ToString();     //維修單別
                        obj.ProductName = dr["ProductName"].ToString();     //單據名稱
                        obj.sumQty = dr["sumQty"].ToString();         //維修單號
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

        public List<RGA_H> RGA_H(string Col, string Condition, string conditionValue)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            List<RGA_H> objRGA_H = new List<RGA_H>();
            try
            {
                string sql = @"select h.*,o.OrderSName,c.CustomerName from RGA_H as h left join 
                   OrderCategory as o on o.OrderType=h.RGAType 
                   left join Customer as c on c.CustomerId=h.CustomerId where 1=1";

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
                        RGA_H obj = new RGA_H();
                        if(dr["StatusCode"].ToString()=="00")
                        {
                            obj.StatusCode = "00.未處理";
                        }
                        if (dr["StatusCode"].ToString() == "05")
                        {
                            obj.StatusCode = "05.已派工";
                        }
                        if (dr["StatusCode"].ToString() == "10")
                        {
                            obj.StatusCode = "10.送廠申請";
                        }
                        if (dr["StatusCode"].ToString() == "20")
                        {
                            obj.StatusCode = "20.已檢測";
                        }
                        if (dr["StatusCode"].ToString() == "25")
                        {
                            obj.StatusCode = "25.送廠維修";
                        }
                        if (dr["StatusCode"].ToString() == "30")
                        {
                            obj.StatusCode = "30.原廠報價";
                        }
                        if (dr["StatusCode"].ToString() == "35")
                        {
                            obj.StatusCode = "35.已報價";
                        }
                        if (dr["StatusCode"].ToString() == "40")
                        {
                            obj.StatusCode = "40.已回覆報價";
                        }
                        if (dr["StatusCode"].ToString() == "45")
                        {
                            obj.StatusCode = "45.送廠歸還";
                        }
                        if (dr["StatusCode"].ToString() == "50")
                        {
                            obj.StatusCode = "50.完工";
                        }
                        if (dr["StatusCode"].ToString() == "55")
                        {
                            obj.StatusCode = "55.結案";
                        }
                        if (dr["StatusCode"].ToString() == "D")
                        {
                            obj.StatusCode = "D.作廢";
                        }
                        obj.RGAType = dr["RGAType"].ToString();
                        obj.OrderSName = dr["OrderSName"].ToString();
                        obj.RGANo = dr["RGANo"].ToString();
                        obj.CustomerId = dr["CustomerId"].ToString();
                        obj.CustomerName = dr["CustomerName"].ToString();
                        obj.ProductName = dr["ProductName"].ToString();
                        obj.ProductNo = dr["ProductNo"].ToString();
                        obj.SerialNo = dr["SerialNo"].ToString();
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
    }
}