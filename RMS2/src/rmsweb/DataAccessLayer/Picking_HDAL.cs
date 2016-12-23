using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Globalization;

namespace DataAccessLayer
{
    public class Picking_HDAL : IPicking_HDAL
    {
        public bool AddPicking_H(Picking_H picking_H, string strPicking_ProductD_ProductNo, string strPicking_ProductD_ProductName,
            string strPicking_ProductD_QTY, string strPicking_ProductD_PickingQTY, string strPicking_ProductD_Unit, string strPicking_ProductD_Remark,
            out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                string count = dbMySQL.ExecuteScalar(@"select count(*) from picking_h where PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
                        DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo)
                    }).ToString();
                string AutoConfirm = "";
                if (int.Parse(count) == 0)
                {
                    tran = dbMySQL.CreateDbTransaction();
                    string sql = "";

                    if (strPicking_ProductD_ProductNo != "")
                    {
                        string[] strPickingProductDProductNoArray = strPicking_ProductD_ProductNo.Split(',');
                        string[] strPickingProductDProductNameArray = strPicking_ProductD_ProductName.Split(',');
                        string[] strPickingProductDQTYArray = strPicking_ProductD_QTY.Split(',');
                        string[] strPickingProductDPickingQTYArray = strPicking_ProductD_PickingQTY.Split(',');
                        string[] strPickingProductDUnitArray = strPicking_ProductD_Unit.Split(',');
                        string[] strPickingProductDRemarkArray = strPicking_ProductD_Remark.Split(',');

                        sql = @"insert into Picking_ProductD (Company,UserGroup,Creator,CreateDate,PickingOrderType,PickingOrderNo,
                        ItemId,ProductNo,ProductName,QTY,PickingQTY,Unit,Remark) values (@Company,@UserGroup,@Creator,@CreateDate,
                        @PickingOrderType,@PickingOrderNo,@ItemId,@ProductNo,@ProductName,@QTY,@PickingQTY,@Unit,@Remark)";

                        for (int i = 0; i < strPickingProductDProductNoArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strPickingProductDProductNoArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,picking_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,picking_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,picking_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
                                DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strPickingProductDProductNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,strPickingProductDProductNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.String,strPickingProductDQTYArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PickingQTY",DbType.Int32,strPickingProductDPickingQTYArray[i].ToString()==""?0:int.Parse(strPickingProductDPickingQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strPickingProductDUnitArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strPickingProductDRemarkArray[i].ToString())
                            });

