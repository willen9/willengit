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
    public class SaleSerialDAL : ISaleSerialDAL
    {
        public bool AddSaleSerial(SaleSerial saleSerial, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                string count = dbMySQL.ExecuteScalar(@"select count(*) from SaleSerial where 
                    ProductNo=@ProductNo and SerialNo=@SerialNo and CustomerId=@CustomerId and SaleDate=@SaleDate",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,saleSerial.ProductNo),
                        DataAccessMySQL.CreateParameter("SerialNo",DbType.String,saleSerial.SerialNo),
                        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,saleSerial.CustomerId),
                        DataAccessMySQL.CreateParameter("SaleDate",DbType.String,saleSerial.SaleDate)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into SaleSerial (Company,UserGroup,Creator,CreateDate,
                        ProductNo,SerialNo,CustomerId,SaleDate,SettingCode,RoutineService,SourceOrderType,SourceOrderNo,SourceOrderItemId,Remark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @ProductNo,@SerialNo,@CustomerId,@SaleDate,@SettingCode,@RoutineService,@SourceOrderType,@SourceOrderNo,@SourceOrderItemId,@Remark)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,saleSerial.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,saleSerial.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,saleSerial.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,saleSerial.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,saleSerial.SerialNo),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,saleSerial.CustomerId),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,saleSerial.SaleDate),
                            DataAccessMySQL.CreateParameter("SettingCode",DbType.String,saleSerial.SettingCode),
                            DataAccessMySQL.CreateParameter("RoutineService",DbType.String,saleSerial.RoutineService),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,saleSerial.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,saleSerial.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,saleSerial.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,saleSerial.Remark)
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

        public bool UpdateSaleSerial(SaleSerial saleSerial, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"update SaleSerial set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,ModiDate=@ModiDate,
                        SettingCode=@SettingCode,RoutineService=@RoutineService,SourceOrderType=@SourceOrderType,SourceOrderNo=@SourceOrderNo,
                        SourceOrderItemId=@SourceOrderItemId,Remark=@Remark where 
                        ProductNo=@ProductNo and SerialNo=@SerialNo and CustomerId=@CustomerId and SaleDate=@SaleDate",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,saleSerial.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,saleSerial.UserGroup),
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,saleSerial.Creator),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,saleSerial.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,saleSerial.SerialNo),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,saleSerial.CustomerId),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,saleSerial.SaleDate),
                            DataAccessMySQL.CreateParameter("SettingCode",DbType.String,saleSerial.SettingCode),
                            DataAccessMySQL.CreateParameter("RoutineService",DbType.String,saleSerial.RoutineService),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,saleSerial.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,saleSerial.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,saleSerial.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,saleSerial.Remark)
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

        public bool DelSaleSerial(SaleSerial saleSerial, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"delete from SaleSerial  where 
                        ProductNo=@ProductNo and SerialNo=@SerialNo and CustomerId=@CustomerId and SaleDate=@SaleDate",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,saleSerial.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,saleSerial.SerialNo),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,saleSerial.CustomerId),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,saleSerial.SaleDate)
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

        //public List<SaleSerial> SearchSaleSerial(string col, string condition, string value,string str)
        //{
        //    DataAccessMySQL dbMySQL = new DataAccessMySQL();
        //    dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

        //    string[] colArray = col.Split(',');
        //    string[] conditionArray = condition.Split(',');
        //    string[] valueArray = value.Split(',');

        //    List<SaleSerial> objSaleSerial = new List<SaleSerial>();

        //    string sql = @"select s.*,c.CustomerName,p.ProductName from SaleSerial as s
        //        left join customer as c on c.CustomerId=s.CustomerId
        //        left join product as p on p.ProductNo=s.ProductNo where 1=1";

        //    string strCondition = "";

        //    DbParameter[] dbParameters = new DbParameter[colArray.Length - 1];
        //    for (int i = 1; i < colArray.Length; i++)
        //    {
        //        if (string.IsNullOrEmpty(valueArray[i]))
        //        {
        //            continue;
        //        }
        //        strCondition += " AND " + colArray[i] + " " + conditionArray[i].Replace("%", "") + " @Parameter" + i + " ";
        //        if (conditionArray[i] == "like%")
        //        {
        //            dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i] + "%");
        //        }
        //        else if (conditionArray[i] == "%like")
        //        {
        //            dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i]);
        //        }
        //        else if (conditionArray[i] == "%like%")
        //        {
        //            dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i] + "%");
        //        }
        //        else
        //        {
        //            dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i]);
        //        }
        //    }

        //    DataTable dtSaleSerial = null;
        //    if (strCondition != ""&& str == "")
        //    {
        //        dtSaleSerial = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
        //    }
        //    if(strCondition == "" && str == "")
        //    {
        //        dtSaleSerial = dbMySQL.ExecuteDataTable(sql);
        //    }
        //    if (strCondition == "" && str != "")
        //    {
        //        dtSaleSerial = dbMySQL.ExecuteDataTable(sql+ " and s.SettingCode='"+str+"'");
        //    }
        //    if (strCondition != "" && str != "")
        //    {
        //        dtSaleSerial = dbMySQL.ExecuteDataTable(sql + strCondition + " and s.SettingCode='" + str + "'", dbParameters);
        //    }
        //    if (dtSaleSerial.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dtSaleSerial.Rows)
        //        {
        //            SaleSerial obj = new SaleSerial();
        //            obj.ProductNo = dr["ProductNo"].ToString();
        //            obj.ProductName = dr["ProductName"].ToString();
        //            obj.SerialNo = dr["SerialNo"].ToString();
        //            obj.CustomerId = dr["CustomerId"].ToString();
        //            obj.CustomerName = dr["CustomerName"].ToString();
        //            obj.SaleDate = dr["SaleDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["SaleDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
        //            obj.SettingCode = dr["SettingCode"].ToString() == "Y" ? "Y:已設定" : "N:未設定";
        //            if (dr["RoutineService"].ToString() == "")
        //            {
        //                obj.RoutineService = "";
        //            }
        //            else
        //            {
        //                obj.RoutineService = dr["RoutineService"].ToString() == "Y" ? "Y:需定保" : "N:不定保";
        //            }
        //            obj.SourceOrderType = dr["SourceOrderType"].ToString();
        //            obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
        //            obj.SourceOrderItemId = dr["SourceOrderItemId"].ToString();
        //            obj.Remark = dr["Remark"].ToString();
        //            objSaleSerial.Add(obj);
        //        }
        //    }

        //    return objSaleSerial;
        //}

        public List<SaleSerial> SearchSaleSerial(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<SaleSerial> objSaleSerial = new List<SaleSerial>();

            string sql = @"select s.*,c.CustomerName,p.ProductName from SaleSerial as s
                left join customer as c on c.CustomerId=s.CustomerId
                left join product as p on p.ProductNo=s.ProductNo where 1=1";

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

            DataTable dtSaleSerial = null;
            if (strCondition != "")
            {
                dtSaleSerial = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtSaleSerial = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtSaleSerial.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSaleSerial.Rows)
                {
                    SaleSerial obj = new SaleSerial();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.SaleDate = dr["SaleDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["SaleDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.SettingCode = dr["SettingCode"].ToString() == "Y" ? "Y:已設定" : "N:未設定";
                    if (dr["RoutineService"].ToString() == "")
                    {
                        obj.RoutineService = "";
                    }
                    else
                    {
                        obj.RoutineService = dr["RoutineService"].ToString() == "Y" ? "Y:需定保" : "N:不定保";
                    }
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.SourceOrderItemId = dr["SourceOrderItemId"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objSaleSerial.Add(obj);
                }
            }

            return objSaleSerial;
        }

        public SaleSerial GetSaleSerialInfo(SaleSerial saleSerial)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            SaleSerial obj = null;

            DataTable dtSaleSerial = dbMySQL.ExecuteDataTable(@"select s.*,c.CustomerName,c.CustomerType,p.ProductName,
                pr.ProductType as ProductTypeId1Name from SaleSerial as s
                left join customer as c on c.CustomerId=s.CustomerId
                left join product as p on p.ProductNo=s.ProductNo
                left join ProductCategory as pr on pr.ProductCategoryType='1' and pr.ProductTypeId=p.ProductTypeId1 where 
                s.ProductNo=@ProductNo and s.SerialNo=@SerialNo and s.CustomerId=@CustomerId and s.SaleDate=@SaleDate",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,saleSerial.ProductNo),
                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,saleSerial.SerialNo),
                    DataAccessMySQL.CreateParameter("CustomerId",DbType.String,saleSerial.CustomerId),
                    DataAccessMySQL.CreateParameter("SaleDate",DbType.String,saleSerial.SaleDate)
                });

            if (dtSaleSerial.Rows.Count > 0)
            {
                obj = new SaleSerial();
                obj.ProductNo = dtSaleSerial.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtSaleSerial.Rows[0]["ProductName"].ToString();
                obj.ProductTypeId1Name = dtSaleSerial.Rows[0]["ProductTypeId1Name"].ToString();
                obj.SerialNo = dtSaleSerial.Rows[0]["SerialNo"].ToString();
                obj.CustomerId = dtSaleSerial.Rows[0]["CustomerId"].ToString();
                obj.CustomerName = dtSaleSerial.Rows[0]["CustomerName"].ToString();
                if(dtSaleSerial.Rows[0]["CustomerType"].ToString()!="")
                {
                    obj.CustomerType = dtSaleSerial.Rows[0]["CustomerType"].ToString() == "A" ? "A:一般客戶" : "B:經銷商";
                }
                else
                {
                    obj.CustomerType = "";
                }

                obj.SaleDate = dtSaleSerial.Rows[0]["SaleDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["SaleDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.SettingCode = dtSaleSerial.Rows[0]["SettingCode"].ToString() == "Y" ? "Y:已設定" : "N:未設定";
                //if (dtSaleSerial.Rows[0]["RoutineService"].ToString() == "")
                //{
                //    obj.RoutineService = "";
                //}
                //else
                //{
                //    obj.RoutineService = dtSaleSerial.Rows[0]["RoutineService"].ToString() == "Y" ? "Y:需定保" : "N:不定保";
                //}
                obj.RoutineService = dtSaleSerial.Rows[0]["RoutineService"].ToString();
                obj.SourceOrderType = dtSaleSerial.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtSaleSerial.Rows[0]["SourceOrderNo"].ToString();
                obj.SourceOrderItemId = dtSaleSerial.Rows[0]["SourceOrderItemId"].ToString();
                obj.Remark = dtSaleSerial.Rows[0]["Remark"].ToString();
            }

            return obj;
        }

        public bool ImportFile(string path, SaleSerial saleSerial)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dataAccessMySql.CreateDbTransaction();
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(fs);
                    bool isFirstRow = true;
                    while (sr.Peek() > -1)
                    {
                        string[] valueArray = sr.ReadLine().Split(',');
                        if (isFirstRow)
                        {
                            if (valueArray.Length < 5)
                            {
                                return false;
                            }
                            isFirstRow = false;
                            continue;
                        }

                        //判斷品號是否存在
                        if(int.Parse(dataAccessMySql.ExecuteScalar("select count(*) from Product where ProductNo=@ProductNo",tran,
                            new DbParameter[] {
                                DataAccessMySQL.CreateParameter("ProductNo", DbType.String, valueArray[0].Trim()),
                            }).ToString())==0)
                        {
                            continue;
                        }

                        //判斷客戶代號是否存在
                        if (int.Parse(dataAccessMySql.ExecuteScalar("select count(*) from Customer where CustomerId=@CustomerId", tran,
                            new DbParameter[] {
                                DataAccessMySQL.CreateParameter("CustomerId", DbType.String, valueArray[2].Trim()),
                            }).ToString()) == 0)
                        {
                            continue;
                        }
                        //判斷主鍵是否重複
                        if (int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from SaleSerial where ProductNo=@ProductNo and SerialNo=@SerialNo and CustomerId=@CustomerId and SaleDate=@SaleDate",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("SerialNo", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("SaleDate", DbType.String, valueArray[3].Trim().Replace("/",""))
                                }).ToString()) ==0)
                        {
                            string[] SourceOrderNo = valueArray[4].Split('-');
                            dataAccessMySql.ExecuteNonQuery(
                               @"insert into SaleSerial(ProductNo,SerialNo,CustomerId,SaleDate,SourceOrderType,SourceOrderNo,SourceOrderItemId,SettingCode,Company,UserGroup,Creator,CreateDate) values
                                (@ProductNo,@SerialNo,@CustomerId,@SaleDate,@SourceOrderType,@SourceOrderNo,@SourceOrderItemId,@SettingCode,@Company,@UserGroup,@Creator,@CreateDate)",
                              tran,
                               new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String,valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("SerialNo", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("SaleDate", DbType.String, valueArray[3].Replace("/","").Trim()),
                                    DataAccessMySQL.CreateParameter("SourceOrderType", DbType.String, SourceOrderNo[0].Trim()),
                                    DataAccessMySQL.CreateParameter("SourceOrderNo", DbType.String, SourceOrderNo[1].Trim()),
                                    DataAccessMySQL.CreateParameter("SourceOrderItemId", DbType.String, SourceOrderNo[2].Trim()),
                                    DataAccessMySQL.CreateParameter("SettingCode", DbType.String, "N"),
                                    DataAccessMySQL.CreateParameter("Company", DbType.String, saleSerial.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup", DbType.String, saleSerial.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator", DbType.String, saleSerial.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String, DateTime.Now.ToString("yyyyMMdd"))
                                });
                        }
                    }
                    tran.Commit();
                    sr.Close();
                    sr.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                throw ex;
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

        public bool AddWarranty(string WarrantyId, Warranty_D warranty_D,string SerArrangeOrderType, string RoutineServiceFreq,out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                string[] WarrantyIdArray = WarrantyId.Split(',');

                string[] valueInfo = null;

                for (int i = 0; i < WarrantyIdArray.Length; i++)
                {
                    valueInfo = WarrantyIdArray[i].Split('#');

                    DataTable dtPurchasedSerial = dbMySQL.ExecuteDataTable(@"select s.*,c.CustomerName,p.ProductName from SaleSerial as s 
                        left join customer as c on c.CustomerId=s.CustomerId
                        left join Product as p on p.ProductNo=s.ProductNo where s.ProductNo=@ProductNo 
                        and s.SerialNo=@SerialNo and s.CustomerId=@CustomerId and s.SaleDate=@SaleDate", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,valueInfo[2].ToString()),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,valueInfo[3].ToString().Replace("/",""))
                        });
                    if (dtPurchasedSerial != null && dtPurchasedSerial.Rows.Count > 0)
                    {
                        dbMySQL.ExecuteNonQuery(@"insert into Warranty_D (Company,UserGroup,Creator,CreateDate,ProductNo,SerialNo,
                            ChangeDate,ChangeOrderType,ChangeOrderNo,ChangeOrderItemId,TargetId,TargetName,DataDate,DataTime,
                            WarrantyPeriod,WarrantySDate,WarrantyEDate,WarrantyType,Remark) values (@Company,@UserGroup,@Creator,@CreateDate,@ProductNo,@SerialNo,
                            @ChangeDate,@ChangeOrderType,@ChangeOrderNo,@ChangeOrderItemId,@TargetId,@TargetName,@DataDate,@DataTime,
                            @WarrantyPeriod,@WarrantySDate,@WarrantyEDate,@WarrantyType,@Remark)", tran,
                            new DbParameter[] {
                                DataAccessMySQL.CreateParameter("Company",DbType.String,warranty_D.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,warranty_D.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,warranty_D.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                                DataAccessMySQL.CreateParameter("ChangeDate",DbType.String,dtPurchasedSerial.Rows[0]["SaleDate"].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("ChangeOrderType",DbType.String,dtPurchasedSerial.Rows[0]["SourceOrderType"].ToString()),
                                DataAccessMySQL.CreateParameter("ChangeOrderNo",DbType.String,dtPurchasedSerial.Rows[0]["SourceOrderNo"].ToString()),
                                DataAccessMySQL.CreateParameter("ChangeOrderItemId",DbType.String,dtPurchasedSerial.Rows[0]["SourceOrderItemId"].ToString()),
                                DataAccessMySQL.CreateParameter("TargetId",DbType.String,dtPurchasedSerial.Rows[0]["CustomerId"].ToString()),
                                DataAccessMySQL.CreateParameter("TargetName",DbType.String,dtPurchasedSerial.Rows[0]["CustomerName"].ToString()),
                                DataAccessMySQL.CreateParameter("DataDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("DataTime",DbType.String,DateTime.Now.ToString("HHmmss")),
                                DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.String,warranty_D.WarrantyPeriod),
                                DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,dtPurchasedSerial.Rows[0]["SaleDate"].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,DateTime.ParseExact(dtPurchasedSerial.Rows[0]["SaleDate"].ToString().Replace("/",""), "yyyyMMdd", CultureInfo.InvariantCulture).AddMonths(int.Parse(warranty_D.WarrantyPeriod)).AddDays(-1).ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("WarrantyType",DbType.String,"2"),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,warranty_D.Remark)
                            });

                        if (int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from Warranty_H where ProductNo=@ProductNo and SerialNo=@SerialNo", tran,
                            new DbParameter[] {
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                            }).ToString()) == 0)
                        {
                            dbMySQL.ExecuteNonQuery(@"insert into Warranty_H (Company,UserGroup,Creator,CreateDate,ProductNo,SerialNo,
                                LastWarrantyType,LastWarrantyRemark,LastWarrantySDate,LastWarrantyEDate) values (@Company,@UserGroup,
                                @Creator,@CreateDate,@ProductNo,@SerialNo,@LastWarrantyType,@LastWarrantyRemark,@LastWarrantySDate,
                                @LastWarrantyEDate)", tran,
                                new DbParameter[] {
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,warranty_D.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,warranty_D.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,warranty_D.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                                    DataAccessMySQL.CreateParameter("LastWarrantyType",DbType.String,"2"),
                                    DataAccessMySQL.CreateParameter("LastWarrantyRemark",DbType.String,warranty_D.Remark),
                                    DataAccessMySQL.CreateParameter("LastWarrantySDate",DbType.String,dtPurchasedSerial.Rows[0]["SaleDate"].ToString().Replace("/","")),
                                    DataAccessMySQL.CreateParameter("LastWarrantyEDate",DbType.String,DateTime.ParseExact(dtPurchasedSerial.Rows[0]["SaleDate"].ToString().Replace("/",""), "yyyyMMdd", CultureInfo.InvariantCulture).AddMonths(int.Parse(warranty_D.WarrantyPeriod)).AddDays(-1).ToString("yyyyMMdd"))
                                });
                        }
                        else
                        {
                            dbMySQL.ExecuteNonQuery(@"update Warranty_H set Modifier=@Modifier,ModiDate=@ModiDate,
                                LastWarrantyType=@LastWarrantyType,LastWarrantyRemark=@LastWarrantyRemark,
                                LastWarrantySDate=@LastWarrantySDate,LastWarrantyEDate=@LastWarrantyEDate where 
                                ProductNo=@ProductNo and SerialNo=@SerialNo", tran,
                                new DbParameter[] {
                                    DataAccessMySQL.CreateParameter("Modifier",DbType.String,warranty_D.Creator),
                                    DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                                    DataAccessMySQL.CreateParameter("LastWarrantyType",DbType.String,"2"),
                                    DataAccessMySQL.CreateParameter("LastWarrantyRemark",DbType.String,warranty_D.Remark),
                                    DataAccessMySQL.CreateParameter("LastWarrantySDate",DbType.String,dtPurchasedSerial.Rows[0]["SaleDate"].ToString().Replace("/","")),
                                    DataAccessMySQL.CreateParameter("LastWarrantyEDate",DbType.String,DateTime.ParseExact(dtPurchasedSerial.Rows[0]["SaleDate"].ToString().Replace("/",""), "yyyyMMdd", CultureInfo.InvariantCulture).AddMonths(int.Parse(warranty_D.WarrantyPeriod)).AddDays(-1).ToString("yyyyMMdd"))
                                });
                        }

                        if(SerArrangeOrderType != ""&& RoutineServiceFreq!="0") 
                        {
                            string[] orderInfo = GetSupportAplOrderNo(SerArrangeOrderType).Split('-');

                            if (orderInfo[0].ToString() == "None")
                            {
                                msg = "單別不存在！";
                                return false;
                            }

                            string ordertype = dbMySQL.ExecuteScalar("select AutoConfirm from OrderCategory where OrderType=@OrderType", tran,
                                new DbParameter[] {
                                    DataAccessMySQL.CreateParameter("OrderType",DbType.String,SerArrangeOrderType),
                                }).ToString();
                            string Confirmor = "";
                            if (ordertype=="Y")
                            {
                                Confirmor = warranty_D.Creator;
                            }

                            dbMySQL.ExecuteNonQuery(@"insert into ServiceArrange_H (Company,UserGroup,Creator,CreateDate,
                                SerArrangeOrderType,SerArrangeOrderNo,OrderDate,ConfirmedDate,CustomerId,Version,
                                SourceOrderType,SourceOrderNo,SourceOrderItemId,ProductNo,ProductName,SerialNo,
                                SaleDate,RoutineServiceFreq,Remark,InternalRemark,WarrantyPeriod,WarrantySDate,WarrantyEDate,Confirmed,Confirmor) 
                                VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                                @SerArrangeOrderType,@SerArrangeOrderNo,@OrderDate,@ConfirmedDate,@CustomerId,@Version,
                                @SourceOrderType,@SourceOrderNo,@SourceOrderItemId,@ProductNo,@ProductName,@SerialNo,
                                @SaleDate,@RoutineServiceFreq,@Remark,@InternalRemark,@WarrantyPeriod,@WarrantySDate,@WarrantyEDate,@Confirmed,@Confirmor)", tran,
                                new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,warranty_D.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,warranty_D.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,warranty_D.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,SerArrangeOrderType),
                                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,orderInfo[1].ToString()),
                                    DataAccessMySQL.CreateParameter("OrderDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtPurchasedSerial.Rows[0]["CustomerId"]),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,"0000"),
                                    DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,""),
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,dtPurchasedSerial.Rows[0]["ProductNo"]),
                                    DataAccessMySQL.CreateParameter("ProductName",DbType.String,dtPurchasedSerial.Rows[0]["ProductName"]),
                                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,dtPurchasedSerial.Rows[0]["SerialNo"]),
                                    DataAccessMySQL.CreateParameter("SaleDate",DbType.String,dtPurchasedSerial.Rows[0]["SaleDate"]),
                                    DataAccessMySQL.CreateParameter("RoutineServiceFreq",DbType.String,RoutineServiceFreq),
                                    DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.String,warranty_D.WarrantyPeriod),
                                    DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,dtPurchasedSerial.Rows[0]["SaleDate"].ToString().Replace("/","")),
                                    DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,DateTime.ParseExact(dtPurchasedSerial.Rows[0]["SaleDate"].ToString().Replace("/",""), "yyyyMMdd", CultureInfo.InvariantCulture).AddMonths(int.Parse(warranty_D.WarrantyPeriod)).AddDays(-1).ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("Confirmed",DbType.String,ordertype),
                                    DataAccessMySQL.CreateParameter("Confirmor",DbType.String,Confirmor),
                                    DataAccessMySQL.CreateParameter("Remark",DbType.String,warranty_D.Remark),
                                    DataAccessMySQL.CreateParameter("InternalRemark",DbType.String,warranty_D.Remark)
                                });

                            
                        }
                        if(RoutineServiceFreq == "0")
                        {
                            dbMySQL.ExecuteNonQuery(@"update SaleSerial set RoutineService='N' where ProductNo=@ProductNo 
                        and SerialNo = @SerialNo and CustomerId = @CustomerId and SaleDate = @SaleDate", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,valueInfo[2].ToString()),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,valueInfo[3].ToString().Replace("/",""))
                        });
                        }

                        dbMySQL.ExecuteNonQuery(@"update SaleSerial set SettingCode='Y' where ProductNo=@ProductNo 
                        and SerialNo = @SerialNo and CustomerId = @CustomerId and SaleDate = @SaleDate", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,valueInfo[2].ToString()),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,valueInfo[3].ToString().Replace("/",""))
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