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
    public class ServiceArrange_HDAL : IServiceArrange_HDAL
    {
        public bool AddServiceArrange_H(ServiceArrange_H serviceArrange_H, string strItemId,
            string strArrangeMonth, string strAddressSName, string strAddress,
            string strIsClosed, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from ServiceArrange_H where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrange_H.SerArrangeOrderNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select AutoConfirm from OrderCategory where OrderType=@OrderType", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("OrderType",DbType.String,serviceArrange_H.SerArrangeOrderType)
                        });
                    string AutoConfirm = "N";
                    if (dtOrderCategory != null && dtOrderCategory.Rows.Count > 0)
                    {
                        if (dtOrderCategory.Rows[0]["AutoConfirm"].ToString() == "Y")
                        {
                            AutoConfirm = "Y";
                        }
                    }
                    dbMySQL.ExecuteNonQuery(@"insert into ServiceArrange_H (Company,UserGroup,Creator,CreateDate,
                        SerArrangeOrderType,SerArrangeOrderNo,OrderDate,ConfirmedDate,CustomerId,Version,
                        SourceOrderType,SourceOrderNo,SourceOrderItemId,ProductNo,ProductName,SerialNo,
                        SaleDate,RoutineServiceFreq,WarrantyPeriod,WarrantySDate,WarrantyEDate,Confirmed,
                        Confirmor,Remark,InternalRemark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @SerArrangeOrderType,@SerArrangeOrderNo,@OrderDate,@ConfirmedDate,@CustomerId,@Version,
                        @SourceOrderType,@SourceOrderNo,@SourceOrderItemId,@ProductNo,@ProductName,@SerialNo,
                        @SaleDate,@RoutineServiceFreq,@WarrantyPeriod,@WarrantySDate,@WarrantyEDate,@Confirmed,
                        @Confirmor,@Remark,@InternalRemark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,serviceArrange_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,serviceArrange_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,serviceArrange_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrange_H.SerArrangeOrderNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,serviceArrange_H.OrderDate),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,serviceArrange_H.ConfirmedDate),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,serviceArrange_H.CustomerId),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,serviceArrange_H.Version),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,serviceArrange_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,serviceArrange_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,serviceArrange_H.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,serviceArrange_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,serviceArrange_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,serviceArrange_H.SerialNo),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,serviceArrange_H.SaleDate),
                            DataAccessMySQL.CreateParameter("RoutineServiceFreq",DbType.String,serviceArrange_H.RoutineServiceFreq),
                            DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.String,serviceArrange_H.WarrantyPeriod),
                            DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,serviceArrange_H.WarrantySDate),
                            DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,serviceArrange_H.WarrantyEDate),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,AutoConfirm),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,serviceArrange_H.Confirmor),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,serviceArrange_H.Remark),
                            DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,serviceArrange_H.InternalRemark)
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

                        sql = @"insert into ServiceArrange_D (Company,UserGroup,Creator,
                        CreateDate,SerArrangeOrderType,SerArrangeOrderNo,ItemId,ArrangeMonth,AddressSName,
                        Address,IsClosed,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@SerArrangeOrderType,@SerArrangeOrderNo,@ItemId,@ArrangeMonth,@AddressSName,
                        @Address,@IsClosed,@Remark)";


                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,serviceArrange_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,serviceArrange_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,serviceArrange_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                                DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrange_H.SerArrangeOrderNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ArrangeMonth",DbType.String,strArrangeMonthArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("AddressSName",DbType.String,strAddressSNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Address",DbType.String,strAddressArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("IsClosed",DbType.String,strIsClosedArray[i].ToString().Substring(0,1)),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
                            });
                        }
                    }
                    if(!String.IsNullOrEmpty(serviceArrange_H.SourceOrderType))
                    {
                        dbMySQL.ExecuteNonQuery(@"update Contract_ProductSerial set IsClosed=@IsClosed
                         where ContractType=@ContractType and ContractNo=@ContractNo and ItemId=@ItemId and SerialNo=@SerialNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("IsClosed",DbType.String,"Y"),
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,serviceArrange_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,serviceArrange_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,serviceArrange_H.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,serviceArrange_H.SerialNo)
                        });
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

        public bool UpdateServiceArrange_H(ServiceArrange_H serviceArrange_H, string strItemId,
            string strArrangeMonth, string strAddressSName, string strAddress,
            string strIsClosed, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                DataTable dtServiceArrange_H = dbMySQL.ExecuteDataTable(@"select * from ServiceArrange_H where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrange_H.SerArrangeOrderNo)
                    });
                if(dtServiceArrange_H!=null&&dtServiceArrange_H.Rows.Count>0)
                {
                    if (!String.IsNullOrEmpty(dtServiceArrange_H.Rows[0]["SourceOrderType"].ToString()))
                    {
                        dbMySQL.ExecuteNonQuery(@"update Contract_ProductSerial set IsClosed=@IsClosed
                         where ContractType=@ContractType and ContractNo=@ContractNo and ItemId=@ItemId and SerialNo=@SerialNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("IsClosed",DbType.String,"N"),
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,dtServiceArrange_H.Rows[0]["SourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,dtServiceArrange_H.Rows[0]["SourceOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,dtServiceArrange_H.Rows[0]["SourceOrderItemId"].ToString()),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,dtServiceArrange_H.Rows[0]["SerialNo"].ToString())
                        });
                    }
                }
                dbMySQL.ExecuteNonQuery(@"update ServiceArrange_H set Modifier=@Modifier,
                        ModiDate=@ModiDate,OrderDate=@OrderDate,ConfirmedDate=@ConfirmedDate,
                        CustomerId=@CustomerId,Version=@Version,SourceOrderType=@SourceOrderType,
                        SourceOrderNo=@SourceOrderNo,SourceOrderItemId=@SourceOrderItemId,
                        ProductNo=@ProductNo,ProductName=@ProductName,SerialNo=@SerialNo,
                        SaleDate=@SaleDate,RoutineServiceFreq=@RoutineServiceFreq,
                        WarrantyPeriod=@WarrantyPeriod,WarrantySDate=@WarrantySDate,
                        WarrantyEDate=@WarrantyEDate,
                        Remark=@Remark,InternalRemark=@InternalRemark where 
                        SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,serviceArrange_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrange_H.SerArrangeOrderNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,serviceArrange_H.OrderDate),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,serviceArrange_H.ConfirmedDate),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,serviceArrange_H.CustomerId),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,serviceArrange_H.Version),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,serviceArrange_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,serviceArrange_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,serviceArrange_H.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,serviceArrange_H.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,serviceArrange_H.ProductName),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,serviceArrange_H.SerialNo),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,serviceArrange_H.SaleDate),
                            DataAccessMySQL.CreateParameter("RoutineServiceFreq",DbType.String,serviceArrange_H.RoutineServiceFreq),
                            DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.String,serviceArrange_H.WarrantyPeriod),
                            DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,serviceArrange_H.WarrantySDate),
                            DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,serviceArrange_H.WarrantyEDate),
                            //DataAccessMySQL.CreateParameter("Confirmed",DbType.String,serviceArrange_H.Confirmed),
                            //DataAccessMySQL.CreateParameter("Confirmor",DbType.String,serviceArrange_H.Confirmor),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,serviceArrange_H.Remark),
                            DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,serviceArrange_H.InternalRemark)
                        });
                dbMySQL.ExecuteNonQuery("delete from ServiceArrange_D where SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrange_H.SerArrangeOrderNo)
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

                    sql = @"insert into ServiceArrange_D (Company,UserGroup,Creator,
                        CreateDate,SerArrangeOrderType,SerArrangeOrderNo,ItemId,ArrangeMonth,AddressSName,
                        Address,IsClosed,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@SerArrangeOrderType,@SerArrangeOrderNo,@ItemId,@ArrangeMonth,@AddressSName,
                        @Address,@IsClosed,@Remark)";


                    for (int i = 0; i < strItemIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strItemIdArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,serviceArrange_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,serviceArrange_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,serviceArrange_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                                DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrange_H.SerArrangeOrderNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ArrangeMonth",DbType.String,strArrangeMonthArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("AddressSName",DbType.String,strAddressSNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Address",DbType.String,strAddressArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("IsClosed",DbType.String,strIsClosedArray[i].ToString().Substring(0,1)),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
                            });
                    }
                }

                if (!String.IsNullOrEmpty(serviceArrange_H.SourceOrderType))
                {
                    dbMySQL.ExecuteNonQuery(@"update Contract_ProductSerial set IsClosed=@IsClosed
                         where ContractType=@ContractType and ContractNo=@ContractNo and ItemId=@ItemId and SerialNo=@SerialNo", tran,
                    new DbParameter[]{
                            DataAccessMySQL.CreateParameter("IsClosed",DbType.String,"Y"),
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,serviceArrange_H.SourceOrderType),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,serviceArrange_H.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,serviceArrange_H.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,serviceArrange_H.SerialNo)
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

        public bool DelServiceArrange_H(ServiceArrange_H serviceArrange_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                DataTable dtServiceArrange_H = dbMySQL.ExecuteDataTable(@"select * from ServiceArrange_H where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrange_H.SerArrangeOrderNo)
                    });
                if (dtServiceArrange_H != null && dtServiceArrange_H.Rows.Count > 0)
                {
                    if (!String.IsNullOrEmpty(dtServiceArrange_H.Rows[0]["SourceOrderType"].ToString()))
                    {
                        dbMySQL.ExecuteNonQuery(@"update Contract_ProductSerial set IsClosed=@IsClosed
                         where ContractType=@ContractType and ContractNo=@ContractNo and ItemId=@ItemId and SerialNo=@SerialNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("IsClosed",DbType.String,"N"),
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,dtServiceArrange_H.Rows[0]["SourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,dtServiceArrange_H.Rows[0]["SourceOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,dtServiceArrange_H.Rows[0]["SourceOrderItemId"].ToString()),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,dtServiceArrange_H.Rows[0]["SerialNo"].ToString())
                        });
                    }
                }

                dbMySQL.ExecuteNonQuery("delete from ServiceArrange_H where SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrange_H.SerArrangeOrderNo)
                        });
                dbMySQL.ExecuteNonQuery("delete from ServiceArrange_D where SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrange_H.SerArrangeOrderNo)
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

        public List<ServiceArrange_H> SearchServiceArrange_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<ServiceArrange_H> objServiceArrange_H = new List<ServiceArrange_H>();

            string sql = @"select s.SerArrangeOrderType,s.SerArrangeOrderNo,s.CustomerId,
                c.CustomerName,c.CustomerType,s.ProductNo,s.ProductName,s.SerialNo,
                s.WarrantySDate,s.WarrantyEDate,Confirmed from ServiceArrange_H as s
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


            DataTable dtServiceArrange_H = null;
            if (strCondition != "")
            {
                dtServiceArrange_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtServiceArrange_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtServiceArrange_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtServiceArrange_H.Rows)
                {
                    ServiceArrange_H obj = new ServiceArrange_H();
                    obj.SerArrangeOrderType = dr["SerArrangeOrderType"].ToString();
                    obj.SerArrangeOrderNo = dr["SerArrangeOrderNo"].ToString();
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
                    obj.Confirmed = dr["Confirmed"].ToString();
                    objServiceArrange_H.Add(obj);
                }
            }

            return objServiceArrange_H;
        }

        public ServiceArrange_H GetServiceArrange_HInfo(ServiceArrange_H serviceArrange_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            ServiceArrange_H obj = null;

            DataTable dtServiceArrange_H = dbMySQL.ExecuteDataTable(@"select h.*,c.CustomerName,c.CustomerType from servicearrange_h as h 
                    left join Customer as c on h.CustomerId=c.CustomerId where 
                    SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrange_H.SerArrangeOrderNo)
                    });

            if (dtServiceArrange_H.Rows.Count > 0)
            {
                obj = new ServiceArrange_H();
                obj.SerArrangeOrderType = dtServiceArrange_H.Rows[0]["SerArrangeOrderType"].ToString();
                obj.SerArrangeOrderNo = dtServiceArrange_H.Rows[0]["SerArrangeOrderNo"].ToString();
                obj.OrderDate = dtServiceArrange_H.Rows[0]["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dtServiceArrange_H.Rows[0]["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.ConfirmedDate = dtServiceArrange_H.Rows[0]["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dtServiceArrange_H.Rows[0]["ConfirmedDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.CustomerId = dtServiceArrange_H.Rows[0]["CustomerId"].ToString();
                obj.CustomerName = dtServiceArrange_H.Rows[0]["CustomerName"].ToString();
                if(dtServiceArrange_H.Rows[0]["CustomerType"].ToString()!="")
                {
                    obj.CustomerType = dtServiceArrange_H.Rows[0]["CustomerType"].ToString() == "A" ? "A:一般客戶" : "B:經銷商";
                }
                else
                {
                    obj.CustomerType = "";
                }

                obj.Version = dtServiceArrange_H.Rows[0]["Version"].ToString();
                obj.SourceOrderType = dtServiceArrange_H.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtServiceArrange_H.Rows[0]["SourceOrderNo"].ToString();
                obj.SourceOrderItemId = dtServiceArrange_H.Rows[0]["SourceOrderItemId"].ToString();
                obj.ProductNo = dtServiceArrange_H.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtServiceArrange_H.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtServiceArrange_H.Rows[0]["SerialNo"].ToString();
                obj.SaleDate = dtServiceArrange_H.Rows[0]["SaleDate"].ToString() == "" ? "" : DateTime.ParseExact(dtServiceArrange_H.Rows[0]["SaleDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.RoutineServiceFreq = dtServiceArrange_H.Rows[0]["RoutineServiceFreq"].ToString();
                obj.WarrantyPeriod = dtServiceArrange_H.Rows[0]["WarrantyPeriod"].ToString();
                obj.WarrantySDate = dtServiceArrange_H.Rows[0]["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dtServiceArrange_H.Rows[0]["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.WarrantyEDate = dtServiceArrange_H.Rows[0]["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dtServiceArrange_H.Rows[0]["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                if (dtServiceArrange_H.Rows[0]["Confirmed"].ToString()=="Y")
                {
                    obj.Confirmed = "Y:已確認";
                }
                if (dtServiceArrange_H.Rows[0]["Confirmed"].ToString() == "N")
                {
                    obj.Confirmed = "N:未確認";
                }
                if (dtServiceArrange_H.Rows[0]["Confirmed"].ToString() == "V")
                {
                    obj.Confirmed = "V:作廢";
                }
                obj.Confirmor = dtServiceArrange_H.Rows[0]["Confirmor"].ToString();
                obj.Remark = dtServiceArrange_H.Rows[0]["Remark"].ToString();
                obj.InternalRemark = dtServiceArrange_H.Rows[0]["InternalRemark"].ToString();
            }

            return obj;
        }

        public List<ServiceArrange_H> GetServiceArrange_H(ServiceArrange_H serviceArrange_H, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<ServiceArrange_H> objServiceArrange_H = new List<ServiceArrange_H>();

            string sql = @"select s.*,c.CustomerName,c.CustomerType from ServiceArrange_H as s 
            left join customer as c on 
            c.CustomerId=s.CustomerId where Confirmed='Y'";

            if (!String.IsNullOrEmpty(serviceArrange_H.SerArrangeOrderType))
            {
                sql += " and SerArrangeOrderType like @SerArrangeOrderType";
            }
            if (!String.IsNullOrEmpty(serviceArrange_H.SerArrangeOrderNo))
            {
                sql += " and SerArrangeOrderNo like @SerArrangeOrderNo";
            }
            if (!string.IsNullOrEmpty(serviceArrange_H.SerialNo))
            {
                sql += " and SerialNo=@SerialNo";
            }
            if (!string.IsNullOrEmpty(serviceArrange_H.ProductNo))
            {
                sql += " and ProductNo=@ProductNo";
            }

            if (Page != 0)
            {
                sql += " limit @Page1,@Page2";
            }

            DataTable dtServiceArrange_H = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,"%"+serviceArrange_H.SerArrangeOrderType+"%"),
                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,"%"+serviceArrange_H.SerArrangeOrderNo+"%"),
                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,serviceArrange_H.SerialNo),
                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,serviceArrange_H.ProductNo),
                    DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
                    DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
                });
            if (dtServiceArrange_H.Rows.Count > 0)
            {

                foreach (DataRow dr in dtServiceArrange_H.Rows)
                {
                    ServiceArrange_H obj = new ServiceArrange_H();
                    obj.SerArrangeOrderType = dr["SerArrangeOrderType"].ToString();
                    obj.SerArrangeOrderNo = dr["SerArrangeOrderNo"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.ConfirmedDate = dr["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ConfirmedDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.CustomerType = dr["CustomerType"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    objServiceArrange_H.Add(obj);
                }
            }

            return objServiceArrange_H;
        }


        public List<ServiceArrange_H> GetServiceArrange_HSerialNo(ServiceArrange_H serviceArrange_H, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<ServiceArrange_H> objServiceArrange_H = new List<ServiceArrange_H>();

            string sql = @"select s.*,d.ItemId,d.ArrangeMonth,d.AddressSName,d.Address,d.IsClosed,
            c.CustomerName,c.CustomerType from ServiceArrange_H as s 
            right join ServiceArrange_D as d on s.SerArrangeOrderType=d.SerArrangeOrderType and 
            s.SerArrangeOrderNo=d.SerArrangeOrderNo left join customer as c on 
            c.CustomerId=s.CustomerId where Confirmed<>'V'";

            if (!String.IsNullOrEmpty(serviceArrange_H.SerArrangeOrderType))
            {
                sql += " and SerArrangeOrderType like @SerArrangeOrderType";
            }
            if (!String.IsNullOrEmpty(serviceArrange_H.SerArrangeOrderNo))
            {
                sql += " and SerArrangeOrderNo like @SerArrangeOrderNo";
            }
            if (!string.IsNullOrEmpty(serviceArrange_H.SerialNo))
            {
                sql += " and SerialNo=@SerialNo";
            }
            if (!string.IsNullOrEmpty(serviceArrange_H.ProductNo))
            {
                sql += " and ProductNo=@ProductNo";
            }

            if (Page != 0)
            {
                sql += " limit @Page1,@Page2";
            }

            DataTable dtServiceArrange_H = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,"%"+serviceArrange_H.SerArrangeOrderType+"%"),
                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,"%"+serviceArrange_H.SerArrangeOrderNo+"%"),
                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,serviceArrange_H.SerialNo),
                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,serviceArrange_H.ProductNo),
                    DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
                    DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
                });
            if (dtServiceArrange_H.Rows.Count > 0)
            {

                foreach (DataRow dr in dtServiceArrange_H.Rows)
                {
                    ServiceArrange_H obj = new ServiceArrange_H();
                    obj.SerArrangeOrderType = dr["SerArrangeOrderType"].ToString();
                    obj.SerArrangeOrderNo = dr["SerArrangeOrderNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.ConfirmedDate = dr["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dtServiceArrange_H.Rows[0]["ConfirmedDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.CustomerType = dr["CustomerType"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.ArrangeMonth = dr["ArrangeMonth"].ToString();
                    obj.SaleDate = dr["SaleDate"].ToString() == "" ? "" : DateTime.ParseExact(dtServiceArrange_H.Rows[0]["SaleDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.WarrantySDate = dr["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dtServiceArrange_H.Rows[0]["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.WarrantyEDate = dr["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dtServiceArrange_H.Rows[0]["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.Remark = dr["Remark"].ToString();
                    obj.InternalRemark = dr["InternalRemark"].ToString();
                    objServiceArrange_H.Add(obj);
                }
            }

            return objServiceArrange_H;
        }

        public void UpdateConfirmed(ServiceArrange_H serviceArrange_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            dbMySQL.ExecuteNonQuery(@"update ServiceArrange_H set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,ModiDate=@ModiDate,Confirmed=@Confirmed,
                    Confirmor=@Confirmor where SerArrangeOrderType=@SerArrangeOrderType 
                    and SerArrangeOrderNo=@SerArrangeOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("Company",DbType.String,serviceArrange_H.Company),
                        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,serviceArrange_H.UserGroup),
                        DataAccessMySQL.CreateParameter("Modifier",DbType.String,serviceArrange_H.Modifier),
                        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                        DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,serviceArrange_H.SerArrangeOrderNo),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String, serviceArrange_H.Confirmed),
                        DataAccessMySQL.CreateParameter("Confirmor",DbType.String, serviceArrange_H.Confirmor)
                    });

        }
    }
}