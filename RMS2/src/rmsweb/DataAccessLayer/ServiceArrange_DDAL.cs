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
    public class ServiceArrange_DDAL : IServiceArrange_DDAL
    {
        public List<ServiceArrange_D> SearchServiceArrange_D(string SerArrangeOrderType, string SerArrangeOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<ServiceArrange_D> objServiceArrange_D = new List<ServiceArrange_D>();

            string sql = @"select * from ServiceArrange_D  where SerArrangeOrderType=@SerArrangeOrderType and SerArrangeOrderNo=@SerArrangeOrderNo";


            DataTable dtServiceArrange_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,SerArrangeOrderType),
                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,SerArrangeOrderNo)
                }); ;

            if (dtServiceArrange_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtServiceArrange_D.Rows)
                {
                    ServiceArrange_D obj = new ServiceArrange_D();
                    obj.SerArrangeOrderType = dr["SerArrangeOrderType"].ToString();
                    obj.SerArrangeOrderNo = dr["SerArrangeOrderNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ArrangeMonth = dr["ArrangeMonth"].ToString();
                    obj.AddressSName = dr["AddressSName"].ToString();
                    obj.Address = dr["Address"].ToString();
                    if (dr["IsClosed"].ToString()=="N")
                    {
                        obj.IsClosed = "N.未拋轉";
                    }
                    if (dr["IsClosed"].ToString() == "Y")
                    {
                        obj.IsClosed = "Y.已拋轉";
                    }
                    if (dr["IsClosed"].ToString() == "F")
                    {
                        obj.IsClosed = "F.指定結案";
                    }
                    
                    obj.Remark = dr["Remark"].ToString();

                    objServiceArrange_D.Add(obj);
                }
            }

            return objServiceArrange_D;
        }

        public List<ServiceArrange_D> SearchServiceArrange_DInfo(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<ServiceArrange_D> objServiceArrange_D = new List<ServiceArrange_D>();

            string sql = @"select d.SerArrangeOrderType,d.SerArrangeOrderNo,d.ItemId,h.CustomerId,
            c.CustomerName,c.CustomerType,h.ProductNo,h.ProductName,h.SerialNo,WarrantyPeriod,h.WarrantySDate,h.WarrantyEDate from ServiceArrange_D as d 
            left join ServiceArrange_H as h on h.SerArrangeOrderType=d.SerArrangeOrderType and h.SerArrangeOrderNo=d.SerArrangeOrderNo
            left join customer as c on c.CustomerId=h.CustomerId where d.IsClosed='N'";

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


            DataTable dtServiceArrange_D = null;
            if (strCondition != "")
            {
                dtServiceArrange_D = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtServiceArrange_D = dbMySQL.ExecuteDataTable(sql);
            }

            if (dtServiceArrange_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtServiceArrange_D.Rows)
                {
                    ServiceArrange_D obj = new ServiceArrange_D();
                    obj.SerArrangeOrderType = dr["SerArrangeOrderType"].ToString();
                    obj.SerArrangeOrderNo = dr["SerArrangeOrderNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.CustomerType = dr["CustomerType"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.WarrantyPeriod = dr["WarrantyPeriod"].ToString();
                    obj.WarrantySDate = dr["WarrantySDate"].ToString();
                    obj.WarrantyEDate = dr["WarrantyEDate"].ToString();

                    objServiceArrange_D.Add(obj);
                }
            }

            return objServiceArrange_D;
        }

        public bool ChangeRoutineSdervice(RoutineService_H RoutineService_H, string ServiceArrangeId, string chkRemark)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                string[] ServiceArrangeIdArray = ServiceArrangeId.Split(',');

                string[] valueInfo = null;
                string[] orderInfo = null;

                for (int i = 0; i < ServiceArrangeIdArray.Length; i++)
                {
                    valueInfo = ServiceArrangeIdArray[i].Split('-');

                    orderInfo = GetSupportAplOrderNo(RoutineService_H.RoutineSerOrderType).Split('-');

                    if (orderInfo[0].ToString() == "None")
                    {
                        return false;
                    }

                    dbMySQL.ExecuteNonQuery(@"update ServiceArrange_D set IsClosed='Y' where SerArrangeOrderType=@SerArrangeOrderType 
                        and SerArrangeOrderNo=@SerArrangeOrderNo and ItemId=@ItemId", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,valueInfo[2].ToString())
                        });
                    DataTable dtServiceArrange_H = dbMySQL.ExecuteDataTable(@"select h.*,d.ItemId,d.ArrangeMonth,
                        d.AddressSName,d.Address,d.IsClosed,d.Remark,c.CustomerName,c.AddressSName,c.Address,c.Contact,
                        c.TelNo,c.FaxNo from ServiceArrange_H as h 
                        right join ServiceArrange_D as d on h.SerArrangeOrderType=d.SerArrangeOrderType and 
                        h.SerArrangeOrderNo=d.SerArrangeOrderNo 
                        left join customer as c on c.CustomerId=h.CustomerId
                        where h.SerArrangeOrderType=@SerArrangeOrderType 
                        and h.SerArrangeOrderNo=@SerArrangeOrderNo and d.ItemId=@ItemId", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,valueInfo[2].ToString())
                        });
                    if (dtServiceArrange_H != null && dtServiceArrange_H.Rows.Count > 0)
                    {
                        dbMySQL.ExecuteNonQuery(@"update ServiceArrangeModi_D set NewIsClosed='Y' where SerArrangeOrderType=@SerArrangeOrderType 
                        and SerArrangeOrderNo=@SerArrangeOrderNo and ItemId=@ItemId and Version=@Version", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String,valueInfo[2].ToString()),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,dtServiceArrange_H.Rows[0]["Version"].ToString())
                        });

                        if(chkRemark=="Y")
                        {
                            dbMySQL.ExecuteNonQuery(@"insert into routineservice_h (RoutineSerOrderType,RoutineSerOrderNo,OrderDate,ConfirmedDate,
                            SourceOrderType,SourceOrderNo,SourceOrderItemId,ArrangeMonth,CustomerId,AddressSName,Address,Contact,TelNo,FaxNo,ProductNo,
                            ProductName,SerialNo,SaleDate,WarrantySDate,WarrantyEDate,Confirmed,Remark,InternalRemark,NoOfPrints) 
                            VALUES (@RoutineSerOrderType,@RoutineSerOrderNo,@OrderDate,@ConfirmedDate,
                            @SourceOrderType,@SourceOrderNo,@SourceOrderItemId,@ArrangeMonth,@CustomerId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo,@ProductNo,
                            @ProductName,@SerialNo,@SaleDate,@WarrantySDate,@WarrantyEDate,@Confirmed,@Remark,@InternalRemark,@NoOfPrints)", tran,
                                                        new DbParameter[]{
                                DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,RoutineService_H.RoutineSerOrderType),
                                DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,orderInfo[1].ToString()),
                                DataAccessMySQL.CreateParameter("OrderDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,RoutineService_H.ConfirmedDate),
                                DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,valueInfo[0].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,valueInfo[1].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,valueInfo[2].ToString()),
                                DataAccessMySQL.CreateParameter("ArrangeMonth",DbType.String,dtServiceArrange_H.Rows[0]["ArrangeMonth"].ToString()),
                                DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtServiceArrange_H.Rows[0]["CustomerId"].ToString()),
                                DataAccessMySQL.CreateParameter("AddressSName",DbType.String,dtServiceArrange_H.Rows[0]["AddressSName"].ToString()),
                                DataAccessMySQL.CreateParameter("Address",DbType.String,dtServiceArrange_H.Rows[0]["Address"].ToString()),
                                DataAccessMySQL.CreateParameter("Contact",DbType.String,dtServiceArrange_H.Rows[0]["Contact"].ToString()),
                                DataAccessMySQL.CreateParameter("TelNo",DbType.String,dtServiceArrange_H.Rows[0]["TelNo"].ToString()),
                                DataAccessMySQL.CreateParameter("FaxNo",DbType.String,dtServiceArrange_H.Rows[0]["FaxNo"].ToString()),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,dtServiceArrange_H.Rows[0]["ProductNo"].ToString()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,dtServiceArrange_H.Rows[0]["ProductName"].ToString()),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,dtServiceArrange_H.Rows[0]["SerialNo"].ToString()),
                                DataAccessMySQL.CreateParameter("SaleDate",DbType.String,dtServiceArrange_H.Rows[0]["SaleDate"].ToString()),
                                DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,dtServiceArrange_H.Rows[0]["WarrantySDate"].ToString()),
                                DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,dtServiceArrange_H.Rows[0]["WarrantyEDate"].ToString()),
                                DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"N"),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,dtServiceArrange_H.Rows[0]["Remark"].ToString()),
                                DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,dtServiceArrange_H.Rows[0]["InternalRemark"].ToString()),
                                DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,0),
                            });
                        }else
                        {
                            dbMySQL.ExecuteNonQuery(@"insert into routineservice_h (RoutineSerOrderType,RoutineSerOrderNo,OrderDate,ConfirmedDate,
                            SourceOrderType,SourceOrderNo,SourceOrderItemId,ArrangeMonth,CustomerId,AddressSName,Address,Contact,TelNo,FaxNo,ProductNo,
                            ProductName,SerialNo,SaleDate,WarrantySDate,WarrantyEDate,Confirmed,Remark,NoOfPrints) 
                            VALUES (@RoutineSerOrderType,@RoutineSerOrderNo,@OrderDate,@ConfirmedDate,
                            @SourceOrderType,@SourceOrderNo,@SourceOrderItemId,@ArrangeMonth,@CustomerId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo,@ProductNo,
                            @ProductName,@SerialNo,@SaleDate,@WarrantySDate,@WarrantyEDate,@Confirmed,@Remark,@NoOfPrints)", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,RoutineService_H.RoutineSerOrderType),
                                DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,orderInfo[1].ToString()),
                                DataAccessMySQL.CreateParameter("OrderDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,valueInfo[0].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,valueInfo[1].ToString()),
                                DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,valueInfo[2].ToString()),
                                DataAccessMySQL.CreateParameter("ArrangeMonth",DbType.String,dtServiceArrange_H.Rows[0]["ArrangeMonth"].ToString()),
                                DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtServiceArrange_H.Rows[0]["CustomerId"].ToString()),
                                DataAccessMySQL.CreateParameter("AddressSName",DbType.String,dtServiceArrange_H.Rows[0]["AddressSName"].ToString()),
                                DataAccessMySQL.CreateParameter("Address",DbType.String,dtServiceArrange_H.Rows[0]["Address"].ToString()),
                                DataAccessMySQL.CreateParameter("Contact",DbType.String,dtServiceArrange_H.Rows[0]["Contact"].ToString()),
                                DataAccessMySQL.CreateParameter("TelNo",DbType.String,dtServiceArrange_H.Rows[0]["TelNo"].ToString()),
                                DataAccessMySQL.CreateParameter("FaxNo",DbType.String,dtServiceArrange_H.Rows[0]["FaxNo"].ToString()),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,dtServiceArrange_H.Rows[0]["ProductNo"].ToString()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,dtServiceArrange_H.Rows[0]["ProductName"].ToString()),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,dtServiceArrange_H.Rows[0]["SerialNo"].ToString()),
                                DataAccessMySQL.CreateParameter("SaleDate",DbType.String,dtServiceArrange_H.Rows[0]["SaleDate"].ToString()),
                                DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,dtServiceArrange_H.Rows[0]["WarrantySDate"].ToString()),
                                DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,dtServiceArrange_H.Rows[0]["WarrantyEDate"].ToString()),
                                DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"N"),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,dtServiceArrange_H.Rows[0]["Remark"].ToString()),
                                DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,0),
                            });
                        }
                        
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
                        OrderNo = ManuscriptNum + (int.Parse(ManuscripNo.ToString().Substring(8)) + 1).ToString("000000000000".Substring(0, int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString())));
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
                        OrderNo = ManuscriptNum + (int.Parse(ManuscripNo.ToString().Substring(6)) + 1).ToString("000000000000".Substring(0, int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString())));
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
                return "None-";
            }

        }
    }
}