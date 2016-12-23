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
    public class Quotation_HDAL : IQuotation_HDAL
    {
        public bool AddQuotation_H(Quotation_H quotation_H, string strItemId,
            string strPartNo, string strPartName, string strQTY,
            string strUnit, string strUnitPrice, string strPreferentialPrice,
            string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from Quotation_H where 
                    QuotationType=@QuotationType and QuotationNo=@QuotationNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("QuotationType",DbType.String,quotation_H.QuotationType),
                        DataAccessMySQL.CreateParameter("QuotationNo",DbType.String,quotation_H.QuotationNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into Quotation_H (Company,UserGroup,Creator,CreateDate,
                        QuotationType,QuotationNo,OrderDate,StatusCode,NoOfPrints,Repairman,SourceOrderType,
                        SourceOrderNo,Responser,StatusOfResponse,ResponseDate,Response,CustomerId,TaxRate,
                        AddressSName,Address,Contact,TelNo,FaxNo,ProductNo,ProductName,SerialNo,AssetNo,
                        UnderWarranty,SaleDate,PurchaseDate,WarrantyStartDate,WarrantyExpiryDate,Remark,
                        TestResult,InternalRemark,TaxExcluded,Tax,TaxIncluded,Confirmed) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @QuotationType,@QuotationNo,@OrderDate,@StatusCode,@NoOfPrints,@Repairman,@SourceOrderType,
                        @SourceOrderNo,@Responser,@StatusOfResponse,@ResponseDate,@Response,@CustomerId,@TaxRate,
                        @AddressSName,@Address,@Contact,@TelNo,@FaxNo,@ProductNo,@ProductName,@SerialNo,@AssetNo,
                        @UnderWarranty,@SaleDate,@PurchaseDate,@WarrantyStartDate,@WarrantyExpiryDate,@Remark,
                        @TestResult,@InternalRemark,@TaxExcluded,@Tax,@TaxIncluded,@Confirmed)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,quotation_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,quotation_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,quotation_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("QuotationType",DbType.String,quotation_H.QuotationType),
                            DataAccessMySQL.CreateParameter("QuotationNo",DbType.String,quotation_H.QuotationNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,quotation_H.OrderDate),
                            DataAccessMySQL.CreateParameter("StatusCode",DbType.String,quotation_H.StatusCode),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,quotation_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("Repairman",DbType.String,quotation_H.Repairman),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,quotation_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,quotation_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("Responser",DbType.String,quotation_H.Responser),
                            DataAccessMySQL.CreateParameter("StatusOfResponse",DbType.String,quotation_H.StatusOfResponse),
                            DataAccessMySQL.CreateParameter("ResponseDate",DbType.String,quotation_H.ResponseDate),
                            DataAccessMySQL.CreateParameter("Response",DbType.String,quotation_H.Response),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,quotation_H.CustomerId),
                            DataAccessMySQL.CreateParameter("TaxRate",DbType.String,quotation_H.TaxRate),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,quotation_H.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,quotation_H.Address),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,quotation_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,quotation_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,quotation_H.FaxNo),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,quotation_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,quotation_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,quotation_H.SerialNo),
                            DataAccessMySQL.CreateParameter("AssetNo",DbType.String,quotation_H.AssetNo),
                            DataAccessMySQL.CreateParameter("UnderWarranty",DbType.String,quotation_H.UnderWarranty),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,quotation_H.SaleDate),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,quotation_H.PurchaseDate),
                            DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,quotation_H.WarrantyStartDate),
                            DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,quotation_H.WarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,quotation_H.Remark),
                            DataAccessMySQL.CreateParameter("TestResult",DbType.String,quotation_H.TestResult),
                            DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,quotation_H.InternalRemark),
                            DataAccessMySQL.CreateParameter("TaxExcluded",DbType.String,quotation_H.TaxExcluded),
                            DataAccessMySQL.CreateParameter("Tax",DbType.String,quotation_H.Tax),
                            DataAccessMySQL.CreateParameter("TaxIncluded",DbType.String,quotation_H.TaxIncluded),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,quotation_H.Confirmed)
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
                        string[] strPreferentialPriceArray = strPreferentialPrice.Split(',');
                        //string[] strSubtotalArray = strSubtotal.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');

                        sql = @"insert into Quotation_D (Company,UserGroup,Creator,
                        CreateDate,QuotationType,QuotationNo,ItemId,PartNo,PartName,
                        QTY,Unit,UnitPrice,PreferentialPrice,Subtotal,Remark) values 
                        (@Company,@UserGroup,@Creator,
                        @CreateDate,@QuotationType,QuotationNo,ItemId,PartNo,PartName,
                        QTY,Unit,UnitPrice,PreferentialPrice,Subtotal,Remark)";


                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,quotation_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,quotation_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,quotation_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("QuotationType",DbType.String,quotation_H.QuotationType),
                                DataAccessMySQL.CreateParameter("QuotationNo",DbType.String,quotation_H.QuotationNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PartNo",DbType.String,strPartNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PartName",DbType.String,strPartNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.Double,double.Parse(strQTYArray[i].ToString()==""?"0":strQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strUnitArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("UnitPrice",DbType.Double,double.Parse(strUnitPriceArray[i].ToString()==""?"0":strUnitPriceArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("PreferentialPrice",DbType.Double,double.Parse(strPreferentialPriceArray[i].ToString()==""?"0":strPreferentialPriceArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Subtotal",DbType.Double,double.Parse(strQTYArray[i].ToString()==""?"0":strQTYArray[i].ToString())*double.Parse(strPreferentialPriceArray[i].ToString()==""?"0":strPreferentialPriceArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
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

        public bool UpdateQuotation_H(Quotation_H quotation_H, string strItemId,
            string strPartNo, string strPartName, string strQTY,
            string strUnit, string strUnitPrice, string strPreferentialPrice,
            string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update Quotation_H set Modifier=@Modifier,ModiDate=@ModiDate,
                        StatusCode=@StatusCode,
                        Repairman=@Repairman,SourceOrderType=@SourceOrderType,SourceOrderNo=@SourceOrderNo,
                        Responser=@Responser,ResponseDate=@ResponseDate,
                        Response=@Response,CustomerId=@CustomerId,TaxRate=@TaxRate,AddressSName=@AddressSName,
                        Address=@Address,Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,ProductNo=@ProductNo,
                        ProductName=@ProductName,SerialNo=@SerialNo,AssetNo=@AssetNo,UnderWarranty=@UnderWarranty,
                        SaleDate=@SaleDate,PurchaseDate=@PurchaseDate,WarrantyStartDate=@WarrantyStartDate,
                        WarrantyExpiryDate=@WarrantyExpiryDate,Remark=@Remark,TestResult=@TestResult,
                        InternalRemark=@InternalRemark,TaxExcluded=@TaxExcluded,Tax=@Tax,TaxIncluded=@TaxIncluded
                        where QuotationType=@QuotationType and QuotationNo=@QuotationNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,quotation_H.Creator),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("QuotationType",DbType.String,quotation_H.QuotationType),
                            DataAccessMySQL.CreateParameter("QuotationNo",DbType.String,quotation_H.QuotationNo),
                            //DataAccessMySQL.CreateParameter("OrderDate",DbType.String,quotation_H.OrderDate),
                            DataAccessMySQL.CreateParameter("StatusCode",DbType.String,quotation_H.StatusCode),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,quotation_H.NoOfPrints),
                            DataAccessMySQL.CreateParameter("Repairman",DbType.String,quotation_H.Repairman),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,quotation_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,quotation_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("Responser",DbType.String,quotation_H.Responser),
                            //DataAccessMySQL.CreateParameter("StatusOfResponse",DbType.String,quotation_H.StatusOfResponse),
                            DataAccessMySQL.CreateParameter("ResponseDate",DbType.String,quotation_H.ResponseDate),
                            DataAccessMySQL.CreateParameter("Response",DbType.String,quotation_H.Response),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,quotation_H.CustomerId),
                            DataAccessMySQL.CreateParameter("TaxRate",DbType.String,quotation_H.TaxRate),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,quotation_H.AddressSName),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,quotation_H.Address),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,quotation_H.Contact),
                            DataAccessMySQL.CreateParameter("TelNo",DbType.String,quotation_H.TelNo),
                            DataAccessMySQL.CreateParameter("FaxNo",DbType.String,quotation_H.FaxNo),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,quotation_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,quotation_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,quotation_H.SerialNo),
                            DataAccessMySQL.CreateParameter("AssetNo",DbType.String,quotation_H.AssetNo),
                            DataAccessMySQL.CreateParameter("UnderWarranty",DbType.String,quotation_H.UnderWarranty),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,quotation_H.SaleDate),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,quotation_H.PurchaseDate),
                            DataAccessMySQL.CreateParameter("WarrantyStartDate",DbType.String,quotation_H.WarrantyStartDate),
                            DataAccessMySQL.CreateParameter("WarrantyExpiryDate",DbType.String,quotation_H.WarrantyExpiryDate),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,quotation_H.Remark),
                            DataAccessMySQL.CreateParameter("TestResult",DbType.String,quotation_H.TestResult),
                            DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,quotation_H.InternalRemark),
                            DataAccessMySQL.CreateParameter("TaxExcluded",DbType.String,quotation_H.TaxExcluded),
                            DataAccessMySQL.CreateParameter("Tax",DbType.String,quotation_H.Tax),
                            DataAccessMySQL.CreateParameter("TaxIncluded",DbType.String,quotation_H.TaxIncluded),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,quotation_H.Confirmed)
                        });

                dbMySQL.ExecuteNonQuery("delete from Quotation_D where QuotationType=@QuotationType and QuotationNo=@QuotationNo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("QuotationType",DbType.String,quotation_H.QuotationType),
                        DataAccessMySQL.CreateParameter("QuotationNo",DbType.String,quotation_H.QuotationNo)
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
                    string[] strPreferentialPriceArray = strPreferentialPrice.Split(',');
                    //string[] strSubtotalArray = strSubtotal.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');

                    sql = @"insert into Quotation_D (Company,UserGroup,Creator,
                        CreateDate,QuotationType,QuotationNo,ItemId,PartNo,PartName,
                        QTY,Unit,UnitPrice,PreferentialPrice,Subtotal,Remark) values 
                        (@Company,@UserGroup,@Creator,
                        @CreateDate,@QuotationType,QuotationNo,ItemId,PartNo,PartName,
                        QTY,Unit,UnitPrice,PreferentialPrice,Subtotal,Remark)";


                    for (int i = 0; i < strItemIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strItemIdArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,quotation_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,quotation_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,quotation_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("QuotationType",DbType.String,quotation_H.QuotationType),
                                DataAccessMySQL.CreateParameter("QuotationNo",DbType.String,quotation_H.QuotationNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PartNo",DbType.String,strPartNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PartName",DbType.String,strPartNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.Double,double.Parse(strQTYArray[i].ToString()==""?"0":strQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strUnitArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("UnitPrice",DbType.Double,double.Parse(strUnitPriceArray[i].ToString()==""?"0":strUnitPriceArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("PreferentialPrice",DbType.Double,double.Parse(strPreferentialPriceArray[i].ToString()==""?"0":strPreferentialPriceArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Subtotal",DbType.Double,double.Parse(strQTYArray[i].ToString()==""?"0":strQTYArray[i].ToString())*double.Parse(strPreferentialPriceArray[i].ToString()==""?"0":strPreferentialPriceArray[i].ToString())),
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

        public bool DelQuotation_H(Quotation_H quotation_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery("delete from Quotation_H where QuotationType=@QuotationType and QuotationNo=@QuotationNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("QuotationType",DbType.String,quotation_H.QuotationType),
                            DataAccessMySQL.CreateParameter("QuotationNo",DbType.String,quotation_H.QuotationNo)
                        });
                dbMySQL.ExecuteNonQuery("delete from Quotation_D where QuotationType=@QuotationType and QuotationNo=@QuotationNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("QuotationType",DbType.String,quotation_H.QuotationType),
                            DataAccessMySQL.CreateParameter("QuotationNo",DbType.String,quotation_H.QuotationNo)
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

        public List<Quotation_H> SearchQuotation_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<Quotation_H> objQuotation_H = new List<Quotation_H>();

            string sql = @"select h.*,o.OrderSName,c.CustomerName,e.EmployeeName as CreatorName from Quotation_H as h 
                left join OrderCategory as o on o.OrderType=h.QuotationType
                left join Customer as c on c.CustomerId=h.CustomerId
                left join Employee as e on e.EmployeeId=h.Creator where 1=1";

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


            DataTable dtQuotation_H = null;
            if (strCondition != "")
            {
                dtQuotation_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtQuotation_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtQuotation_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtQuotation_H.Rows)
                {
                    Quotation_H obj = new Quotation_H();
                    obj.QuotationType = dr["QuotationType"].ToString();
                    obj.OrderSName = dr["OrderSName"].ToString();
                    obj.QuotationNo = dr["QuotationNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    if (dr["StatusCode"].ToString() == "0")
                    {
                        obj.StatusCode = "未報價";
                    }
                    if (dr["StatusCode"].ToString() == "1")
                    {
                        obj.StatusCode = "已報價";
                    }
                    if (dr["StatusCode"].ToString() == "2")
                    {
                        obj.StatusCode = "已回覆";
                    }
                    if (dr["StatusCode"].ToString() == "3")
                    {
                        obj.StatusCode = "作廢";
                    }
                    if (dr["StatusCode"].ToString() == "4")
                    {
                        obj.StatusCode = "指定結案";
                    }
                    if (dr["StatusOfResponse"].ToString() == "0")
                    {
                        obj.StatusOfResponse = "未回復";
                    }
                    if (dr["StatusOfResponse"].ToString() == "1")
                    {
                        obj.StatusOfResponse = "允許";
                    }
                    if (dr["StatusOfResponse"].ToString() == "2")
                    {
                        obj.StatusOfResponse = "拒絕";
                    }
                    obj.Response = dr["Response"].ToString();
                    obj.Creator = dr["Creator"].ToString();
                    obj.CreatorName = dr["CreatorName"].ToString();
                    obj.TaxExcluded = dr["TaxExcluded"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    objQuotation_H.Add(obj);
                }
            }

            return objQuotation_H;
        }

        public Quotation_H GetQuotation_HInfo(Quotation_H quotation_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            Quotation_H obj = new Quotation_H();

            DataTable dtQuotation_H = dbMySQL.ExecuteDataTable(@"select h.*,o.OrderSName from Quotation_H as h 
                   left join OrderCategory as o on o.OrderType=h.QuotationType where 
                   QuotationType=@QuotationType and QuotationNo=@QuotationNo",
                   new DbParameter[]{
                       DataAccessMySQL.CreateParameter("QuotationType",DbType.String,quotation_H.QuotationType),
                       DataAccessMySQL.CreateParameter("QuotationNo",DbType.String,quotation_H.QuotationNo),
                   });

            if (dtQuotation_H.Rows.Count > 0)
            {
                obj.CreateDate = dtQuotation_H.Rows[0]["CreateDate"].ToString() == "" ? "" : DateTime.ParseExact(dtQuotation_H.Rows[0]["CreateDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.ModiDate = dtQuotation_H.Rows[0]["ModiDate"].ToString() == "" ? "" : DateTime.ParseExact(dtQuotation_H.Rows[0]["ModiDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.Confirmor = dtQuotation_H.Rows[0]["Confirmor"].ToString();
                obj.ConfirmedDate = dtQuotation_H.Rows[0]["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dtQuotation_H.Rows[0]["ConfirmedDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.QuotationType = dtQuotation_H.Rows[0]["QuotationType"].ToString();
                obj.OrderSName = dtQuotation_H.Rows[0]["OrderSName"].ToString();
                obj.QuotationNo = dtQuotation_H.Rows[0]["QuotationNo"].ToString();
                obj.OrderDate = dtQuotation_H.Rows[0]["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dtQuotation_H.Rows[0]["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                if (dtQuotation_H.Rows[0]["StatusCode"].ToString() == "0")
                {
                    obj.StatusCode = "0.未報價";
                }
                if (dtQuotation_H.Rows[0]["StatusCode"].ToString() == "1")
                {
                    obj.StatusCode = "1.已報價";
                }
                if (dtQuotation_H.Rows[0]["StatusCode"].ToString() == "2")
                {
                    obj.StatusCode = "2.已回覆";
                }
                if (dtQuotation_H.Rows[0]["StatusCode"].ToString() == "3")
                {
                    obj.StatusCode = "3.作廢";
                }
                if (dtQuotation_H.Rows[0]["StatusCode"].ToString() == "4")
                {
                    obj.StatusCode = "4.指定結案";
                }
                obj.NoOfPrints = int.Parse(dtQuotation_H.Rows[0]["NoOfPrints"].ToString());
                obj.Repairman = dtQuotation_H.Rows[0]["Repairman"].ToString();
                obj.SourceOrderType = dtQuotation_H.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtQuotation_H.Rows[0]["SourceOrderNo"].ToString();
                obj.Responser = dtQuotation_H.Rows[0]["Responser"].ToString();
                obj.StatusOfResponse = dtQuotation_H.Rows[0]["StatusOfResponse"].ToString();
                obj.ResponseDate = dtQuotation_H.Rows[0]["ResponseDate"].ToString();
                obj.Response = dtQuotation_H.Rows[0]["Response"].ToString();
                obj.CustomerId = dtQuotation_H.Rows[0]["CustomerId"].ToString();
                obj.TaxRate = dtQuotation_H.Rows[0]["TaxRate"].ToString();
                obj.AddressSName = dtQuotation_H.Rows[0]["AddressSName"].ToString();
                obj.Address = dtQuotation_H.Rows[0]["Address"].ToString();
                obj.Contact = dtQuotation_H.Rows[0]["Contact"].ToString();
                obj.TelNo = dtQuotation_H.Rows[0]["TelNo"].ToString();
                obj.FaxNo = dtQuotation_H.Rows[0]["FaxNo"].ToString();
                obj.ProductNo = dtQuotation_H.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtQuotation_H.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtQuotation_H.Rows[0]["SerialNo"].ToString();
                obj.AssetNo = dtQuotation_H.Rows[0]["AssetNo"].ToString();
                obj.UnderWarranty = dtQuotation_H.Rows[0]["UnderWarranty"].ToString();
                obj.SaleDate = dtQuotation_H.Rows[0]["SaleDate"].ToString() == "" ? "" : DateTime.ParseExact(dtQuotation_H.Rows[0]["SaleDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.PurchaseDate = dtQuotation_H.Rows[0]["PurchaseDate"].ToString() == "" ? "" : DateTime.ParseExact(dtQuotation_H.Rows[0]["PurchaseDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.WarrantyStartDate = dtQuotation_H.Rows[0]["WarrantyStartDate"].ToString() == "" ? "" : DateTime.ParseExact(dtQuotation_H.Rows[0]["WarrantyStartDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.WarrantyExpiryDate = dtQuotation_H.Rows[0]["WarrantyExpiryDate"].ToString() == "" ? "" : DateTime.ParseExact(dtQuotation_H.Rows[0]["WarrantyExpiryDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.Remark = dtQuotation_H.Rows[0]["Remark"].ToString();
                obj.TestResult = dtQuotation_H.Rows[0]["TestResult"].ToString();
                obj.InternalRemark = dtQuotation_H.Rows[0]["InternalRemark"].ToString();
                obj.TaxExcluded = dtQuotation_H.Rows[0]["TaxExcluded"].ToString();
                obj.Tax = dtQuotation_H.Rows[0]["Tax"].ToString();
                obj.TaxIncluded = dtQuotation_H.Rows[0]["TaxIncluded"].ToString();
                obj.Confirmed = dtQuotation_H.Rows[0]["Confirmed"].ToString();
            }

            return obj;
        }

        public bool ConfirmedQuotation_H(Quotation_H quotation_H, out string msg)
        {
            try
            {
                DataAccessMySQL dbMySQL = new DataAccessMySQL();
                dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

                dbMySQL.ExecuteNonQuery("update Quotation_H set Confirmed=@Confirmed,Confirmor=@Confirmor,ConfirmedDate=@ConfirmedDate,StatusCode=@StatusCode WHERE QuotationType=@QuotationType and QuotationNo=@QuotationNo",
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("QuotationType",DbType.String,quotation_H.QuotationType),
                        DataAccessMySQL.CreateParameter("QuotationNo",DbType.String,quotation_H.QuotationNo),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,quotation_H.Confirmed),
                        DataAccessMySQL.CreateParameter("Confirmor",DbType.String,quotation_H.Confirmor),
                        DataAccessMySQL.CreateParameter("StatusCode",DbType.String,quotation_H.StatusCode),
                        DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,quotation_H.ConfirmedDate)
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

    }
}