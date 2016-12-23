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
    public class RGAChange_HDAL : IRGAChange_HDAL
    {
        public bool AddRGAChange_H(RGAChange_H rGAChange_H, string strQQuestionNo, string strQQuestion,
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
                DataTable dtRGA_H = dbMySQL.ExecuteDataTable(@"select * from RGA_H where 
                    RGAType=@RGAType and RGANo=@RGANo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo)
                    });
                if (dtRGA_H != null && dtRGA_H.Rows.Count > 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into RGAChange_H (Company,UserGroup,Creator,CreateDate,
                        RGAType,RGANo,Version,OrderDate,NoOfPrints,SourceOrderType,
                        SourceOrderNo,RepairAll,Repairman,ReasonOfChange,NewCustomerId,NewAddressSName,NewAddress,NewContact,
                        NewTelNo,NewFaxNo,NewProductNo,NewProductName,NewSerialNo,NewAssetNo,
                        NewWarrantyStartDate,NewWarrantyExpiryDate,NewUnderWarranty,NewSaleDate,NewPurchaseDate,
                        NewContractType,NewContractNo,NewPCompletionDate,Confirmed,NewOptionDetail,NewRemark,NewRepairDetail,
                        NewReturned,NewMalfunctionReason,NewTestResult,NewInternalRemark,OldCustomerId,OldAddressSName,
                        OldAddress,OldContact,OldTelNo,OldFaxNo,OldProductNo,OldProductName,OldSerialNo,OldAssetNo,OldWarrantyStartDate,
                        OldWarrantyExpiryDate,OldUnderWarranty,OldSaleDate,OldPurchaseDate,OldContractType,OldContractNo,OldPCompletionDate,
                        OldOptionDetail,OldRemark,OldReturned,OldMalfunctionReason,OldTestResult,OldInternalRemark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @RGAType,@RGANo,@Version,@OrderDate,@NoOfPrints,@SourceOrderType,
                        @SourceOrderNo,@RepairAll,@Repairman,@ReasonOfChange,@NewCustomerId,@NewAddressSName,@NewAddress,@NewContact,
                        @NewTelNo,@NewFaxNo,@NewProductNo,@NewProductName,@NewSerialNo,@NewAssetNo,
                        @NewWarrantyStartDate,@NewWarrantyExpiryDate,@NewUnderWarranty,@NewSaleDate,@NewPurchaseDate,
                        @NewContractType,@NewContractNo,@NewPCompletionDate,@Confirmed,@NewOptionDetail,@NewRemark,@NewRepairDetail,
                        @NewReturned,@NewMalfunctionReason,@NewTestResult,@NewInternalRemark,@OldCustomerId,@OldAddressSName,
                        @OldAddress,@OldContact,@OldTelNo,@OldFaxNo,@OldProductNo,@OldProductName,@OldSerialNo,@OldAssetNo,@OldWarrantyStartDate,
                        @OldWarrantyExpiryDate,@OldUnderWarranty,@OldSaleDate,@OldPurchaseDate,@OldContractType,@OldContractNo,@OldPCompletionDate,
                        @OldOptionDetail,@OldRemark,@OldReturned,@OldMalfunctionReason,@OldTestResult,@OldInternalRemark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRGA_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rGAChange_H.OrderDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rGAChange_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,rGAChange_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,rGAChange_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("RepairAll",DbType.String,rGAChange_H.RepairAll),
                            DataAccessMySQL.CreateParameter("Repairman",DbType.String,rGAChange_H.Repairman),
                            DataAccessMySQL.CreateParameter("ReasonOfChange",DbType.String,rGAChange_H.ReasonOfChange),
                            DataAccessMySQL.CreateParameter("NewCustomerId",DbType.String,rGAChange_H.NewCustomerId),
                            DataAccessMySQL.CreateParameter("NewAddressSName",DbType.String,rGAChange_H.NewAddressSName),
                            DataAccessMySQL.CreateParameter("NewAddress",DbType.String,rGAChange_H.NewAddress),
                            DataAccessMySQL.CreateParameter("NewContact",DbType.String,rGAChange_H.NewContact),
                            DataAccessMySQL.CreateParameter("NewTelNo",DbType.String,rGAChange_H.NewTelNo),
                            DataAccessMySQL.CreateParameter("NewFaxNo",DbType.String,rGAChange_H.NewFaxNo),
                            DataAccessMySQL.CreateParameter("NewProductNo",DbType.String,rGAChange_H.NewProductNo),
                            DataAccessMySQL.CreateParameter("NewProductName",DbType.String,rGAChange_H.NewProductName),
                            DataAccessMySQL.CreateParameter("NewSerialNo",DbType.String,rGAChange_H.NewSerialNo),
                            DataAccessMySQL.CreateParameter("NewAssetNo",DbType.String,rGAChange_H.NewAssetNo),
                            DataAccessMySQL.CreateParameter("NewWarrantyStartDate",DbType.String,rGAChange_H.NewWarrantyStartDate),
                            DataAccessMySQL.CreateParameter("NewWarrantyExpiryDate",DbType.String,rGAChange_H.NewWarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("NewUnderWarranty",DbType.String,rGAChange_H.NewUnderWarranty),
                            DataAccessMySQL.CreateParameter("NewSaleDate",DbType.String,rGAChange_H.NewSaleDate),
                            DataAccessMySQL.CreateParameter("NewPurchaseDate",DbType.String,rGAChange_H.NewPurchaseDate),
                            DataAccessMySQL.CreateParameter("NewContractType",DbType.String,rGAChange_H.NewContractType),
                            DataAccessMySQL.CreateParameter("NewContractNo",DbType.String,rGAChange_H.NewContractNo),
                            DataAccessMySQL.CreateParameter("NewPCompletionDate",DbType.String,rGAChange_H.NewPCompletionDate),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rGAChange_H.Confirmed),
                            DataAccessMySQL.CreateParameter("NewOptionDetail",DbType.String,rGAChange_H.NewOptionDetail),
                            DataAccessMySQL.CreateParameter("NewRemark",DbType.String,rGAChange_H.NewRemark),
                            DataAccessMySQL.CreateParameter("NewRepairDetail",DbType.String,rGAChange_H.NewRepairDetail),
                            DataAccessMySQL.CreateParameter("NewReturned",DbType.String,rGAChange_H.NewReturned),
                            DataAccessMySQL.CreateParameter("NewMalfunctionReason",DbType.String,rGAChange_H.NewMalfunctionReason),
                            DataAccessMySQL.CreateParameter("NewTestResult",DbType.String,rGAChange_H.NewTestResult),
                            DataAccessMySQL.CreateParameter("NewInternalRemark",DbType.String,rGAChange_H.NewInternalRemark),
                            DataAccessMySQL.CreateParameter("OldCustomerId",DbType.String,dtRGA_H.Rows[0]["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAddressSName",DbType.String,dtRGA_H.Rows[0]["AddressSName"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAddress",DbType.String,dtRGA_H.Rows[0]["Address"].ToString()),
                            DataAccessMySQL.CreateParameter("OldContact",DbType.String,dtRGA_H.Rows[0]["Contact"].ToString()),
                            DataAccessMySQL.CreateParameter("OldTelNo",DbType.String,dtRGA_H.Rows[0]["TelNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldFaxNo",DbType.String,dtRGA_H.Rows[0]["FaxNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldProductNo",DbType.String,dtRGA_H.Rows[0]["ProductNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldProductName",DbType.String,dtRGA_H.Rows[0]["ProductName"].ToString()),
                            DataAccessMySQL.CreateParameter("OldSerialNo",DbType.String,dtRGA_H.Rows[0]["SerialNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAssetNo",DbType.String,dtRGA_H.Rows[0]["AssetNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldWarrantyStartDate",DbType.String,dtRGA_H.Rows[0]["WarrantyStartDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldWarrantyExpiryDate",DbType.String,dtRGA_H.Rows[0]["WarrantyExpiryDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldUnderWarranty",DbType.String,dtRGA_H.Rows[0]["UnderWarranty"].ToString()),
                            DataAccessMySQL.CreateParameter("OldSaleDate",DbType.String,dtRGA_H.Rows[0]["SaleDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldPurchaseDate",DbType.String,dtRGA_H.Rows[0]["PurchaseDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldContractType",DbType.String,dtRGA_H.Rows[0]["ContractType"].ToString()),
                            DataAccessMySQL.CreateParameter("OldContractNo",DbType.String,dtRGA_H.Rows[0]["ContractNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldPCompletionDate",DbType.String,dtRGA_H.Rows[0]["PCompletionDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldOptionDetail",DbType.String,dtRGA_H.Rows[0]["OptionDetail"].ToString()),
                            DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtRGA_H.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("OldReturned",DbType.String,dtRGA_H.Rows[0]["Returned"].ToString()),
                            DataAccessMySQL.CreateParameter("OldMalfunctionReason",DbType.String,dtRGA_H.Rows[0]["MalfunctionReason"].ToString()),
                            DataAccessMySQL.CreateParameter("OldTestResult",DbType.String,dtRGA_H.Rows[0]["TestResult"].ToString()),
                            DataAccessMySQL.CreateParameter("OldInternalRemark",DbType.String,dtRGA_H.Rows[0]["InternalRemark"].ToString()),
                        });

                    dbMySQL.ExecuteNonQuery(@"update RGA_H set Version=@Version,CustomerId=@CustomerId,AddressSName=@AddressSName,
                        Address=@Address,Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,ProductNo=@ProductNo,ProductName=@ProductName,
                        SerialNo=@SerialNo,AssetNo=@AssetNo,WarrantyStartDate=@WarrantyStartDate,WarrantyExpiryDate=@WarrantyExpiryDate,
                        UnderWarranty=@UnderWarranty,SaleDate=@SaleDate,PurchaseDate=@PurchaseDate,ContractType=@ContractType,
                        ContractNo=@ContractNo,PCompletionDate=@PCompletionDate,OptionDetail=@OptionDetail,Remark=@Remark,
                        Returned=@Returned,MalfunctionReason=@MalfunctionReason,TestResult=@TestResult,InternalRemark=@InternalRemark where 
                        RGAType=@RGAType and RGANo=@RGANo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRGA_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,rGAChange_H.NewCustomerId),
                        DataAccessMySQL.CreateParameter("AddressSName",DbType.String,rGAChange_H.NewAddressSName),
                        DataAccessMySQL.CreateParameter("Address",DbType.String,rGAChange_H.NewAddress),
                        DataAccessMySQL.CreateParameter("Contact",DbType.String,rGAChange_H.NewContact),
                        DataAccessMySQL.CreateParameter("TelNo",DbType.String,rGAChange_H.NewTelNo),
                        DataAccessMySQL.CreateParameter("FaxNo",DbType.String,rGAChange_H.NewFaxNo),
                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,rGAChange_H.NewProductNo),
                        DataAccessMySQL.CreateParameter("ProductName",DbType.String,rGAChange_H.NewProductName),
                        DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rGAChange_H.NewSerialNo),
                        DataAccessMySQL.CreateParameter("AssetNo",DbType.String,rGAChange_H.NewAssetNo),
                        DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,rGAChange_H.NewWarrantyStartDate),
                        DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,rGAChange_H.NewWarrantyExpiryDate),
                        DataAccessMySQL.CreateParameter("UnderWarranty",DbType.String,rGAChange_H.NewUnderWarranty),
                        DataAccessMySQL.CreateParameter("SaleDate",DbType.String,rGAChange_H.NewSaleDate),
                        DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,rGAChange_H.NewPurchaseDate),
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,rGAChange_H.NewContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,rGAChange_H.NewContractNo),
                        DataAccessMySQL.CreateParameter("PCompletionDate",DbType.String,rGAChange_H.NewPCompletionDate),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rGAChange_H.Confirmed),
                        DataAccessMySQL.CreateParameter("OptionDetail",DbType.String,rGAChange_H.NewOptionDetail),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,rGAChange_H.NewRemark),
                        DataAccessMySQL.CreateParameter("RepairDetail",DbType.String,rGAChange_H.NewRepairDetail),
                        DataAccessMySQL.CreateParameter("Returned",DbType.String,rGAChange_H.NewReturned),
                        DataAccessMySQL.CreateParameter("MalfunctionReason",DbType.String,rGAChange_H.NewMalfunctionReason),
                        DataAccessMySQL.CreateParameter("TestResult",DbType.String,rGAChange_H.NewTestResult),
                        DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,rGAChange_H.NewInternalRemark)
                    });

                    string sql = "";

                    if (strQQuestionNo != "" && strQQuestionNo != null)
                    {
                        //string[] strQItemIdArray = strQItemId.Split(',');
                        string[] strQQuestionNoArray = strQQuestionNo.Split(',');
                        string[] strQQuestionArray = strQQuestion.Split(',');
                        string[] strQDescriptionArray = strQDescription.Split(',');

                        sql = @"insert into RGAChange_Q (Company,UserGroup,Creator,
                        CreateDate,RGAType,RGANo,Version,ItemId,NewQuestionNo,NewQuestion,
                        NewDescription,OldQuestionNo,OldQuestion,OldDescription) 
                        values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RGAType,@RGANo,@Version,@ItemId,@NewQuestionNo,@NewQuestion,
                        @NewDescription,@OldQuestionNo,@OldQuestion,@OldDescription)";

                        DataTable dtRGA_Q = null;

                        for (int i = 0; i < strQQuestionNoArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strQQuestionNoArray[i]))
                            {
                                continue;
                            }
                            
                            dtRGA_Q = dbMySQL.ExecuteDataTable(@"select * from RGA_Q where 
                            RGAType=@RGAType and RGANo=@RGANo and ItemId=@ItemId", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000"))
                            });

                            if (dtRGA_Q!=null&&dtRGA_Q.Rows.Count>0)
                            {
                                dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRGA_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString()),
                                    DataAccessMySQL.CreateParameter("NewQuestionNo",DbType.String,strQQuestionNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewQuestion",DbType.String,strQQuestionArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewDescription",DbType.String,strQDescriptionArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldQuestionNo",DbType.String,dtRGA_Q.Rows[0]["QuestionNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldQuestion",DbType.String,dtRGA_Q.Rows[0]["Question"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldDescription",DbType.String,dtRGA_Q.Rows[0]["Description"].ToString())
                                });

                                dbMySQL.ExecuteNonQuery(@"update RGA_Q set QuestionNo=@QuestionNo,Question=@Question,Description=@Description
                                    where RGAType=@RGAType and RGANo=@RGANo and ItemId=@ItemId", tran,
                                new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,strQQuestionNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Question",DbType.String,strQQuestionArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Description",DbType.String,strQDescriptionArray[i].ToString())
                                });
                            }
                            else
                            {
                                dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRGA_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString()),
                                    DataAccessMySQL.CreateParameter("NewQuestionNo",DbType.String,strQQuestionNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewQuestion",DbType.String,strQQuestionArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewDescription",DbType.String,strQDescriptionArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldQuestionNo",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldQuestion",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldDescription",DbType.String,"")
                                });

                                dbMySQL.ExecuteNonQuery(@"insert into RGA_Q (Company,UserGroup,Creator,
                                    CreateDate,RGAType,RGANo,ItemId,QuestionNo,Question,Description) values (@Company,@UserGroup,@Creator,
                                    @CreateDate,@RGAType,@RGANo,@ItemId,@QuestionNo,@Question,@Description)", tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,strQQuestionNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Question",DbType.String,strQQuestionArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Description",DbType.String,strQDescriptionArray[i].ToString())
                                });
                            }
                            
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

                        sql = @"insert into RGAChange_D (Company,UserGroup,Creator,
                        CreateDate,RGAType,RGANo,Version,ItemId,NewPartNo,NewPartName,
                        NewQTY,NewUnit,NewRemark,NewRepaired,NewSourceOrderType,
                        NewSourceOrderNo,NewSourceItemId,OldPartNo,OldPartName,OldQTY,OldUnit,OldRemark,OldRepaired,
                        OldSourceOrderType,OldSourceOrderNo,OldSourceItemId,OldResponseOrderType,
                        OldResponseOrderNo,OldResponseDate) 
                        values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RGAType,@RGANo,@Version,@ItemId,@NewPartNo,@NewPartName,
                        @NewQTY,@NewUnit,@NewRemark,@NewRepaired,@NewSourceOrderType,
                        @NewSourceOrderNo,@NewSourceItemId,@OldPartNo,@OldPartName,@OldQTY,@OldUnit,@OldRemark,@OldRepaired,
                        @OldSourceOrderType,@OldSourceOrderNo,@OldSourceItemId,@OldResponseOrderType,
                        @OldResponseOrderNo,@OldResponseDate)";

                        DataTable dtRGA_D = null;

                        for (int i = 0; i < strDItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strDItemIdArray[i]))
                            {
                                continue;
                            }

                            dtRGA_D = dbMySQL.ExecuteDataTable(@"select * from RGA_D where 
                            RGAType=@RGAType and RGANo=@RGANo and ItemId=@ItemId", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000"))
                            });

                            if (dtRGA_D != null && dtRGA_D.Rows.Count > 0)
                            {
                                dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRGA_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strDItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewPartNo",DbType.String,strDPartNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewPartName",DbType.String,strDPartNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewQTY",DbType.String,strDQTYArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewUnit",DbType.String,strDUnitArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewRemark",DbType.String,strDRemarkArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewRepaired",DbType.String,strDRepairedArray[i].ToString()=="on"?"Y":"N"),
                                    DataAccessMySQL.CreateParameter("NewSourceOrderType",DbType.String,strDSourceOrderTypeArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewSourceOrderNo",DbType.String,strDSourceOrderNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewSourceItemId",DbType.String,strDSourceItemIdArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("NewResponseOrderType",DbType.String,strDResponseOrderArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("NewResponseOrderNo",DbType.String,strDResponseOrderNoArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("NewResponseDate",DbType.String,strDResponseDateArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldPartNo",DbType.String,dtRGA_D.Rows[0]["PartNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldPartName",DbType.String,dtRGA_D.Rows[0]["PartName"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldQTY",DbType.String,dtRGA_D.Rows[0]["QTY"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldUnit",DbType.String,dtRGA_D.Rows[0]["Unit"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtRGA_D.Rows[0]["Remark"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldRepaired",DbType.String,dtRGA_D.Rows[0]["Repaired"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldSourceOrderType",DbType.String,dtRGA_D.Rows[0]["SourceOrderType"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldSourceOrderNo",DbType.String,dtRGA_D.Rows[0]["SourceOrderNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldSourceItemId",DbType.String,dtRGA_D.Rows[0]["SourceItemId"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldResponseOrderType",DbType.String,dtRGA_D.Rows[0]["ResponseOrderType"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldResponseOrderNo",DbType.String,dtRGA_D.Rows[0]["ResponseOrderNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldResponseDate",DbType.String,dtRGA_D.Rows[0]["ResponseDate"].ToString())
                                });

                                dbMySQL.ExecuteNonQuery(@"update RGA_D set PartNo=@PartNo,PartName=@PartName,QTY=@QTY,
                                    Unit=@Unit,Remark=@Remark,Repaired=@Repaired,SourceOrderType=@SourceOrderType,
                                    SourceOrderNo=@SourceOrderNo,SourceItemId=@SourceItemId
                                    where RGAType=@RGAType and RGANo=@RGANo and ItemId=@ItemId", tran,
                                new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strDItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("PartNo",DbType.String,strDPartNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("PartName",DbType.String,strDPartNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("QTY",DbType.Double,double.Parse(strDQTYArray[i].ToString()==""?"0":strDQTYArray[i].ToString())),
                                    DataAccessMySQL.CreateParameter("Unit",DbType.String,strDUnitArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Remark",DbType.String,strDRemarkArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Repaired",DbType.String,strDRepairedArray[i].ToString()=="on"?"Y":"N"),
                                    DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,strDSourceOrderTypeArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,strDSourceOrderNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("SourceItemId",DbType.String,strDSourceItemIdArray[i].ToString()),
                                });
                            }
                            else
                            {
                                dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRGA_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strDItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewPartNo",DbType.String,strDPartNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewPartName",DbType.String,strDPartNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewQTY",DbType.String,strDQTYArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewUnit",DbType.String,strDUnitArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewRemark",DbType.String,strDRemarkArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewRepaired",DbType.String,strDRepairedArray[i].ToString()=="on"?"Y":"N"),
                                    DataAccessMySQL.CreateParameter("NewSourceOrderType",DbType.String,strDSourceOrderTypeArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewSourceOrderNo",DbType.String,strDSourceOrderNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewSourceItemId",DbType.String,strDSourceItemIdArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("NewResponseOrderType",DbType.String,strDResponseOrderArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("NewResponseOrderNo",DbType.String,strDResponseOrderNoArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("NewResponseDate",DbType.String,strDResponseDateArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldPartNo",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldPartName",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldQTY",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldUnit",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldRemark",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldRepaired",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldSourceOrderType",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldSourceOrderNo",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldSourceItemId",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldResponseOrderType",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldResponseOrderNo",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldResponseDate",DbType.String,"")
                                });

                                dbMySQL.ExecuteNonQuery(@"insert into RGA_D (Company,UserGroup,Creator,
                                    CreateDate,RGAType,RGANo,ItemId,PartNo,PartName,QTY,Unit,Remark,
                                    Repaired,SourceOrderType,SourceOrderNo,SourceItemId) values (@Company,@UserGroup,@Creator,
                                    @CreateDate,@RGAType,@RGANo,@ItemId,@PartNo,@PartName,@QTY,@Unit,@Remark,
                                    @Repaired,@SourceOrderType,@SourceOrderNo,@SourceItemId)", tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strDItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("PartNo",DbType.String,strDPartNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("PartName",DbType.String,strDPartNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("QTY",DbType.Double,double.Parse(strDQTYArray[i].ToString()==""?"0":strDQTYArray[i].ToString())),
                                    DataAccessMySQL.CreateParameter("Unit",DbType.String,strDUnitArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Remark",DbType.String,strDRemarkArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Repaired",DbType.String,strDRepairedArray[i].ToString()=="on"?"Y":"N"),
                                    DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,strDSourceOrderTypeArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,strDSourceOrderNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("SourceItemId",DbType.String,strDSourceItemIdArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("ResponseOrderType",DbType.String,strDResponseOrderTypeArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("ResponseOrderNo",DbType.String,sstrDResponseOrderNoArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("ResponseDate",DbType.String,strDResponseDateArray[i].ToString())
                                });
                            }

                        }
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

        public bool UpdateRGAChange_H(RGAChange_H rGAChange_H, string strQQuestionNo, string strQQuestion,
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
                DataTable dtRGA_H = dbMySQL.ExecuteDataTable(@"select * from RGA_H where 
                    RGAType=@RGAType and RGANo=@RGANo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo)
                    });
                if (dtRGA_H != null && dtRGA_H.Rows.Count > 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into RGAChange_H (Company,UserGroup,Creator,CreateDate,
                        RGAType,RGANo,Version,OrderDate,NoOfPrints,SourceOrderType,
                        SourceOrderNo,RepairAll,Repairman,ReasonOfChange,NewCustomerId,NewAddressSName,NewAddress,NewContact,
                        NewTelNo,NewFaxNo,NewProductNo,NewProductName,NewSerialNo,NewAssetNo,
                        NewWarrantyStartDate,NewWarrantyExpiryDate,NewUnderWarranty,NewSaleDate,NewPurchaseDate,
                        NewContractType,NewContractNo,NewPCompletionDate,Confirmed,NewOptionDetail,NewRemark,NewRepairDetail,
                        NewReturned,NewMalfunctionReason,NewTestResult,NewInternalRemark,OldCustomerId,OldAddressSName,
                        OldAddress,OldContact,OldTelNo,OldFaxNo,OldProductNo,OldProductName,OldSerialNo,OldAssetNo,OldWarrantyStartDate,
                        OldWarrantyExpiryDate,OldUnderWarranty,OldSaleDate,OldPurchaseDate,OldContractType,OldContractNo,OldPCompletionDate,
                        OldOptionDetail,OldRemark,OldReturned,OldMalfunctionReason,OldTestResult,OldInternalRemark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @RGAType,@RGANo,@Version,@OrderDate,@NoOfPrints,@SourceOrderType,
                        @SourceOrderNo,@RepairAll,@Repairman,@ReasonOfChange,@NewCustomerId,@NewAddressSName,@NewAddress,@NewContact,
                        @NewTelNo,@NewFaxNo,@NewProductNo,@NewProductName,@NewSerialNo,@NewAssetNo,
                        @NewWarrantyStartDate,@NewWarrantyExpiryDate,@NewUnderWarranty,@NewSaleDate,@NewPurchaseDate,
                        @NewContractType,@NewContractNo,@NewPCompletionDate,@Confirmed,@NewOptionDetail,@NewRemark,@NewRepairDetail,
                        @NewReturned,@NewMalfunctionReason,@NewTestResult,@NewInternalRemark,@OldCustomerId,@OldAddressSName,
                        @OldAddress,@OldContact,@OldTelNo,@OldFaxNo,@OldProductNo,@OldProductName,@OldSerialNo,@OldAssetNo,@OldWarrantyStartDate,
                        @OldWarrantyExpiryDate,@OldUnderWarranty,@OldSaleDate,@OldPurchaseDate,@OldContractType,@OldContractNo,@OldPCompletionDate,
                        @OldOptionDetail,@OldRemark,@OldReturned,@OldMalfunctionReason,@OldTestResult,@OldInternalRemark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRGA_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rGAChange_H.OrderDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rGAChange_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,rGAChange_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,rGAChange_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("RepairAll",DbType.String,rGAChange_H.RepairAll),
                            DataAccessMySQL.CreateParameter("Repairman",DbType.String,rGAChange_H.Repairman),
                            DataAccessMySQL.CreateParameter("ReasonOfChange",DbType.String,rGAChange_H.ReasonOfChange),
                            DataAccessMySQL.CreateParameter("NewCustomerId",DbType.String,rGAChange_H.NewCustomerId),
                            DataAccessMySQL.CreateParameter("NewAddressSName",DbType.String,rGAChange_H.NewAddressSName),
                            DataAccessMySQL.CreateParameter("NewAddress",DbType.String,rGAChange_H.NewAddress),
                            DataAccessMySQL.CreateParameter("NewContact",DbType.String,rGAChange_H.NewContact),
                            DataAccessMySQL.CreateParameter("NewTelNo",DbType.String,rGAChange_H.NewTelNo),
                            DataAccessMySQL.CreateParameter("NewFaxNo",DbType.Int32,rGAChange_H.NewFaxNo),
                            DataAccessMySQL.CreateParameter("NewProductNo",DbType.Int32,rGAChange_H.NewProductNo),
                            DataAccessMySQL.CreateParameter("NewProductName",DbType.String,rGAChange_H.NewProductName),
                            DataAccessMySQL.CreateParameter("NewSerialNo",DbType.String,rGAChange_H.NewSerialNo),
                            DataAccessMySQL.CreateParameter("NewAssetNo",DbType.String,rGAChange_H.NewAssetNo),
                            DataAccessMySQL.CreateParameter("NewWarrantyStartDate",DbType.String,rGAChange_H.NewWarrantyStartDate),
                            DataAccessMySQL.CreateParameter("NewWarrantyExpiryDate",DbType.String,rGAChange_H.NewWarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("NewUnderWarranty",DbType.String,rGAChange_H.NewUnderWarranty),
                            DataAccessMySQL.CreateParameter("NewSaleDate",DbType.String,rGAChange_H.NewSaleDate),
                            DataAccessMySQL.CreateParameter("NewPurchaseDate",DbType.String,rGAChange_H.NewPurchaseDate),
                            DataAccessMySQL.CreateParameter("NewContractType",DbType.String,rGAChange_H.NewContractType),
                            DataAccessMySQL.CreateParameter("NewContractNo",DbType.String,rGAChange_H.NewContractNo),
                            DataAccessMySQL.CreateParameter("NewPCompletionDate",DbType.String,rGAChange_H.NewPCompletionDate),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rGAChange_H.Confirmed),
                            DataAccessMySQL.CreateParameter("NewOptionDetail",DbType.String,rGAChange_H.NewOptionDetail),
                            DataAccessMySQL.CreateParameter("NewRemark",DbType.String,rGAChange_H.NewRemark),
                            DataAccessMySQL.CreateParameter("NewRepairDetail",DbType.String,rGAChange_H.NewRepairDetail),
                            DataAccessMySQL.CreateParameter("NewReturned",DbType.String,rGAChange_H.NewReturned),
                            DataAccessMySQL.CreateParameter("NewMalfunctionReason",DbType.String,rGAChange_H.NewMalfunctionReason),
                            DataAccessMySQL.CreateParameter("NewTestResult",DbType.String,rGAChange_H.NewTestResult),
                            DataAccessMySQL.CreateParameter("NewInternalRemark",DbType.String,rGAChange_H.NewInternalRemark),
                            DataAccessMySQL.CreateParameter("OldCustomerId",DbType.String,dtRGA_H.Rows[0]["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAddressSName",DbType.String,dtRGA_H.Rows[0]["AddressSName"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAddress",DbType.String,dtRGA_H.Rows[0]["Address"].ToString()),
                            DataAccessMySQL.CreateParameter("OldContact",DbType.String,dtRGA_H.Rows[0]["Contact"].ToString()),
                            DataAccessMySQL.CreateParameter("OldTelNo",DbType.String,dtRGA_H.Rows[0]["TelNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldFaxNo",DbType.String,dtRGA_H.Rows[0]["FaxNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldProductNo",DbType.String,dtRGA_H.Rows[0]["ProductNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldProductName",DbType.String,dtRGA_H.Rows[0]["ProductName"].ToString()),
                            DataAccessMySQL.CreateParameter("OldSerialNo",DbType.String,dtRGA_H.Rows[0]["SerialNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAssetNo",DbType.String,dtRGA_H.Rows[0]["AssetNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldWarrantyStartDate",DbType.String,dtRGA_H.Rows[0]["WarrantyStartDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldWarrantyExpiryDate",DbType.String,dtRGA_H.Rows[0]["WarrantyExpiryDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldUnderWarranty",DbType.String,dtRGA_H.Rows[0]["UnderWarranty"].ToString()),
                            DataAccessMySQL.CreateParameter("OldSaleDate",DbType.String,dtRGA_H.Rows[0]["SaleDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldPurchaseDate",DbType.String,dtRGA_H.Rows[0]["PurchaseDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldContractType",DbType.String,dtRGA_H.Rows[0]["ContractType"].ToString()),
                            DataAccessMySQL.CreateParameter("OldContractNo",DbType.String,dtRGA_H.Rows[0]["ContractNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldPCompletionDate",DbType.String,dtRGA_H.Rows[0]["PCompletionDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldOptionDetail",DbType.String,dtRGA_H.Rows[0]["OptionDetail"].ToString()),
                            DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtRGA_H.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("OldReturned",DbType.String,dtRGA_H.Rows[0]["Returned"].ToString()),
                            DataAccessMySQL.CreateParameter("OldMalfunctionReason",DbType.String,dtRGA_H.Rows[0]["MalfunctionReason"].ToString()),
                            DataAccessMySQL.CreateParameter("OldTestResult",DbType.String,dtRGA_H.Rows[0]["TestResult"].ToString()),
                            DataAccessMySQL.CreateParameter("OldInternalRemark",DbType.String,dtRGA_H.Rows[0]["OldInternalRemark"].ToString()),
                        });

                    dbMySQL.ExecuteNonQuery(@"update RGA_H set Version=@Version,CustomerId=@CustomerId,AddressSName=@AddressSName,
                        Address=@Address,Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,ProductNo=@ProductNo,ProductName=@ProductName,
                        SerialNo=@SerialNo,AssetNo=@AssetNo,WarrantyStartDate=@WarrantyStartDate,WarrantyExpiryDate=@WarrantyExpiryDate,
                        UnderWarranty=@UnderWarranty,SaleDate=@SaleDate,PurchaseDate=@PurchaseDate,ContractType=@ContractType,
                        ContractNo=@ContractNo,PCompletionDate=@PCompletionDate,OptionDetail=@OptionDetail,Remark=@Remark,
                        Returned=@Returned,MalfunctionReason=@MalfunctionReason,TestResult=@TestResult,OldInternalRemark=@OldInternalRemark where 
                        RGAType=@RGAType and RGANo=@RGANo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRGA_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,rGAChange_H.NewCustomerId),
                        DataAccessMySQL.CreateParameter("AddressSName",DbType.String,rGAChange_H.NewAddressSName),
                        DataAccessMySQL.CreateParameter("Address",DbType.String,rGAChange_H.NewAddress),
                        DataAccessMySQL.CreateParameter("Contact",DbType.String,rGAChange_H.NewContact),
                        DataAccessMySQL.CreateParameter("TelNo",DbType.String,rGAChange_H.NewTelNo),
                        DataAccessMySQL.CreateParameter("FaxNo",DbType.Int32,rGAChange_H.NewFaxNo),
                        DataAccessMySQL.CreateParameter("ProductNo",DbType.Int32,rGAChange_H.NewProductNo),
                        DataAccessMySQL.CreateParameter("ProductName",DbType.String,rGAChange_H.NewProductName),
                        DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rGAChange_H.NewSerialNo),
                        DataAccessMySQL.CreateParameter("AssetNo",DbType.String,rGAChange_H.NewAssetNo),
                        DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,rGAChange_H.NewWarrantyStartDate),
                        DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,rGAChange_H.NewWarrantyExpiryDate),
                        DataAccessMySQL.CreateParameter("UnderWarranty",DbType.String,rGAChange_H.NewUnderWarranty),
                        DataAccessMySQL.CreateParameter("SaleDate",DbType.String,rGAChange_H.NewSaleDate),
                        DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,rGAChange_H.NewPurchaseDate),
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,rGAChange_H.NewContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,rGAChange_H.NewContractNo),
                        DataAccessMySQL.CreateParameter("PCompletionDate",DbType.String,rGAChange_H.NewPCompletionDate),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rGAChange_H.Confirmed),
                        DataAccessMySQL.CreateParameter("OptionDetail",DbType.String,rGAChange_H.NewOptionDetail),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,rGAChange_H.NewRemark),
                        DataAccessMySQL.CreateParameter("RepairDetail",DbType.String,rGAChange_H.NewRepairDetail),
                        DataAccessMySQL.CreateParameter("Returned",DbType.String,rGAChange_H.NewReturned),
                        DataAccessMySQL.CreateParameter("MalfunctionReason",DbType.String,rGAChange_H.NewMalfunctionReason),
                        DataAccessMySQL.CreateParameter("TestResult",DbType.String,rGAChange_H.NewTestResult),
                        DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,rGAChange_H.NewInternalRemark)
                    });

                    string sql = "";

                    if (strQQuestionNo != "" && strQQuestionNo != null)
                    {
                        //string[] strQItemIdArray = strQItemId.Split(',');
                        string[] strQQuestionNoArray = strQQuestionNo.Split(',');
                        string[] strQQuestionArray = strQQuestion.Split(',');
                        string[] strQDescriptionArray = strQDescription.Split(',');

                        sql = @"insert into RGAChange_Q (Company,UserGroup,Creator,
                        CreateDate,RGAType,RGANo,Version,ItemId,NewQuestionNo,NewQuestion,
                        NewDescription,OldQuestionNo,OldQuestion,OldDescription) 
                        values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RGAType,@RGANo,@Version,@ItemId,@NewQuestionNo,@NewQuestion,
                        @NewDescription,@OldQuestionNo,@OldQuestion,@OldDescription)";

                        DataTable dtRGA_Q = null;

                        for (int i = 0; i < strQQuestionNoArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strQQuestionNoArray[i]))
                            {
                                continue;
                            }

                            dtRGA_Q = dbMySQL.ExecuteDataTable(@"select * from RGA_Q where 
                            RGAType=@RGAType and RGANo=@RGANo and ItemId=@ItemId", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000"))
                            });

                            if (dtRGA_Q != null && dtRGA_Q.Rows.Count > 0)
                            {
                                dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRGA_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString()),
                                    DataAccessMySQL.CreateParameter("NewQuestionNo",DbType.String,strQQuestionNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewQuestion",DbType.String,strQQuestionArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewDescription",DbType.String,strQDescriptionArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldQuestionNo",DbType.String,dtRGA_Q.Rows[0]["QuestionNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldQuestion",DbType.String,dtRGA_Q.Rows[0]["Question"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldDescription",DbType.String,dtRGA_Q.Rows[0]["Description"].ToString())
                                });

                                dbMySQL.ExecuteNonQuery(@"update RGA_Q set QuestionNo=@QuestionNo,Question=@Question,Description=@Description
                                    where RGAType=@RGAType and RGANo=@RGANo and ItemId=@ItemId", tran,
                                new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,strQQuestionNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Question",DbType.String,strQQuestionArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Description",DbType.String,strQDescriptionArray[i].ToString())
                                });
                            }
                            else
                            {
                                dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRGA_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString()),
                                    DataAccessMySQL.CreateParameter("NewQuestionNo",DbType.String,strQQuestionNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewQuestion",DbType.String,strQQuestionArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewDescription",DbType.String,strQDescriptionArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldQuestionNo",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldQuestion",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldDescription",DbType.String,"")
                                });

                                dbMySQL.ExecuteNonQuery(@"insert into RGA_Q (Company,UserGroup,Creator,
                                    CreateDate,RGAType,RGANo,ItemId,QuestionNo,Question,Description) values (@Company,@UserGroup,@Creator,
                                    @CreateDate,@RGAType,@RGANo,@ItemId,@QuestionNo,@Question,@Description)", tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,strQQuestionNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Question",DbType.String,strQQuestionArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("Description",DbType.String,strQDescriptionArray[i].ToString())
                                });
                            }

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

                        sql = @"insert into RGAChange_Q (Company,UserGroup,Creator,
                        CreateDate,RGAType,RGANo,Version,ItemId,NewPartNo,NewPartName,
                        NewQTY,NewUnit,NewRemark,NewRepaired,NewSourceOrderType,
                        NewSourceOrderNo,NewSourceItemId,NewResponseOrderType,NewResponseOrderNo,
                        NewResponseDate,OldPartNo,OldPartName,OldQTY,OldUnit,OldRemark,OldRepaired,
                        OldSourceOrderType,OldSourceOrderNo,OldSourceItemId,OldResponseOrderType,
                        OldResponseOrderNo,OldResponseDate) 
                        values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RGAType,@RGANo,@Version,@ItemId,@NewPartNo,@NewPartName,
                        @NewQTY,@NewUnit,@NewRemark,@NewRepaired,@NewSourceOrderType,
                        @NewSourceOrderNo,@NewSourceItemId,@NewResponseOrderType,@NewResponseOrderNo,
                        @NewResponseDate,@OldPartNo,@OldPartName,@OldQTY,@OldUnit,@OldRemark,@OldRepaired,
                        @OldSourceOrderType,@OldSourceOrderNo,@OldSourceItemId,@OldResponseOrderType,
                        @OldResponseOrderNo,@OldResponseDate)";

                        DataTable dtRGA_D = null;

                        for (int i = 0; i < strDItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strDItemIdArray[i]))
                            {
                                continue;
                            }

                            dtRGA_D = dbMySQL.ExecuteDataTable(@"select * from RGA_D where 
                            RGAType=@RGAType and RGANo=@RGANo and ItemId=@ItemId", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000"))
                            });

                            if (dtRGA_D != null && dtRGA_D.Rows.Count > 0)
                            {
                                dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRGA_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strDItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewPartNo",DbType.String,strDPartNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewPartName",DbType.String,strDPartNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewQTY",DbType.String,strDQTYArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewUnit",DbType.String,strDUnitArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewRemark",DbType.String,strDRemarkArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewRepaired",DbType.String,strDRepairedArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewSourceOrderType",DbType.String,strDSourceOrderTypeArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewSourceOrderNo",DbType.String,strDSourceOrderNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewSourceItemId",DbType.String,strDSourceItemIdArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("NewResponseOrderType",DbType.String,strDResponseOrderArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("NewResponseOrderNo",DbType.String,strDResponseOrderNoArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("NewResponseDate",DbType.String,strDResponseDateArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldPartNo",DbType.String,dtRGA_D.Rows[0]["PartNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldPartName",DbType.String,dtRGA_D.Rows[0]["PartName"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldQTY",DbType.String,dtRGA_D.Rows[0]["QTY"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtRGA_D.Rows[0]["Remark"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldRepaired",DbType.String,dtRGA_D.Rows[0]["Repaired"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldSourceOrderType",DbType.String,dtRGA_D.Rows[0]["SourceOrderType"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldSourceOrderNo",DbType.String,dtRGA_D.Rows[0]["SourceOrderNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldSourceItemId",DbType.String,dtRGA_D.Rows[0]["SourceItemId"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldResponseOrderType",DbType.String,dtRGA_D.Rows[0]["ResponseOrderType"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldResponseOrderNo",DbType.String,dtRGA_D.Rows[0]["ResponseOrderNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("OldResponseDate",DbType.String,dtRGA_D.Rows[0]["ResponseDate"].ToString())
                                });

                                dbMySQL.ExecuteNonQuery(@"update RGA_D set PartNo=@PartNo,PartName=@PartName,QTY=@QTY,
                                    Unit=@Unit,Remark=@Remark,Repaired=@Repaired,SourceOrderType=@SourceOrderType,
                                    SourceOrderNo=@SourceOrderNo,SourceItemId=@SourceItemId
                                    where RGAType=@RGAType and RGANo=@RGANo and ItemId=@ItemId", tran,
                                new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
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
                                });
                            }
                            else
                            {
                                dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtRGA_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strDItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewPartNo",DbType.String,strDPartNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewPartName",DbType.String,strDPartNameArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewQTY",DbType.String,strDQTYArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewUnit",DbType.String,strDUnitArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewRemark",DbType.String,strDRemarkArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewRepaired",DbType.String,strDRepairedArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewSourceOrderType",DbType.String,strDSourceOrderTypeArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewSourceOrderNo",DbType.String,strDSourceOrderNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("NewSourceItemId",DbType.String,strDSourceItemIdArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("NewResponseOrderType",DbType.String,strDResponseOrderArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("NewResponseOrderNo",DbType.String,strDResponseOrderNoArray[i].ToString()),
                                    //DataAccessMySQL.CreateParameter("NewResponseDate",DbType.String,strDResponseDateArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldPartNo",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldPartName",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldQTY",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldRemark",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldRepaired",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldSourceOrderType",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldSourceOrderNo",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldSourceItemId",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldResponseOrderType",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldResponseOrderNo",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("OldResponseDate",DbType.String,"")
                                });

                                dbMySQL.ExecuteNonQuery(@"insert into RGA_D (Company,UserGroup,Creator,
                                    CreateDate,RGAType,RGANo,ItemId,PartNo,PartName,QTY,Unit,Remark,
                                    Repaired,SourceOrderType,SourceOrderNo,SourceItemId) values (@Company,@UserGroup,@Creator,
                                    @CreateDate,@RGAType,@RGANo,@ItemId,@PartNo,@PartName,@QTY,@Unit,@Remark,
                                    @Repaired,@SourceOrderType,@SourceOrderNo,@SourceItemId)", tran, new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,rGAChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rGAChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,rGAChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
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

        public bool DelRGAChange_H(RGAChange_H rGAChange_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                DataTable dtRGAChange_H = dbMySQL.ExecuteDataTable(@"select * from RGAChange_H where 
                    RGAType=@RGAType and RGANo=@RGANo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,rGAChange_H.Version)
                    });
                if (dtRGAChange_H != null && dtRGAChange_H.Rows.Count > 0)
                {
                    dbMySQL.ExecuteNonQuery(@"update RGA_H set Version=@Version,Remark=@Remark,InternalRemark=@InternalRemark where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,rGAChange_H.RGAType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,rGAChange_H.RGANo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(rGAChange_H.Version)-1).ToString("0000")),
                        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtRGAChange_H.Rows[0]["OldCustomerId"].ToString()),
                        DataAccessMySQL.CreateParameter("AddressSName",DbType.String,dtRGAChange_H.Rows[0]["OldAddressSName"].ToString()),
                        DataAccessMySQL.CreateParameter("Address",DbType.String,dtRGAChange_H.Rows[0]["OldAddress"].ToString()),
                        DataAccessMySQL.CreateParameter("Contact",DbType.String,dtRGAChange_H.Rows[0]["OldContact"].ToString()),
                        DataAccessMySQL.CreateParameter("TelNo",DbType.String,dtRGAChange_H.Rows[0]["OldTelNo"].ToString()),
                        DataAccessMySQL.CreateParameter("FaxNo",DbType.String,dtRGAChange_H.Rows[0]["OldFaxNo"].ToString()),
                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,dtRGAChange_H.Rows[0]["OldProductNo"].ToString()),
                        DataAccessMySQL.CreateParameter("ProductName",DbType.String,dtRGAChange_H.Rows[0]["OldProductName"].ToString()),
                        DataAccessMySQL.CreateParameter("SerialNo",DbType.String,dtRGAChange_H.Rows[0]["OldSerialNo"].ToString()),
                        DataAccessMySQL.CreateParameter("AssetNo",DbType.String,dtRGAChange_H.Rows[0]["OldAssetNo"].ToString()),
                        DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,dtRGAChange_H.Rows[0]["OldWarrantyStartDate"].ToString()),
                        DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,dtRGAChange_H.Rows[0]["OldWarrantyExpiryDate"].ToString()),
                        DataAccessMySQL.CreateParameter("UnderWarranty",DbType.String,dtRGAChange_H.Rows[0]["OldUnderWarranty"].ToString()),
                        DataAccessMySQL.CreateParameter("SaleDate",DbType.String,dtRGAChange_H.Rows[0]["OldSaleDate"].ToString()),
                        DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,dtRGAChange_H.Rows[0]["OldPurchaseDate"].ToString()),
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,dtRGAChange_H.Rows[0]["OldContractType"].ToString()),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,dtRGAChange_H.Rows[0]["OldContractNo"].ToString()),
                        DataAccessMySQL.CreateParameter("PCompletionDate",DbType.String,dtRGAChange_H.Rows[0]["OldPCompletionDate"].ToString()),
                        DataAccessMySQL.CreateParameter("OptionDetail",DbType.String,dtRGAChange_H.Rows[0]["OldOptionDetail"].ToString()),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,dtRGAChange_H.Rows[0]["OldRemark"].ToString()),
                        DataAccessMySQL.CreateParameter("Returned",DbType.String,dtRGAChange_H.Rows[0]["OldReturned"].ToString()),
                        DataAccessMySQL.CreateParameter("MalfunctionReason",DbType.String,dtRGAChange_H.Rows[0]["OldMalfunctionReason"].ToString()),
                        DataAccessMySQL.CreateParameter("TestResult",DbType.String,dtRGAChange_H.Rows[0]["OldTestResult"].ToString()),
                        DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,dtRGAChange_H.Rows[0]["OldInternalRemark"].ToString())
                    });

                    dbMySQL.ExecuteNonQuery(@"delete from RGAChange_H where 
                    RGAType=@RGAType and RGANo=@RGANo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,rGAChange_H.Version)
                    });
                }

                DataTable dtRGAChange_Q = dbMySQL.ExecuteDataTable(@"select * from RGAChange_Q where 
                    RGAType=@RGAType and RGANo=@RGANo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,rGAChange_H.Version)
                    });
                if (dtRGAChange_Q != null && dtRGAChange_Q.Rows.Count > 0)
                {
                    foreach(DataRow dr in dtRGAChange_Q.Rows)
                    {
                        dbMySQL.ExecuteNonQuery(@"update RGA_Q set QuestionNo=@QuestionNo,Question=@Question,Description=@Description where 
                        RGAType=@RGAType and RGANo=@RGANo and ItemId=@ItemId", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,dr["ItemId"].ToString()),
                            DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,dr["OldQuestionNo"].ToString()),
                            DataAccessMySQL.CreateParameter("Question",DbType.String,dr["OldQuestion"].ToString()),
                            DataAccessMySQL.CreateParameter("Description",DbType.String,dr["OldDescription"].ToString())
                        });
                    }

                    dbMySQL.ExecuteNonQuery(@"delete from RGAChange_Q where 
                    RGAType=@RGAType and RGANo=@RGANo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,rGAChange_H.Version)
                    });
                }

                DataTable dtRGAChange_D = dbMySQL.ExecuteDataTable(@"select * from RGAChange_D where 
                    RGAType=@RGAType and RGANo=@RGANo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,rGAChange_H.Version)
                    });

                if(dtRGAChange_Q != null && dtRGAChange_Q.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRGAChange_Q.Rows)
                    {
                        dbMySQL.ExecuteNonQuery(@"update RGA_D set PartNo=@PartNo,PartName=@PartName,QTY=@QTY,
                        Unit=@Unit,Remark=@Remark,Repaired=@Repaired,SourceOrderType=@SourceOrderType,
                        SourceOrderNo=@SourceOrderNo,SourceItemId=@SourceItemId,ResponseOrderType=@ResponseOrderType,
                        ResponseOrderNo=@ResponseOrderNo,ResponseDate=@ResponseDate where 
                        RGAType=@RGAType and RGANo=@RGANo and ItemId=@ItemId", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,dr["ItemId"].ToString()),
                            DataAccessMySQL.CreateParameter("PartNo",DbType.String,dr["OldPartNo"].ToString()),
                            DataAccessMySQL.CreateParameter("PartName",DbType.String,dr["OldPartName"].ToString()),
                            DataAccessMySQL.CreateParameter("QTY",DbType.String,dr["OldQTY"].ToString()),
                            DataAccessMySQL.CreateParameter("Unit",DbType.String,dr["OldUnit"].ToString()),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,dr["OldRemark"].ToString()),
                            DataAccessMySQL.CreateParameter("Repaired",DbType.String,dr["OldRepaired"].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,dr["OldSourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,dr["OldSourceOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("SourceItemId",DbType.String,dr["OldSourceItemId"].ToString()),
                            DataAccessMySQL.CreateParameter("ResponseOrderType",DbType.String,dr["OldResponseOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("ResponseOrderNo",DbType.String,dr["OldResponseOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("ResponseDate",DbType.String,dr["OldResponseDate"].ToString()),
                        });
                    }

                    dbMySQL.ExecuteNonQuery(@"delete from RGAChange_D where 
                    RGAType=@RGAType and RGANo=@RGANo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,rGAChange_H.Version)
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

        public List<RGAChange_H> SearchRGAChange_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RGAChange_H> objRGAChange_H = new List<RGAChange_H>();

            string sql = @"select * from RGAChange_H   where 1=1";

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


            DataTable dtRGAChange_H = null;
            if (strCondition != "")
            {
                dtRGAChange_H = dbMySQL.ExecuteDataTable(sql + strCondition , dbParameters);
            }
            else
            {
                dtRGAChange_H = dbMySQL.ExecuteDataTable(sql );
            }
            if (dtRGAChange_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGAChange_H.Rows)
                {
                    RGAChange_H obj = new RGAChange_H();
                    obj.RGAType = dr["RGAType"].ToString();
                    obj.RGANo = dr["RGANo"].ToString();
                    objRGAChange_H.Add(obj);
                }
            }

            return objRGAChange_H;
        }

        public RGAChange_H GetRGAChange_HInfo(RGAChange_H rGAChange_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            RGAChange_H obj = null;

            DataTable dtRGAChange_H = dbMySQL.ExecuteDataTable(@"select * from  RGAChange_H where RGAType=@RGAType and RGANo=@RGANo and Version=@Version",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_H.RGAType),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_H.RGANo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,rGAChange_H.Version),
                    });

            if (dtRGAChange_H.Rows.Count > 0)
            {
                obj = new RGAChange_H();
                obj.RGAType = dtRGAChange_H.Rows[0]["RGAType"].ToString();
                obj.RGANo = dtRGAChange_H.Rows[0]["RGANo"].ToString();
                obj.Version = dtRGAChange_H.Rows[0]["Version"].ToString();
                obj.OrderDate = dtRGAChange_H.Rows[0]["OrderDate"].ToString();
                obj.NoOfPrints = int.Parse(dtRGAChange_H.Rows[0]["NoOfPrints"].ToString());
                obj.SourceOrderType = dtRGAChange_H.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtRGAChange_H.Rows[0]["SourceOrderNo"].ToString();
                obj.RepairAll = dtRGAChange_H.Rows[0]["RepairAll"].ToString();
                obj.Repairman = dtRGAChange_H.Rows[0]["Repairman"].ToString();
                obj.ReasonOfChange = dtRGAChange_H.Rows[0]["ReasonOfChange"].ToString();
                obj.NewCustomerId = dtRGAChange_H.Rows[0]["NewCustomerId"].ToString();
                obj.NewAddressSName = dtRGAChange_H.Rows[0]["NewAddressSName"].ToString();
                obj.NewAddress = dtRGAChange_H.Rows[0]["NewAddress"].ToString();
                obj.NewContact = dtRGAChange_H.Rows[0]["NewContact"].ToString();
                obj.NewTelNo = dtRGAChange_H.Rows[0]["NewTelNo"].ToString();
                obj.NewFaxNo = dtRGAChange_H.Rows[0]["NewFaxNo"].ToString();
                obj.NewProductNo = dtRGAChange_H.Rows[0]["NewProductNo"].ToString();
                obj.NewProductName = dtRGAChange_H.Rows[0]["NewProductName"].ToString();
                obj.NewSerialNo = dtRGAChange_H.Rows[0]["NewSerialNo"].ToString();
                obj.NewAssetNo = dtRGAChange_H.Rows[0]["NewAssetNo"].ToString();
                obj.NewWarrantyStartDate = dtRGAChange_H.Rows[0]["NewWarrantyStartDate"].ToString();
                obj.NewWarrantyExpiryDate = dtRGAChange_H.Rows[0]["NewWarrantyExpiryDate"].ToString();
                obj.NewUnderWarranty = dtRGAChange_H.Rows[0]["NewUnderWarranty"].ToString();
                obj.NewSaleDate = dtRGAChange_H.Rows[0]["NewSaleDate"].ToString();
                obj.NewPurchaseDate = dtRGAChange_H.Rows[0]["NewPurchaseDate"].ToString();
                obj.NewContractType = dtRGAChange_H.Rows[0]["NewContractType"].ToString();
                obj.NewContractNo = dtRGAChange_H.Rows[0]["NewContractNo"].ToString();
                obj.NewPCompletionDate = dtRGAChange_H.Rows[0]["NewPCompletionDate"].ToString();
                obj.Confirmed = dtRGAChange_H.Rows[0]["Confirmed"].ToString();
                obj.NewOptionDetail = dtRGAChange_H.Rows[0]["NewOptionDetail"].ToString();
                obj.NewRemark = dtRGAChange_H.Rows[0]["NewRemark"].ToString();
                obj.NewRepairDetail = dtRGAChange_H.Rows[0]["NewRepairDetail"].ToString();
                obj.NewReturned = dtRGAChange_H.Rows[0]["NewReturned"].ToString();
                obj.NewMalfunctionReason = dtRGAChange_H.Rows[0]["NewMalfunctionReason"].ToString();
                obj.NewTestResult = dtRGAChange_H.Rows[0]["NewTestResult"].ToString();
                obj.NewInternalRemark = dtRGAChange_H.Rows[0]["NewInternalRemark"].ToString();
                obj.OldCustomerId = dtRGAChange_H.Rows[0]["OldCustomerId"].ToString();
                obj.OldAddressSName = dtRGAChange_H.Rows[0]["OldAddressSName"].ToString();
                obj.OldAddress = dtRGAChange_H.Rows[0]["OldAddress"].ToString();
                obj.OldContact = dtRGAChange_H.Rows[0]["OldContact"].ToString();
                obj.OldTelNo = dtRGAChange_H.Rows[0]["OldTelNo"].ToString();
                obj.OldFaxNo = dtRGAChange_H.Rows[0]["OldFaxNo"].ToString();
                obj.OldProductNo = dtRGAChange_H.Rows[0]["OldProductNo"].ToString();
                obj.OldProductName = dtRGAChange_H.Rows[0]["OldProductName"].ToString();
                obj.OldSerialNo = dtRGAChange_H.Rows[0]["OldSerialNo"].ToString();
                obj.OldAssetNo = dtRGAChange_H.Rows[0]["OldAssetNo"].ToString();
                obj.OldWarrantyStartDate = dtRGAChange_H.Rows[0]["OldWarrantyStartDate"].ToString();
                obj.OldWarrantyExpiryDate = dtRGAChange_H.Rows[0]["OldWarrantyExpiryDate"].ToString();
                obj.OldUnderWarranty = dtRGAChange_H.Rows[0]["OldUnderWarranty"].ToString();
                obj.OldSaleDate = dtRGAChange_H.Rows[0]["OldSaleDate"].ToString();
                obj.OldPurchaseDate = dtRGAChange_H.Rows[0]["OldPurchaseDate"].ToString();
                obj.OldContractType = dtRGAChange_H.Rows[0]["OldContractType"].ToString();
                obj.OldContractNo = dtRGAChange_H.Rows[0]["OldContractNo"].ToString();
                obj.OldPCompletionDate = dtRGAChange_H.Rows[0]["OldPCompletionDate"].ToString();
                obj.OldOptionDetail = dtRGAChange_H.Rows[0]["OldOptionDetail"].ToString();
                obj.OldRemark = dtRGAChange_H.Rows[0]["OldRemark"].ToString();
                obj.OldReturned = dtRGAChange_H.Rows[0]["OldReturned"].ToString();
                obj.OldMalfunctionReason = dtRGAChange_H.Rows[0]["OldMalfunctionReason"].ToString();
                obj.OldTestResult = dtRGAChange_H.Rows[0]["OldTestResult"].ToString();
                obj.OldInternalRemark = dtRGAChange_H.Rows[0]["OldInternalRemark"].ToString();

            }

            return obj;
        }

    }
}