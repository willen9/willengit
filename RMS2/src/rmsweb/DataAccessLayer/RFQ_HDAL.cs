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
    public class RFQ_HDAL : IRFQ_HDAL
    {
        public bool AddRFQ_H(RFQ_H rFQ_H, string strItemId,
            string strPartNo, string strPartName,
            string strQTY, string strUnit, string strUnitPrice,
            string strPrice, string strListPrice, string strRemark,
            string strRepaired, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from RFQ_H where 
                    RFQType=@RFQType and RFQNo=@RFQNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RFQType",DbType.String,rFQ_H.RFQType),
                        DataAccessMySQL.CreateParameter("RFQNo",DbType.String,rFQ_H.RFQNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into RFQ_H (Company,UserGroup,Creator,CreateDate,
                        RFQType,RFQNo,OrderDate,StatusCode,NoOfPrints,SourceOrderType,SourceOrderNo,Remark,VendorId,
                        TaxRate,Contact,TelNo,FaxNo,Address,ProductNo,ProductName,SerialNo,PurchaseDate,WarrantyStartDate,
                        WarrantyExpiryDate,TaxExcluded,Tax,TaxIncluded,Confirmed,Responser,ResponseDate,StatusOfResponse) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @RFQType,@RFQNo,@OrderDate,@StatusCode,@NoOfPrints,@SourceOrderType,@SourceOrderNo,@Remark,@VendorId,
                        @TaxRate,@Contact,@TelNo,@FaxNo,@Address,@ProductNo,@ProductName,@SerialNo,@PurchaseDate,@WarrantyStartDate,
                        @WarrantyExpiryDate,@TaxExcluded,@Tax,@TaxIncluded,@Confirmed,@Responser,@ResponseDate,@StatusOfResponse)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,rFQ_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rFQ_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,rFQ_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RFQType",DbType.String,rFQ_H.RFQType),
                            DataAccessMySQL.CreateParameter("RFQNo",DbType.String,rFQ_H.RFQNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rFQ_H.OrderDate),
                            DataAccessMySQL.CreateParameter("StatusCode",DbType.String,rFQ_H.StatusCode),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rFQ_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,rFQ_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,rFQ_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rFQ_H.Remark),
                            DataAccessMySQL.CreateParameter("VendorId",DbType.String,rFQ_H.VendorId),
                            DataAccessMySQL.CreateParameter("TaxRate",DbType.Double,rFQ_H.TaxRate),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,rFQ_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,rFQ_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,rFQ_H.FaxNo),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,rFQ_H.Address),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,rFQ_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,rFQ_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rFQ_H.SerialNo),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,rFQ_H.PurchaseDate),
                            DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,rFQ_H.WarrantyStartDate),
                            DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,rFQ_H.WarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("TaxExcluded",DbType.Double,rFQ_H.TaxExcluded),
                            DataAccessMySQL.CreateParameter("Tax",DbType.Double,rFQ_H.Tax),
                            DataAccessMySQL.CreateParameter("TaxIncluded",DbType.Double,rFQ_H.TaxIncluded),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rFQ_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Responser",DbType.String,rFQ_H.Responser),
                            DataAccessMySQL.CreateParameter("ResponseDate",DbType.String,rFQ_H.ResponseDate),
                            DataAccessMySQL.CreateParameter("StatusOfResponse",DbType.String,rFQ_H.StatusOfResponse)
                        });

                    string sql = "";

                    if (strItemId != "" && strItemId != null)
                    {
                        string[] strItemIdArray = strItemId.Split(',');
                        string[] strPartNoArray = strPartNo.Split(',');
                        string[] strPartNameArray = strPartName.Split(',');
                        string[] strQTYArray = strQTY.Split(',');
                        string[] strUnitArray = strUnit.Split(',');
                        string[] strUnitPriceArray = strUnitPrice.Split(',');
                        string[] strPriceArray = strPrice.Split(',');
                        string[] strListPriceArray = strListPrice.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');
                        string[] strRepairedArray = strRepaired.Split(',');

                        sql = @"insert into RFQ_D (Company,UserGroup,Creator,
                        CreateDate,RFQType,RFQNo,ItemId,PartNo,PartName,
                        QTY,Unit,UnitPrice,Price,ListPrice,Remark,Repaired) 
                        values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RFQType,@RFQNo,@ItemId,@PartNo,@PartName,
                        @QTY,@Unit,@UnitPrice,@Price,@ListPrice,@Remark,@Repaired)";


                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,rFQ_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rFQ_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,rFQ_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RFQType",DbType.String,rFQ_H.RFQType),
                                DataAccessMySQL.CreateParameter("RFQNo",DbType.String,rFQ_H.RFQNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PartNo",DbType.String,strPartNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PartName",DbType.String,strPartNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.String,double.Parse(strQTYArray[i].ToString()==""?"0":strQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strUnitArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("UnitPrice",DbType.Double,double.Parse(strUnitPriceArray[i].ToString()==""?"0":strUnitPriceArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Price",DbType.Double,double.Parse(strPriceArray[i].ToString()==""?"0":strPriceArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("ListPrice",DbType.Double,double.Parse(strListPriceArray[i].ToString()==""?"0":strListPriceArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Repaired",DbType.String,strRepairedArray[i].ToString())
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

        public bool UpdateRFQ_H(RFQ_H rFQ_H, string strItemId,
            string strPartNo, string strPartName,
            string strQTY, string strUnit, string strUnitPrice,
            string strPrice, string strListPrice, string strRemark,
            string strRepaired, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update RFQ_H set Modifier=@Modifier,ModiDate=@ModiDate,
                        SourceOrderType=@SourceOrderType,SourceOrderNo=@SourceOrderNo,
                        Remark=@Remark,VendorId=@VendorId,TaxRate=@TaxRate,Contact=@Contact,TelNo=@TelNo,
                        FaxNo=@FaxNo,Address=@Address,ProductNo=@ProductNo,ProductName=@ProductName,
                        SerialNo=@SerialNo,PurchaseDate=@PurchaseDate,WarrantyStartDate=@WarrantyStartDate,
                        WarrantyExpiryDate=@WarrantyExpiryDate,TaxExcluded=@TaxExcluded,Tax=@Tax,
                        TaxIncluded=@TaxIncluded,Confirmed=@Confirmed,Responser=@Responser,
                        ResponseDate=@ResponseDate,StatusOfResponse=@StatusOfResponse
                        where RFQType=@RFQType and RFQNo=@RFQNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,rFQ_H.Creator),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("RFQType",DbType.String,rFQ_H.RFQType),
                            DataAccessMySQL.CreateParameter("RFQNo",DbType.String,rFQ_H.RFQNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,rFQ_H.OrderDate),
                            DataAccessMySQL.CreateParameter("StatusCode",DbType.String,rFQ_H.StatusCode),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,rFQ_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,rFQ_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,rFQ_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,rFQ_H.Remark),
                            DataAccessMySQL.CreateParameter("VendorId",DbType.String,rFQ_H.VendorId),
                            DataAccessMySQL.CreateParameter("TaxRate",DbType.Double,rFQ_H.TaxRate),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,rFQ_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,rFQ_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,rFQ_H.FaxNo),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,rFQ_H.Address),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,rFQ_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,rFQ_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,rFQ_H.SerialNo),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,rFQ_H.PurchaseDate),
                            DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,rFQ_H.WarrantyStartDate),
                            DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,rFQ_H.WarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("TaxExcluded",DbType.Double,rFQ_H.TaxExcluded),
                            DataAccessMySQL.CreateParameter("Tax",DbType.Double,rFQ_H.Tax),
                            DataAccessMySQL.CreateParameter("TaxIncluded",DbType.Double,rFQ_H.TaxIncluded),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,rFQ_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Responser",DbType.String,rFQ_H.Responser),
                            DataAccessMySQL.CreateParameter("ResponseDate",DbType.String,rFQ_H.ResponseDate),
                            DataAccessMySQL.CreateParameter("StatusOfResponse",DbType.String,rFQ_H.StatusOfResponse)
                        });

                dbMySQL.ExecuteNonQuery("delete from RFQ_D where RFQType=@RFQType and RFQNo=@RFQNo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RFQType",DbType.String,rFQ_H.RFQType),
                        DataAccessMySQL.CreateParameter("RFQNo",DbType.String,rFQ_H.RFQNo)
                    });
                string sql = "";

                if (strItemId != "" && strItemId != null)
                {
                    string[] strItemIdArray = strItemId.Split(',');
                    string[] strPartNoArray = strPartNo.Split(',');
                    string[] strPartNameArray = strPartName.Split(',');
                    string[] strQTYArray = strQTY.Split(',');
                    string[] strUnitArray = strUnit.Split(',');
                    string[] strUnitPriceArray = strUnitPrice.Split(',');
                    string[] strPriceArray = strPrice.Split(',');
                    string[] strListPriceArray = strListPrice.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');
                    string[] strRepairedArray = strRepaired.Split(',');

                    sql = @"insert into RFQ_D (Company,UserGroup,Creator,
                        CreateDate,RFQType,RFQNo,ItemId,PartNo,PartName,
                        QTY,Unit,UnitPrice,Price,ListPrice,Remark,Repaired) 
                        values (@Company,@UserGroup,@Creator,
                        @CreateDate,@RFQType,@RFQNo,@ItemId,@PartNo,@PartName,
                        @QTY,@Unit,@UnitPrice,@Price,@ListPrice,@Remark,@Repaired)";


                    for (int i = 0; i < strItemIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strItemIdArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,rFQ_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,rFQ_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,rFQ_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RFQType",DbType.String,rFQ_H.RFQType),
                                DataAccessMySQL.CreateParameter("RFQNo",DbType.String,rFQ_H.RFQNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PartNo",DbType.String,strPartNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PartName",DbType.String,strPartNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.String,double.Parse(strQTYArray[i].ToString()==""?"0":strQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strUnitArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("UnitPrice",DbType.Double,double.Parse(strUnitPriceArray[i].ToString()==""?"0":strUnitPriceArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Price",DbType.Double,double.Parse(strPriceArray[i].ToString()==""?"0":strPriceArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("ListPrice",DbType.Double,double.Parse(strListPriceArray[i].ToString()==""?"0":strListPriceArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Repaired",DbType.String,strRepairedArray[i].ToString())
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

        public bool DelRFQ_H(RFQ_H rFQ_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery("delete from RFQ_H where RFQType=@RFQType and RFQNo=@RFQNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RFQType",DbType.String,rFQ_H.RFQType),
                            DataAccessMySQL.CreateParameter("RFQNo",DbType.String,rFQ_H.RFQNo)
                        });
                dbMySQL.ExecuteNonQuery("delete from RFQ_D where RFQType=@RFQType and RFQNo=@RFQNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("RFQType",DbType.String,rFQ_H.RFQType),
                            DataAccessMySQL.CreateParameter("RFQNo",DbType.String,rFQ_H.RFQNo)
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

        public List<RFQ_H> SearchRFQ_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RFQ_H> objRFQ_H = new List<RFQ_H>();

            string sql = @"select * from RFQ_H where 1=1";

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


            DataTable dtRFQ_H = null;
            if (strCondition != "")
            {
                dtRFQ_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRFQ_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtRFQ_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRFQ_H.Rows)
                {
                    RFQ_H obj = new RFQ_H();
                    obj.RFQType = dr["RFQType"].ToString();
                    obj.RFQNo = dr["RFQNo"].ToString();
                    objRFQ_H.Add(obj);
                }
            }

            return objRFQ_H;
        }

        public RFQ_H GetRFQ_HInfo(RFQ_H rFQ_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            RFQ_H obj = null;

            DataTable dtRFQ_H = dbMySQL.ExecuteDataTable(@"select * from RFQ_H where 
                   RFQType=@RFQType and RFQNo=@RFQNo",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("RFQType",DbType.String,rFQ_H.RFQType),
                        DataAccessMySQL.CreateParameter("RFQNo",DbType.String,rFQ_H.RFQNo)
                   });

            if (dtRFQ_H.Rows.Count > 0)
            {
                obj = new RFQ_H();
                obj.RFQType = dtRFQ_H.Rows[0]["RFQType"].ToString();
                obj.RFQNo = dtRFQ_H.Rows[0]["RFQNo"].ToString();
                obj.OrderDate = dtRFQ_H.Rows[0]["OrderDate"].ToString();
                obj.StatusCode = dtRFQ_H.Rows[0]["StatusCode"].ToString();
                obj.NoOfPrints = int.Parse(dtRFQ_H.Rows[0]["NoOfPrints"].ToString());
                obj.SourceOrderType = dtRFQ_H.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtRFQ_H.Rows[0]["SourceOrderNo"].ToString();
                obj.Remark = dtRFQ_H.Rows[0]["Remark"].ToString();
                obj.VendorId = dtRFQ_H.Rows[0]["VendorId"].ToString();
                obj.TaxRate = double.Parse(dtRFQ_H.Rows[0]["TaxRate"].ToString());
                obj.Contact = dtRFQ_H.Rows[0]["Contact"].ToString();
                obj.TelNo = dtRFQ_H.Rows[0]["TelNo"].ToString();
                obj.FaxNo = dtRFQ_H.Rows[0]["FaxNo"].ToString();
                obj.Address = dtRFQ_H.Rows[0]["Address"].ToString();
                obj.ProductNo = dtRFQ_H.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtRFQ_H.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtRFQ_H.Rows[0]["SerialNo"].ToString();
                obj.PurchaseDate = dtRFQ_H.Rows[0]["PurchaseDate"].ToString();
                obj.WarrantyStartDate = dtRFQ_H.Rows[0]["WarrantyStartDate"].ToString();
                obj.WarrantyExpiryDate = dtRFQ_H.Rows[0]["WarrantyExpiryDate"].ToString();
                obj.TaxExcluded = double.Parse(dtRFQ_H.Rows[0]["TaxExcluded"].ToString());
                obj.Tax = double.Parse(dtRFQ_H.Rows[0]["Tax"].ToString());
                obj.TaxIncluded = double.Parse(dtRFQ_H.Rows[0]["TaxIncluded"].ToString());
                obj.Confirmed = dtRFQ_H.Rows[0]["Confirmed"].ToString();
                obj.Responser = dtRFQ_H.Rows[0]["Responser"].ToString();
                obj.ResponseDate = dtRFQ_H.Rows[0]["ResponseDate"].ToString();
                obj.StatusOfResponse = dtRFQ_H.Rows[0]["StatusOfResponse"].ToString();
            }

            return obj;
        }
        
    }
}