                            if(AutoConfirm=="")
                            {
                                if(double.Parse(dbMySQL.ExecuteScalar("select count(*) from Picking_ProductSerial where PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo and ItemId=@ItemId",tran,
                                    new DbParameter[] {
                                        DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
                                        DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo),
                                        DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000"))
                                    }).ToString())!= double.Parse(strPickingProductDQTYArray[i].ToString()))
                                {
                                    AutoConfirm = "N";
                                }
                            }

                        }
                    }
                    if (AutoConfirm == "")
                    {
                        AutoConfirm = dbMySQL.ExecuteScalar("select AutoConfirm from OrderCategory where OrderType=@OrderType", tran,
                       new DbParameter[] {
                            DataAccessMySQL.CreateParameter("OrderType",DbType.String,picking_H.PickingOrderType)
                       }).ToString();
                    }
                    dbMySQL.ExecuteNonQuery(@"insert into picking_h (Company,UserGroup,Creator,CreateDate,PickingOrderType,PickingOrderNo,
                        OrderDate,SupportAplOrderType,SupportAplOrderNo,CustomerId,CustomerType,Sales,RequestDate,RequestTime,SupportDept,
                        PickingDate,PickingMan,Remark,Confirmed) values (@Company,@UserGroup,@Creator,@CreateDate,@PickingOrderType,@PickingOrderNo,
                        @OrderDate,@SupportAplOrderType,@SupportAplOrderNo,@CustomerId,@CustomerType,@Sales,@RequestDate,@RequestTime,
                        @SupportDept,@PickingDate,@PickingMan,@Remark,@Confirmed)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,picking_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,picking_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,picking_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
                            DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,picking_H.OrderDate),
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,picking_H.SupportAplOrderType),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,picking_H.SupportAplOrderNo),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,picking_H.CustomerId),
                            DataAccessMySQL.CreateParameter("CustomerType",DbType.String,picking_H.CustomerType),
                            DataAccessMySQL.CreateParameter("Sales",DbType.String,picking_H.Sales),
                            DataAccessMySQL.CreateParameter("RequestDate",DbType.String,picking_H.RequestDate),
                            DataAccessMySQL.CreateParameter("RequestTime",DbType.String,picking_H.RequestTime),
                            DataAccessMySQL.CreateParameter("SupportDept",DbType.String,picking_H.SupportDept),
                            DataAccessMySQL.CreateParameter("PickingDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("PickingMan",DbType.String,picking_H.PickingMan),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,picking_H.Remark),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,AutoConfirm)
                        });

                    dbMySQL.ExecuteNonQuery(@"update SupportApl_H set IsPicking=@IsPicking where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,picking_H.SupportAplOrderType),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,picking_H.SupportAplOrderNo),
                            DataAccessMySQL.CreateParameter("IsPicking",DbType.String,AutoConfirm)
                        });
                    tran.Commit();
                    msg = "";
                    return true;
                }else
                {
                    msg = "單號已存在！";
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

        //public bool UpdatePicking_H(Picking_H picking_H, string strPicking_ProductD_ProductNo, string strPicking_ProductD_ProductName,
        //    string strPicking_ProductD_QTY, string strPicking_ProductD_PickingQTY, string strPicking_ProductD_Unit, string strPicking_ProductD_Remark,
        //    out string msg)
        //{
        //    DataAccessMySQL dbMySQL = new DataAccessMySQL();
        //    dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
        //    DbTransaction tran = null;
        //    try
        //    {
        //        string AutoConfirm = "";
        //        tran = dbMySQL.CreateDbTransaction();
        //        string sql = "";

        //            if (strPicking_ProductD_ProductNo != "")
        //            {
        //                string[] strPickingProductDProductNoArray = strPicking_ProductD_ProductNo.Split(',');
        //                string[] strPickingProductDProductNameArray = strPicking_ProductD_ProductName.Split(',');
        //                string[] strPickingProductDQTYArray = strPicking_ProductD_QTY.Split(',');
        //                string[] strPickingProductDPickingQTYArray = strPicking_ProductD_PickingQTY.Split(',');
        //                string[] strPickingProductDUnitArray = strPicking_ProductD_Unit.Split(',');
        //                string[] strPickingProductDRemarkArray = strPicking_ProductD_Remark.Split(',');

        //                sql = @"insert into Picking_ProductD (Company,UserGroup,Creator,CreateDate,PickingOrderType,PickingOrderNo,
        //                ItemId,ProductNo,ProductName,QTY,PickingQTY,Unit,Remark) values (@Company,@UserGroup,@Creator,@CreateDate,
        //                @PickingOrderType,@PickingOrderNo,@ItemId,@ProductNo,@ProductName,@QTY,@PickingQTY,@Unit,@Remark)";

        //                for (int i = 0; i < strPickingProductDProductNoArray.Length; i++)
        //                {
        //                    if (string.IsNullOrEmpty(strPickingProductDProductNoArray[i]))
        //                    {
        //                        continue;
        //                    }
        //                    dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
        //                        DataAccessMySQL.CreateParameter("Company",DbType.String,picking_H.Company),
        //                        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,picking_H.UserGroup),
        //                        DataAccessMySQL.CreateParameter("Creator",DbType.String,picking_H.Creator),
        //                        DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
        //                        DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
        //                        DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo),
        //                        DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
        //                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strPickingProductDProductNoArray[i].ToString()),
        //                        DataAccessMySQL.CreateParameter("ProductName",DbType.String,strPickingProductDProductNameArray[i].ToString()),
        //                        DataAccessMySQL.CreateParameter("QTY",DbType.String,strPickingProductDQTYArray[i].ToString()),
        //                        DataAccessMySQL.CreateParameter("PickingQTY",DbType.Int32,strPickingProductDPickingQTYArray[i].ToString()==""?0:int.Parse(strPickingProductDPickingQTYArray[i].ToString())),
        //                        DataAccessMySQL.CreateParameter("Unit",DbType.String,strPickingProductDUnitArray[i].ToString()),
        //                        DataAccessMySQL.CreateParameter("Remark",DbType.String,strPickingProductDRemarkArray[i].ToString())
        //                    });

        //                    if (AutoConfirm == "")
        //                    {
        //                        if (double.Parse(dbMySQL.ExecuteScalar("select count(*) from Picking_ProductSerial where PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo and ItemId=@ItemId", tran,
        //                            new DbParameter[] {
        //                                DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
        //                                DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo),
        //                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000"))
        //                            }).ToString()) != double.Parse(strPickingProductDQTYArray[i].ToString()))
        //                        {
        //                            AutoConfirm = "N";
        //                        }
        //                    }

        //                }
        //            }
        //            if (AutoConfirm == "")
        //            {
        //                AutoConfirm = dbMySQL.ExecuteScalar("select AutoConfirm from OrderCategory where OrderType=@OrderType", tran,
        //               new DbParameter[] {
        //                    DataAccessMySQL.CreateParameter("OrderType",DbType.String,picking_H.PickingOrderType)
        //               }).ToString();
        //            }
        //            dbMySQL.ExecuteNonQuery(@"insert into picking_h (Company,UserGroup,Creator,CreateDate,PickingOrderType,PickingOrderNo,
        //                OrderDate,SupportAplOrderType,SupportAplOrderNo,CustomerId,CustomerType,Sales,RequestDate,RequestTime,SupportDept,
        //                PickingDate,PickingMan,Remark,Confirmed) values (@Company,@UserGroup,@Creator,@CreateDate,@PickingOrderType,@PickingOrderNo,
        //                @OrderDate,@SupportAplOrderType,@SupportAplOrderNo,@CustomerId,@CustomerType,@Sales,@RequestDate,@RequestTime,
        //                @SupportDept,@PickingDate,@PickingMan,@Remark,@Confirmed)", tran,
        //                new DbParameter[]{
        //                    DataAccessMySQL.CreateParameter("Company",DbType.String,picking_H.Company),
        //                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,picking_H.UserGroup),
        //                    DataAccessMySQL.CreateParameter("Creator",DbType.String,picking_H.Creator),
        //                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
        //                    DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
        //                    DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo),
        //                    DataAccessMySQL.CreateParameter("OrderDate",DbType.String,picking_H.OrderDate),
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,picking_H.SupportAplOrderType),
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,picking_H.SupportAplOrderNo),
        //                    DataAccessMySQL.CreateParameter("CustomerId",DbType.String,picking_H.CustomerId),
        //                    DataAccessMySQL.CreateParameter("CustomerType",DbType.String,picking_H.CustomerType),
        //                    DataAccessMySQL.CreateParameter("Sales",DbType.String,picking_H.Sales),
        //                    DataAccessMySQL.CreateParameter("RequestDate",DbType.String,picking_H.RequestDate),
        //                    DataAccessMySQL.CreateParameter("RequestTime",DbType.String,picking_H.RequestTime),
        //                    DataAccessMySQL.CreateParameter("SupportDept",DbType.String,picking_H.SupportDept),
        //                    DataAccessMySQL.CreateParameter("PickingDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
        //                    DataAccessMySQL.CreateParameter("PickingMan",DbType.String,picking_H.PickingMan),
        //                    DataAccessMySQL.CreateParameter("Remark",DbType.String,picking_H.Remark),
        //                    DataAccessMySQL.CreateParameter("Confirmed",DbType.String,AutoConfirm)
        //                });

        //            dbMySQL.ExecuteNonQuery(@"update SupportApl_H set IsPicking=@IsPicking where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
        //                new DbParameter[]{
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,picking_H.SupportAplOrderType),
        //                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,picking_H.SupportAplOrderNo),
        //                    DataAccessMySQL.CreateParameter("IsPicking",DbType.String,AutoConfirm)
        //                });
        //            tran.Commit();
        //            msg = "";
        //            return true;

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

        public List<Picking_H> GetPicking_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Picking_H> objPicking_H = new List<Picking_H>();

            string sql = @"select h.PickingOrderType,h.PickingOrderNo,h.CustomerId,c.CustomerName,
                    h.SupportDept,d.DepartmentName as SupportDeptName,h.RequestDate,h.sales,e.EmployeeName as salesName,
                    h.creator,ee.EmployeeName as creatorName,
                    h.customertype,h.Confirmed,
                    h.SupportAplOrderType,
                    h.SupportAplOrderNo from picking_H as h 
                    left join Customer as c on c.CustomerId=h.CustomerId
                    left join Department as d on d.DepartmentId=h.SupportDept
                    left join Employee as e on e.EmployeeId=h.sales
                    left join Employee as ee on ee.EmployeeId=h.creator where 1=1";

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

            DataTable dtPicking_H = null;
            if (strCondition != "")
            {
                dtPicking_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtPicking_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtPicking_H.Rows.Count > 0)
            {

                foreach (DataRow dr in dtPicking_H.Rows)
                {
                    Picking_H obj = new Picking_H();
                    obj.PickingOrderType = dr["PickingOrderType"].ToString();           //單別
                    obj.PickingOrderNo = dr["PickingOrderNo"].ToString();               //單號
                    obj.CustomerId = dr["CustomerId"].ToString();                       //客戶ID
                    obj.CustomerName = dr["CustomerName"].ToString();                   //客戶名稱
                    obj.SupportDept = dr["SupportDept"].ToString();                     //支援單位ID
                    obj.SupportDeptName = dr["SupportDeptName"].ToString();             //支援單位名稱
                    obj.RequestDate = dr["RequestDate"].ToString();                     //需求日期
                    obj.Sales = dr["sales"].ToString();                                 //業務員ID
                    obj.SalesName = dr["salesName"].ToString();                         //業務員名稱
                    obj.Creator = dr["creator"].ToString();                             //建立人員ID
                    obj.CreatorName = dr["creatorName"].ToString();                     //建立人員名稱
                    obj.CustomerType = "";//客戶形態
                    if (dr["customertype"].ToString().Trim() != "")
                    {
                        obj.CustomerType = dr["customertype"].ToString() == "A" ? "A:一般客戶" : "B:經銷商";
                    }
                    obj.SupportAplOrderType = dr["SupportAplOrderType"].ToString();     //來源類別
                    obj.SupportAplOrderNo = dr["SupportAplOrderNo"].ToString();         //來源單號
                    if(dr["Confirmed"].ToString()=="Y")
                    {
                        obj.Confirmed = "Y.已發料";
                    }
                    if (dr["Confirmed"].ToString() == "N")
                    {
                        obj.Confirmed = "N.未發料";
                    }
                    if (dr["Confirmed"].ToString() == "V")
                    {
                        obj.Confirmed = "V.作廢";
                    }
                    objPicking_H.Add(obj);
                }
            }

            return objPicking_H;
        }

        public bool DeletePicking_H(string PickingOrderType, string PickingOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                DataTable dtPicking_H=dbMySQL.ExecuteDataTable(@"select * from Picking_H where PickingOrderType=@PickingOrderType and 
                    PickingOrderNo=@PickingOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,PickingOrderType),
                        DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,PickingOrderNo)
                    });

                if(dtPicking_H!=null&&dtPicking_H.Rows.Count>0)
                {
                    dbMySQL.ExecuteNonQuery("update SupportApl_H set IsPicking='N' WHERE SupportAplOrderType=@SupportAplOrderType AND SupportAplOrderNo=@SupportAplOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtPicking_H.Rows[0]["SupportAplOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtPicking_H.Rows[0]["SupportAplOrderNo"].ToString())
                    });
                }

                dbMySQL.ExecuteNonQuery(@"delete from Picking_H where PickingOrderType=@PickingOrderType and 
                    PickingOrderNo=@PickingOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,PickingOrderType),
                        DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,PickingOrderNo)
                    });

                dbMySQL.ExecuteNonQuery(@"delete from Picking_ProductD where PickingOrderType=@PickingOrderType and 
                    PickingOrderNo=@PickingOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,PickingOrderType),
                        DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,PickingOrderNo)
                    });

                dbMySQL.ExecuteNonQuery(@"delete from Picking_ProductSerial where PickingOrderType=@PickingOrderType and 
                    PickingOrderNo=@PickingOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,PickingOrderType),
                        DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,PickingOrderNo)
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

        public Picking_H GetPicking_HByNo(string PickingOrderType, string PickingOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            Picking_H picking_H = new Picking_H();

            DataTable dtPicking_H = dbMySQL.ExecuteDataTable(@"SELECT p.*,s.*,o.OrderSName,c.CustomerName,e.EmployeeName as SalesName FROM picking_H as p 
                left join SupportApl_H as s on p.SupportAplOrderType=s.SupportAplOrderType and p.SupportAplOrderNo=s.SupportAplOrderNo
                left join OrderCategory as o on o.OrderType=p.PickingOrderType
                left join Customer as c on c.CustomerId=p.CustomerId
                left join Employee as e on e.EmployeeId=p.Sales WHERE 
                PickingOrderType=@PickingOrderType AND PickingOrderNo=@PickingOrderNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,PickingOrderType),
                    DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,PickingOrderNo)
                });
            if (dtPicking_H.Rows.Count > 0)
            {
                //單別
                picking_H.PickingOrderType = dtPicking_H.Rows[0]["PickingOrderType"].ToString();
                //單別名稱
                //單號
                picking_H.PickingOrderNo = dtPicking_H.Rows[0]["PickingOrderNo"].ToString();
                //單據時間
                picking_H.OrderDate = dtPicking_H.Rows[0]["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dtPicking_H.Rows[0]["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                //來源單別
                picking_H.SupportAplOrderType = dtPicking_H.Rows[0]["SupportAplOrderType"].ToString();

                picking_H.SupportAplOrderNo = dtPicking_H.Rows[0]["SupportAplOrderNo"].ToString();
                picking_H.CustomerId = dtPicking_H.Rows[0]["CustomerId"].ToString();
                picking_H.CustomerType = "";
                picking_H.CustomerName = dtPicking_H.Rows[0]["CustomerName"].ToString();
                if (dtPicking_H.Rows[0]["CustomerType"].ToString().Trim()!="")
                {
                    picking_H.CustomerType = dtPicking_H.Rows[0]["CustomerType"].ToString()=="A"? "A:一般客戶" : "B:經銷商";
                }
                picking_H.Sales = dtPicking_H.Rows[0]["Sales"].ToString();
                picking_H.SalesName = dtPicking_H.Rows[0]["SalesName"].ToString();
                picking_H.RequestDate = dtPicking_H.Rows[0]["RequestDate"].ToString() == "" ? "" : DateTime.ParseExact(dtPicking_H.Rows[0]["RequestDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                picking_H.RequestTime = dtPicking_H.Rows[0]["RequestTime"].ToString() == "" ? "" : DateTime.ParseExact(dtPicking_H.Rows[0]["RequestTime"].ToString(), "HHmmss", CultureInfo.InvariantCulture).ToString("HH:mm:ss");
                picking_H.SupportDept = dtPicking_H.Rows[0]["SupportDept"].ToString();
                picking_H.PickingDate = dtPicking_H.Rows[0]["PickingDate"].ToString() == "" ? "" : DateTime.ParseExact(dtPicking_H.Rows[0]["PickingDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                picking_H.PickingMan = dtPicking_H.Rows[0]["PickingMan"].ToString();
                picking_H.Remark = dtPicking_H.Rows[0]["Remark"].ToString();
                picking_H.Confirmed = dtPicking_H.Rows[0]["Confirmed"].ToString();
                return picking_H;
            }else
            {
                return picking_H;
            }

        }

        public bool UpdatePicking_H(Picking_H picking_H, string strPicking_ProductD_ProductNo, string strPicking_ProductD_ProductName,
            string strPicking_ProductD_QTY, string strPicking_ProductD_PickingQTY, string strPicking_ProductD_Unit, string strPicking_ProductD_Remark,
            out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                dbMySQL.ExecuteNonQuery(@"update Picking_H set Modifier=@Modifier , ModiDate=@ModiDate , OrderDate=@OrderDate,
                    SupportAplOrderType=@SupportAplOrderType,SupportAplOrderNo=@SupportAplOrderNo,CustomerId=@CustomerId,
                    CustomerType=@CustomerType,Sales=@Sales,RequestDate=@RequestDate,RequestTime=@RequestTime,
                    SupportDept=@SupportDept,PickingDate=@PickingDate,PickingMan=@PickingMan,Remark=@Remark where 
                    PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo", tran,
                    new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,picking_H.Creator),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,picking_H.OrderDate),
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,picking_H.SupportAplOrderType),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,picking_H.SupportAplOrderNo),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,picking_H.CustomerId),
                            DataAccessMySQL.CreateParameter("CustomerType",DbType.String,picking_H.CustomerType),
                            DataAccessMySQL.CreateParameter("Sales",DbType.String,picking_H.Sales),
                            DataAccessMySQL.CreateParameter("RequestDate",DbType.String,picking_H.RequestDate),
                            DataAccessMySQL.CreateParameter("RequestTime",DbType.String,picking_H.RequestTime),
                            DataAccessMySQL.CreateParameter("SupportDept",DbType.String,picking_H.SupportDept),
                            DataAccessMySQL.CreateParameter("PickingDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("PickingMan",DbType.String,picking_H.PickingMan),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,picking_H.Remark),
                            DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
                            DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo)
                        });

                dbMySQL.ExecuteNonQuery("delete from Picking_ProductD where PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
                        DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo),
                    });

                string sql = "";

                if (strPicking_ProductD_ProductNo != "")
                {
                    string[] strPickingProductDProductNoArray = strPicking_ProductD_ProductNo.Split(',');
                    string[] strPickingProductDProductNameArray = strPicking_ProductD_ProductName.Split(',');
                    string[] strPickingProductDQTYArray = strPicking_ProductD_QTY.Split(',');
                    string[] strPickingProductDPickingQTYArray = strPicking_ProductD_PickingQTY.Split(',');
                    string[] strPickingProductDUnitArray = strPicking_ProductD_Unit.Split(',');
                    string[] strPickingProductDRemarkArray = strPicking_ProductD_Remark.Split(',');

                    sql = @"insert into Picking_ProductD (Company,UserGroup,Creator,CreateDate,PickingOrderType,PickingOrderNo,
                        ItemId,ProductNo,ProductName,QTY,PickingQTY,Unit,Remark) values (@Company,@UserGroup,@Creator,@CreateDate,
                        @PickingOrderType,@PickingOrderNo,@ItemId,@ProductNo,@ProductName,@QTY,@PickingQTY,@Unit,@Remark)";


                    for (int i = 0; i < strPickingProductDProductNoArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strPickingProductDProductNoArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,picking_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,picking_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,picking_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
                                DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,(i+1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strPickingProductDProductNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,strPickingProductDProductNameArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.String,strPickingProductDQTYArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("PickingQTY",DbType.Int32,strPickingProductDPickingQTYArray[i].ToString()==""?0:int.Parse(strPickingProductDPickingQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strPickingProductDUnitArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strPickingProductDRemarkArray[i].ToString())
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

        public List<Picking_H> GetInfo(string SupportAplOrderType, string SupportAplOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Picking_H> objPicking_H = new List<Picking_H>();

            DataTable dtPicking_H = dbMySQL.ExecuteDataTable(@"SELECT * from Picking_H where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,SupportAplOrderType),
                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,SupportAplOrderNo)
                });
            if (dtPicking_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPicking_H.Rows)
                {
                    Picking_H obj = new Picking_H();
                    obj.PickingOrderType = dr["PickingOrderType"].ToString();    //單別
                    obj.PickingOrderNo = dr["PickingOrderNo"].ToString();    //單號
                    if(dr["Confirmed"].ToString()=="Y")
                    {
                        obj.Confirmed = "Y.已發料";
                    }
                    if (dr["Confirmed"].ToString() == "N")
                    {
                        obj.Confirmed = "N:未發料";
                    }
                    if (dr["Confirmed"].ToString() == "V")
                    {
                        obj.Confirmed = "V:作廢";
                    }
                    objPicking_H.Add(obj);
                }
            }
            return objPicking_H;

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

        public bool IssuePicking_H(Picking_H picking_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();


                string[] orderInfo = GetSupportAplOrderNo(picking_H.PickingOrderType).Split('-');

                if (orderInfo[0].ToString() == "None")
                {
                    return false;
                }

                DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable(@"select * from SupportApl_H where 
                    SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo",tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,picking_H.SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,picking_H.SupportAplOrderNo),
                    });
                if(dtSupportApl_H!=null&&dtSupportApl_H.Rows.Count>0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert picking_h (Company,UserGroup,Creator,CreateDate,
                        PickingOrderType,PickingOrderNo,OrderDate,SupportAplOrderType,SupportAplOrderNo,CustomerId,CustomerType,
                        Sales,RequestDate,RequestTime,SupportDept,PickingDate,PickingMan,Remark) values (@Company,
                        @UserGroup,@Creator,@CreateDate,@PickingOrderType,@PickingOrderNo,@OrderDate,
                        @SupportAplOrderType,@SupportAplOrderNo,@CustomerId,@CustomerType,@Sales,@RequestDate,@RequestTime,@SupportDept,
                        @PickingDate,@PickingMan,@Remark)", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("Company",DbType.String, picking_H.Company),
                        DataAccessMySQL.CreateParameter("UserGroup",DbType.String, picking_H.UserGroup),
                        DataAccessMySQL.CreateParameter("Creator",DbType.String, picking_H.Creator),
                        DataAccessMySQL.CreateParameter("CreateDate",DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String, picking_H.PickingOrderType),
                        DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String, orderInfo[1].ToString()),
                        DataAccessMySQL.CreateParameter("OrderDate",DbType.String, dtSupportApl_H.Rows[0]["OrderDate"].ToString()),
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String, dtSupportApl_H.Rows[0]["SupportAplOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String, dtSupportApl_H.Rows[0]["SupportAplOrderNo"].ToString()),
                        DataAccessMySQL.CreateParameter("CustomerId",DbType.String, dtSupportApl_H.Rows[0]["CustomerId"].ToString()),
                        DataAccessMySQL.CreateParameter("CustomerType",DbType.String, dtSupportApl_H.Rows[0]["CustomerType"].ToString()),
                        DataAccessMySQL.CreateParameter("Sales",DbType.String, dtSupportApl_H.Rows[0]["SalesId"].ToString()),
                        DataAccessMySQL.CreateParameter("RequestDate",DbType.String, dtSupportApl_H.Rows[0]["RequestDate"].ToString()),
                        DataAccessMySQL.CreateParameter("RequestTime",DbType.String, dtSupportApl_H.Rows[0]["RequestTime"].ToString()),
                        DataAccessMySQL.CreateParameter("SupportDept",DbType.String, dtSupportApl_H.Rows[0]["SupportDept"].ToString()),
                        DataAccessMySQL.CreateParameter("PickingDate",DbType.String, picking_H.PickingDate),
                        DataAccessMySQL.CreateParameter("PickingMan",DbType.String, picking_H.PickingMan),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String, dtSupportApl_H.Rows[0]["Remark"].ToString())
                    });
                }

                DataTable dtSupportApl_SupportItem = dbMySQL.ExecuteDataTable(@"select * from SupportApl_ProductD where 
                    SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo",tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,picking_H.SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,picking_H.SupportAplOrderNo),
                    });

                foreach (DataRow dr in dtSupportApl_SupportItem.Rows)
                {
                    dbMySQL.ExecuteNonQuery(@"insert picking_productd (Company,UserGroup,Creator,CreateDate,
                        PickingOrderType,PickingOrderNo,ItemId,ProductNo,ProductName,QTY,PickingQTY,
                        Unit,Remark) values (@Company,
                        @UserGroup,@Creator,@CreateDate,@PickingOrderType,@PickingOrderNo,@ItemId,@ProductNo,
                        @ProductName,@QTY,@PickingQTY,@Unit,@Remark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String, picking_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String, picking_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String, picking_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String, picking_H.PickingOrderType),
                            DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String, orderInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("ItemId",DbType.String, dr["ItemId"].ToString()),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String, dr["ProductNo"].ToString()),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String, dr["ProductName"].ToString()),
                            DataAccessMySQL.CreateParameter("QTY",DbType.Double, dr["QTY"]),
                            DataAccessMySQL.CreateParameter("PickingQTY",DbType.Double, dr["PickingQTY"]),
                            DataAccessMySQL.CreateParameter("Unit",DbType.String, dr["Unit"].ToString()),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String, dr["Remark"].ToString()),
                        });
                }


                dbMySQL.ExecuteNonQuery(@"update SupportApl_H set IsPicking='Y' where 
                    SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,picking_H.SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,picking_H.SupportAplOrderNo),
                    });

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

        public bool Confirmed(Picking_H picking_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtPicking_H = dbMySQL.ExecuteDataTable(@"select * from Picking_H where 
                    PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
                        DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo)
                    });
                if (dtPicking_H == null || dtPicking_H.Rows.Count == 0)
                {
                    tran.Commit();
                    msg = "";
                    return true;
                }

                DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable("select * from SupportApl_H where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                        new DbParameter[] {
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtPicking_H.Rows[0]["SupportAplOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtPicking_H.Rows[0]["SupportAplOrderNo"].ToString())
                        });

                if (dtSupportApl_H != null && dtSupportApl_H.Rows.Count != 0)
                {
                    
                    if (picking_H.Confirmed == "V" || picking_H.Confirmed == "N")
                    {
                        dbMySQL.ExecuteNonQuery("update SupportApl_H set IsPicking='N' where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtPicking_H.Rows[0]["SupportAplOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtPicking_H.Rows[0]["SupportAplOrderNo"].ToString())
                        });
                    }

                    if (picking_H.Confirmed == "Y")
                    {
                        if (dtSupportApl_H.Rows[0]["IsPicking"].ToString() == "Y")
                        {
                            tran.Commit();
                            msg = "當前支援申請單已領料";
                            return false;
                        }
                        dbMySQL.ExecuteNonQuery("update SupportApl_H set IsPicking='Y' where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtPicking_H.Rows[0]["SupportAplOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtPicking_H.Rows[0]["SupportAplOrderNo"].ToString())
                        });
                    }
                }

                if (picking_H.Confirmed == "V")
                {
                    dbMySQL.ExecuteNonQuery("update Picking_H set Confirmed='V',PickingDate='',PickingMan='' where PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
                            DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo)
                        });
                }

                if (picking_H.Confirmed == "N")
                {
                    dbMySQL.ExecuteNonQuery("update Picking_H set Confirmed='N',PickingDate='',PickingMan='' where PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
                            DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo)
                        });
                }

                if (picking_H.Confirmed == "Y")
                {
                    if(int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from Picking_ProductD as pd left join (
                            select PickingOrderType,PickingOrderNo,ItemId,count(*) as psqty from 
                            Picking_ProductSerial
                            where PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo
                            group by PickingOrderType,PickingOrderNo,ItemId) as ps
                            on ps.PickingOrderType=pd.PickingOrderType and 
                            ps.PickingOrderNo=pd.PickingOrderNo
                            and ps.ItemId=pd.ItemId 
                            where pd.PickingOrderType=@PickingOrderType and pd.PickingOrderNo=@PickingOrderNo and pd.qty<>ps.psqty", tran,
                            new DbParameter[] {
                                DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
                                DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo)
                            }).ToString())>0)
                    {
                        tran.Commit();
                        msg = "序號數量與品項數量不符!";
                        return false;
                    }

                    dbMySQL.ExecuteNonQuery("update Picking_H set Confirmed='Y',PickingDate=@PickingDate,PickingMan=@PickingMan where PickingOrderType=@PickingOrderType and PickingOrderNo=@PickingOrderNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("PickingDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("PickingMan",DbType.String,picking_H.PickingMan),
                            DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,picking_H.PickingOrderType),
                            DataAccessMySQL.CreateParameter("PickingOrderNo",DbType.String,picking_H.PickingOrderNo)
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
    }
}