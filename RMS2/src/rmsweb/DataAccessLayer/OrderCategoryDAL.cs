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

namespace DataAccessLayer
{
    public class OrderCategoryDAL : IOrderCategoryDAL
    {
        public bool AddOrderCategory(OrderCategory orderCategory, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                string count = dbMySQL.ExecuteScalar(@"select count(*) from OrderCategory where 
                    OrderType=@OrderType ",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("OrderType",DbType.String,orderCategory.OrderType)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into OrderCategory (Company,UserGroup,Creator,CreateDate,OrderType,OrderSName,OrderFName,OrderCategory,CodeMode,SerialNrCodeLength,AutoConfirm,CheckExOrder,Remark,DaysAfterCreate) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,@OrderType,@OrderSName,@OrderFName,@OrderCategory,@CodeMode,@SerialNrCodeLength,@AutoConfirm,@CheckExOrder,@Remark,@DaysAfterCreate)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,orderCategory.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,orderCategory.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,orderCategory.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("OrderType",DbType.String,orderCategory.OrderType),
                            DataAccessMySQL.CreateParameter("OrderSName",DbType.String,orderCategory.OrderSName),
                            DataAccessMySQL.CreateParameter("OrderFName",DbType.String,orderCategory.OrderFName),
                            DataAccessMySQL.CreateParameter("OrderCategory",DbType.String,orderCategory.OrderCategoryID),
                            DataAccessMySQL.CreateParameter("CodeMode",DbType.String,orderCategory.CodeMode),
                            DataAccessMySQL.CreateParameter("SerialNrCodeLength",DbType.String,orderCategory.SerialNrCodeLength),
                            DataAccessMySQL.CreateParameter("AutoConfirm",DbType.String,orderCategory.AutoConfirm),
                            DataAccessMySQL.CreateParameter("CheckExOrder",DbType.String,orderCategory.CheckExOrder),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,orderCategory.Remark),
                            DataAccessMySQL.CreateParameter("DaysAfterCreate",DbType.String,orderCategory.DaysAfterCreate)
                        });
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "單別已存在！";
                    return false;
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public List<OrderCategory> GetOrderCategory(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<OrderCategory> objOrderCategory = new List<OrderCategory>();

            string[] colArray = Col.Split(',');
            string[] conditionArray = Condition.Split(',');
            string[] valueArray = conditionValue.Split(',');

            string sql = @"select * from OrderCategory where 1=1";

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



            DataTable dtOrderCategory = null;
            if (strCondition != "")
            {

                dtOrderCategory = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtOrderCategory = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }

            if (dtOrderCategory.Rows.Count > 0)
            {

                foreach (DataRow dr in dtOrderCategory.Rows)
                {
                    OrderCategory obj = new OrderCategory();
                    obj.OrderType = dr["OrderType"].ToString();
                    obj.OrderSName = dr["OrderSName"].ToString();
                    obj.OrderFName = dr["OrderFName"].ToString();
                    if(dr["OrderCategory"].ToString()== "A1")
                    {
                        obj.OrderCategoryID = "A1:支援申請";
                    }
                    if (dr["OrderCategory"].ToString() == "A2")
                    {
                        obj.OrderCategoryID = "A2:支援派工";
                    }
                    if (dr["OrderCategory"].ToString() == "A3")
                    {
                        obj.OrderCategoryID = "A3:倉庫發料";
                    }
                    if (dr["OrderCategory"].ToString() == "E1")
                    {
                        obj.OrderCategoryID = "E1.合約";
                    }
                    if (dr["OrderCategory"].ToString() == "D1")
                    {
                        obj.OrderCategoryID = "D1:保固計劃";
                    }
                    if (dr["OrderCategory"].ToString() == "D2")
                    {
                        obj.OrderCategoryID = "D2:定期保養";
                    }
                    if (dr["OrderCategory"].ToString() == "D3")
                    {
                        obj.OrderCategoryID = "D3:定保派工";
                    }
                    if (dr["OrderCategory"].ToString() == "B1")
                    {
                        obj.OrderCategoryID = "B1:維修申請";
                    }
                    if (dr["OrderCategory"].ToString() == "B2")
                    {
                        obj.OrderCategoryID = "B2:維修收件";
                    }
                    if (dr["OrderCategory"].ToString() == "B3")
                    {
                        obj.OrderCategoryID = "B3:維修派工";
                    }
                    if (dr["OrderCategory"].ToString() == "B4")
                    {
                        obj.OrderCategoryID = "B4:維修報價";
                    }
                    if (dr["OrderCategory"].ToString() == "B5")
                    {
                        obj.OrderCategoryID = "B5:完工";
                    }
                    if (dr["OrderCategory"].ToString() == "B6")
                    {
                        obj.OrderCategoryID = "B6:維修出件";
                    }
                    if (dr["OrderCategory"].ToString() == "C1")
                    {
                        obj.OrderCategoryID = "C1:送廠申請";
                    }
                    if (dr["OrderCategory"].ToString() == "C2")
                    {
                        obj.OrderCategoryID = "C2:送廠維修";
                    }
                    if (dr["OrderCategory"].ToString() == "C3")
                    {
                        obj.OrderCategoryID = "C3:廠修歸還";
                    }
                    if (dr["OrderCategory"].ToString() == "C4")
                    {
                        obj.OrderCategoryID = "C4:原廠報價";
                    }
                    obj.CodeMode = dr["CodeMode"].ToString();
                    obj.SerialNrCodeLength = dr["SerialNrCodeLength"].ToString();
                    obj.AutoConfirm = dr["AutoConfirm"].ToString();
                    obj.CheckExOrder = dr["CheckExOrder"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.DaysAfterCreate = dr["DaysAfterCreate"].ToString();
                    objOrderCategory.Add(obj);
                }
            }

            return objOrderCategory;
        }

        public bool DelOrderCategory(string OrderType, string OrderCategory, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {

                if (OrderCategory == "A1")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from SupportApl_H where SupportAplOrderType=@SupportAplOrderType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "支援申請單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "A2")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from Assignment where AssignOrderType=@AssignOrderType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "支援派工單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "A3")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from Picking_H where PickingOrderType=@PickingOrderType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("PickingOrderType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "發料單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "E1")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from Contract_H where ContractType=@ContractType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "合約單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "D1")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from ServiceArrange_H where SerArrangeOrderType=@SerArrangeOrderType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "定保計畫單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "D2")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from RoutineService_H where RoutineSerOrderType=@RoutineSerOrderType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "定保單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "D3")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from Assignment where AssignOrderType=@AssignOrderType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "定保派工單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "B1")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from PhoneService_H where PhoneSerType=@PhoneSerType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "電話服務單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "B2")
                {
                    if(int.Parse(dbMySQL.ExecuteScalar("select count(*) from RGA_H where RGAType=@RGAType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,OrderType)
                        }).ToString())>0)
                    {
                        msg = "維修單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "B3")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from Assignment where AssignOrderType=@AssignOrderType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "維修派工單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "B4")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from Quotation_H where QuotationType=@QuotationType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("QuotationType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "報價單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "B5")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from CompletionOrder where CompletionType=@CompletionType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("CompletionType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "完工單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "B6")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from RGAReturn_H where RGAReturnType=@RGAReturnType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("RGAReturnType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "出件單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "C1")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from RMAApl where RMAAplType=@RMAAplType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("RMAAplType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "原廠申請單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "C2")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from RMA_H where RMAType=@RMAType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("RMAType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "送廠維修單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "C3")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from RMAReturn_H where RMAReturnType=@RMAReturnType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("RMAReturnType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "送廠歸還單別已存在，無法刪除！";
                        return false;
                    }
                }
                if (OrderCategory == "C4")
                {
                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from RFQ_H where RFQType=@RFQType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("RFQType",DbType.String,OrderType)
                        }).ToString()) > 0)
                    {
                        msg = "送廠歸還單別已存在，無法刪除！";
                        return false;
                    }
                }

                dbMySQL.ExecuteNonQuery(@"delete from OrderCategory where OrderType=@OrderType",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("OrderType",DbType.String,OrderType)
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

        public OrderCategory GetOrderCategoryInfo(string OrderType)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            OrderCategory orderCategory = new OrderCategory();


            DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select * from OrderCategory where OrderType=@OrderType",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("OrderType",DbType.String,OrderType)
                });

            if (dtOrderCategory != null && dtOrderCategory.Rows.Count > 0)
            {
                orderCategory.OrderType = dtOrderCategory.Rows[0]["OrderType"].ToString();
                orderCategory.OrderSName = dtOrderCategory.Rows[0]["OrderSName"].ToString();
                orderCategory.OrderFName = dtOrderCategory.Rows[0]["OrderFName"].ToString();
                orderCategory.OrderCategoryID = dtOrderCategory.Rows[0]["OrderCategory"].ToString();
                orderCategory.CodeMode = dtOrderCategory.Rows[0]["CodeMode"].ToString();
                orderCategory.SerialNrCodeLength = dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString();
                orderCategory.AutoConfirm = dtOrderCategory.Rows[0]["AutoConfirm"].ToString();
                orderCategory.CheckExOrder = dtOrderCategory.Rows[0]["CheckExOrder"].ToString();
                orderCategory.Remark = dtOrderCategory.Rows[0]["Remark"].ToString();
                orderCategory.DaysAfterCreate = dtOrderCategory.Rows[0]["DaysAfterCreate"].ToString();
                return orderCategory;
            }
            else
            {
                return null;
            }
        }

        public bool IsOrderCategory(string OrderType)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("select count(*) from OrderCategory where OrderType=@OrderType",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("OrderType",DbType.String,OrderType)
                }).ToString();

            if(int.Parse(count)>0)
            {
                return false;
            }else
            {
                return true;
            }
        }

        public bool UpdataOrderCategory(OrderCategory orderCategory)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"update OrderCategory set Modifier=@Modifier,ModiDate=@ModiDate,OrderSName=@OrderSName,OrderFName=@OrderFName,
                    AutoConfirm=@AutoConfirm,CheckExOrder=@CheckExOrder,
                    Remark=@Remark,DaysAfterCreate=@DaysAfterCreate where OrderType=@OrderType",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("Modifier",DbType.String,orderCategory.Modifier),
                        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("OrderType",DbType.String,orderCategory.OrderType),
                        DataAccessMySQL.CreateParameter("OrderSName",DbType.String,orderCategory.OrderSName),
                        DataAccessMySQL.CreateParameter("OrderFName",DbType.String,orderCategory.OrderFName),
                        //DataAccessMySQL.CreateParameter("SerialNrCodeLength",DbType.String,orderCategory.SerialNrCodeLength),
                        DataAccessMySQL.CreateParameter("AutoConfirm",DbType.String,orderCategory.AutoConfirm),
                        DataAccessMySQL.CreateParameter("CheckExOrder",DbType.String,orderCategory.CheckExOrder),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,orderCategory.Remark),
                        DataAccessMySQL.CreateParameter("DaysAfterCreate",DbType.String,orderCategory.DaysAfterCreate)
                    });
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<OrderCategory> SearchOrderCategory(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<OrderCategory> objOrderCategory = new List<OrderCategory>();

            string sql = @"select * from OrderCategory where 1=1";

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


            DataTable dtOrderCategory = null;
            if (strCondition != "")
            {
                dtOrderCategory = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtOrderCategory = dbMySQL.ExecuteDataTable(sql);
            }

            if (dtOrderCategory.Rows.Count > 0)
            {
                foreach (DataRow dr in dtOrderCategory.Rows)
                {
                    OrderCategory obj = new OrderCategory();
                    obj.OrderType = dr["OrderType"].ToString();
                    obj.OrderSName = dr["OrderSName"].ToString();
                    obj.OrderFName = dr["OrderFName"].ToString();
                    obj.OrderCategoryID = dr["OrderCategory"].ToString();
                    if(dr["OrderCategory"].ToString()=="A1")
                    {
                        obj.OrderCategoryName = "A1.支援申請";
                    }
                    if (dr["OrderCategory"].ToString() == "A2")
                    {
                        obj.OrderCategoryName = "A2.支援派工";
                    }
                    if (dr["OrderCategory"].ToString() == "A3")
                    {
                        obj.OrderCategoryName = "A3.倉庫發料";
                    }
                    if (dr["OrderCategory"].ToString() == "E1")
                    {
                        obj.OrderCategoryName = "E1.合約";
                    }
                    if (dr["OrderCategory"].ToString() == "D1")
                    {
                        obj.OrderCategoryName = "D1.保固計劃";
                    }
                    if (dr["OrderCategory"].ToString() == "D2")
                    {
                        obj.OrderCategoryName = "D2.定期保養";
                    }
                    if (dr["OrderCategory"].ToString() == "D3")
                    {
                        obj.OrderCategoryName = "D3.定保派工";
                    }
                    if (dr["OrderCategory"].ToString() == "B1")
                    {
                        obj.OrderCategoryName = "B1.維修申請";
                    }
                    if (dr["OrderCategory"].ToString() == "B2")
                    {
                        obj.OrderCategoryName = "B2.維修收件";
                    }
                    if (dr["OrderCategory"].ToString() == "B3")
                    {
                        obj.OrderCategoryName = "B3.維修派工";
                    }
                    if (dr["OrderCategory"].ToString() == "B4")
                    {
                        obj.OrderCategoryName = "B4.維修報價";
                    }
                    if (dr["OrderCategory"].ToString() == "B5")
                    {
                        obj.OrderCategoryName = "B5.完工";
                    }
                    if (dr["OrderCategory"].ToString() == "B6")
                    {
                        obj.OrderCategoryName = "B6.維修出件";
                    }
                    if (dr["OrderCategory"].ToString() == "C1")
                    {
                        obj.OrderCategoryName = "C1.送廠申請";
                    }
                    if (dr["OrderCategory"].ToString() == "C2")
                    {
                        obj.OrderCategoryName = "C2.送廠維修";
                    }
                    if (dr["OrderCategory"].ToString() == "C3")
                    {
                        obj.OrderCategoryName = "C3.廠商歸還";
                    }
                    if (dr["OrderCategory"].ToString() == "C4")
                    {
                        obj.OrderCategoryName = "C4.原廠報價";
                    }
                    obj.CodeMode = dr["CodeMode"].ToString();
                    obj.SerialNrCodeLength = dr["SerialNrCodeLength"].ToString();
                    obj.AutoConfirm = dr["AutoConfirm"].ToString();
                    obj.CheckExOrder = dr["CheckExOrder"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objOrderCategory.Add(obj);
                }
            }

            return objOrderCategory;
        }

        public OrderCategory GetOrderTypeName(string OrderType, string OrderCategory)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            OrderCategory orderCategory = null;

            DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select * from OrderCategory where OrderType=@OrderType and OrderCategory=@OrderCategory",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("OrderType",DbType.String,OrderType),
                    DataAccessMySQL.CreateParameter("OrderCategory",DbType.String,OrderCategory)
                });
            if (dtOrderCategory != null&& dtOrderCategory.Rows.Count>0)
            {
                orderCategory = new OrderCategory();
                orderCategory.OrderType = dtOrderCategory.Rows[0]["OrderType"].ToString();
                orderCategory.OrderSName = dtOrderCategory.Rows[0]["OrderSName"].ToString();
                orderCategory.OrderFName = dtOrderCategory.Rows[0]["OrderFName"].ToString();
                orderCategory.OrderCategoryID = dtOrderCategory.Rows[0]["OrderCategory"].ToString();
                orderCategory.CodeMode = dtOrderCategory.Rows[0]["CodeMode"].ToString();
                orderCategory.SerialNrCodeLength = dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString();
                orderCategory.AutoConfirm = dtOrderCategory.Rows[0]["AutoConfirm"].ToString();
                orderCategory.CheckExOrder = dtOrderCategory.Rows[0]["CheckExOrder"].ToString();
                orderCategory.Remark = dtOrderCategory.Rows[0]["Remark"].ToString();
                orderCategory.DaysAfterCreate = dtOrderCategory.Rows[0]["DaysAfterCreate"].ToString();
            }

            return orderCategory;

        }
    }
}